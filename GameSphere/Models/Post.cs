using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSphere.Models
{
    public class Post
    {
        public int PostID { get; set; }       
        public string Message { get; set; }

        public AppUser User { get; set; }
        public int UserID { get; set; }
    }
}
