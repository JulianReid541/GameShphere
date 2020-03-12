using System.Collections.Generic;

namespace GameSphere.Models
{
    public interface IRepository
    {
        List<Post> Posts { get; }
        void AddUser(AppUser user);
        void UpdateUser(AppUser user);
        void AddPost(Post p, AppUser u);
    }
}
