using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IDbSessionDal
    {
        Task<SessionModel?> Get(Guid sessionId);
        Task Update(Guid dbSessionId, string sessionData);
        Task Create(SessionModel model);
        Task Lock(Guid sessionId);
        Task Extend(Guid dbSessionId);
    }
}