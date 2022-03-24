using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManufactureEF.Model;
using ManufactureEF.Entities.Base;

namespace ManufactureEF.Repos
{
    /// <summary>
    /// Базовый класс для остальных хранилищ.
    /// Предоставляет базовые функции для каждой сущности.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly ManufactureEntities _db;

        protected ManufactureEntities Context => _db;

        public BaseRepo()
        {
            _db = new ManufactureEntities();
            _table = _db.Set<T>();
        }

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
            _db.Entry(new T { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int DeleteRange(IList<T> entity)
        {
            _table.RemoveRange(entity);
            return SaveChanges();
        }

        #endregion

        #region Освобождение ресурсов

        //Если подключение нестатичное, тогда необходим метод Dispose
        public void Dispose() 
            => _db.Dispose();

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
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        #endregion

        #region Сохранение изменений контекста

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            // Генерируется, когда обновление базы данных терпит неудачу.
            // Проверить внутреннее исключение (исключения), чтобы получить
            // дополнительные сведения и выяснить, на какие объекты это повлияло.
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }
            // Обработать здесь отказы транзакции.
            catch (CommitFailedException ex)
            {
                throw new CommitFailedException(ex.Message);
            }
            //Обработка другого исключения
            catch(Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
