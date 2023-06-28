using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Resume
{
    public interface IResume
    {
        Task<ResumeModel?> GetAsync(int profileId);
        Task<IEnumerable<ProfileModel>> Search(int top);
    }

    public class Resume : IResume
    {
        private readonly IProfileDal _profileDal;
        public Resume(IProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public async Task<ResumeModel?> GetAsync(int profileId)
        {
            var profileModel = await _profileDal.GetByProfileId(profileId);
            return profileModel != null ? new ResumeModel { Profile = profileModel } : null;
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await _profileDal.Search(top);
        }
    }
}
