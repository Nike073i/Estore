using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Resunet.BL.Auth;

namespace Resunet.Filters
{
    public class SiteAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly ICurrentUser _currentUser;
        private readonly string _redirectUrl;
        private readonly bool _isRequired;

        public SiteAuthorizeFilter(ICurrentUser currentUser, string redirectUrl, bool isRequired)
        {

            _currentUser = currentUser;
            _redirectUrl = redirectUrl;
            _isRequired = isRequired;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isLoggedIn = await _currentUser.IsLoggedInAsync();
            if (isLoggedIn != _isRequired)
            {
                context.Result = new RedirectResult(_redirectUrl);
            }
        }
    }
}
