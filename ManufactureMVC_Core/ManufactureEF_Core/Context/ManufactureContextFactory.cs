using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Context
{
    /// <summary>
    /// Класс, реализующий интерфейс IDesignTimeDbContextFactory, для запуска миграций EF
    /// </summary>
    public class ManufactureContextFactory : IDesignTimeDbContextFactory<ManufactureContext>
    {
        /// <summary>
        /// Создание контекста без помощи открытого конструктора ManufactureContext без параметров
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ManufactureContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManufactureContext>();

            string connectionString = @"server=.\sqlexpress;database=ManufactureDB;
                    integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            optionsBuilder.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure());

            return new ManufactureContext(optionsBuilder.Options);
        }
    }
}
