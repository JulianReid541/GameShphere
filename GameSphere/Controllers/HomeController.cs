using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSphere.Models;

namespace GameSphere.Controllers
{
    public class HomeController : Controller
    {
        //testingdata
        #region
        User test1; 
        public HomeController()
        {
            if(Repository.Users.Count == 0)
            {
                test1 = new User()
                {
                    UserName = "test"
                };
                Repository.Users.Add(test1);
            }
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {         
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Index(string u)
        {
            List<User> users = Repository.Users;
            User user = Repository.GetUserByUserName(u);          
            if (user == null)
                return RedirectToAction("Index");
            else
                return RedirectToAction("HomePage", user);
        }

        public ActionResult HomePage(User user)
        {
            return View(user);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            //TO DO finish questions store with username
            return View();
        }

        [HttpPost]
        public RedirectToActionResult SignUp(string username)
        {
            User user = new User();
            user.UserName = username;
            Repository.Users.Add(user);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            //TODO create setting check to allow quiz results to be saved and shown or not
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
