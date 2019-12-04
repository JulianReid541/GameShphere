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
        //TODO make pages look better looking and MAKE COMMENTS
        //TODO write some tests 
        //TODO use EF to make a database
        //testingdata
        #region

        public HomeController()
        {
            if(Repository.Users.Count == 0)
            {
                User test1 = new User()
                {
                    UserName = "test",
                    Game = "Call of Duty",
                    Console = "Xbox",
                    Genre = "FPS",
                    Platform = "Twitch",
                    Privacy = true
                };               
                User test2 = new User()
                {
                    UserName = "test2",
                    Game = "Halo 4",
                    Console = "PC",
                    Genre = "Horror",
                    Platform = "YoutubeGaming",
                    Privacy = false                  
                };               
                Post p = new Post()
                {
                    User = test2,
                    Message = "This new site is amazing"
                };
                User test3 = new User()
                {
                    UserName = "test3",
                    Game = "Halo 5",
                    Console = "PC",
                    Genre = "Horror",
                    Platform = "YoutubeGaming",
                    Privacy = true
                };
                Post p2 = new Post()
                {
                    User = test3,
                    Message = "This is WAY COOLER THAN FACEBOOK"
                };
                test2.AddPost(p);
                test3.AddPost(p2);
                test1.AddFollowing(test2);
                test1.AddFollowing(test3);
                test1.AddFollower(test2);
                test2.AddFollower(test1);
                test2.AddFollowing(test1);
                test3.AddFollower(test1);
                Repository.Users.Add(test3);
                Repository.Users.Add(test2);
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
            List<User> users = Repository.Users;           
            ViewBag.usercount = users.Count;
            return View(users);
        }

        public RedirectToActionResult Unfollow(string title)
        {
            User u = Repository.GetUserByUserName(title);
            User u2 = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            u2.RemoveFollow(u);
            return RedirectToAction("HomePage", u2);
        }       

        public ActionResult Follow(string title)
        {
            User u = Repository.GetUserByUserName(title);
            User u2 = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            if (u2.UserName == u.UserName)
            {
                return Content("You can't follow yourself");
            }
            if (u2.Following.Contains(u))
            {
                return Content("You are already following that person");
            }
            else
                u2.AddFollowing(u);           
            return View("UserList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
