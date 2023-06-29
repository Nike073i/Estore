using Microsoft.AspNetCore.Mvc;

namespace Estore.Filters
{
    public class SiteAuthorizeAttribute : TypeFilterAttribute
    {
        public SiteAuthorizeAttribute(string redirectUrl = "/", bool isRequired = true) 
            : base(typeof(SiteAuthorizeFilter))
        {
            Arguments = new object[] { redirectUrl, isRequired };
        }
    }
}
