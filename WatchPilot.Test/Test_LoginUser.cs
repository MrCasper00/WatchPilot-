using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;

//namespace WatchPilot.Test
//{
//    public class Test_LoginUser
//    {
//        private IUserLogic userLogic;

//        [SetUp]
//        public void Setup()
//        {
//            MockUserDAO mockUserDAO = new MockUserDAO();
//            userLogic = new UserLogic(mockUserDAO);
//            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("TestPassword", workFactor: 15);
//            mockUserDAO.Add("TestUser", hashedPassword);
//        }

//        [Test]
//        public void LoginUser_ExpectedBehavior()
//        {
//            UserDTO user = userLogic.LoginUser("TestUser", "TestPassword");

//            Assert.AreEqual(1, user.UserID);
//            Assert.AreEqual("TestUser", user.Username);
//            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("TestPassword", user.Password));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForInvalidPassword()
//        {
//            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser("TestUser", "InvalidPassword"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForInvalidUser()
//        {
//            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser("InvalidUser", "TestPassword"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForInvalidUserAndPassword()
//        {
//            Assert.Throws<UserInfoNoMatchException>(() => userLogic.LoginUser("InvalidUser", "InvalidPassword"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForToLongUser()
//        {
//            Assert.Throws<UsernameException>(() => userLogic.LoginUser("123456789012345678901234567890123456789012345678901234567890", "TestPassword"));
//        }


//        [Test]
//        public void LoginUser_ThrowsExceptionForToLongPassword()
//        {
//            Assert.Throws<PasswordException>(() => userLogic.LoginUser("TestUser", "123456789012345678901234567890123456789012345678901234567890"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForToLongUserAndPassword()
//        {
//            Assert.Throws<UsernameException>(() => userLogic.LoginUser("123456789012345678901234567890123456789012345678901234567890", "123456789012345678901234567890123456789012345678901234567890"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForEmptyPassword()
//        {
//            Assert.Throws<PasswordException>(() => userLogic.LoginUser("TestUser", ""));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForEmptyUser()
//        {
//            Assert.Throws<UsernameException>(() => userLogic.LoginUser("", "TestPassword"));
//        }

//        [Test]
//        public void LoginUser_ThrowsExceptionForEmptyUserAndPassword()
//        {
//            Assert.Throws<UsernameException>(() => userLogic.LoginUser("", ""));
//        }


//    }
//}
