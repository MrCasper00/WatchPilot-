using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;

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
            UserDTO user = userLogic.CreateUser("TestUser", "TestPassword");

            Assert.AreEqual(1, user.UserID);
            Assert.AreEqual("TestUser", user.Username);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("TestPassword", user.Password));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForExistingUser()
        {
            userLogic.CreateUser("TestUser", "TestPassword");

            Assert.Throws<UserAlreadyExistsException>(() => userLogic.CreateUser("TestUser", "TestPassword"));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForToLongUser()
        {
            Assert.Throws<UsernameException>(() => userLogic.CreateUser("123456789012345678901234567890123456789012345678901234567890", "TestPassword"));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForToLongPassword()
        {
            Assert.Throws<PasswordException>(() => userLogic.CreateUser("TestUser", "123456789012345678901234567890123456789012345678901234567890"));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForToLongUserAndPassword()
        {
            Assert.Throws<UsernameException>(() => userLogic.CreateUser("123456789012345678901234567890123456789012345678901234567890", "123456789012345678901234567890123456789012345678901234567890"));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForEmptyPassword()
        {
            Assert.Throws<PasswordException>(() => userLogic.CreateUser("TestUser", ""));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForEmptyUser()
        {
            Assert.Throws<UsernameException>(() => userLogic.CreateUser("", "TestPassword"));
        }

        [Test]
        public void CreateUser_ThrowsExceptionForEmptyUserAndPassword()
        {
            Assert.Throws<UsernameException>(() => userLogic.CreateUser("", ""));
        }
    }
}
