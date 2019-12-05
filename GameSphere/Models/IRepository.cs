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
        User GetUserByUserName(string username);
    }
}
