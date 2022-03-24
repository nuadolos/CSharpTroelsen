using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ManufactureEF.Entities;
using ManufactureEF.Repos;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace ManufactureWebApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        IMapper mapper;
        private readonly UserRepo _repo = new UserRepo();

        public UserController()
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<User, User>()
                    .ForMember(x => x.Role, opt => opt.Ignore()));
            mapper = config.CreateMapper();
        }

        #region Get

        [HttpGet, Route("")]
        public IEnumerable<User> GetUser()
        {
            List<User> users = _repo.GetAll();
            return mapper.Map<List<User>, List<User>>(users);
        }

        // GET api/values/5
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = _repo.GetOne(id);

            if (user == null)
                return NotFound();

            return Ok(mapper.Map<User, User>(user));
        }

        #endregion

        #region Put

        [HttpPut, Route("{id}", Name = "DisplayRoute")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest();

            try
            {
                _repo.Save(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region POST

        // POST: api/User
        [HttpPost, Route("")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _repo.Add(user);
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtRoute("DisplayRoute", new { id = user.Id }, user);
        }

        #endregion

        #region DELETE

        [HttpDelete, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            try
            {
                _repo.Delete(user);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
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