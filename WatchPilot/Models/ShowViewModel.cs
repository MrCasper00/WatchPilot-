using WatchPilot.Logic.DataTransferObjects;
namespace WatchPilot.Models
{
    public class ShowViewModel
    {
        public int ?ShowID { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
        public int ?TotalEpisodes { get; set; }
        public int ?CurrentEpisode { get; set; }
        public DateTime ?LastEdited { get; set; }
        public string ?Picture { get; set; }
        public int ?ShowOverViewID { get; set; }


        public ShowDTO ToDTO()
        {
            return new ShowDTO
            {
                ShowID = this.ShowID,
                Title = this.Title,
                Description = this.Description,
                TotalEpisodes = this.TotalEpisodes,
                CurrentEpisode = this.CurrentEpisode,
                LastEdited = this.LastEdited,
                Picture = this.Picture,
                ShowOverViewID = this.ShowOverViewID
            };
        }


        public ShowViewModel FromDTO(ShowDTO showDTO)
        {
            return new ShowViewModel
            {
                ShowID = showDTO.ShowID,
                Title = showDTO.Title,
                Description = showDTO.Description,
                TotalEpisodes = showDTO.TotalEpisodes,
                CurrentEpisode = showDTO.CurrentEpisode,
                LastEdited = showDTO.LastEdited,
                Picture = showDTO.Picture,
                ShowOverViewID = showDTO.ShowOverViewID
            };
        }


    }
}
