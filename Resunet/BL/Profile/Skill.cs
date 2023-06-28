using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public class Skill : ISkill
    {
        private readonly ISkillDal _skillDal;

        public Skill(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        public async Task<IEnumerable<SkillModel>> Search(int top, string filter)
        {
            return await _skillDal.Search(top, filter);
        }
    }
}
