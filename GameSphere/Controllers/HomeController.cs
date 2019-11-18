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
        [HttpGet]
        public IActionResult Index()
        {
            //TO DO add account via username
            // allow login via username
            return View();
        }

        public IActionResult SignUp()
        {
            //TO DO finish questions store with username
            return View();
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
