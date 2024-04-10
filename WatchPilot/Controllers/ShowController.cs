using Microsoft.AspNetCore.Mvc;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Logic.Logic;
using WatchPilot.Models;

namespace WatchPilot.Controllers
{
    public class ShowController : Controller
    {
        private readonly IImageLogic _ImageLogic;
        private readonly IShowLogic _ShowLogic;

        public ShowController(IImageLogic imageLogic, IShowLogic showLogic)
        {
            _ImageLogic = imageLogic;
            _ShowLogic = showLogic;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> NewShow(IFormFile picture, int totalEpisodes, string title, string description)
        {
            ShowViewModel NewShow = new ShowViewModel
            {
                Title = title,
                Description = description,
                TotalEpisodes = totalEpisodes,
                CurrentEpisode = 0,
                LastEdited = DateTime.UtcNow,
                ShowOverViewID = 1
            };

            //Dit later verplaatsen naar anderen plek
            if (picture == null)
            {
                NewShow.Picture = "";
            } else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    var bytes = memoryStream.ToArray();
                    string path = await _ImageLogic.UploadImage(bytes, picture.FileName);
                    NewShow.Picture = path;
                }
            }

            ShowDTO showDTO = NewShow.ToDTO();
            

            _ShowLogic.AddShow(showDTO);


            return View("~/Views/Home/privacy.cshtml");
        }


        [HttpGet]
        public IActionResult GetAllInOverview(int showOverviewID)
        {
            List<ShowDTO> showsDTO = _ShowLogic.GetAll(showOverviewID);
            List<ShowViewModel> showsViewModel = new List<ShowViewModel>();
            foreach (ShowDTO show in showsDTO)
            {
                showsViewModel.Add(new ShowViewModel().FromDTO(show));
            }

            return View("~/Views/Home/ShowOverview.cshtml", showsViewModel);
        }



    }
}
