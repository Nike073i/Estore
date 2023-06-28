using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;

namespace Resunet.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly ICurrentUser _currentUser;

        public MainMenuViewComponent(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await _currentUser.IsLoggedInAsync();
            return View("Index", isLoggedIn);
        }
    }
}
