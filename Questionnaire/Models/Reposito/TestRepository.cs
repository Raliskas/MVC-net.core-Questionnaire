using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models
{
    public class TestRepository : IRepository<Test> 
    {
        
        private MyDbContext db;
        
        public TestRepository(MyDbContext context)
        {
            this.db = context;          
        }
        public int Create(Test item)
        {            
                item = db.Add(item).Entity;
                db.SaveChanges();
                return item.TestID;            
        }

        public void Delete(int id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
                db.Tests.Remove(test);
        }

        public Test GetItem(int? id)
        {
            return db.Tests.Find(id);
        }

        public IEnumerable<Test> GetList()
        {
            return db.Tests;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Test item)
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

        public Test GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
