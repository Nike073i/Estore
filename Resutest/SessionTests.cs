using Resutest.Helpers;

namespace Resutest
{
    public class SessionTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            (_webCookie as TestCookie)?.Clear();
            _dbSession.ResetSessionCache();
        }

        [Test]
        [NonParallelizable]
        public async Task CreateSessionTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var session = await _dbSession.GetSession();
                var dbSession = await _dbSessionDal.Get(session.DbSessionId);
                Assert.That(dbSession, Is.Not.Null);
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));
                var session2 = await _dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var session = await _dbSession.GetSession();
                await _dbSession.SetUserId(10);

                var dbSession = await _dbSessionDal.Get(session.DbSessionId);
                Assert.That(dbSession, Is.Not.Null);
                Assert.That(dbSession.UserId, Is.EqualTo(10));
                var session2 = await _dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
                int? userId = await _currentUser.GetCurrentUserIdAsync();
                Assert.That(userId, Is.EqualTo(10));
            }
        }
    }
}
