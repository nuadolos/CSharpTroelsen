using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace gds.Controllers
{
    [RoutePrefix("api/Pipu")]
    public class Class1 : ApiController
    {
        [HttpGet, Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet, Route("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }
    }
}