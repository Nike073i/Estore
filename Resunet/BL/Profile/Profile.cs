using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDal _profileDal;

        public Profile(IProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public async Task AddOrUpdateAsync(ProfileModel model)
        {
            if (model.ProfileId == null)
                model.ProfileId = await _profileDal.AddAsync(model);
            else
                await _profileDal.UpdateAsync(model);
        }

        public async Task<IEnumerable<ProfileModel>> GetAsync(int userId)
        {
            return await _profileDal.GetByUserIdAsync(userId);
        }
    }
}
