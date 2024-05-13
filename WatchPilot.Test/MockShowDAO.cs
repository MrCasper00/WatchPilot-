using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Test
{
    public class MockShowDAO : IShowDAO
    {
        public int Add(ShowDTO show)
        {
            return 1;
        }

        public List<ShowDTO> GetAll(int ShowOverviewID)
        {
            if (ShowOverviewID == 1)
            {
                return new List<ShowDTO> { new ShowDTO { ShowID = 1, Picture = "Path/Location/image.png", CurrentEpisode = 0, Description = "test", LastEdited = DateTime.UtcNow, ShowOverViewID= 1, Title= "Title", TotalEpisodes= 12 } };
            } else
            {
                return new List<ShowDTO>();
            }
        }

        public ShowDTO Get(int ShowID)
        {
            if (ShowID == 1)
            {
                return new ShowDTO { ShowID = ShowID, Picture = "Path/Location/image.png", CurrentEpisode = 0, Description = "test", LastEdited = DateTime.UtcNow, ShowOverViewID = 1, Title = "Title", TotalEpisodes = 12 };
            } else 
            {
                return null;
            }
            
        }

        public ShowDTO Get(int ShowID, int ShowOverviewID)
        {
            if (ShowID == 1 || ShowOverviewID == 1)
            {
                return new ShowDTO { ShowID = ShowID, Picture = "Path/Location/image.png", CurrentEpisode = 0, Description = "test", LastEdited = DateTime.UtcNow, ShowOverViewID = ShowOverviewID, Title = "Title", TotalEpisodes = 12 };
            }
            else
            {
                return null;
            }
        }

        public int Update(ShowDTO showUpdate)
        {
            return 1;
        }

        public int Delete(int ShowID)
        {
            return 1;
        }
    }
}
