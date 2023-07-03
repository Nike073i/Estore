namespace Estore.DAL
{
    public class UserTokenDal : IUserTokenDal
    {
        private readonly IDbHelper _dbHelper;

        public UserTokenDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<Guid> Create(int userId)
        {
            Guid tokenId = Guid.NewGuid();
            string sql = @"
                INSERT INTO UserToken (UserTokenId, UserId, Created)
                VALUES (@tokenId, @userId, NOW())";
            await _dbHelper.ExecuteAsync(sql, new { tokenId, userId });
            return tokenId;
        }

        public async Task<int?> Get(Guid tokenId)
        {
            string sql = @"
                SELECT UserId
                FROM UserToken
                WHERE UserTokenId = @tokenId";
            return await _dbHelper.QueryScalarAsync<int>(sql, new { tokenId });
        }
    }
}
