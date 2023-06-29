using Estore.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estore.Filters
{
    public class SiteAllowAnonymousFilter : IAsyncAuthorizationFilter
    {
        private readonly ICurrentUser _currentUser;
        private readonly string _redirectUrl;
        private readonly bool _isOnlyAnonymous;

        public SiteAllowAnonymousFilter(ICurrentUser currentUser, string redirectUrl, bool isOnlyAnonymous)
        {

            _currentUser = currentUser;
            _redirectUrl = redirectUrl;
            _isOnlyAnonymous = isOnlyAnonymous;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_isOnlyAnonymous)
            {
                var isLoggedIn = await _currentUser.IsLoggedInAsync();
                if (isLoggedIn)
                    context.Result = new RedirectResult(_redirectUrl);
            }
        }
    }
}