using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IShowOverviewDAO
    {
        ShowOverviewDTO Add(int userID, string overviewName);

        ShowOverviewDTO Get(int showOverviewID);

        List<ShowOverviewDTO> GetAllOfUser(int userId);
    }
}

