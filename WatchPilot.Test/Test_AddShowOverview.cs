using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;
using System.Xml;

namespace WatchPilot.Test
{
    public class Test_AddShowOverview
    {
        private IShowOverviewLogic showOverviewLogic;

        [SetUp]
        public void Setup()
        {
            MockShowOverviewDAO mockShowOverviewDAO = new MockShowOverviewDAO();
            showOverviewLogic = new ShowOverviewLogic(mockShowOverviewDAO);
        }

        [Test]
        public void TestAddShowOverview_ExpectedBehaviour()
        {
            int userId = 1;
            string overviewName = "TestOverview";

            ShowOverviewDTO showOverviewDTO = showOverviewLogic.Add(userId, overviewName);

            Assert.AreEqual(1, showOverviewDTO.ShowOverviewID);
            Assert.AreEqual(userId, showOverviewDTO.UserID);
            Assert.AreEqual(overviewName, showOverviewDTO.OverviewName);
        }

        [Test]
        public void TestAddShowOverview_OverviewNameToLong()
        {
            int userId = 1;
            string overviewName = "123456789012345678901234567890123456789012345678901234567890";

            Assert.Throws<ShowException>(() => showOverviewLogic.Add(userId, overviewName));
        }

        [Test]
        public void TestAddShowOverview_OverviewNameToShort()
        {
            int userId = 1;
            string overviewName = "";

            Assert.Throws<ShowException>(() => showOverviewLogic.Add(userId, overviewName));
        }

        [Test]
        public void TestAddShowOverview_OverviewNameNull()
        {
            int userId = 1;
            string overviewName = null;
            Assert.Throws<ShowException>(() => showOverviewLogic.Add(userId, overviewName));
        }

    }
}
