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
    public class Test_GetShowOverview
    {
        private IShowOverviewLogic showOverviewLogic;

        [SetUp]
        public void Setup()
        {
            MockShowOverviewDAO mockShowOverviewDAO = new MockShowOverviewDAO();
            showOverviewLogic = new ShowOverviewLogic(mockShowOverviewDAO);
            mockShowOverviewDAO.Add(1, "TestOverview");
            mockShowOverviewDAO.Add(1, "TestOverview2");
        }

        [Test]
        public void TestGetShowOverview_ExpectedBehavior()
        {
            ShowOverviewDTO showOverview = showOverviewLogic.Get(1, 1);
            
            Assert.AreEqual("TestOverview", showOverview.OverviewName);
            Assert.AreEqual(1, showOverview.UserID);
            Assert.AreEqual(1, showOverview.ShowOverviewID);
        }

        [Test]
        public void TestGetShowOverview_UnauthorizedUser()
        {
            Assert.Throws<UnauthorizedAccessException>(() => showOverviewLogic.Get(1, 2));
        }

        [Test] 
        public void TestGetShowOverview_OverviewDoesNotExist()
        {
            Assert.Throws<UnkownErrorException>(() => showOverviewLogic.Get(3, 1));
        }

        [Test]
        public void TestGetShowOverview_ExpectedBehaviorForAllOfUser()
        {
            List<ShowOverviewDTO> showOverviews = showOverviewLogic.GetAllOfUser(1);
            
            Assert.AreEqual(2, showOverviews.Count);
            Assert.AreEqual("TestOverview", showOverviews[0].OverviewName);
            Assert.AreEqual(1, showOverviews[0].UserID);
            Assert.AreEqual(1, showOverviews[0].ShowOverviewID);

            Assert.AreEqual("TestOverview2", showOverviews[1].OverviewName);
            Assert.AreEqual(1, showOverviews[1].UserID);
            Assert.AreEqual(2, showOverviews[1].ShowOverviewID);
        }
    }
}
