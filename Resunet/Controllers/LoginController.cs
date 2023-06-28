using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.BL.Exceptions;
using Resunet.Filters;
using Resunet.ViewModels;

namespace Resunet.Controllers
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
