using Microsoft.AspNetCore.Mvc;

namespace Resunet.Controllers
{
    [ApiController]
    public class SkillsController : ControllerBase
    {
        static List<Skill> skills = new()
        {
            new Skill { Level = 3, Name = "C#" },
            new Skill { Level = 0, Name = "Blazor" },
        };

        [HttpGet("/skills/my")]
        public IActionResult My()
        {
            return Ok(skills);
        }

        [HttpPut("/skills/add")]
        public IActionResult My([FromBody] Skill skill)
        {
            skills.Add(skill);
            return Ok();
        }

        public class Skill
        {
            public string? Name { get; set; }
            public int Level { get; set; }
        }
    }
}
