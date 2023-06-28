using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Resume;

namespace Resunet.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResume _resume;

        public ResumeController(IResume resume)
        {
            _resume = resume;
        }

        [HttpGet("resume/{profileId:int}")]
        public async Task<IActionResult> Index(int profileId)
        {
            var model = await _resume.GetAsync(profileId);
            if (model == null)
                return NotFound($"Не найден по Id = {profileId}");
            return View("Index", model);
        }
    }
}
