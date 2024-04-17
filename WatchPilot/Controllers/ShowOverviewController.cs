using Microsoft.AspNetCore.Mvc;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;
using WatchPilot.Models;
namespace WatchPilot.Controllers
{
    public class ShowOverviewController : Controller
    {
        private readonly IShowOverviewLogic _showOverviewLogic;
        private readonly IShowLogic _showLogic;

        public ShowOverviewController(IShowOverviewLogic showOverviewLogic, IShowLogic showLogic)
        {
            _showOverviewLogic = showOverviewLogic;
            _showLogic = showLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateShowOverview(string overviewName)
        {
            //UserID word later opgehaald met authoriazation claims. Vandaar dat deze nu gehardcode is.
            int userId = 1;


            _showOverviewLogic.Add(userId, overviewName);
            


            return RedirectToAction("GetShowOverviews", new { userId = userId});
        }

        [HttpGet]
        public IActionResult GetShowOverviews(int userId)
        {
            List<ShowOverviewDTO> showOverviews = _showOverviewLogic.GetAllOfUser(userId);

            List<ShowOverviewViewModel> showOverviewModel = new List<ShowOverviewViewModel>();
            foreach (ShowOverviewDTO showOverview in showOverviews)
            {
                showOverviewModel.Add(new ShowOverviewViewModel().FromDTO(showOverview));
            }

            ShowDashboardViewModel viewModel = new ShowDashboardViewModel();
            viewModel.ShowOverviews = showOverviewModel;


            return View("~/Views/Home/ShowOverview.cshtml", viewModel);
        }
    }
}
