using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<ProfileModel>> GetAsync(int userId);
        Task AddOrUpdateAsync(ProfileModel model);
    }
}
