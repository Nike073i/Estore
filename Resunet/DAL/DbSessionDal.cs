using Estore.DAL.Models;

namespace Estore.DAL
{
    public class DbSessionDal : IDbSessionDal
    {
        private readonly IDbHelper _dbHelper;

        public DbSessionDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task Create(SessionModel model)
        {
            string sql = @"
                INSERT INTO DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                VALUES (@DbSessionId, @SessionData, @Created, @LastAccessed, @UserId)";
            await _dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"
                SELECT DbSessionId, SessionData, Created, LastAccessed, UserId
                FROM DbSession
                WHERE DbSessionId = @sessionId";
            return await _dbHelper.QueryScalarAsync<SessionModel>(sql, new { sessionId = sessionId });
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"
                SELECT DbSessionId
                FROM DbSession
                WHERE DbSessionId = @sessionId
                FOR UPDATE";
            await _dbHelper.ExecuteAsync(sql, new { sessionId = sessionId });
        }

        public async Task Update(Guid dbSessionId, string sessionData)
        {
            string sql = @"
                UPDATE DbSession
                SET SessionData = @SessionData
                WHERE DbSessionId = @DbSessionId";
            await _dbHelper.ExecuteAsync(sql, new { DbSessionId = dbSessionId, SessionData = sessionData });
        }

        public async Task Extend(Guid dbSessionId)
        {
            string sql = @"
                UPDATE DbSession
                SET LastAccessed = @LastAccessed
                WHERE DbSessionId = @DbSessionId";
            await _dbHelper.ExecuteAsync(sql, new { DbSessionId = dbSessionId, LastAccessed = DateTime.Now });
        }
    }
}
