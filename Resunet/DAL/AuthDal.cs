using Estore.DAL.Models;

namespace Estore.DAL
{
    public class AuthDal : IAuthDal
    {
        public async Task<int> CreateUserAsync(UserModel model)
        {
            string sql = @"
                INSERT INTO AppUser(Email, Password, Salt, Status) 
				VALUES(@Email, @Password, @Salt, @Status) returning UserId";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            var result = await DbHelper.QueryScalarAsync<UserModel>(@"
				SELECT UserId, Email, Password, Salt, Status 
				FROM AppUser 
				Where UserId = @id", new { id = id });
            return result ?? new UserModel();
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var result = await DbHelper.QueryScalarAsync<UserModel>(@"
				SELECT UserId, Email, Password, Salt, Status 
				FROM AppUser 
				Where Email = @email", new { email = email });
            return result ?? new UserModel();
        }
    }
}
