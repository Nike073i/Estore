using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedInAsync();
        Task<int?> GetCurrentUserIdAsync();
        Task<IEnumerable<ProfileModel>> GetCurrentProfiles();
    }
}
