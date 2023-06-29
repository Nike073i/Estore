using Microsoft.AspNetCore.Mvc;

namespace Estore.Filters
{
    public class SiteAuthorizeAttribute : TypeFilterAttribute
    {
        public SiteAuthorizeAttribute(string redirectUrl = "/login", bool isRequiredAdmin = false)
            : base(typeof(SiteAuthorizeFilter))
        {
            Arguments = new object[] { redirectUrl, isRequiredAdmin };
        }
    }
}
