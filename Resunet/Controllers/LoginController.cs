using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;
using Estore.BL.Exceptions;
using Estore.Filters;
using Estore.ViewModels;

namespace Estore.Controllers
{
    [SiteAuthorize("/", false)]
    public class LoginController : Controller
    {
        private readonly IAuth _authBl;

        public LoginController(IAuth authBl)
        {
            _authBl = authBl;
        }

        [HttpGet("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authBl.Authenticate(
                        viewModel.Email!,
                        viewModel.Password!,
                        viewModel.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверный");
                }
            }
            return View("Index", viewModel);
        }
    }
}
