using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }
}
