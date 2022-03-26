using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.EF;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureEF_Core.Repos
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo() : base()
        { }

        public UserRepo(ManufactureContext context) : base(context)
        { }

        /// <summary>
        /// Получение отсортированных пользователей
        /// </summary>
        /// <returns></returns>
        public override List<User> GetAll() => 
            GetAll(u => u.RoleId, true);

        /// <summary>
        /// Получение пользователей с ролью "Директор"
        /// </summary>
        /// <returns></returns>
        public List<User> GetDirectors() =>
            GetSome(u => u.RoleId == 1);

        /// <summary>
        /// Получение всех данных таблицы, включая все ключи,
        /// с помощью энергичной загурзки Include
        /// (ThenInclude - получение данных из текущей позиции)
        /// </summary>
        /// <returns></returns>
        public List<User> GetRelatedData() =>
            Context.User.FromSqlRaw("SELECT * FROM [User]").Include(role => role.Role).ToList();

        /// <summary>
        /// Выполнение запроса SQL с операцией LIKE
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<User> Search(string searchString) =>
            Context.User.Where(u => Functions.Like(u.Fullname, $"%{searchString}%")).ToList();
    }
}
