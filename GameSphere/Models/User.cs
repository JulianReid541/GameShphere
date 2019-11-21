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

        public string UserName { get; set; }

        public List<User> Followers { get { return followers; } }
        public List<User> Following { get { return following; } }
    }
}
