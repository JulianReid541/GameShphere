using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSphere.Models;
using System.Web;
using System.Data;

namespace GameSphere.Controllers
{  
    public class HomeController : Controller
    {       
        //TODO Cleaning Up
        IRepository Repository;
        public HomeController(IRepository r)
        {
            Repository = r;
        }

        //Sign in PAGE
        [HttpGet]
        public IActionResult Index()
        {         
            return View();
        }

        //takes username as string and checks repository to see if user exists
        //if user is null it returns to sign in page
        //Otherwise it saves user as Tempdata/SignedInUser and redirects to the homepage
        [HttpPost]
        public RedirectToActionResult Index(string u)
        {
            User user = Repository.GetUserByUserName(u);
            TempData["signedInUser"] = u as string;
            if (user == null)
                return RedirectToAction("Index");
            else
                return RedirectToAction("HomePage", user);
        }

        //Homepage taking user object and displays posts and counts for user posts/follows/following
        [HttpGet]       
        public ActionResult HomePage(User user)
        {
            List<User> dbUsers = Repository.Users;
            List<Post> dbLists = Repository.Posts;
            User u = Repository.GetUserByUserName(user.UserName);

            ViewBag.userName = u.UserName;
            ViewBag.postCount = u.Posts.Count;
            return View(dbLists);
        }

        //Takes postmessage and adds user to post saves it to post
        [HttpPost]
        public RedirectToActionResult HomePage(string postMessage)
        {
            User u = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            Post p = new Post()
            {
                User = u,
                Message = postMessage
            };

            Repository.AddPost(p, u);
            return RedirectToAction("HomePage", u);
        }

        //Returns signup page
        [HttpGet]
        public IActionResult SignUp()
        {          
            return View();
        }

        //Takes username and quiz answers. Creates a new user
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
            Repository.AddUser(user);
            return RedirectToAction("Index");
        }

        //Privacy view. Allows user to change quiz result privacy T/F
        [HttpGet]
        public IActionResult Privacy(string title)
        {         
            return View("Privacy", title);
        }

        //Changes user Privacy and redirects to homepage when done
        [HttpPost]
        public RedirectToActionResult Privacy(string title, bool privacy)
        {
            User user = Repository.GetUserByUserName(title);
            user.ChangeUserPrivacy(privacy);
            Repository.UpdateUser(user);
            return RedirectToAction("Homepage", user);
        }

        //List of posts from user
        public IActionResult PostList(string title)
        {
            List<Post> posts = Repository.Posts;
            User u = Repository.GetUserByUserName(title);

            ViewBag.user = u.UserName;
            return View(u);
        }

        //Displays username and quiz results for selected user
        public IActionResult ProfilePage(string title)
        {
            User u = Repository.GetUserByUserName(title);
            return View(u);
        }

        //List of all users on the site
        public IActionResult UserList()
        {
            List<User> users = new List<User>();
            List<User> dbUsers = Repository.Users;
            foreach (User u in dbUsers)
            {
                users.Add(u);
            }

            ViewBag.usercount = users.Count;
            return View(users);
        }    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
