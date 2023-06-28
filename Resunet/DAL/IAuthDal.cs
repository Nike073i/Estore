using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IAuthDal
	{
		Task<UserModel> GetUserAsync(int id);
		Task<UserModel> GetUserAsync(string email);
		Task<int> CreateUserAsync(UserModel model);
	}
}
