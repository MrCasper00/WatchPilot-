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

        private void ShowValidation(ShowDTO show)
        {
            if (show.Title.Length > 50 || show.Title.Length < 1 || show.Title == null)
            {
                throw new ShowException("Title must be between 1 and 50 characters");
            }
            if (show.TotalEpisodes == null || show.TotalEpisodes <= 0)
            {
                throw new ShowException("Total episodes must be greater than 0");
            }
            if (show.Description.Length <= 0 || show.Description == null)
            {
                throw new ShowException("Description cannot be empty");
            }
        }



        public void AddShow(ShowDTO show, int userID)
        {
            try
            {
                ShowOverviewDTO test = _showOverviewDAO.Get((int)show.ShowOverViewID);
                if (test.UserID != userID)
                {
                    throw new UnauthorizedAccessException("User is not authorized to add a show to this overview");
                }
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    throw;
                }
                throw new UnkownErrorException();
            }
            try
            {
                ShowValidation(show);
                _showDAO.Add(show);
            } catch (Exception e)
            {
                if (e is ShowException)
                {
                    throw;
                }

                throw new UnkownErrorException();
            }
            
        }

        public List<ShowDTO> GetAll(int showOverviewID, int userID)
        {
            try
            {
                ShowOverviewDTO test = _showOverviewDAO.Get(showOverviewID);
                if (test == null)
                {
                    return new List<ShowDTO>();
                }
                if (test.UserID != userID)
                {
                    throw new UnauthorizedAccessException("User is not authorized to view this show");
                }
                
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    throw;
                }
                throw new UnkownErrorException();
            }

            try
            {
                return _showDAO.GetAll(showOverviewID);
            }
            catch (Exception e)
            {
                throw new UnkownErrorException();
            }
            
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
                if (show.ShowOverViewID != showOverviewID)
                {
                    throw new UnauthorizedAccessException("User is not authorized to view this show");
                }
                return show;
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    throw;
                }
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
