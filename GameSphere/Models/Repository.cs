using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameSphere.Models
{
    public class Repository : IRepository
    {
        private ApplicationDbContext context;

        public Repository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }     

        public List<Post> Posts {  get { return context.Posts
                                                             .ToList(); } }

        public void AddUser(AppUser user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(AppUser u)
        {
            context.Users.Update(u);
            context.SaveChanges();
        }

        public void AddPost(Post p , AppUser u)
        {
            context.Posts.Add(p);
            u.AddPost(p);
            context.Users.Update(u);
            context.SaveChanges();
        }  
    }    
}
