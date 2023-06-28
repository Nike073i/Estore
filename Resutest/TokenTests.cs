using Resutest.Helpers;

namespace Resutest
{
    public class TokenTests : BaseTest
    {
        [Test]
        public async Task BaseTokenTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var tokenId = await _userTokenDal.Create(10);
                var userId = await _userTokenDal.Get(tokenId);
                Assert.That(userId, Is.EqualTo(10));
            }
        }
    }
}
