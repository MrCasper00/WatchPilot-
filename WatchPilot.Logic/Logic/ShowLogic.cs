using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Exceptions;

namespace WatchPilot.Logic.Logic
{
    public class ShowLogic : IShowLogic
    {
        private readonly IShowDAO _showDAO;
        private readonly IShowOverviewDAO _showOverviewDAO;
        public ShowLogic(IShowDAO showDAO, IShowOverviewDAO showOverviewDAO)
        {
            _showDAO = showDAO;
            _showOverviewDAO = showOverviewDAO;
        }

        public void AddShow(ShowDTO Show)
        {
            _showDAO.Add(Show);
        }

        public List<ShowDTO> GetAll(int showOverviewID)
        {
            return _showDAO.GetAll(showOverviewID);
            
        }


        public ShowDTO GetShowDetails(int showID, int showOverviewID, int userID)
        {
            ShowOverviewDTO overview = _showOverviewDAO.Get(showOverviewID);

            if (overview.UserID != userID)
            {
                throw new UnauthorizedAccessException("User is not authorized to view this show");
            }

            try
            {
                ShowDTO show = _showDAO.Get(showID);
                if (show == null)
                {
                    throw new UnkownErrorException();
                }
                return show;
            } catch (Exception e)
            {
                throw new UnkownErrorException();
            }
        }

        public void UpdateShow(ShowDTO showUpdate, int userID)
        {
            ShowOverviewDTO overview = _showOverviewDAO.Get((int)showUpdate.ShowOverViewID);
            if (overview.UserID != userID)
            {
                throw new UnauthorizedAccessException("User is not authorized to edit this show");
            }

            try
            {
                ShowDTO show = _showDAO.Get((int)showUpdate.ShowID, (int)showUpdate.ShowOverViewID);
                if (show == null)
                {
                    throw new UnkownErrorException();
                }

                
                foreach (var property in typeof(ShowDTO).GetProperties())
                {
                    if (property.GetValue(showUpdate) == null)
                    {
                        property.SetValue(showUpdate, property.GetValue(show));
                    }
                }

                _showDAO.Update(showUpdate);
            }
            catch (Exception e)
            {
                throw new UnkownErrorException();
            }
        }

        public void DeleteShow(int showID, int showOverviewID, int userID)
        {
            ShowOverviewDTO overview = _showOverviewDAO.Get(showOverviewID);
            if (overview.UserID != userID)
            {
                throw new UnauthorizedAccessException("User is not authorized to delete this show");
            }

            try
            {
                ShowDTO show = _showDAO.Get(showID, showOverviewID);
                if (show == null)
                {
                    throw new UnkownErrorException();
                }

                _showDAO.Delete(showID);
                
            }
            catch (Exception e)
            {
                throw new UnkownErrorException();
            }

        }

    }
}
