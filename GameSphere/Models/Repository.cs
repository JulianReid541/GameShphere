using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public static class Repository
    {
        private static List<User> users = new List<User>();

        public static List<User> Users { get { return users; } }

        public static void AddUser(User user)
        {
            Users.Add(user);
        }

        public static User GetUserByUserName(string username)
        {
            User user = users.Find(u => u.UserName == username);
            return user;
        }
    }
}
