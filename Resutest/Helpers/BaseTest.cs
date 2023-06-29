using Estore.BL.Auth;
using Estore.BL.General;
using Estore.DAL;
using Microsoft.AspNetCore.Http;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDal _authDal = new AuthDal();
        protected IEncrypt _encrypt = new Encrypt();
        protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        protected IDbSessionDal _dbSessionDal = new DbSessionDal();
        protected IUserTokenDal _userTokenDal = new UserTokenDal();
        protected IDbSession _dbSession;
        protected IWebCookie _webCookie;
        protected ICurrentUser _currentUser;
        protected IAuth _authBl;
        public BaseTest()
        {
            _webCookie = new TestCookie();
            _dbSession = new DbSession(_dbSessionDal, _webCookie);
            _currentUser = new CurrentUser(_dbSession, _webCookie, _userTokenDal);
            _authBl = new Auth(_authDal, _encrypt, _webCookie, _dbSession, _userTokenDal);
        }
    }
}
