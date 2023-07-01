using Estore.BL.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Estore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthor _author;

        public AuthorController(IAuthor author)
        {
            _author = author;
        }

        [HttpGet("/author/{uniqueId}")]
        public async Task<IActionResult> Index(string uniqueId)
        {
            var author = await _author.GetAuthor(uniqueId);
            return View("Index", author);
        }
    }
}
