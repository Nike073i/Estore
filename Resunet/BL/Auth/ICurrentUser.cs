namespace Estore.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedInAsync();
        Task<int?> GetCurrentUserIdAsync();
    }
}
