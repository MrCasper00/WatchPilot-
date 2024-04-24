using WatchPilot.Logic.DataTransferObjects;

namespace WatchPilot.Models
{
    public class ShowDetailsViewModel
    {
        public int ShowID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalEpisodes { get; set; }
        public int CurrentEpisode { get; set; }
        public DateTime LastEdited { get; set; }
        public string Picture { get; set; }
        public int ShowOverViewID { get; set; }

        public string? ShowsDetailsError { get; set; }

        public void FromDTO(ShowDTO showDTO)
        {

            this.ShowID = (int)showDTO.ShowID;
            this.Title = showDTO.Title;
            this.Description = showDTO.Description;
            this.TotalEpisodes = (int)showDTO.TotalEpisodes;
            this.CurrentEpisode = (int)showDTO.CurrentEpisode;
            this.LastEdited = (DateTime)showDTO.LastEdited;
            this.Picture = showDTO.Picture;
            this.ShowOverViewID = (int)showDTO.ShowOverViewID;
        }

    }
}
