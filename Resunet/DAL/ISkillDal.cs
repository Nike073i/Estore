using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface ISkillDal
    {
        Task<int> AddProfileSkill(ProfileSkillModel model);
        Task<int> Create(string skillName);
        Task<SkillModel?> Get(string skillName);
        Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);
        Task<IEnumerable<SkillModel>> Search(int count, string filter);
    }
}
