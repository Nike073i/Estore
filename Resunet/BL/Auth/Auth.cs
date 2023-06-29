using Estore.BL.Exceptions;
using Estore.BL.General;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDal _authDal;
        private readonly IEncrypt _encrypt;
        private readonly IWebCookie _webCookie;
        private readonly IDbSession _dbSession;
        private readonly IUserTokenDal _userTokenDal;

        public Auth(
            IAuthDal authDal,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDal userTokenDal
            )
        {
            _authDal = authDal;
            _encrypt = encrypt;
            _dbSession = dbSession;
            _userTokenDal = userTokenDal;
            _webCookie = webCookie;
        }

        public async Task<int> Authenticate(string email, string passowrd, bool rememberMe)
        {
            var user = await _authDal.GetUserAsync(email);
            if (user.UserId != null && user.Password == _encrypt.HashPassword(passowrd, user.Salt))
            {
                int userId = user.UserId ?? 0;
                await Login(userId);
                if (rememberMe)
                {
                    Guid tokenId = await _userTokenDal.Create(userId);
                    _webCookie.AddSecure(
                        AuthConstants.RememberMeCookieName,
                        tokenId.ToString(),
                        AuthConstants.RememberMeDays
                    );
                }
                return userId;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUserAsync(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = _encrypt.HashPassword(user.Password, user.Salt);
            int id = await _authDal.CreateUserAsync(user);
            await Login(id);
            return id;
        }

        public async Task Login(int id)
        {
            await _dbSession.SetUserId(id);
            var roles = await _authDal.GetRoles(id);
            if (roles.Any(roles => roles.Abbreviation == AuthConstants.AdminRoleAbbr))
            {
                _dbSession.AddValue(AuthConstants.AdminRoleKey, AuthConstants.AdminRoleAbbr);
                await _dbSession.UpdateSessionData();
            }
        }

        public async Task ValidateEmail(string email)
        {
            var user = await _authDal.GetUserAsync(email);
            if (user.UserId != null)
                throw new DuplicateEmailException();
        }

        public async Task RegisterAsync(UserModel user)
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                await _dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUserAsync(user);
                scope.Complete();
            }
        }
    }
}
