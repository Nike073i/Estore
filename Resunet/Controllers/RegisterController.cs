using Microsoft.AspNetCore.Mvc;
using Estore.BL.Auth;
using Estore.BL.Exceptions;
using Estore.Filters;
using Estore.ViewMappers;
using Estore.ViewModels;

namespace Estore.Controllers
{
    [SiteAuthorize("/", false)]
    public class RegisterController : Controller
    {
        private readonly IAuth _authBl;

        public RegisterController(IAuth authBl)
        {
            _authBl = authBl;
        }

        [HttpGet("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authBl.RegisterAsync(AuthMapper.MapRegisterViewModelToUserModel(viewModel));
                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.TryAddModelError("Email", "Email уже существует");
                }
            }
            return View("Index", viewModel);
        }
    }
}
