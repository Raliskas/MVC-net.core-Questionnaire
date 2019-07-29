using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Questionnaire.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questionnaire.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        public static int userId;
        DBManager manager;
        public UserController(MyDbContext context, ILogger<UserController> logger)
        {
            _logger = logger;
            manager = new DBManager(context);
            userId = 1;
        }
        public IActionResult Index()
        {
            //throw new Exception("{Хардкод");
            return View();
        }
        [HttpGet]
        public IActionResult ChooseTest()
        {
            ViewBag.Tests = manager.GetTests();
            return View();
        }
        
        public IActionResult Results()
        {
            ViewBag.Tests = manager.GetTests();
            return View();
        }
        [HttpGet]
        public IActionResult Testing(int testId)
        {
            try
            {
                if(testId==0)
                {
                    return BadRequest("Тест не выбран");
                }

                HttpContext.Session.SetInt32("testId", testId);
                HttpContext.Session.SetInt32("questNum", 1);
                if(manager.GetQuestion(testId, 1)==null)
                {
                    return BadRequest("Вопросы не найдены");
                }
                if (manager.GetQuestionChoices(testId, 1).Count == 0)
                {
                    return BadRequest("Варианты ответов не найдены");
                }
                ViewBag.Question = manager.GetQuestion(testId, 1);
                ViewBag.Choices = manager.GetQuestionChoices(testId, 1);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Testing action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult Testing(List<Answer> answers)
        {
            int questNum = HttpContext.Session.GetInt32("questNum") ?? throw new Exception("Сессия пустая, перейдите на главную страницу");
            int testId = HttpContext.Session.GetInt32("testId") ?? throw new Exception();
            questNum = manager.SaveAnswers(answers, questNum, testId, userId);
            if (questNum > manager.GetQuestions(testId).Count())
            {
                HttpContext.Session.Remove("questNum");
                return RedirectToAction("TestEnd");
            }
            HttpContext.Session.SetInt32("questNum", questNum);
            ViewBag.Question = manager.GetQuestion(testId, questNum);
            ViewBag.Choices = manager.GetQuestionChoices(testId, questNum);
            return View();
        }
        public ViewResult TestEnd(int testId)
        {
            try
            {
                if (testId == 0) testId = HttpContext.Session.GetInt32("testId") ?? throw new ArgumentException("Вы не выбрали тест");
            }
            catch (ArgumentException ex)
            {
                View("ErrorView", ex.Message);
            }
            ViewBag.Answers = manager.GetAnswers(testId, userId);
            var quest = manager.GetQuestions(testId);
            if(quest.Any(q=>q.Choices.Any(c=>c.Answers==null)))
                    return View("ErrorView", "Ответы на вопрос не найдены.Возможно вы ещё не проходили тест. Вы можете начать его на главной странице");
            ViewBag.Rating = manager.Rating(testId);
            HttpContext.Session.Remove("testId");

            return View(quest);
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public virtual IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404 || statusCode == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View();
        }
    }
}
