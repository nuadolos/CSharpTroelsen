using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Repos
{
    /// <summary>
    /// Реализация паттерна "Хранилище"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepo<T>
    {
        int Add(T entity);
        int AddRange(IList<T> entity);
        int Save(T entity);
        int Delete(int id, byte[] timeStamp);
        int Delete(T entity);
        int DeleteRange(IList<T> entity);
        T GetOne(int? id);
        T First(Func<T, bool> func);
        List<T> GetAll();
        List<T> Where(Func<T, bool> func);
        List<T> ExecuteQuery(string sql);
        List<T> ExecuteQuery(string sql, object[] sqlParametersObjects);
    }
}
