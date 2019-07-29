using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models
{
    public class AnswerRepository : IRepository<Answer>
    {
        private MyDbContext db;
        public AnswerRepository(MyDbContext context)
        {
            this.db = context;
        }
        public int Create(Answer item)
        {
            item = db.Add(item).Entity;
            db.SaveChanges();
            return item.AnswerID;
        }

        public void Delete(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer != null)
                db.Answers.Remove(answer);
        }
        public Answer GetItem(int id)
        {
            return db.Answers.Find(id);
        }

        public IEnumerable<Answer> GetList()
        {
            return db.Answers;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Answer item)
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
