using WatchPilot.Logic;
using WatchPilot.Logic.Exceptions;

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
        }

        [Test]
        public void ObtainUser_ExpectedBehavior()
        {
            var user = userLogic.ObtainUser(1);
            
            Assert.AreEqual(1, user.UserID);
            Assert.AreEqual("TestUser", user.Username);
            
        }


        [Test]
        public void ObtainUser_ThrowsExceptionForInvalid()
        {
            //var user = userLogic.ObtainUser(2);
            Assert.Throws<UserNotFoundException>(() => userLogic.ObtainUser(-1));
            
        }

        

    }
}