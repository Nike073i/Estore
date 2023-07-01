using Microsoft.AspNetCore.Mvc;

namespace Estore.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
