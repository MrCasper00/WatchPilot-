using WatchPilot.Logic.Entities;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;

namespace WatchPilot.Test
{
    public class Test_ObtainUser
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
        public void ObtainUser_ExpectedBehavior()
        {
            User userToObtain = new User
            (
                username: null,
                password: null,
                userID: 1
            );


            var user = userLogic.ObtainUser(userToObtain);

            Assert.AreEqual(1, user.UserID);
            Assert.AreEqual("TestUser", user.Username);

        }


        [Test]
        public void ObtainUser_ThrowsExceptionForInvalid()
        {
            User userToObtain = new User
            (
                username: null,
                password: null,
                userID: 2
            );
            Assert.Throws<UserNotFoundException>(() => userLogic.ObtainUser(userToObtain));

        }



    }
}