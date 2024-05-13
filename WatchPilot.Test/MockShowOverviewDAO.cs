using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Test
{
    public class MockShowOverviewDAO : IShowOverviewDAO
    {
        List<ShowOverviewDTO> showOverviews = new List<ShowOverviewDTO>();

        public ShowOverviewDTO Add(int userID, string overviewName)
        {
            ShowOverviewDTO showOverview = new ShowOverviewDTO { ShowOverviewID = showOverviews.Count+1, UserID = userID, OverviewName = overviewName };
            showOverviews.Add(showOverview);
            return showOverview;
        }

        public ShowOverviewDTO Get(int showOverviewID)
        {
            foreach (ShowOverviewDTO showOverview in showOverviews)
            {
                if (showOverview.ShowOverviewID == showOverviewID)
                {
                    return showOverview;
                }
            }
            return null;
        }

        public List<ShowOverviewDTO> GetAllOfUser(int userId)
        {
            List<ShowOverviewDTO> showOverviewsOfUser = new List<ShowOverviewDTO>();
            foreach (ShowOverviewDTO showOverview in showOverviews)
            {
                if (showOverview.UserID == userId)
                {
                    showOverviewsOfUser.Add(showOverview);
                }
            }
            if (showOverviewsOfUser.Count == 0)
            {
                return null;
            } else
            {
                return showOverviewsOfUser;
            }

        }
    }
}
