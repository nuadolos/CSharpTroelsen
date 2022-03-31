using ManufactureEF.Entities.Base;
using ManufactureEF_Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly ManufactureContext _db;

        protected ManufactureContext Context => _db;

        public BaseRepo() : this(new ManufactureContext())
        { }

        public BaseRepo(ManufactureContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        #region Добавление

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        #endregion

        #region Обновление

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }

        #endregion

        #region Удаление

        public int Delete(int id, byte[] timestamp)
        {
            _db.Entry(new T() { Id = id, Timestamp = timestamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Remove(entity);
            return SaveChanges();
        }

        #endregion

        #region Выборка

        public T GetOne(int? id) => 
            _table.Find(id);

        public List<T> GetSome(Expression<Func<T, bool>> where) =>
            _table.Where(where).ToList();

        public virtual List<T> GetAll() =>
            _table.ToList();

        public List<T> GetAll(Expression<Func<T, dynamic>> orderby, bool ascending) =>
            ascending ? _table.OrderBy(orderby).ToList() : _table.OrderByDescending(orderby).ToList();

        public List<T> ExecuteQuery(string sql) =>
            _table.FromSqlRaw(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects) => 
            _table.FromSqlRaw(sql, sqlParametersObjects).ToList();

        #endregion

        #region Сохранение изменений

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Генерируется при возникновении ошибки, связанной с паралелизмом
            }
            catch (RetryLimitExceededException)
            {
                //Генерируется при достижении максимального кол-ва попыток
            }
            catch (DbUpdateException)
            {
                //Генерируется при неудачном обновление базы данных
            }
            catch (Exception)
            {
                //Генерируется при возникновении любого другого исключения
            }

            return 0;
        }

        #endregion

        #region Освобождение ресурсов

        public void Dispose() => _db.Dispose();

        #endregion
    }
}
