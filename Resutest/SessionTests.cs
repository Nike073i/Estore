using Resutest.Helpers;
using System.Text.Json;

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

        [Test]
        [NonParallelizable]
        public async Task AddValueTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var session = await _dbSession.GetSession();
                _dbSession.AddValue("Test", "TestValue");
                await _dbSession.UpdateSessionData();

                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("TestValue"));
                await _dbSession.SetUserId(10);
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("TestValue"));

                var dbSession = await _dbSessionDal.Get(session.DbSessionId);
                var sessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(dbSession?.SessionData ?? "");

                Assert.That(dbSession, Is.Not.Null);
                Assert.That(sessionData?.ContainsKey("Test"), Is.True);
                Assert.That(sessionData?["Test"].ToString(), Is.EqualTo("TestValue"));

                _dbSession.ResetSessionCache();
                await _dbSession.GetSession();
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("TestValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var session = await _dbSession.GetSession();
                _dbSession.AddValue("Test", "TestValue");
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("TestValue"));

                _dbSession.AddValue("Test", "UpdatedValue");
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("UpdatedValue"));

                await _dbSession.UpdateSessionData();

                _dbSession.ResetSessionCache();
                await _dbSession.GetSession();
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo("UpdatedValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var session = await _dbSession.GetSession();
                _dbSession.AddValue("Test", "TestValue");
                await _dbSession.UpdateSessionData();

                _dbSession.RemoveValue("Test");
                await _dbSession.UpdateSessionData();

                _dbSession.ResetSessionCache();
                await _dbSession.GetSession();
                Assert.That(_dbSession.TryGetOrDefault("Test", "").ToString(), Is.EqualTo(""));
            }
        }
    }
}
