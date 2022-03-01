using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SkillBoxCourseCSharp.ADO
{
    internal class ADONETExample
    {
        public static void ExampleProviderFactory()
        {
            //Для демонстрации второго способа получения значения ключа из App.config
            AppSettingsReader appSetReader = new AppSettingsReader();
            string testProvider = appSetReader.GetValue("provider", typeof(string)).ToString();
            string testConnectionString = ConfigurationManager.AppSettings["connectionString"];

            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.ConnectionStrings["SqlProvider"].ConnectionString;

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection = factory.CreateConnection())
            {
                Console.WriteLine($"Объект подключения: {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                DbCommand command = connection.CreateCommand();
                Console.WriteLine($"Объект команды: {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "Select * From [User]";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($"Объект чтения данных: {reader.GetType().Name}");
                    Console.WriteLine("\n\tВсе записи таблицы User");

                    while (reader.Read())
                    {
                        Console.WriteLine($"-> Пользователь #{reader[0]}: Логин - {reader[1]}\tПароль - {reader[2]}");
                    }
                }
            }
        }

        #region SqlConnetion

        public static void ExampleSqlConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlProvider"].ConnectionString;
            SqlConnectionStringBuilder newConnString = new SqlConnectionStringBuilder(connString)
            {
                InitialCatalog = "ManufactureDB",
                DataSource = @"DESKTOP-OD8ATN8\SQLEXPRESS",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };


            using (SqlConnection sqlCon = new SqlConnection(newConnString.ConnectionString))
            {
                sqlCon.Open();
                ShowConnectionStatus(sqlCon);

                string commandText = "Select * From [Role] Order By Name";
                SqlCommand sqlCom = new SqlCommand(commandText, sqlCon);

                using (SqlDataReader sqlReader = sqlCom.ExecuteReader())
                {
                    Console.WriteLine("\tРоли предприятия");
                    while (sqlReader.Read())
                        Console.WriteLine($"-> Роль #{sqlReader["Id"]}: Наименование - {sqlReader["Name"]}");
                }

                Console.ReadKey();
                Console.Clear();

                sqlCom = new SqlCommand("Select * From [Role] Order By Name;Select * From [User] Order By RoleId", sqlCon);

                using (SqlDataReader sqlReader = sqlCom.ExecuteReader())
                {
                    do
                    {
                        while (sqlReader.Read())
                        {
                            Console.WriteLine("\n\tЗапись");
                            for (int i = 0; i < sqlReader.FieldCount; i++)
                            {
                                Console.WriteLine($"{sqlReader.GetName(i)} = {sqlReader.GetValue(i)}");
                            }
                        }
                        Console.WriteLine("\n\n");
                    }
                    while(sqlReader.NextResult());
                }
            }
        }

        public static void ShowConnectionStatus(SqlConnection tempSqlConn)
        {
            Console.WriteLine("\tИнформация о подключении");
            Console.WriteLine($"Расположение базы данных: {tempSqlConn.DataSource}");
            Console.WriteLine($"Наименование базы данных: {tempSqlConn.Database}");
            Console.WriteLine($"Строка подключения к базе данных: {tempSqlConn.ConnectionString}");
            Console.WriteLine($"Время ожидания: {tempSqlConn.ConnectionTimeout}");
            Console.WriteLine($"Состояние: {tempSqlConn.State}");
        }

        #endregion

        public static void ExampleCRUDUser()
        {
            ManufactureConnection admin = new ManufactureConnection();

            List<User> lUsers = admin.GetAllUsers();
            foreach (User user in lUsers)
            {
                Console.WriteLine(user);
            }

            Console.ReadKey();
            Console.Clear();


            Console.WriteLine("\tadmin.InsertUser\n");
            admin.InsertUser(new User { Login = "BebraМихаил", Password = "gdsp242", Fullname = "Саламалейкулов Андрей", RoleId = 3 });
            foreach (User user in admin.GetAllUsers())
            {
                Console.WriteLine(user);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("\tadmin.UpdateUser\n");
            admin.UpdateUser(new User { Id = 10, Login = "ПростоМихайл", Password = "gdsg226", Fullname = "Саламалейкулов Андрей", RoleId = 4 });
            foreach (User user in admin.GetAllUsers())
            {
                Console.WriteLine(user);
            }

            Console.ReadKey();
            Console.Clear();

            admin.DeleteUser(10);
            Console.WriteLine("\tadmin.DeleteUser(10)\n");
            foreach (User user in admin.GetAllUsers())
            {
                Console.WriteLine(user);
            }

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("admin.GetFullNameUser(2): " + admin.GetFullNameUser(2));

            Console.ReadKey();
            Console.Clear();

            admin.ProcessDeleteUser(15);
        }
    }
}
