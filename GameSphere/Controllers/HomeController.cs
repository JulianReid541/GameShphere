using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSphere.Models;
using System.Web;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GameSphere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //TODO Cleaning Up/Make work with identity
        IRepository Repository;
        private UserManager<AppUser> userManager;

        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public HomeController(IRepository r, UserManager<AppUser> userMgr)
        {
            Repository = r;
            userManager = userMgr;
        }

        //Homepage taking user object and displays posts and counts for user posts/follows/following
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AppUser> dbUsers = userManager.Users.ToList();
            List<Post> dbLists = Repository.Posts;
            AppUser u = await CurrentUser;

            ViewBag.userName = u.UserName;
            ViewBag.postCount = u.Posts.Count;
            return View(dbLists);
        }

        //Takes postmessage and adds user to post saves it to post
        [HttpPost]
        public async Task<IActionResult> Index(string postMessage)
        {
            AppUser u = await CurrentUser;
            Post p = new Post()
            {
                UserName = u,
                Message = postMessage
            };
            u.AddPost(p);
            Repository.AddPost(p, u);
            await userManager.UpdateAsync(u);
            List<Post> dbLists = Repository.Posts;
            ViewBag.userName = u.UserName;
            ViewBag.postCount = u.Posts.Count;
            return View(dbLists);
        }

            //    //Returns signup page
            //    [HttpGet]
            //    public IActionResult SignUp()
            //    {          
            //        return View();
            //    }

            //    //Takes username and quiz answers. Creates a new user
            //    [HttpPost]
            //    public RedirectToActionResult SignUp(string username, string game, string console,
            //                                         string genre, string platform, bool privacy)
            //    {
            //        User user = new User();
            //        user.UserName = username;
            //        user.Game = game;
            //        user.Console = console;
            //        user.Genre = genre;
            //        user.Platform = platform;
            //        user.Privacy = privacy;
            //        Repository.AddUser(user);
            //        return RedirectToAction("Index");
            //    }

            //    //Privacy view. Allows user to change quiz result privacy T/F
            //    [HttpGet]
            //    public IActionResult Privacy(string title)
            //    {         
            //        return View("Privacy", title);
            //    }

            //    //Changes user Privacy and redirects to homepage when done
            //    [HttpPost]
            //    public RedirectToActionResult Privacy(string title, bool privacy)
            //    {
            //        User user = Repository.GetUserByUserName(title);
            //        user.ChangeUserPrivacy(privacy);
            //        Repository.UpdateUser(user);
            //        return RedirectToAction("Homepage", user);
            //    }

            //    //List of posts from user
            //    public IActionResult PostList(string title)
            //    {
            //        List<Post> posts = Repository.Posts;
            //        User u = Repository.GetUserByUserName(title);

            //        ViewBag.user = u.UserName;
            //        return View(u);
            //    }

            //    //Displays username and quiz results for selected user
            //    public IActionResult ProfilePage(string title)
            //    {
            //        User u = Repository.GetUserByUserName(title);
            //        return View(u);
            //    }

            //    //List of all users on the site
            //    public IActionResult UserList()
            //    {
            //        List<User> users = new List<User>();
            //        List<User> dbUsers = Repository.Users;
            //        foreach (User u in dbUsers)
            //        {
            //            users.Add(u);
            //        }

            //        ViewBag.usercount = users.Count;
            //        return View(users);
            //    }    

            //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //    public IActionResult Error()
            //    {
            //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //    }
            //}
    }
    
}
