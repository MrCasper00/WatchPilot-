using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Test
{
    public class MockShowDAO : IShowDAO
    {
        List<ShowDTO> shows = new List<ShowDTO>();

        public int Add(ShowDTO show)
        {
            show.ShowID = shows.Count + 1;

            if (shows.Contains(show))
            {
                return 0;
            } else
            {
                shows.Add(show);
                return 1;
            }
        }

        public List<ShowDTO> GetAll(int ShowOverviewID)
        {
            List<ShowDTO> showz = new List<ShowDTO>();
            foreach (var show in showz.ToList())
            {
                if (show.ShowOverViewID == ShowOverviewID)
                {
                    showz.Add(show);
                }
            }
            return showz;
        }

        public ShowDTO Get(int ShowID)
        {
            foreach (var show in shows.ToList())
            {
                if (show.ShowID == ShowID)
                {
                    return show;
                }
            }
            return null;
        }

        public ShowDTO Get(int ShowID, int ShowOverviewID)
        {
            foreach(var show in shows.ToList())
            {
                if (show.ShowID == ShowID && show.ShowOverViewID == ShowOverviewID)
                {
                    return show;
                }
            }
            return null;
        }

        public int Update(ShowDTO showUpdate)
        {
            foreach (var show in shows.ToList())
            {
                if (show.ShowID == showUpdate.ShowID)
                {
                    shows.Remove(show);
                    shows.Add(showUpdate);
                    return 1;
                }
            }
            return 0;
        }

        public int Delete(int ShowID)
        {
            foreach(var show in shows.ToList())
            {
                if (show.ShowID == ShowID)
                {
                    shows.Remove(show);
                    return 1;
                }
            }
            return 0;
        }
    }
}
