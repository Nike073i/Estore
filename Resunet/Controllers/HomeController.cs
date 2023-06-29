using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;
using Estore.Models;
using Estore.ViewMappers;
using System.Diagnostics;

namespace Estore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser _currentUser;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
		{
			_logger = logger;
            _currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View();
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