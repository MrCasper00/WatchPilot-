using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Exceptions;

namespace WatchPilot.Logic.Logic
{
    public class ShowOverviewLogic : Interfaces.IShowOverviewLogic
    {
        private Interfaces.IShowOverviewDAO _showOverviewDAO;
        int showOverviewNameMaxLength = 50;
        int showOverviewnameMinLength = 1;

        public ShowOverviewLogic(Interfaces.IShowOverviewDAO showOverviewDAO)
        {
            _showOverviewDAO = showOverviewDAO;
        }

        private void ValidateShowOverviewName(string overviewName)
        {
            if (string.IsNullOrWhiteSpace(overviewName))
            {
                throw new ShowException("Show overview name cannot be empty or null");
            }
            if (overviewName.Length > showOverviewNameMaxLength || overviewName.Length < showOverviewnameMinLength)
            {
                throw new ShowException("Show overview name cannot be longer than 50 characters or shorter than 1");
            }
        }

        public ShowOverviewDTO Add(int userID, string overviewName)
        {
            //userID is assumed to be the user id of the logged in user and thus trusted
            ValidateShowOverviewName(overviewName);
            try
            {
                return _showOverviewDAO.Add(userID, overviewName);
            } catch (Exception e)
            {
                throw new UnkownErrorException();
            }
        }

        public ShowOverviewDTO Get(int showOverviewID, int userID)
        {
            try
            {
                ShowOverviewDTO showOverview = _showOverviewDAO.Get(showOverviewID);
                
                if (showOverview.UserID != userID)
                {
                    throw new UnauthorizedAccessException("User is not authorized to view this show overview");
                }
                
            } catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    throw;
                }
                throw new UnkownErrorException();
            }


            return _showOverviewDAO.Get(showOverviewID);
        }

        public List<ShowOverviewDTO> GetAllOfUser(int userId)
        {
            //userID is assumed to be the user id of the logged in user and thus trusted
            try
            {
                return _showOverviewDAO.GetAllOfUser(userId);
            } catch (Exception e)
            {
                throw new UnkownErrorException();
            }
            
        }
    }
}
