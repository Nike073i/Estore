using Resunet.BL.General;
using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDal _sessionDal;
        private readonly IWebCookie _webCookie;

        public DbSession(IDbSessionDal sessionDal, IWebCookie webCookie)
        {
            _sessionDal = sessionDal;
            _webCookie = webCookie;
        }

        private SessionModel? sessionModel = null;
        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var sessionString = _webCookie.Get(AuthConstants.SessionCookieName);
            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();

            var data = await _sessionDal.Get(sessionId);
            if (data == null)
            {
                data = await CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }
            sessionModel = data;
            return data;
        }

        public async Task<int?> GetUserId()
        {
            var data = await GetSession();
            return data.UserId;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await GetSession();
            return data.UserId != null;
        }

        public async Task Lock()
        {
            var data = await GetSession();
            await _sessionDal.Lock(data.DbSessionId);
        }

        public async Task SetUserId(int userId)
        {
            var data = await GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            await _sessionDal.Create(data);
        }

        public void ResetSessionCache()
        {
            sessionModel = null;
        }

        private void CreateSessionCookie(Guid sessionId)
        {
            _webCookie.Delete(AuthConstants.SessionCookieName);
            _webCookie.AddSecure(AuthConstants.SessionCookieName, sessionId.ToString());
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.Now,
                LastAccessed = DateTime.Now,
            };
            await _sessionDal.Create(data);
            return data;
        }
    }
}
