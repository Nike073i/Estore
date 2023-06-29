using Estore.BL.General;
using Estore.DAL;

namespace Estore.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession _dbSession;
        private readonly IWebCookie _webCookie;
        private readonly IUserTokenDal _userTokenDal;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDal userTokenDal
        )
        {
            _dbSession = dbSession;
            _webCookie = webCookie;
            _userTokenDal = userTokenDal;
        }

        public async Task<int?> GetCurrentUserIdAsync()
        {
            return await _dbSession.GetUserId();
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = _webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            Guid? token = Helpers.StringToGuidDef(tokenCookie);
            if (token == null)
                return null;
            return await _userTokenDal.Get(token.Value);
        }

        public async Task<bool> IsLoggedInAsync()
        {
            bool isLoggedIn = await _dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userId = await GetUserIdByToken();
                if (userId != null)
                {
                    await _dbSession.SetUserId(userId.Value);
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }

        public bool IsAdmin()
        {
            return _dbSession.TryGetOrDefault(AuthConstants.AdminRoleKey, "")
                    .ToString() == AuthConstants.AdminRoleAbbr;
        }
    }
}
