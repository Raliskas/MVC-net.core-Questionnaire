using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models
{
    public class ChoiseRepository : IRepository<Choice>
    {
        private MyDbContext db;
        public ChoiseRepository(MyDbContext context)
        {
            this.db = context;
        }

        public int Create(Choice item)
        {
            item = db.Add(item).Entity;
            db.SaveChanges();
            return item.ChoiceID;
        }

        public void Delete(int id)
        {
            Choice choice = db.Choices.Find(id);
            if (choice != null)
                db.Choices.Remove(choice);
        }

        public Choice GetItem(int id)
        {
            return db.Choices.Find(id);
        }

        public IEnumerable<Choice> GetList()
        {
            return db.Choices.Include(c=>c.Answers);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Choice item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
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
      
    }
}
