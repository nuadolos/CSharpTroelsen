#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManufactureEF.Entities;
using ManufactureEF_Core.Context;
using ManufactureEF_Core.Repos;
using AutoMapper;
using Newtonsoft.Json;

namespace ManufactureWebApi_Core.Controllers
{
    [Route("api/User")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _repo;

        public UserController(IUserRepo repo)
        {
            _repo = repo;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<User, User>()
                .ForMember(x => x.Role, opt => opt.Ignore()));
            _mapper = config.CreateMapper();
        }

        #region Get

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = _repo.GetAll();
            return _mapper.Map<List<User>, List<User>>(users);
        }

        // GET: api/User/Roles
        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = _repo.GetRoles();
            return _mapper.Map<List<Role>, List<Role>>(roles);
        }

        // GET: api/User/5
        //Свойство Name именует маршрут
        //Атрибут FromRoute используется для гарантии,
        //что id поступает из маршрута ,а не какого-то другого источника
        [HttpGet("{id}", Name = "DisplayRoute")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            var user = _repo.GetOne(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<User, User>(user));
        }

        #endregion

        #region Put

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Атрибут FromBody привязывает параметры к значениям
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest();

            _repo.Update(user);
            return NoContent();
        }

        #endregion

        #region Post

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            _repo.Add(user);
            return CreatedAtRoute("DisplayRoute", new { id = user.Id }, user);
        }

        #endregion

        #region Delete

        // DELETE: api/User/5
        [HttpDelete("{id}/{timestamp}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, [FromRoute] string timestamp)
        {
            //Добавление ковычек, чтобы десерилизовать обратно в массив байтов
            if (!timestamp.StartsWith("\""))
                timestamp = $"\"{timestamp}\"";

            //Десерилизация timestamp и удаления объекта User из БД
            var ts = JsonConvert.DeserializeObject<byte[]>(timestamp);
            _repo.Delete(id, ts);
            return Ok();
        }

        #endregion
    }
}
