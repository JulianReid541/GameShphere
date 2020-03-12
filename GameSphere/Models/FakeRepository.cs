using System.Collections.Generic;

namespace GameSphere.Models
{
    public class FakeRepository : IRepository
    {
        private List<Post> posts = new List<Post>();
        public List<Post> Posts { get { return Posts; } }

        public void UpdateUser(AppUser user) { }

        public void AddPost(Post p, AppUser U) { }
        public void AddUser(AppUser u) { }

    }
}
