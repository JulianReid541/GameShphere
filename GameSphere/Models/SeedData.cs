using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSphere.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GameSphere.Models
{
    public class SeedData
    {
        public static void  Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                User test1 = new User()
                {
                    UserName = "test",
                    Game = "Call of Duty",
                    Console = "Xbox",
                    Genre = "FPS",
                    Platform = "Twitch",
                    Privacy = true
                };
                context.Users.Add(test1);
                User test2 = new User()
                {
                    UserName = "test2",
                    Game = "Halo 4",
                    Console = "PC",
                    Genre = "Horror",
                    Platform = "YoutubeGaming",
                    Privacy = false
                };                
                context.Users.Add(test2);
                User test3 = new User()
                {
                    UserName = "test3",
                    Game = "Halo 5",
                    Console = "PC",
                    Genre = "Horror",
                    Platform = "YoutubeGaming",
                    Privacy = true
                };
                context.Users.Add(test3);
                Post p = new Post()
                {
                    User = test2,
                    Message = "This new site is amazing"
                };
                test2.AddPost(p);
                context.Posts.Add(p);
                Post p2 = new Post()
                {
                    User = test3,
                    Message = "This is WAY COOLER THAN FACEBOOK"
                };
                test3.AddPost(p2);
                context.Posts.Add(p2);

                context.SaveChanges();
            }
        }
    }
}
