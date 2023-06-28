using Resunet.DAL.Models;
using Resunet.ViewModels;

namespace Resunet.ViewMappers
{
    public class ProfileMapper
	{
		public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
		{
			return new ProfileViewModel
			{
				ProfileId = model.ProfileId,
				FirstName = model.FirstName,
				LastName = model.LastName,
				ProfileName = model.ProfileName,
				ProfileImage= model.ProfileImage
			};
		}

		public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
		{
			return new ProfileModel
			{
                ProfileId = model.ProfileId,
                FirstName = model.FirstName,
				LastName = model.LastName,
				ProfileName = model.ProfileName,
            };
		}
	}
}
