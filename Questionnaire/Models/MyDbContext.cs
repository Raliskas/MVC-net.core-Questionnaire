using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models

{
    public class MyDbContext : DbContext
        
    {       
        public MyDbContext() : base() {}
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
        }
    }
}
