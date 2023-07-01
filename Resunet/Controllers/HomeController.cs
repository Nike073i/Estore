using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.Filters;
using Estore.Models;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Estore.Controllers
{
    [SiteAllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IProduct _product;

        public HomeController(ICurrentUser currentUser, IProduct product)
        {
            _currentUser = currentUser;
            _product = product;
        }

        public async Task<IActionResult>Index()
        {
            var newProducts = await _product.GetNew(6);
            return View("Index", new HomePageViewModel
            {
                NewProducts = newProducts.ToList()
            });
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