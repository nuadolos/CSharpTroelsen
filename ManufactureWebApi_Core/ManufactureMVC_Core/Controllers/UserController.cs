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
using Newtonsoft.Json;
using System.Text;

namespace ManufactureMVC_Core.Controllers
{
    public class UserController : Controller
    {
        /*
         * Примечание к JsonConvert.SerializeObject:
         *      - Если при сериализации объекта, в котором свойства недопускают значение null,
         *              т.е. не помечены знаком вопроса (int?), то запрос вернет ошибку.
         *      - Исключить ошибку с кодом 400 возможно с помощью 
         *              пометки типа знаком вопроса каждого свойства,
         *              который должен принимать null (Id никогда не принимает null!!)
         */

        private readonly string _baseUrl;

        /// <summary>
        /// Хранит список ролей
        /// </summary>
        private SelectList RolesList { get; }

        /// <summary>
        /// Получает URL Api для отправки и получения запросов.
        /// Также заполняет RolesList данными из БД.
        /// </summary>
        /// <param name="repo"></param>
        public UserController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceAddress").Value;

            var client = new HttpClient();
            var response = client.GetAsync($"{_baseUrl}/Roles").Result;
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<Role>>(
                     response.Content.ReadAsStringAsync().Result);
                RolesList = new SelectList(items, "Id", "Name", new User().RoleId);
            }
        }

        /// <summary>
        /// Получает запись о пользователе.
        /// Метод используется с целью сокращение кода.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<User> GetUserRecord(int id)
        {
            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(
                     await response.Content.ReadAsStringAsync());
                return user;
            }

            return null;
        }

        #region Index/Details

        // GET: User
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();

            //Асинхронный вызов метода с атрибутом HttpGet
            var response = await client.GetAsync($"{_baseUrl}");

            //Если вызов сработал, то получаем Json-файл с его содержимым
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<User>>(
                     await response.Content.ReadAsStringAsync());
                return View(items);
            }

            return NotFound();
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await GetUserRecord(id.Value);
            return user != null ? (IActionResult)View(user) : NotFound();
        }

        #endregion

        #region Create

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = RolesList;
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,Password,Fullname,RoleId")]User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = new HttpClient();

                    //Преобразование объекта User в формат JSON
                    string json = JsonConvert.SerializeObject(user);

                    //Асинхронный вызов метода с атрибутом HttpPost
                    var response = await client.PostAsync(_baseUrl,
                        new StringContent(json, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await GetUserRecord(id.Value);

            ViewData["RoleId"] = RolesList;

            return user != null ? (IActionResult)View(user) : NotFound();
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Login,Password,Fullname,RoleId,Id,Timestamp")] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                //Преобразование объекта User в формат JSON
                string json = JsonConvert.SerializeObject(user);

                //Асинхронный вызов метода с атрибутом HttpPut
                var response = await client.PutAsync($"{_baseUrl}/{user.Id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["RoleId"] = RolesList;
            return View(user);
        }

        #endregion

        #region Delete
        
        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await GetUserRecord(id.Value);

            return user != null ? (IActionResult)View(user) : NotFound();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id, Timestamp")] User user)
        {
            var client = new HttpClient();

            //Сериализация массива байтов в строку для вставки в маршрут
            var timeStampString = JsonConvert.SerializeObject(user.Timestamp);

            //Ручная подготовка HTTP-запроса
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Delete, $"{_baseUrl}/{user.Id}/{timeStampString}");

            //Асинхронный вызов метода с атрибутом HttpDelete
            await client.SendAsync(request);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
