using Estore.DAL.Models;

namespace Estore.DAL
{
    public class AuthorDal : IAuthorDal
    {
        public async Task<AuthorModel?> GetAuthorId(string uniqueId)
        {
            string sql = @"
                SELECT AuthorId, FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId
                FROM Author
                WHERE UniqueId = @uniqueId";
            return await DbHelper.QueryScalarAsync<AuthorModel>(sql, new { uniqueId });
        }
    }
}
