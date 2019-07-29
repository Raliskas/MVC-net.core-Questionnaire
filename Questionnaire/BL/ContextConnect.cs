using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Models;

namespace Questionnaire.BL
{
    public class ContextConnect 
    {
        IRepository<Test> dbTest;
        IRepository<Question> dbQuestion;
     //   IRepository<Choice> dbChoices;
        IRepository<User> dbUser;
        MyDbContext db=new MyDbContext();


        public ContextConnect(MyDbContext context)
        {
            db = context;
            dbUser = new UserRepository(context);
            dbTest = new TestRepository(context);
            dbQuestion = new QuestionRepository(context);
       //     dbChoices = new ChoiseRepository(context);

        }
        public int CreateTes(Test item, int AuthorID)
        {       
                int questTestId = dbTest.Create(item);
                return questTestId;                       
        }

        public void CreateQust(Question question, int questTestId, int questionNumber)
        {
            question.TestID = questTestId;
            question.QuestionNumber = questionNumber;

            foreach (Choice choice in question.Choices)
            {
                choice.QuestionNumber = questionNumber;
                choice.TestID = questTestId;
            }
            dbQuestion.Create(question);
        }

        public object ListTest()
        {
            return dbTest.GetList();
        }

        public object EditFind(int? id)
        {
            Test test = db.Tests.Find(id);
            return test;
        }
        public void Create(Test item)
        {
            item.AuthorID = 0;
            db.Entry(item).State=EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteFind(int id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
            {
                db.Tests.Remove(test);
                db.SaveChanges();
            }
        }
        public void DeleteTestFind(int? id)
        {
            Test test = db.Tests.Find(id);
            var questDelete=db.Questions.Where(x => x.TestID == id);
            var choiceDelete = db.Choices.Where(x => x.TestID == id);
            db.Tests.Remove(test);
            foreach (var c in questDelete) { db.Questions.Remove(c); }    //оставить табуляцию?
            foreach (var x in choiceDelete) { db.Choices.Remove(x); }
            db.SaveChanges();
        }

        public void CreateUse(User item)
        {
            dbUser.Create(item);
            dbUser.Save();
        }

        public object ListUser()
        {
            return dbUser.GetList();
        }

        public object EditUserFind(int? id)
        {
            User user= db.Users.Find(id);
            return user;
        }

        public void CreateUs(User item)
        {
            
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUse(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public void DeleteGet(int? id)
        {
            User user = db.Users.Find(id);
        }

        public void DeleteConfirmed(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }
        public object ListQuestion(int id)
        {var c = dbQuestion.GetList().Select(x => x).Where(x => x.TestID == id).ToList();
            return c; 
        }

    }
}
