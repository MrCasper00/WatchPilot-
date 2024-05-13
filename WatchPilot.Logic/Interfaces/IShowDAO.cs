using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IShowDAO
    {
        int Add(ShowDTO show);

        List<ShowDTO> GetAll(int ShowOverviewID);

        ShowDTO Get(int ShowID);

        ShowDTO Get(int ShowID, int ShowOverviewID);

        int Update(ShowDTO showUpdate);

        int Delete(int ShowID);

    }
}
