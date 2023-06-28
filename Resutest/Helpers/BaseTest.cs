using Microsoft.AspNetCore.Http;
using Resunet.BL.Auth;
using Resunet.BL.General;
using Resunet.BL.Profile;
using Resunet.DAL;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDal _authDal = new AuthDal();
        protected IEncrypt _encrypt = new Encrypt();
        protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        protected IDbSessionDal _dbSessionDal = new DbSessionDal();
        protected IUserTokenDal _userTokenDal = new UserTokenDal();
        protected IProfileDal _profileDal = new ProfileDal();
        protected IDbSession _dbSession;
        protected IWebCookie _webCookie;
        protected IProfile _profile;
        protected ICurrentUser _currentUser;
        protected IAuth _authBl;
        public BaseTest()
        {
            _webCookie = new TestCookie();
            _dbSession = new DbSession(_dbSessionDal, _webCookie);
            _profile = new Profile(_profileDal);
            _currentUser = new CurrentUser(_dbSession, _webCookie, _userTokenDal, _profileDal);
            _authBl = new Auth(_authDal, _encrypt, _webCookie, _dbSession, _userTokenDal);
        }
    }
}
