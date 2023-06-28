using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.BL.Profile;
using Resunet.Filters;
using Resunet.ViewMappers;

namespace Resunet.Controllers
{
    [ApiController]
    [SiteAuthorize("/login", true)]
    public class SkillsController : ControllerBase
    {
        private readonly ISkill _skill;
        private readonly ICurrentUser _currentUser;
        private readonly IProfile _profile;

        public SkillsController(ISkill skill, ICurrentUser currentUser, IProfile profile)
        {
            _skill = skill;
            _currentUser = currentUser;
            _profile = profile;
        }

        [HttpGet("/skills/my")]
        public async Task<IActionResult> My()
        {
            var profiles = await _currentUser.GetCurrentProfiles();
            var mySkills = await _profile.GetProfileSkills(profiles.FirstOrDefault()?.ProfileId ?? 0);
            return Ok(mySkills.Select(mySkills => new SkillViewModel
            {
                Name = mySkills.SkillName,
                Level = mySkills.Level
            }));
        }

        [HttpPut("/skills/add")]
        public async Task<IActionResult> My([FromBody] SkillViewModel skill)
        {
            var profiles = await _currentUser.GetCurrentProfiles();
            var profileSkillModel = SkillMapper.MapSkillViewModelToProfileSkillModel(skill);
            profileSkillModel.ProfileId = profiles.FirstOrDefault()?.ProfileId ?? 0;
            await _profile.AddProfileSkill(profileSkillModel);
            return Ok();
        }

        [HttpGet("/skills/search")]
        public async Task<IActionResult> Search([FromQuery] string filter)
        {
            var skills = await _skill.Search(5, filter);
            return Ok(skills.Select(model => model.SkillName));
        }
    }
}
