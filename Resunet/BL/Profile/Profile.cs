using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDal _profileDal;
        private readonly ISkillDal _skillDal;

        public Profile(IProfileDal profileDal, ISkillDal skillDal)
        {
            _profileDal = profileDal;
            _skillDal = skillDal;
        }

        public async Task AddOrUpdateAsync(ProfileModel model)
        {
            if (model.ProfileId == null)
                model.ProfileId = await _profileDal.AddAsync(model);
            else
                await _profileDal.UpdateAsync(model);
        }

        public async Task AddProfileSkill(ProfileSkillModel model)
        {
            var skill = await _skillDal.Get(model.SkillName);
            model.SkillId = skill != null ? skill.SkillId!.Value : await _skillDal.Create(model.SkillName);
            await _skillDal.AddProfileSkill(model);
        }

        public async Task<IEnumerable<ProfileModel>> GetAsync(int userId)
        {
            return await _profileDal.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            return await _skillDal.GetProfileSkills(profileId);
        }
    }
}
