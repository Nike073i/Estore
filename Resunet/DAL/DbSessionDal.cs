using Estore.DAL.Models;

namespace Estore.DAL
{
    public class DbSessionDal : IDbSessionDal
    {
        public async Task Create(SessionModel model)
        {
            string sql = @"
                INSERT INTO DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                VALUES (@DbSessionId, @SessionData, @Created, @LastAccessed, @UserId)";
            await DbHelper.ExecuteAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"
                SELECT DbSessionId, SessionData, Created, LastAccessed, UserId
                FROM DbSession
                WHERE DbSessionId = @sessionId";
            return await DbHelper.QueryScalarAsync<SessionModel>(sql, new { sessionId = sessionId });
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"
                SELECT DbSessionId
                FROM DbSession
                WHERE DbSessionId = @sessionId
                FOR UPDATE";
            await DbHelper.ExecuteAsync(sql, new { sessionId = sessionId });
        }

        public async Task Update(Guid dbSessionId, string sessionData)
        {
            string sql = @"
                UPDATE DbSession
                SET SessionData = @SessionData
                WHERE DbSessionId = @DbSessionId";
            await DbHelper.ExecuteAsync(sql, new { DbSessionId = dbSessionId, SessionData = sessionData });
        }

        public async Task Extend(Guid dbSessionId)
        {
            string sql = @"
                UPDATE DbSession
                SET LastAccessed = @LastAccessed
                WHERE DbSessionId = @DbSessionId";
            await DbHelper.ExecuteAsync(sql, new { DbSessionId = dbSessionId, LastAccessed = DateTime.Now });
        }
    }
}
