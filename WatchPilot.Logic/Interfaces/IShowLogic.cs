﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Logic.Interfaces
{
    public interface IShowLogic
    {
        void AddShow(ShowDTO Show);

        List<ShowDTO> GetAll(int ShowOverviewID);
    }
}
