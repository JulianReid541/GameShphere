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
        public List<User> Users { get { return users; } }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User GetUserByUserName(string username)
        {
            User user = users.Find(u => u.UserName == username);
            return user;
        }
    }
}
