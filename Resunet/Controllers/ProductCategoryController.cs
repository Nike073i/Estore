using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProduct _product;

        public ProductCategoryController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("/product-category/{*categories}")]
        public async Task<IActionResult> Index(string categories)
        {
            var uniqueIds = categories.Split('/');
            int? categoryId = await _product.GetCategoryId(uniqueIds.Where(m => !string.IsNullOrEmpty(m)));
            if (categoryId == null)
                return NotFound();
            var products = await _product.GetByCategory(categoryId.Value);
            return View(new CatalogViewModel { Products = products.ToList() });
        }
    }
}
