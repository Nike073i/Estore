using Estore.BL.Catalog;
using Estore.BL.Models;
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
            if (model == null)
                return NotFound();
            if (model.Categories != null)
            {
                model.Breadcrumps = model?.Categories.Select((m, i) => new BreadcrumpModel()
                {
                    Name = m.CategoryName,
                    Link = "/product-category/" + model.CategoryPath(i)
                }).Reverse().ToList();
            }
            return View("Index", model);
        }
    }
}
