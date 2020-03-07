using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSphere.Models;

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
