using Estore.DAL.Models;

namespace Estore.DAL
{
    public class AuthDal : IAuthDal
    {
        private readonly IDbHelper _dbHelper;

        public AuthDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> CreateUserAsync(UserModel model)
        {
            string sql = @"
                INSERT INTO AppUser(Email, Password, Salt, Status) 
				VALUES(@Email, @Password, @Salt, @Status) returning UserId";
            return await _dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<AppRoleModel>> GetRoles(int userId)
        {
            string sql = @"
                SELECT ar.AppRoleId, ar.RoleName, ar.Abbreviation
                FROM AppRole ar
                    JOIN AppUserAppRole auar ON ar.AppRoleId = auar.AppRoleId
                WHERE auar.AppUserId = @userId";
            return await _dbHelper.QueryAsync<AppRoleModel>(sql, new { userId });
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            var result = await _dbHelper.QueryScalarAsync<UserModel>(@"
				SELECT UserId, Email, Password, Salt, Status 
				FROM AppUser 
				Where UserId = @id", new { id = id });
            return result ?? new UserModel();
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var result = await _dbHelper.QueryScalarAsync<UserModel>(@"
				SELECT UserId, Email, Password, Salt, Status 
				FROM AppUser 
				Where Email = @email", new { email = email });
            return result ?? new UserModel();
        }
    }
}
