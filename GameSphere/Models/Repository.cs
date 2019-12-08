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
       
        public List<User> Users {  get { return context.Users.Include("Posts")
                                                             .ToList(); } }

        public List<Post> Posts {  get { return context.Posts
                                                             .ToList(); } }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User u)
        {
            context.Users.Update(u);
            context.SaveChanges();
        }

        public void AddPost(Post p , User u)
        {
            context.Posts.Add(p);
            u.AddPost(p);
            context.Users.Update(u);
            context.SaveChanges();
        }

        public User GetUserByUserName(string username)
        {
            User user;
            user = context.Users.First(u => u.UserName == username);                       
            return user;
        }       
    }    
}
