using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class User
    {
        private List<User> followers = new List<User>();
        private List<User> following = new List<User>();
        private List<Post> posts = new List<Post>();

        public int userID { get; set; }
        public string UserName { get; set; }
        public string Game { get; set; }
        public string Console { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public bool Privacy { get; set; }

        public List<User> Followers { get { return followers; } }
        public List<User> Following { get { return following; } }
        public List<Post> Posts { get { return posts; } }

        public void AddFollowing(User user)
        {
            Following.Add(user);
        }

        public void AddFollower(User user)
        {
            Followers.Add(user);
        }

        public void AddPost(Post p)
        {
            Posts.Add(p);
        }

        public void RemoveFollow(User user)
        {
            Following.Remove(user);
        }

        public void RemovingFollower (User user)
        {
            Followers.Remove(user);
        }

        public bool changeUserPrivacy(bool tf)
        {
            if (tf == Privacy)
            {
                return Privacy;
            }
            else
            {
                Privacy = tf;
                return Privacy;
            }
        }
    }
}
