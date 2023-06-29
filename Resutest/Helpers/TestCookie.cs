using Estore.BL.General;

namespace Resutest.Helpers
{
    public class TestCookie : IWebCookie
    {
        private readonly Dictionary<string, string> _cookies;
        public TestCookie()
        {
            _cookies = new Dictionary<string, string>();
        }
        public void Add(string cookieName, string value, int days = 0)
        {
            _cookies.Add(cookieName, value);
        }

        public void AddSecure(string cookieName, string value, int days = 0)
        {
            _cookies.Add(cookieName, value);
        }

        public void Delete(string cookieName)
        {
            _cookies.Remove(cookieName);
        }

        public string? Get(string cookieName)
        {
            if (_cookies.ContainsKey(cookieName))
                return _cookies[cookieName];
            return null;
        }

        public void Clear()
        {
            _cookies.Clear();
        }
    }
}
