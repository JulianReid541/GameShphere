using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class Repository : IRepository
    {
        private static List<User> users = new List<User>();

        public List<User> Users { get { return users; } }

        public Repository()
        {
            if (users.Count == 0)
            {
                AddSeedData();
            }
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User GetUserByUserName(string username)
        {
            User user = users.Find(u => u.UserName == username);
            return user;
        }

        void AddSeedData()
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
            AddUser(test3);
            AddUser(test2);
            AddUser(test1);
        }
    }    
}
