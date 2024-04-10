using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Logic.Logic
{
    public class ShowLogic : IShowLogic
    {
        private readonly IShowDAO _showDAO;

        public ShowLogic(IShowDAO showDAO)
        {
            _showDAO = showDAO;
        }

        public void AddShow(ShowDTO Show)
        {
            _showDAO.Add(Show);
        }

        public List<ShowDTO> GetAll(int ShowOverviewID)
        {
            return _showDAO.GetAll(ShowOverviewID);
            
        }
    }
}
