using Resunet.BL.General;
using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession _dbSession;
        private readonly IWebCookie _webCookie;
        private readonly IUserTokenDal _userTokenDal;
        private readonly IProfileDal _profileDal;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDal userTokenDal,
            IProfileDal profileDal
        )
        {
            _dbSession = dbSession;
            _webCookie = webCookie;
            _userTokenDal = userTokenDal;
            _profileDal = profileDal;
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

        public async Task<IEnumerable<ProfileModel>> GetCurrentProfiles()
        {
            int? userId = await GetCurrentUserIdAsync();
            if (userId == null)
                throw new Exception("Пользователь не найден");
            return await _profileDal.GetByUserIdAsync(userId.Value);
        }
    }
}
