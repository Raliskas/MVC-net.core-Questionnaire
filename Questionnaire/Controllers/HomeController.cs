using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IRepository<User> dbUser;
    
        public HomeController(MyDbContext context)
        {
            dbUser = new UserRepository(context);
     
        }
        public IActionResult Index()
        {
            return View();
        }            
       
        //Усеры 
        public ViewResult UsersList()
        {
            return View(dbUser.GetList());
        }
        
        public IActionResult Privacy()
        {
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
