using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Entities;    

namespace WatchPilot.Test
{
    public class Test_LoginUser
    {
        private IUserLogic userLogic;

        [SetUp]
        public void Setup()
        {
            MockUserDAO mockUserDAO = new MockUserDAO();
            userLogic = new UserLogic(mockUserDAO);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("TestPassword", workFactor: 15);
            UserDTO userToAdd = new UserDTO
            {
                Username = "TestUser",
                Password = hashedPassword
            };

            mockUserDAO.Add(userToAdd);
        }

        [Test]
        public void LoginUser_ExpectedBehavior()
        {
            User userToLogin = new User
            (
                username: "TestUser",
                password: "TestPassword",
                userID: null
            );


            UserDTO user = userLogic.LoginUser(userToLogin);

            Assert.AreEqual(1, user.UserID);
            Assert.AreEqual("TestUser", user.Username);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("TestPassword", user.Password));
        }

        [Test]
        public void LoginUser_ThrowsExceptionForInvalidPassword()
        {
            User userToLogin = new User
            (
                username: "TestUser",
                password: "InvalidPassword",
                userID: null
            );

            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser(userToLogin));
        }

        [Test]
        public void LoginUser_ThrowsExceptionForInvalidUser()
        {
            User userToLogin = new User
            (
                username: "InvalidUser",
                password: "TestPassword",
                userID: null
            );

            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser(userToLogin));
        }


        [Test]
        public void LoginUser_ThrowsExceptionForInvalidUserAndPassword()
        {
            User userToLogin = new User
            (
                username: "InvalidUser",
                password: "InvalidPassword",
                userID: null
            );

            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser(userToLogin));
        }




    }
}
