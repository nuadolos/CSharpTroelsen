using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManufactureEFExample.Model;
using ManufactureEFExample.Model.Base;

namespace ManufactureEFExample.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;

        public BaseRepo()
            => _table = ManufactureEntities.Context.Set<T>();

        #region Добавление

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entity)
        {
            _table.AddRange(entity);
            return SaveChanges();
        }

        #endregion

        #region Удаление

        public int Delete(int id, byte[] timeStamp)
        {
            ManufactureEntities.Context.Entry(new T { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            ManufactureEntities.Context.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        #endregion

        #region Освобождение ресурсов

        //Если подключение нестатичное, тогда необходим метод Dispose
        //Здесь используется в качестве примера
        public void Dispose() 
            => ManufactureEntities.Context?.Dispose();

        #endregion

        #region Выборка

        public List<T> ExecuteQuery(string sql) 
            => _table.SqlQuery(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects) 
            => _table.SqlQuery(sql, sqlParametersObjects).ToList();

        public virtual List<T> GetAll()
            => _table.ToList();

        public T GetOne(int? id)
            => _table.Find(id);

        public List<T> Where(Func<T, bool> func)
            => _table.Where(func).ToList();

        public T First(Func<T, bool> func)
            => _table.FirstOrDefault(func);

        #endregion

        #region Изменение

        public int Save(T entity)
        {
            ManufactureEntities.Context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        #endregion

        #region Сохранение изменений контекста

        internal int SaveChanges()
        {
            try
            {
                return ManufactureEntities.Context.SaveChanges();
            }
            // Генерируется, когда обновление базы данных терпит неудачу.
            // Проверить внутреннее исключение (исключения), чтобы получить
            // дополнительные сведения и выяснить, на какие объекты это повлияло.
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            // Обработать здесь отказы транзакции.
            catch (CommitFailedException ex)
            {
                throw new Exception(ex.Message);
            }
            //Обработка другого исключения
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #endregion
    }
}
