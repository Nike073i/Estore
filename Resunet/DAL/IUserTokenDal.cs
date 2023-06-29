namespace Estore.DAL
{
    public interface IUserTokenDal
    {
        Task<Guid> Create(int userId);
        Task<int?> Get(Guid tokenId);
    }
}
