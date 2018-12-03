using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Back_end.Models;
using System.Web.Http.Cors;

namespace Back_end.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        static readonly IUserRepository repository = new UserRepository();

        public IHttpActionResult GetAllUsers()
        {
            return Ok(repository.GetAll());
        }

        public IHttpActionResult GetUser(int id)
        {
            User item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        public IHttpActionResult PostUser(User item)
        {
            item = repository.Add(item);
            return Ok(item);
        }
        
        public IHttpActionResult PutUser(int id, User item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteUser(int id)
        {
            User item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            repository.Remove(id);
            return Ok(item);
        }
    }
}
