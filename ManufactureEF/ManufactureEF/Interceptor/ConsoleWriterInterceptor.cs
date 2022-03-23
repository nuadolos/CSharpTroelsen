using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF.Interceptor
{
    /// <summary>
    /// Перехватывает информацию о происходимых операциях пользователем
    /// </summary>
    public class ConsoleWriterInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
            => WriteInfo(interceptionContext.IsAsync, command.CommandText);

        /// <summary>
        /// Перехватчик выводит на консоль текст команды и является ли вызов асинхронным
        /// </summary>
        /// <param name="IsAsync"></param>
        /// <param name="cmdText"></param>
        private void WriteInfo(bool IsAsync, string cmdText)
            => Console.WriteLine($"IsAsync: {IsAsync}, Command Text: {cmdText}");
    }
}
