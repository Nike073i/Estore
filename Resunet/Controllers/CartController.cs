using Estore.BL.Catalog;
using Estore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;

        public CartController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet("/cart")]
        public async Task<IActionResult> Index()
        {
            var model = await _cart.GetCurrentUserCart();
            return View("Index", model);
        }

        [HttpPost("/cart/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId)
        {
            await _cart.AddCurrentUserCartProduct(productId);
            return Redirect("/cart");
        }

        [HttpPost("/cart/update")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Update(CartUpdateViewModel model)
        {
            await _cart.UpdateCurrentUserCartProduct(model.ProductId, model.ProductCount);
            return Redirect("/cart");
        }
    }
}
