using Estore.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estore.Filters
{
    public class SiteAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly ICurrentUser _currentUser;
        private readonly string _redirectUrl;
        private readonly bool _isRequireAdmin;

        public SiteAuthorizeFilter(ICurrentUser currentUser, string redirectUrl, bool isRequireAdmin)
        {

            _currentUser = currentUser;
            _redirectUrl = redirectUrl;
            _isRequireAdmin = isRequireAdmin;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isLoggedIn = await _currentUser.IsLoggedInAsync();
            if (isLoggedIn == false)
            {
                context.Result = new RedirectResult(_redirectUrl);
            }
            if (_isRequireAdmin)
            {
                bool isAdmin = _currentUser.IsAdmin();
                if (isAdmin == false)
                    context.Result = new RedirectResult(_redirectUrl);
            }
        }
    }
}