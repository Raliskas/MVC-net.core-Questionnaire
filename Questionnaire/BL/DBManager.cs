using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Models;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Controllers
{
    public class DBManager
    {

        IRepository<User> dbUser;
        IRepository<Test> dbTest;
        IRepository<Question> dbQuestion;
        IRepository<Choice> dbChoise;
        IRepository<Answer> dbAnswer;
        public DBManager(MyDbContext context)
        {
            dbUser = new UserRepository(context);
            dbTest = new TestRepository(context);
            dbQuestion = new QuestionRepository(context);
            dbChoise = new ChoiseRepository(context);
            dbAnswer = new AnswerRepository(context);
            userid = 1;// dbUser.GetList().Where(u=>u.Email== ).FirstOrDefault().ID;
        }
        public static int buffer;
        public static int testNum;
        public static int userid;
        public List<Test> GetTests()
        {
            return dbTest.GetList().ToList();
        }
        public Question GetQuestion(int testId, int questNum)
        {
            try
            {
                return dbQuestion.GetList().Where(q => q.TestID == testId && q.QuestionNumber == questNum).FirstOrDefault();
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }
        public List<Question> GetQuestions(int testId)
        {
            var query = dbQuestion.GetList().Where(q => q.TestID == testId).ToList();
            return query;
        }
        public List<Choice> GetChoices(int testId)
        {
            return dbChoise.GetList().Where(c=>c.TestID==testId).ToList();
        }
        public List<Choice> GetQuestionChoices(int testId, int questNum)
        {
            return dbChoise.GetList().Where(c => c.TestID == testId && c.QuestionNumber == questNum)?.ToList();
        }
        public List<Answer> GetAnswers(int testId, int userId)
        {
            return dbAnswer.GetList().Where(a => a.TestID == testId && a.UserID == userId).ToList();
        }
        public int SaveAnswers(List<Answer> answers, int currentQuestionNum,int testId,int userId)
        {
            foreach(Answer ans in answers)
            {
                ans.UserID = userId;
                ans.TestID = testId;
                ans.QuestionNumber = currentQuestionNum;
                dbAnswer.Create(ans);
                dbAnswer.Save();
            }
            return ++currentQuestionNum;
        }
        public int Rating(int testId)
        {
            return GetQuestions(testId).Select(q => q.Choices.All(c => c.IsCorrect == c.Answers.Last().UserChoices)).Count(a => a);
        }
        public dynamic Result(int testId, int userId)
        {
            var query = GetQuestions(testId).Select(s => s.Choices);
            return query;
        }
    }
}
