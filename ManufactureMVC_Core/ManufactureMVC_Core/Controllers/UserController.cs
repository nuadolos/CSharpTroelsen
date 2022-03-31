#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using ManufactureEF_Core.Repos;

namespace ManufactureMVC_Core.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _repo;

        /// <summary>
        /// Хранит список ролей
        /// </summary>
        private SelectList RolesList { get; }

        /// <summary>
        /// Передает IUserRepo как параметр, контейнер DI
        /// выполнит остальную работу по созданию экземпляра UserRepo
        /// </summary>
        /// <param name="repo"></param>
        public UserController(IUserRepo repo)
        {
            _repo = repo;

            RolesList = new SelectList(_repo.GetRoles(), "Id", "Name", new User().RoleId);
        }

        #region Index/Details

        // GET: User
        public IActionResult Index()
        {
            //Возвращает представление Index
            //return View(_repo.GetRelatedData());

            //Возвращает представление IndexWithViewComponent
            return View("IndexWithViewComponent");
        }

        // GET: User/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _repo.GetOne(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        #endregion

        #region Create

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = RolesList;
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            ModelState.Remove("Role");
            ModelState.Remove("Timestamp");

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Add(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Не удается создать запись.");
                }
            }

            ViewData["RoleId"] = RolesList;
            return View(user);
        }

        #endregion

        #region Edit

        // GET: User/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _repo.GetOne(id);

            if (user == null)
            {
                return NotFound();
            }

            ViewData["RoleId"] = RolesList;

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Login,Password,Fullname,RoleId,Id,Timestamp")] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty,
                        "Не удается сохранить запись. Данную запись уже обновили.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Не удается сохранить запись.");
                }
            }

            ViewData["RoleId"] = RolesList;
            return View(user);
        }

        #endregion

        #region Delete
        
        // GET: User/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _repo.GetOne(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("Id, Timestamp")] User user)
        {
            try
            {
                _repo.Delete(user);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty,
                        "Не удается удалить запись. Данная запись была обновилена.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                        "Не удается удалить запись.");
            }

            return View(user);
        }

        #endregion
    }
}
