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
        //TODO write some tests 
        //TODO use EF to make a database
        //testingdata

        IRepository Repository;
        public HomeController(IRepository r)
        {
            Repository = r;
        }

        [HttpGet]
        public IActionResult Index()
        {         
            return View();
        }

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

        [HttpGet]
        public IActionResult HomePage(User user)
        {
            User u = Repository.GetUserByUserName(user.UserName);            
            ViewBag.postCount = u.Posts.Count;
            ViewBag.followingCount = u.Following.Count;
            ViewBag.followerCount = u.Followers.Count;
            return View(u);
        }

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

            u.AddPost(p);
            return RedirectToAction("HomePage", u);
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
            return View("Privacy", title);
        }

        [HttpPost]
        public RedirectToActionResult Privacy(string title, bool privacy)
        {
            User user = Repository.GetUserByUserName(title);
            user.changeUserPrivacy(privacy);
            return RedirectToAction("Homepage", user);
        }

        public IActionResult PostList(string title)
        {
            List<Post> posts = new List<Post>();
            User u = Repository.GetUserByUserName(title);
            foreach (Post p in u.Posts)
            {
                posts.Add(p);
            }
            ViewBag.user = u.UserName;
            return View(posts);
        }

        public IActionResult FollowingList(string title)
        {
            List<User> following = new List<User>();
            User u = Repository.GetUserByUserName(title);
            foreach (User f in u.Following)
            {
                following.Add(f);
            }
            ViewBag.user = u.UserName;
            return View(following);
        }

        public IActionResult FollowersList(string title)
        {
            List<User> followers = new List<User>();
            User u = Repository.GetUserByUserName(title);
            foreach (User f in u.Followers)
            {
                followers.Add(f);
            }
            ViewBag.user = u.UserName;
            return View(followers);
        }

        public IActionResult ProfilePage(string title)
        {
            User u = Repository.GetUserByUserName(title);
            return View(u);
        }

        public IActionResult UserList()
        {
            List<User> users = new List<User>();
            foreach (User u in Repository.Users)
            {
                users.Add(u);
            }

            ViewBag.usercount = users.Count;
            return View(users);
        }

        public RedirectToActionResult Unfollow(string title)
        {
            User u = Repository.GetUserByUserName(title);
            User u2 = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            u2.RemoveFollow(u);
            u.RemovingFollower(u2);
            return RedirectToAction("HomePage", u2);
        }       

        public RedirectToActionResult Follow(string title)
        {
            User u = Repository.GetUserByUserName(title);
            User u2 = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            if (u2.UserName == u.UserName)
            {                
                return RedirectToAction("FollowResult1");
            }
            if (u2.Following.Contains(u))
            {               
                return RedirectToAction("FollowResult2");
            }
            else
            {
                u2.AddFollowing(u);
                u.AddFollower(u2);
            }               
            return RedirectToAction("UserList");
        }

        public IActionResult FollowResult1()
        {
            ViewBag.followResult = "You can't follow yourself";
            return View();
        }

        public IActionResult FollowResult2()
        {
            ViewBag.followResult = "You already follow that person";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
