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
    public class Test_CreateUser
    {
        private IUserLogic userLogic;

        [SetUp]
        public void Setup()
        {
            MockUserDAO mockUserDAO = new MockUserDAO();
            userLogic = new UserLogic(mockUserDAO);
        }

        [Test]
        public void CreateUser_ExpectedBehavior()
        {
            User userToCreate = new User
            (
                username: "TestUser",
                password: "TestPassword",
                userID: null
            );

            UserDTO user = userLogic.CreateUser(userToCreate);

            Assert.AreEqual(1, user.UserID);
            Assert.AreEqual("TestUser", user.Username);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("TestPassword", user.Password));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForExistingUser()
        {
            User userToCreate = new User
            (
                username: "TestUser",
                password: "TestPassword",
                userID: null
            );


            userLogic.CreateUser(userToCreate);
            Assert.Throws<UserAlreadyExistsException>(() => userLogic.CreateUser(userToCreate));
        }
    }
}