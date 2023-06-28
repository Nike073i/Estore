using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.BL.Resume;
using Resunet.Models;
using Resunet.ViewMappers;
using System.Diagnostics;

namespace Resunet.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser _currentUser;
        private readonly IResume _resume;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IResume resume)
		{
			_logger = logger;
            _currentUser = currentUser;
            _resume = resume;
        }

		public async Task<IActionResult> Index()
		{
			var latestModels = await _resume.Search(4);
			var resumes = latestModels.Select(resume => ProfileMapper.MapProfileModelToProfileViewModel(resume));
			return View(resumes);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}