using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSphere.Models;

namespace GameSphere.Models
{
    public class FakeRepository : IRepository
    {
        private List<User> users = new List<User>();
        private List<Post> posts = new List<Post>();
        public List<User> Users { get { return users; } }
        public List<Post> Posts { get { return Posts; } }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user) { }

        public void AddPost(Post p, User U) { }

        public User GetUserByUserName(string username)
        {
            User user = users.Find(u => u.UserName == username);
            return user;
        }
    }
}
