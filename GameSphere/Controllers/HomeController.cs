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
        //TODO make post model add post lists to user
        //testingdata
        #region
        User test1;
        User test2;
        Post p;
        public HomeController()
        {
            if(Repository.Users.Count == 0)
            {
                test1 = new User()
                {
                    UserName = "test",
                    Game = "Call of Duty",
                    Console = "Xbox",
                    Genre = "FPS",
                    Platform = "Twitch",
                    Privacy = true
                };
                Repository.Users.Add(test1);
                test2 = new User()
                {
                    UserName = "test2",
                    Game = "Halo 4",
                    Console = "PC",
                    Genre = "Horror",
                    Platform = "YoutubeGaming",
                    Privacy = false                  
                };
                Repository.Users.Add(test2);
                p = new Post()
                {
                    User = test2,
                    Message = "This new site is amazing"
                };
                test2.AddPost(p);
                test1.AddFollowing(test2);
                test1.AddFollower(test2);
                test2.AddFollower(test1);
                test2.AddFollowing(test1);
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
            //TODO create wall displaying USER's Posts and following posts chronologically
            ViewBag.postCount = user.Posts.Count;
            ViewBag.followingCount = user.Following.Count;
            ViewBag.followerCount = user.Followers.Count;
            return View(user);
        }

        [HttpGet]
        public IActionResult SignUp()
        {          
            return View();
        }

        [HttpPost]
        public RedirectToActionResult SignUp(string username, string game, string console,
                                             string genre, string platform, bool privacy)
        {
            User user = new User();
            user.UserName = username;
            user.Game = game;
            user.Console = console;
            user.Genre = genre;
            user.Platform = platform;
            user.Privacy = privacy;
            Repository.Users.Add(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Privacy(string title)
        {
            //TODO create setting check to allow quiz results to be saved and shown or not
            return View("Privacy", title);
        }

        [HttpPost]
        public RedirectToActionResult Privacy(string title, bool privacy)
        {
            User user = Repository.GetUserByUserName(title);
            user.changeUserPrivacy(privacy);
            return RedirectToAction("Homepage", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
