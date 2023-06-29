using Estore.DAL.Models;
using Estore.ViewModels;

namespace Estore.ViewMappers
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
