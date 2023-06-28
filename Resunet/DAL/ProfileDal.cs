using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class ProfileDal : IProfileDal
    {
        public async Task<int> AddAsync(ProfileModel model)
        {
            string sql = @"
                INSERT INTO Profile (UserId, ProfileName, FirstName, LastName, ProfileImage)
                VALUES (@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage) returning ProfileId";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<ProfileModel>> GetByUserIdAsync(int userId)
        {
            string sql = @"
                SELECT ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                FROM Profile
                WHERE UserId = @userId";
            return await DbHelper.QueryAsync<ProfileModel>(sql, new { userId = userId });
        }

        public async Task<ProfileModel?> GetByProfileId(int profileId)
        {
            string sql = @"
                SELECT ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                FROM Profile
                WHERE ProfileId = @profileId";
            return await DbHelper.QueryScalarAsync<ProfileModel>(sql, new { profileId = profileId });
        }

        public Task<IEnumerable<ProfileModel>> Search(int top)
        {
            string sql = @"
                SELECT ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                FROM Profile
                ORDER BY ProfileId DESC
                LIMIT @top";
            return DbHelper.QueryAsync<ProfileModel>(sql, new { top = top });
        }

        public async Task UpdateAsync(ProfileModel model)
        {
            string sql = @"
                UPDATE Profile 
                SET ProfileName = @ProfileName, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    ProfileImage = @ProfileImage
                WHERE ProfileId = @ProfileId";
            await DbHelper.ExecuteAsync(sql, model);
        }
    }
}
