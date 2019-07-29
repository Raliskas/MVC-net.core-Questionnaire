using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Questionnaire.BL;
using Questionnaire.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questionnaire.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger _logger;
        public static int questTestId;
        public static int questIdPerem;

        ContextConnect connect;
        public AdminController(MyDbContext context, ILogger<AdminController> logger)
        {
            _logger = logger;
            connect = new ContextConnect(context);
        }

        // GET: /<controller>/        

        public IActionResult AdminView()
        {
            return View();
        }
        //Контроллеры создания теста
        //Создание теста

        public ActionResult CreateTest()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest(Test test)
        {

            if (ModelState.IsValid)
            {
                int AuthorID = 1;                   //Изменить после авторизации
                HttpContext.Session.SetInt32("questionNumber", 0);
                HttpContext.Session.SetInt32("questTestId", connect.CreateTes(test, AuthorID));
                return RedirectToAction("CreateQuestion");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }

            return View(test);

        }
        //Контроллеры создания текста вопросов 
        [HttpGet]
        public ActionResult CreateQuestion()
        {
            int questionNumber = Convert.ToInt32(HttpContext.Session.GetInt32("questionNumber"));
            questionNumber++;
            ViewBag.NumQuest = questionNumber;
            HttpContext.Session.SetInt32("questionNumber", questionNumber);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(Question quest)
        {
            if (ModelState.IsValid)
            {
                int questTestId = Convert.ToInt32(HttpContext.Session.GetInt32("questTestId"));
                connect.CreateQust(quest, questTestId, Convert.ToInt32(HttpContext.Session.GetInt32("questionNumber")));
                return RedirectToAction("CreateQuestion");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            return View(quest);
        }

        //TestList       
        public IActionResult TestList()
        {
            return View(connect.ListTest());
        }
        // QuestionList
        [HttpGet]
        public IActionResult QuestionList(int id)
        {
            return View(connect.ListQuestion(id));
        }

        // ChoiceList 
        public IActionResult ChoiceList()
        {
            return View();
        }
        // GET: TestList/Edit/5
        [HttpGet]
        public ActionResult EditTest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }       
            return View(connect.EditFind(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTest(Test test)
        {
            if (ModelState.IsValid)
            {
                connect.Create(test);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            return RedirectToAction("AdminView");
        }
        // GET: TestList/Delete/5

        public ActionResult DeleteTest(int id)
        {           
            connect.DeleteFind(id);
            return View();
        }

        [HttpGet]
        public ActionResult DeleteTest()
        {
            return View();
        }
        // Удаление теста и вопросов к нему 
        [HttpPost, ActionName("DeleteTest")]
        public ActionResult DeleteTestConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            connect.DeleteTestFind(id);
            return RedirectToAction("AdminView");
        }
               
        //User и его создание админом
        public ActionResult CreateUser()
        {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                connect.CreateUse(user);
                return RedirectToAction("UserList");
            }
            else
            {
                //ModelState.AddModelError("", "Некорректные данные");
            }
         return View(user);
        } 
        //User List 
        public ActionResult UserList()
        {
            return View(connect.ListUser());
        }
        // GET: User/Edit/5

            
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = connect.EditUserFind(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                connect.CreateUs(user);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }
            return RedirectToAction("AdminView");
        }
        // GET: User/Delete/5

        public ActionResult DeleteUser(int id)
        {           
            connect.DeleteUse(id);
            return View();
        }

       [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if(id==null)
            connect.DeleteGet(id);          
            return View();
        }

        [HttpPost,ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int? id)
        {
           connect.DeleteConfirmed(id); 
           return RedirectToAction("AdminView");
        }
        
        // Http Error
         
    }
}
