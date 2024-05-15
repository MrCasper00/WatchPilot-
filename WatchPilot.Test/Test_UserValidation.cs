using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Logic;
using WatchPilot.Logic.Entities;

namespace WatchPilot.Test
{
    public class Test_UserValidation
    {
        [Test]
        public void User_ThrowsExceptionForToLongUser()
        {
            Assert.Throws<UsernameException>(() => new User("123456789012345678901234567890123456789012345678901234567890", "TestPassword", 1));
        }

        [Test]
        public void User_ThrowsExceptionForToLongPassword()
        {
            Assert.Throws<PasswordException>(() => new User("TestUser", "123456789012345678901234567890123456789012345678901234567890", 1));
        }

        [Test]
        public void User_ThrowsExceptionForToLongUserAndPassword()
        {
            Assert.Throws<UsernameException>(() => new User("123456789012345678901234567890123456789012345678901234567890", "123456789012345678901234567890123456789012345678901234567890", 1));
        }

        [Test]
        public void User_ThrowsExceptionForEmptyPassword()
        {
            Assert.Throws<PasswordException>(() => new User("TestUser", "", 1));
        }

        [Test]
        public void User_ThrowsExceptionForEmptyUser()
        {
            Assert.Throws<UsernameException>(() => new User("", "TestPassword", 1));
        }

        [Test]
        public void User_ThrowsExceptionForEmptyUserAndPassword()
        {
            Assert.Throws<UsernameException>(() => new User("", "", 1));
        }
        public void User_ThrowsExceptionForNegativeUserID()
        {
            Assert.Throws<ArgumentException>(() => new User("TestUser", "TestPassword", -1));
        }
    }
}

