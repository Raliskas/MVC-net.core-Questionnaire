using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Models
{
    public class UserRepository:IRepository<User>
    {
        private MyDbContext db;
        public UserRepository(MyDbContext context)
        {
            this.db = context;
        }
        public int Create(User item)
        {
            return db.Add(item).Entity.ID;            
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User GetItem(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetList()
        {
            return db.Users;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        #region IDisposable Support
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) db.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
