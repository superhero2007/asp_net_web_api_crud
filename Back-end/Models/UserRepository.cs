using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_end.Models
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = null;
            using (var ctx = new CalendarEntities())
            {
                users = ctx.Users.ToList<User>().AsEnumerable();
            }
            return users;
        }

        public User Get(int id)
        {
            using (var ctx = new CalendarEntities())
            {
                return ctx.Users.Where(e => e.Id == id).FirstOrDefault<User>();
            }
        }

        public User Add(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var ctx = new CalendarEntities())
            {
                item = ctx.Users.Add(new User()
                {
                    View = item.View
                });
                ctx.SaveChanges();
            }
            return item;
        }

        public void Remove(int id)
        {
            using (var ctx = new CalendarEntities())
            {
                var item = ctx.Users.Where(e => e.Id == id).FirstOrDefault<User>();
                ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public bool Update(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (var ctx = new CalendarEntities())
            {
                var existingItem = ctx.Users.Where(e => e.Id == item.Id).FirstOrDefault<User>();
                if (existingItem == null)
                {
                    return false;
                }
                existingItem.View = item.View;
                ctx.SaveChanges();
            }
            return true;
        }
    }
}
