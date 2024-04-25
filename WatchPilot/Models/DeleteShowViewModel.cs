namespace WatchPilot.Models
{
    public class DeleteShowViewModel
    {
        public int ShowID { get; set; }
        public int ShowOverviewID { get; set; }

        public string? DeleteShowError { get; set; }
    }
}
