using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models
{
    public class QuestionRepository : IRepository<Question>
    {
        private MyDbContext db;
        public QuestionRepository(MyDbContext context)
        {
            this.db = context;
        }
        public int Create(Question item)
        {
            item = db.Add(item).Entity;
            db.SaveChanges();
            return item.QuestionID;
        }

        public void Delete(int id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
                db.Questions.Remove(question);
        }
        public Question GetItem(int id)
        {
            return db.Questions.Find(id);
        }

        public IEnumerable<Question> GetList()
        {
            return db.Questions.Include(q=>q.Choices);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Question item)
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
