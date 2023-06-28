using Resunet.BL.General;
using Resunet.DAL;
using Resunet.DAL.Models;
using System.Text.Json;

namespace Resunet.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDal _sessionDal;
        private readonly IWebCookie _webCookie;

        private SessionModel? _sessionModel;
        private Dictionary<string, object> _sessionData;

        public DbSession(IDbSessionDal sessionDal, IWebCookie webCookie)
        {
            _sessionDal = sessionDal;
            _webCookie = webCookie;
            _sessionData = new Dictionary<string, object>();
        }

        public async Task<SessionModel> GetSession()
        {
            if (_sessionModel != null)
                return _sessionModel;

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
            _sessionModel = data;
            if (data.SessionData != null)
                _sessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionData) ?? new();

            await _sessionDal.Extend(sessionId);
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
            data.SessionData = JsonSerializer.Serialize(_sessionData);
            await _sessionDal.Create(data);
        }

        public void ResetSessionCache()
        {
            _sessionModel = null;
        }

        public void AddValue(string key, object value)
        {
            if (_sessionData.ContainsKey(key))
                _sessionData[key] = value;
            else
                _sessionData.Add(key, value);
        }

        public void RemoveValue(string key)
        {
            _sessionData.Remove(key);
        }

        public async Task UpdateSessionData()
        {
            if (_sessionModel != null)
            {
                string dataJson = JsonSerializer.Serialize(_sessionData);
                await _sessionDal.Update(_sessionModel.DbSessionId, dataJson);
            }
            else
                throw new Exception("Сессия не загружена");
        }

        public object TryGetOrDefault(string key, object defaultValue)
        {
            if (_sessionData.ContainsKey(key))
                return _sessionData[key];
            return defaultValue;
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
