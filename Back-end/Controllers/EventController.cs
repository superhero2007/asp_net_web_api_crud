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
    public class EventController : ApiController
    {
        static readonly IEventRepository repository = new EventRepository();

        public IHttpActionResult GetAllEvents()
        {
            return Ok(repository.GetAll());
        }

        public IHttpActionResult GetEvent(int id)
        {
            Event item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        public IHttpActionResult PostEvent(Event item)
        {
            item = repository.Add(item);
            return Ok(item);
        }

        public IHttpActionResult PutEvent(int id, Event item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteEvent(int id)
        {
            Event item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            repository.Remove(id);
            return Ok(item);
        }
    }
}
