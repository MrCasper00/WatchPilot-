using WatchPilot.Logic.DataTransferObjects;
namespace WatchPilot.Models
{
    public class ShowOverviewViewModel
    {
        public int ShowOverviewID { get; set; }
        public int UserID { get; set; }
        public string OverviewName { get; set; }

        public ShowOverviewDTO ToDTO()
        {
            return new ShowOverviewDTO
            {
                ShowOverviewID = this.ShowOverviewID,
                UserID = this.UserID,
                OverviewName = this.OverviewName
            };
        }

        public ShowOverviewViewModel FromDTO(ShowOverviewDTO showOverviewDTO)
        {
            return new ShowOverviewViewModel
            {
                ShowOverviewID = showOverviewDTO.ShowOverviewID,
                UserID = showOverviewDTO.UserID,
                OverviewName = showOverviewDTO.OverviewName
            };
        }

    }
}
