using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManufactureEF.Entities;
using ManufactureEF.Model;
using ManufactureEF.Repos;
using Newtonsoft.Json;

namespace ManufactureMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepo _userRepo = new UserRepo();
        private readonly RoleRepo _roleRepo = new RoleRepo();
        private string _baseUrl = "http://localhost:44359/api/User";

        #region Index/Details

        // GET: User
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync(_baseUrl);

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(
                    await response.Content.ReadAsStringAsync());
                return View(users);
            }

            return HttpNotFound();
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(
                    await response.Content.ReadAsStringAsync());
                return View(user);
            }

            return HttpNotFound();
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
        public async Task<ActionResult> Create([Bind(Include = "Login,Password,Fullname,RoleId")] User user)
        {
            //Явная привязка модели
            //var us = new User();
            //if (TryUpdateModel(us))
            //{

            //}

            ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name,
                Selected = true
            });

            //Неявная привязка модели (Тип модели в качестве параметра)
            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                //Преобразование объекта User в формат JSON
                string json = JsonConvert.SerializeObject(user);

                //Асинхронный вызов метода с атрибутом HttpPost
                var response = await client.PostAsync(
                    _baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty,
                        $"Не удается создать запись. Повторите позже.");
            }

            //При неудаче возвращается представление Create
            return View(user);
        }

        #endregion

        #region Edit

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(
                    await response.Content.ReadAsStringAsync());

                ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem
                { 
                    Value = v.Id.ToString(), 
                    Text = v.Name, 
                    Selected = user.RoleId == v.Id ? true : false 
                });

                return View(user);
            }

            return new HttpNotFoundResult();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Fullname,RoleId,Timestamp")] User user)
        {
            ViewBag.RoleId = _roleRepo.GetAll().Select(v => new SelectListItem 
            { 
                Value = v.Id.ToString(),
                Text = v.Name, 
                Selected = true 
            });

            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                //Преобразование объекта User в формат JSON
                string json = JsonConvert.SerializeObject(user);

                //Асинхронный вызов метода с атрибутом HttpPut
                var response = await client.PutAsync(
                    $"{_baseUrl}/{user.Id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    //Переход на представление Index
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty,
                    $"Не удается сохранить запись. Повторите позже.");
            }

            //При неудаче возвращается представление Edit
            return View(user);
        }

        #endregion

        #region Delete

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(
                    await response.Content.ReadAsStringAsync());
                return View(user);
            }

            return new HttpNotFoundResult();
        }

        // POST: User/Delete/5
        //ActionName["Delete"] - если метод имеет отличающееся имя от действия,
        //то используется данный атрибут, с указанным именем действия
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id,Timestamp")] User user)
        {
            try
            {
                var client = new HttpClient();

                //Ручная подготовка HTTP-запроса
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{user.Id}")
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
                };

                //Асинхронный вызов метода с атрибутом HttpDelete
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty,
                    $"Не удается удалить запись.");
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
                _userRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
