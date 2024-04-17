using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Logic
{
    public class ShowOverviewLogic : Interfaces.IShowOverviewLogic
    {
        private Interfaces.IShowOverviewDAO _showOverviewDAO;

        public ShowOverviewLogic(Interfaces.IShowOverviewDAO showOverviewDAO)
        {
            _showOverviewDAO = showOverviewDAO;
        }

        public ShowOverviewDTO Add(int userID, string overviewName)
        {
            return _showOverviewDAO.Add(userID, overviewName);
        }

        public ShowOverviewDTO Get(int showOverviewID)
        {
            return _showOverviewDAO.Get(showOverviewID);
        }

        public List<ShowOverviewDTO> GetAllOfUser(int userId)
        {
            return _showOverviewDAO.GetAllOfUser(userId);
        }
    }
}
