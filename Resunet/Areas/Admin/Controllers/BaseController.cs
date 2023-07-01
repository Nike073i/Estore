using Estore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SiteAuthorize(isRequiredAdmin: true)]
    [Route("[area]/[controller]")]
    public class BaseController : Controller {  }
}
