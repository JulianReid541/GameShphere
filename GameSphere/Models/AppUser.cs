using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class AppUser
    {
        private List<Post> posts = new List<Post>();

        public string UserName { get; set; }
        public string Game { get; set; }
        public string Console { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public bool Privacy { get; set; }

        public List<Post> Posts { get { return posts; } }

        public void AddPost(Post p)
        {
            Posts.Add(p);
        }

        public bool ChangeUserPrivacy(bool tf)
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
