using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_end.Models
{
    public class EventRepository : IEventRepository
    {
        public IEnumerable<Event> GetAll()
        {
            IEnumerable<Event> events = null;
            using (var ctx = new CalendarEntities())
            {
                events = ctx.Events.ToList<Event>().AsEnumerable();
            }
            return events;
        }

        public Event Get(int id)
        {
            using (var ctx = new CalendarEntities())
            {
                return ctx.Events.Where(e => e.Id == id).FirstOrDefault<Event>();
            }
        }

        public Event Add(Event item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var ctx = new CalendarEntities())
            {
                item = ctx.Events.Add(new Event()
                {
                    Title = item.Title,
                    Start = item.Start,
                    End = item.End,
                    AllDay = item.AllDay,
                    Resource = item.Resource
                });
                ctx.SaveChanges();
            }
            return item;
        }

        public void Remove(int id)
        {
            using (var ctx = new CalendarEntities())
            {
                var item = ctx.Events.Where(e => e.Id == id).FirstOrDefault<Event>();
                ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public bool Update(Event item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var ctx = new CalendarEntities())
            {
                var existingItem = ctx.Events.Where(e => e.Id == item.Id).FirstOrDefault<Event>();
                if (existingItem == null)
                {
                    return false;
                }
                existingItem.Title = item.Title;
                existingItem.Start = item.Start;
                existingItem.End = item.End;
                existingItem.AllDay = item.AllDay;
                existingItem.Resource = item.Resource;
                ctx.SaveChanges();
            }
            return true;
        }
    }
}
