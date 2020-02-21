using System.Collections.Generic;
using GameSphere.Controllers;
using GameSphere.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GameSphere.Tests
{
    public class UserTest
    {
        //Signs up a user and asserts that the username is the same
        [Fact]
        public void SignUpTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            var controller = new HomeController(repo);

            //Act
            controller.SignUp("yellow", "Halo 3", "Xbox", "Horror", "Twitch", true);

            //Assert
            Assert.Equal("yellow", repo.Users[repo.Users.Count - 1].UserName);
        }

        //test that gets user list and checks to see if the order is correct(unsorted)
        [Fact]
        public void UserListTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            AddTestData(repo);
            var controller = new HomeController(repo);

            //Act
            var result = (ViewResult)controller.UserList();
            var users = (List<User>)result.Model;

            //Assert
            Assert.Equal(4, users.Count);
            Assert.True(string.Compare(users[0].UserName, users[1].UserName) > 0 &&
                        string.Compare(users[1].UserName, users[2].UserName) > 0 &&
                        string.Compare(users[2].UserName, users[3].UserName) > 0);
        }

        [Fact]
        public void AddPostTest()
        {
            //Arrange 
            var repo = new FakeRepository();
            AddTestData(repo);
            var controller = new HomeController(repo);

            //Act
            User u = repo.GetUserByUserName("test");
            Post p = new Post()
            {
                User = u,
                Message = "This is amazing"
            };
            u.AddPost(p);

            //Assert
            Assert.Single(u.Posts);
            Assert.Equal("This is amazing", u.Posts[0].Message);
        }  

        //TestData
        #region
        private void AddTestData(FakeRepository repo)
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
            User test2 = new User()
            {
                UserName = "test2",
                Game = "Halo 4",
                Console = "PC",
                Genre = "Horror",
                Platform = "YoutubeGaming",
                Privacy = false
            };
            Post p = new Post()
            {
                User = test2,
                Message = "This new site is amazing"
            };
            User test3 = new User()
            {
                UserName = "test3",
                Game = "Halo 5",
                Console = "PC",
                Genre = "Horror",
                Platform = "YoutubeGaming",
                Privacy = true
            };
            User test4 = new User()
            {
                UserName = "test4",
                Game = "Halo 5",
                Console = "PC",
                Genre = "Horror",
                Platform = "YoutubeGaming",
                Privacy = true
            };
            Post p2 = new Post()
            {
                User = test3,
                Message = "This is WAY COOLER THAN FACEBOOK"
            };
            test2.AddPost(p);
            test3.AddPost(p2);
            repo.AddUser(test4);
            repo.AddUser(test3);
            repo.AddUser(test2);
            repo.AddUser(test1);
        }
        #endregion
    }
}
