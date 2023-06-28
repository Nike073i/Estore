using Resunet.Controllers;
using Resunet.DAL.Models;

namespace Resunet.ViewMappers
{
    public class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel
            {
                SkillName = model.Name,
                Level = model.Level
            };
        }
    }
}