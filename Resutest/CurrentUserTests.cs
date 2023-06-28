using Resunet.BL.Auth;
using Resunet.DAL.Models;
using Resutest.Helpers;

namespace Resutest
{
    public class CurrentUserTests : BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                await CreateAndAuthUser();

                bool isLoggedIn = await _currentUser.IsLoggedInAsync();
                Assert.That(isLoggedIn, Is.True);

                _webCookie.Delete(AuthConstants.SessionCookieName);
                _dbSession.ResetSessionCache();

                isLoggedIn = await _currentUser.IsLoggedInAsync();
                Assert.That(isLoggedIn, Is.True);

                _webCookie.Delete(AuthConstants.SessionCookieName);
                _webCookie.Delete(AuthConstants.RememberMeCookieName);
                _dbSession.ResetSessionCache();

                isLoggedIn = await _currentUser.IsLoggedInAsync();
                Assert.That(isLoggedIn, Is.False);
            }
        }

        private async Task CreateAndAuthUser()
        {
            string email = Guid.NewGuid().ToString() + "@test.com";
            int userId = await _authBl.CreateUserAsync(new UserModel
            {
                Email = email,
                Password = "qwer1234"
            });
            await _authBl.Authenticate(email, "qwer1234", true);
        }
    }
}
