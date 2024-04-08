using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Data.Entities
{
    internal class Show
    {
        int ShowID { get; set; }
        string title { get; set; }
        string description { get; set; }
        int AmountOfEpisodes { get; set; }
        int CurrentEpisode { get; set; }
        string image { get; set; }
        DateTime LastEdited { get; set; }
        int ShowOverViewID { get; set; }
    }
}
