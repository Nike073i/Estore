using Resunet.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Resunet.BL.Auth
{
	public interface IAuth
	{
		Task<int> CreateUserAsync(UserModel model);
		Task<int> Authenticate(string email, string passowrd, bool rememberMe);
		Task ValidateEmail(string email);
		Task RegisterAsync(UserModel user);
	}
}
