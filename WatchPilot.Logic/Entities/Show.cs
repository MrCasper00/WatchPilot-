using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Entities 
{
    public class Show
    {
        public int? ShowID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? TotalEpisodes { get; set; }
        public int? CurrentEpisode { get; set; }
        public DateTime? LastEdited { get; set; }
        public string? Picture { get; set; }
        public int? ShowOverViewID { get; set; }

    }
}
