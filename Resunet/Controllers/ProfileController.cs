using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.BL.Profile;
using Resunet.DAL.Models;
using Resunet.Filters;
using Resunet.Service;
using Resunet.ViewMappers;
using Resunet.ViewModels;

namespace Resunet.Controllers
{
    [SiteAuthorize("/login")]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IProfile _profile;

        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            _currentUser = currentUser;
            _profile = profile;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await _currentUser.GetCurrentProfiles();
            var firstProfile = profiles.FirstOrDefault() ?? new ProfileModel();
            return View("Index", ProfileMapper.MapProfileModelToProfileViewModel(firstProfile));
        }

        [HttpPost("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel viewModel)
        {
            int? userId = await _currentUser.GetCurrentUserIdAsync();
            if (userId == null)
                throw new Exception("Пользователь не найден");

            var profiles = await _profile.GetAsync(userId.Value);
            if (viewModel.ProfileId != null && !profiles.Any(m => m.ProfileId == viewModel.ProfileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                var profileModel = profiles.FirstOrDefault(m => m.ProfileId == viewModel.ProfileId) ?? new ProfileModel();
                profileModel.FirstName = viewModel.FirstName;
                profileModel.LastName = viewModel.LastName;
                profileModel.ProfileName = viewModel.ProfileName;
                profileModel.UserId = userId.Value;
                await _profile.AddOrUpdateAsync(profileModel);
                return Redirect("/");
            }
            return View("Index", new ProfileViewModel());
        }

        [HttpPost("/profile/upload-image")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ImageSave(int? profileId)
        {
            int? userId = await _currentUser.GetCurrentUserIdAsync();
            if (userId == null)
                throw new Exception("Пользователь не найден");

            var profiles = await _profile.GetAsync(userId.Value);
            if (profileId != null && !profiles.Any(m => m.ProfileId == profileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                var profileModel = profiles.FirstOrDefault(m => m.ProfileId == profileId) ?? new ProfileModel();
                profileModel.UserId = userId.Value;
                var files = Request.Form.Files;
                if (files.Any() && files[0] != null)
                {
                    var imageData = files[0];
                    var webFile = new WebFile();
                    string fileName = webFile.GetWebFileName(imageData.FileName);
                    await webFile.UploadAndResiseImage(imageData.OpenReadStream(), fileName, 800, 600);
                    profileModel.ProfileImage = fileName;
                    await _profile.AddOrUpdateAsync(profileModel);
                }
            }
            return Redirect("/profile");
        }
    }
}