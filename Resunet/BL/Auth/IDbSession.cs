using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface IDbSession
    {
        Task<SessionModel> GetSession();
        Task SetUserId(int userId);
        Task<int?> GetUserId();
        Task<bool> IsLoggedIn();
        Task Lock();
        void ResetSessionCache();
        void AddValue(string key, object value);
        void RemoveValue(string key);
        Task UpdateSessionData();
        object TryGetOrDefault(string key, object defaultValue);
    }
}
