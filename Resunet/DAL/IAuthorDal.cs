using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IAuthorDal
    {
        Task<AuthorModel?> GetAuthorId(string uniqueId);
    }
}
