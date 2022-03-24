using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufactureEF.Entities;
using ManufactureEF.Model;
using ManufactureEF.Repos;

namespace ManufactureMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepo _repo = new UserRepo();
        RoleRepo _roleRepo = new RoleRepo();

        #region Index/Details

        // GET: User
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _repo.GetOne(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        #endregion

        #region Create

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name});
            return View();
        }

        // POST: User/Create
        //Bind - исключает свойства из процесса привязки модели,
        //неуказанные в свойстве Include
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Password,Fullname,RoleId")] User user)
        {
            //Явная привязка модели
            //var us = new User();
            //if (TryUpdateModel(us))
            //{
                
            //}

            //Неявная привязка модели (Тип модели в качестве параметра)
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Add(user);

                    //Переход на представление Index
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //Если сохранение невозможно, то об этом информирует сообщение
                    ModelState.AddModelError(string.Empty, 
                        $"Не удается создать запись: {ex.Message}");
                }
            }

            //При неудаче возвращается представление Create
            return View(user);
        }

        #endregion

        #region Edit

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _repo.GetOne(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name, Selected = user.RoleId == v.Id ? true : false });
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Fullname,RoleId,Timestamp")] User user)
        {
            ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name, Selected = true });
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Save(user);

                    //Переход на представление Index
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty,
                        $"Не удается сохранить запись. Другой пользователь обновил ее.");
                }
                catch (Exception)
                {
                    //Если сохранение невозможно, то об этом информирует сообщение
                    ModelState.AddModelError(string.Empty,
                        $"Не удается сохранить запись. Повторите позже.");
                }
            }

            //При неудаче возвращается представление Edit
            return View(user);
        }

        #endregion

        #region Delete

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _repo.GetOne(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        //ActionName["Delete"] - если метод имеет отличающееся имя от действия,
        //то используется данный атрибут, с указанным именем действия
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,Timestamp")] User user)
        {
            try
            {
                _repo.Delete(user);

                //Переход на представление Index
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(String.Empty,
                    $"Не удается удалить запись. Другой пользователь обновил ее. {ex.Message}");
            }
            catch (Exception ex)
            {
                //Если сохранение невозможно, то об этом информирует сообщение
                ModelState.AddModelError(String.Empty,
                    $"Не удается удалить запись: {ex.Message}");
            }

            //При неудаче возвращается представление Delete
            return View(user);
        }

        #endregion

        #region Освобождение ресурсов

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
