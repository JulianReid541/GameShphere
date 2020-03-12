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
            dbLists.Reverse();
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
            if (postMessage != null)
            {
                
                Post p = new Post()
                {
                    UserName = u,
                    Message = postMessage
                };
                u.AddPost(p);
                Repository.AddPost(p, u);
                await userManager.UpdateAsync(u);
            }          
            List<AppUser> dbUsers = userManager.Users.ToList();
            List<Post> dbLists = Repository.Posts;
            dbLists.Reverse();
            ViewBag.userName = u.UserName;
            ViewBag.postCount = u.Posts.Count;
            return View(dbLists);

        }

        //Privacy view. Allows user to change quiz result privacy T/F
        [HttpGet]
        public IActionResult Privacy(string title)
        {
            return View("Privacy", title);
        }

        //Changes user Privacy and redirects to homepage when done
        [HttpPost]
        public async Task<IActionResult> Privacy(string title, bool privacy)
        {
            AppUser user = await CurrentUser;
            user.ChangeUserPrivacy(privacy);
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        //List of posts from user
        public async Task<IActionResult> PostList(string title)
        {
            List<AppUser> dbUsers = userManager.Users.ToList();
            List<Post> posts = Repository.Posts;
            AppUser u = await userManager.FindByNameAsync(title);

            ViewBag.user = u.UserName;
            return View(u);
        }

        //Displays username and quiz results for selected user
        public async Task<IActionResult> ProfilePage(string title)
        {
            AppUser u = await userManager.FindByNameAsync(title);
            return View(u);
        }

        //List of all users on the site
        public IActionResult UserList()
        {
            List<AppUser> users = new List<AppUser>();
            List<AppUser> dbUsers = userManager.Users.ToList();
            foreach (AppUser u in dbUsers)
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
