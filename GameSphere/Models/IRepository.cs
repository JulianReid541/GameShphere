using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSphere.Models;

namespace GameSphere.Models
{
    public interface IRepository
    {
        List<User> Users { get; }
        void AddUser(User user);
        void UpdateUser(User user);
        void AddPost(Post p, User u);
        User GetUserByUserName(string username);
    }
}
