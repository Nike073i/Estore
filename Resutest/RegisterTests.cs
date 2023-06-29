using Estore.BL.Exceptions;
using Estore.DAL.Models;
using Resutest.Helpers;
using System.Security.Authentication;

namespace Resutest
{
    public class RegisterTests : BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.ru";

                await _authBl.ValidateEmail(email);

                var userId = await _authBl.CreateUserAsync(new UserModel
                {
                    Email = email,
                    Password = "qwer1234"
                });

                Assert.Greater(userId, 0);

                var userDalResult = await _authDal.GetUserAsync(userId);
                Assert.That(userDalResult.Email, Is.EqualTo(email));
                Assert.NotNull(userDalResult.Salt);

                var userDalByEmailResult = await _authDal.GetUserAsync(email);
                Assert.That(userDalResult.Email, Is.EqualTo(email));

                Assert.ThrowsAsync<DuplicateEmailException>(async () => await _authBl.ValidateEmail(email));

                string encPassword = _encrypt.HashPassword("qwer1234", userDalByEmailResult.Salt);
                Assert.That(userDalByEmailResult.Password, Is.EqualTo(encPassword));
            }
        }
    }
}