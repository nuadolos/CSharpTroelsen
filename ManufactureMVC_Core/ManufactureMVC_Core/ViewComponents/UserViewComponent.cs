using System.Threading.Tasks;
using ManufactureEF_Core.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ManufactureMVC_Core.ViewComponents
{
    /// <summary>
    /// Северная сторона представления UserPartialView
    /// </summary>
    public class UserViewComponent : ViewComponent
    {
        private readonly IUserRepo _repo;

        /// <summary>
        /// Компоненты представлений также задействуют контейнер DI
        /// </summary>
        /// <param name="repo"></param>
        public UserViewComponent(IUserRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Визуализирует компонент представления
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = _repo.GetAll();

            //Возвращает частичное представление UserPartialView
            if (users != null)
                return View("UserPartialView", users);

            return new ContentViewComponentResult("Unable to locate records.");
        }
    }
}
