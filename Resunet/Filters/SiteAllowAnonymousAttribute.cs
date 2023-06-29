using Microsoft.AspNetCore.Mvc;

namespace Estore.Filters
{
    public class SiteAllowAnonymousAttribute : TypeFilterAttribute
    {
        public SiteAllowAnonymousAttribute(string redirectUrl = "/", bool isOnlyAnonymous = false)
            : base(typeof(SiteAllowAnonymousFilter))
        {
            Arguments = new object[] { redirectUrl, isOnlyAnonymous };
        }
    }
}
