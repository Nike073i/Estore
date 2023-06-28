using Resunet.DAL.Models;
using Resunet.ViewModels;

namespace Resunet.ViewMappers
{
    public class AuthMapper
	{
		public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel viewModel)
		{
			return new UserModel
			{
				Email = viewModel.Email!,
				Password = viewModel.Password!
			};
		}
	}
}
