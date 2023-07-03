using Estore.DAL.Models;

namespace Estore.DAL
{
    public class AuthorDal : IAuthorDal
    {
        private readonly IDbHelper _dbHelper;

        public AuthorDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<AuthorModel?> GetAuthorId(string uniqueId)
        {
            string sql = @"
                SELECT AuthorId, FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId
                FROM Author
                WHERE UniqueId = @uniqueId";
            return await _dbHelper.QueryScalarAsync<AuthorModel>(sql, new { uniqueId });
        }
    }
}
