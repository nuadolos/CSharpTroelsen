using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.ADO
{
    internal class ManufactureConnection
    {
        private readonly string connectionString;
        private SqlConnection sqlConnection;

        public ManufactureConnection() : this(@"Data Source=.\SQLEXPRESS;Initial Catalog=ManufactureDB;Integrated Security=True")
        { }

        public ManufactureConnection(string connString) => connectionString = connString;

        #region Подключение

        private void OpenConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        private void CloseConnection()
        {
            if (sqlConnection?.State != ConnectionState.Closed)
                sqlConnection?.Close();
        }

        #endregion

        #region Выборка пользователей

        public List<User> GetAllUsers()
        {
            OpenConnection();

            List<User> users = new List<User>();

            string comText = "Select * From [User]";
            using (SqlCommand cmd = new SqlCommand(comText, sqlConnection))
            {
                cmd.CommandType = CommandType.Text;

                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)sqlReader["Id"],
                        Login = (string)sqlReader["Login"],
                        Password = (string)sqlReader["Password"],
                        Fullname = (string)sqlReader["Fullname"],
                        RoleId = (int)sqlReader["RoleId"]
                    });
                }
                sqlReader.Close();
            }
            return users;
        }

        #endregion

        #region Добавление пользователя

        public void InsertUser(User createUser)
        {
            OpenConnection();

            string cmdText = "Insert Into [User] (Login, Password, Fullname, RoleId)" +
                $"Values ('{createUser.Login}', '{createUser.Password}', '{createUser.Fullname}', '{createUser.RoleId}')";

            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConnection))
            {
                cmd.ExecuteNonQuery();
            }

            CloseConnection();
        }

        #endregion

        #region Редактирование пользователя

        public void UpdateUser(User updUser)
        {
            OpenConnection();

            //Параметризованный запрос, имеющий заполнители
            string cmdText = $"Update [User] Set Login = @Login," +
                $"Password = @Password, Fullname = @Fullname, RoleId = @RoleId " +
                $"Where Id = '{updUser.Id}'";
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConnection))
            {
                //Заполнение коллекции параметров
                cmd.Parameters.AddWithValue("@Login", updUser.Login); 
                cmd.Parameters.AddWithValue("@Password", updUser.Password); 
                cmd.Parameters.AddWithValue("@Fullname", updUser.Fullname);

                SqlParameter roleParam = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = updUser.RoleId,
                    SqlDbType = SqlDbType.Int
                };
                cmd.Parameters.Add(roleParam);
                cmd.ExecuteNonQuery();
            }

            CloseConnection();
        }

        #endregion

        #region Удаление пользователя

        public void DeleteUser(int id)
        {
            OpenConnection();

            string cmdText = $"Delete From [User] where Id = '{id}'";
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConnection))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            CloseConnection();
        }

        #endregion

        #region Получение имени пользователя с помощью хранимой процедуры

        public string GetFullNameUser(int id)
        {
            OpenConnection();

            string fullName;

            using (SqlCommand cmd = new SqlCommand("GetFullNameUser", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idUser", id);

                SqlParameter outParam = new SqlParameter
                {
                    ParameterName = "@fullName",
                    SqlDbType = SqlDbType.Char,
                    Size = 40,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                cmd.ExecuteNonQuery();

                fullName = cmd.Parameters["@fullName"].Value.ToString();
            }

            CloseConnection();

            return fullName;
        }

        #endregion

        #region Транзакция

        public void ProcessDeleteUser(int idUser)
        {
            OpenConnection();

            //Объекты команд, представляющие шаги операции
            SqlCommand cmd1 = new SqlCommand($"Delete From [User] Where Id = '{idUser}'", sqlConnection);
            SqlCommand cmd2 = new SqlCommand($"Delete From [User] Where Id = '{idUser}'", sqlConnection);

            SqlTransaction transaction = null;
            try
            {
                //Получение транзакции из объекта подключения
                transaction = sqlConnection.BeginTransaction();

                //Включение команд в транзакцию
                cmd1.Transaction = transaction;
                cmd2.Transaction = transaction;

                //Выполнение команд
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                //Фиксирование транзакции
                transaction.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка транзакции!");

                //Любая ошибка приведет к откату транзакции
                transaction?.Rollback();
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion
    }
}
