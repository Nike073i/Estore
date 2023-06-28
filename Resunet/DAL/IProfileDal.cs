using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IProfileDal
    {
        Task<IEnumerable<ProfileModel>> GetByUserIdAsync(int userId);
        Task<int> AddAsync(ProfileModel model);
        Task UpdateAsync(ProfileModel model);
        Task<IEnumerable<ProfileModel>> Search(int top);
        Task<ProfileModel?> GetByProfileId(int profileId);
    }
}
