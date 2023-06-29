namespace Estore.BL.General
{
    public class WebCookie : IWebCookie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebCookie(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Add(string cookieName, string value, int days)
        {
            var options = new CookieOptions
            {
                Path = "/",
            };
            if (days > 0)
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        public void AddSecure(string cookieName, string value, int days)
        {
            var options = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true
            };
            if (days > 0)
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        public void Delete(string cookieName)
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(cookieName);
        }

        public string? Get(string cookieName)
        {
            var cookie = _httpContextAccessor?.HttpContext?.Request?.Cookies
                .FirstOrDefault(m => m.Key == cookieName);
            if (cookie != null && cookie.Value.Value != null)
                return cookie.Value.Value;
            return null;
        }
    }
}
