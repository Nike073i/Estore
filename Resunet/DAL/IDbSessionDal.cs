using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IDbSessionDal
    {
        Task<SessionModel?> Get(Guid sessionId);
        Task Update(SessionModel model);
        Task Create(SessionModel model);
        Task Lock(Guid sessionId);
    }
}