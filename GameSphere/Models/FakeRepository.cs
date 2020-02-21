using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSphere.Models;

namespace GameSphere.Models
{
    public class FakeRepository : IRepository
    {
        private List<AppUser> users = new List<AppUser>();
        private List<Post> posts = new List<Post>();
        public List<AppUser> Users { get { return users; } }
        public List<Post> Posts { get { return Posts; } }

        public void AddUser(AppUser user)
        {
            users.Add(user);
        }

        public void UpdateUser(AppUser user) { }

        public void AddPost(Post p, AppUser U) { }

        public AppUser GetUserByUserName(string username)
        {
            AppUser user = users.Find(u => u.UserName == username);
            return user;
        }
    }
}
