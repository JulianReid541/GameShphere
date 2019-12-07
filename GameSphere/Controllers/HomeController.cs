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
        //TODO use EF to make a database
        
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
            ViewBag.followingCount = u.Following.Count;
            ViewBag.followerCount = u.Followers.Count;
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
            List<Post> userPosts = new List<Post>();
            List<Post> posts = Repository.Posts;
            User u = Repository.GetUserByUserName(title);

            foreach (Post p in posts.Where(p => p.User.UserName == u.UserName))
            {
                userPosts.Add(p);
            }
            ViewBag.user = u.UserName;
            return View(userPosts);
        }

        //List of every person the signed in user is following
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

        //List of followers the signed in user has
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

        //Unfollows selected user
        public RedirectToActionResult Unfollow(string title)
        {
            User u = Repository.GetUserByUserName(title);
            User u2 = Repository.GetUserByUserName(TempData["signedInUser"] as string);
            TempData.Keep();
            u2.RemoveFollow(u);
            Repository.UpdateUser(u2);
            u.RemovingFollower(u2);
            Repository.UpdateUser(u);
            return RedirectToAction("HomePage", u2);
        }       

        //Follows selected user
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
                Repository.UpdateUser(u2);
                u.AddFollower(u2);
                Repository.UpdateUser(u);
            }               
            return RedirectToAction("UserList");
        }

        //Result if User tries to follow himself
        public IActionResult FollowResult1()
        {
            ViewBag.followResult = "You can't follow yourself";
            return View();
        }

        //Result if user is already following someone 
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
