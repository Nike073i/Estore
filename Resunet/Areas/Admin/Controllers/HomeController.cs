﻿using Estore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
