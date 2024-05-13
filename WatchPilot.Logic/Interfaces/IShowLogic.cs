using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IShowLogic
    {
        void AddShow(ShowDTO Show, int userID);

        List<ShowDTO> GetAll(int ShowOverviewID, int userID);

        ShowDTO GetShowDetails(int showID, int showOverviewID, int userID);

        void UpdateShow(ShowDTO show, int userID);

        void DeleteShow(int showID, int showOverviewID, int userID);
    }
}
