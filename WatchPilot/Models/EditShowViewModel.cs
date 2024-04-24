namespace WatchPilot.Models
{
    public class EditShowViewModel
    {
        public int ShowID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? TotalEpisodes { get; set; }
        public int? CurrentEpisode { get; set; }
        public DateTime? LastEdited { get; set; }
        public string? Picture { get; set; }

        public int ShowOverviewID { get; set; }
    }
}
