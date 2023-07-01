using Estore.BL.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("/product/{uniqueId}")]
        public async Task<IActionResult> Index(string uniqueId)
        {
            var model = await _product.GetProduct(uniqueId);
            return View("Index", model);
        }
    }
}
