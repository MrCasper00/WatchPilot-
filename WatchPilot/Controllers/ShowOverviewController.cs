using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Authorize]
        public IActionResult CreateShowOverview(string overviewName)
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);


            _showOverviewLogic.Add(userId, overviewName);
            


            return RedirectToAction("GetShowOverviews", new { userId = userId});
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetShowOverviews()
        {
            string userIdstring = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdstring);
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
