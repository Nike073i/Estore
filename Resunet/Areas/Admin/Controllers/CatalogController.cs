using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Areas.Admin.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IProduct _product;

        public CatalogController(IProduct product)
        {
            _product = product;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("categories/{*categories}")]
        public async Task<IActionResult> Categories(string? categories)
        {
            var uniqueIds = categories?.Split('/') ?? Array.Empty<string>();
            int? categoryId = await _product.GetCategoryId(uniqueIds.Where(m => !string.IsNullOrEmpty(m)));
            var childCategories = await _product.GetChildCategories(categoryId);

            var model = new CategoryViewModel
            {
                BaseUrl = "/admin/catalog/categories/" + categories,
                CurrentCategoryId = categoryId,
                Categories = childCategories.ToList()
            };
            return View(model);
        }

        [HttpPost("categories/{*categories}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategorySave([FromForm] EditCategoryViewModel model, string categories)
        {
            if (ModelState.IsValid)
                await _product.AddCategory(model.CategoryId, model.Name);
            return Redirect("/admin/catalog/categories/" + categories);
        }
    }
}
