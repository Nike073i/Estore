using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProduct _product;
        private readonly int PageSize = 4;

        public ProductCategoryController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("/product-category/{*categories}")]
        public async Task<IActionResult> Index(string categories, int page = 1)
        {
            var uniqueIds = categories.Split('/');
            int? categoryId = await _product.GetCategoryId(uniqueIds.Where(m => !string.IsNullOrEmpty(m)));
            if (categoryId == null)
                return NotFound();
            (int totalProducts, var products) = await _product.GetByCategory(categoryId.Value, PageSize, page);
            var childCategories = await _product.GetChildCategories(categoryId.Value);
            return View(new CatalogViewModel
            {
                Products = products.ToList(),
                Pagination = new()
                {
                    BaseUrl = $"/product-category/{categories}",
                    Page = page,
                    PageSize = PageSize,
                    TotalCount = totalProducts,
                    TotalPages = (int)Math.Ceiling((float)totalProducts / PageSize)
                },
                ChildCategories = childCategories.ToList(),
            });
        }
    }
}
