using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IAuthDal
	{
		Task<UserModel> GetUserAsync(int id);
		Task<UserModel> GetUserAsync(string email);
		Task<int> CreateUserAsync(UserModel model);
	}
}
