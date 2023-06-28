using Resunet.BL.Auth;
using Resunet.BL.Exceptions;
using Resunet.DAL.Models;
using Resutest.Helpers;

namespace Resutest
{
    public class AuthTests : BaseTest
    {
        [Test]
        public async Task AuthRegistrationTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.ru";

                var userId = await _authBl.CreateUserAsync(new UserModel
                {
                    Email = email,
                    Password = "qwer1234"
                });

                Assert.ThrowsAsync<AuthorizationException>(async () => await _authBl.Authenticate(email, "bad_passord", false));
                Assert.ThrowsAsync<AuthorizationException>(async () => await _authBl.Authenticate("bad_email", "bad_passord", false));
                Assert.ThrowsAsync<AuthorizationException>(async () => await _authBl.Authenticate("bad_email", "qwer1234", false));
                await _authBl.Authenticate(email, "qwer1234", false);

                string? authCookie = _webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = _webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
        }

        [Test]
        public async Task RememberMeTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.ru";

                var userId = await _authBl.CreateUserAsync(new UserModel
                {
                    Email = email,
                    Password = "qwer1234"
                });

                await _authBl.Authenticate(email, "qwer1234", true);

                string? authCookie = _webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = _webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.NotNull(rememberMeCookie);
            }
        }
    }
}