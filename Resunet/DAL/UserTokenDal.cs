namespace Estore.DAL
{
    public class UserTokenDal : IUserTokenDal
    {
        public async Task<Guid> Create(int userId)
        {
            Guid tokenId = Guid.NewGuid();
            string sql = @"
                INSERT INTO UserToken (UserTokenId, UserId, Created)
                VALUES (@tokenId, @userId, NOW())";
            await DbHelper.ExecuteAsync(sql, new { tokenId = tokenId, userId = userId });
            return tokenId;
        }

        public async Task<int?> Get(Guid tokenId)
        {
            string sql = @"
                SELECT UserId
                FROM UserToken
                WHERE UserTokenId = @tokenId";
            return await DbHelper.QueryScalarAsync<int>(sql, new { tokenId = tokenId });
        }
    }
}
