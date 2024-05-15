using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Exceptions;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;

namespace WatchPilot.Test
{
    public class Test_ShowAdd
    {
        private IShowLogic showLogic;
        int User1 = 1;
        int User2 = 2;



        [SetUp]
        public void Setup()
        {
            MockShowDAO mockShowDAO = new MockShowDAO();
            MockShowOverviewDAO mockShowOverviewDAO = new MockShowOverviewDAO();
            showLogic = new ShowLogic(mockShowDAO, mockShowOverviewDAO);

            mockShowOverviewDAO.Add(1, "TestOverview");
            mockShowOverviewDAO.Add(2, "TestOverview2");
        }

        [Test]
        public void TestAddShow_ExpectedBehaviour()
        {

            ShowDTO show = new ShowDTO()
            {
                Title = "TestShow",
                Description = "TestDescription",
                ShowOverViewID = 1,
                Picture = "TestPicture",
                LastEdited = DateTime.Now,
                TotalEpisodes = 10,
            };

            showLogic.AddShow(show, User1);  
        }

        [Test]
        public void TestAddShow_UnauthorizedUser()
        {
            ShowDTO show = new ShowDTO()
            {
                Title = "TestShow",
                Description = "TestDescription",
                ShowOverViewID = 1,
                Picture = "TestPicture",
                LastEdited = DateTime.Now,
                TotalEpisodes = 10,
            };

            Assert.Throws<UnauthorizedAccessException>(() => showLogic.AddShow(show, User2));
        }


        [Test]
        public void TestAddShow_InvalidTitle()
        {
            string showName = "";

            ShowDTO show = new ShowDTO()
            {
                Title = showName,
                Description = "TestDescription",
                ShowOverViewID = 1,
                Picture = "TestPicture",
                LastEdited = DateTime.Now,
                TotalEpisodes = 10,
            };

            Assert.Throws<ShowException>(() => showLogic.AddShow(show, User1));
        }

        [Test]
        public void TestAddShow_InvalidTitleLength()
        {
            string showName = "This is a very long show name that is over 50 characters long";

            ShowDTO show = new ShowDTO()
            {
                Title = showName,
                Description = "TestDescription",
                ShowOverViewID = 1,
                Picture = "TestPicture",
                LastEdited = DateTime.Now,
                TotalEpisodes = 10,
            };

            Assert.Throws<ShowException>(() => showLogic.AddShow(show, User1));
        }

        [Test]
        public void TestAddShow_NullTitle()
        {
            
            ShowDTO show = new ShowDTO()
            {
                Title = null,
                Description = "TestDescription",
                ShowOverViewID = 1,
                Picture = "TestPicture",
                LastEdited = DateTime.Now,
                TotalEpisodes = 10,
            };

            Assert.Throws<ShowException>(() => showLogic.AddShow(show, User1));
        }
    }
}
