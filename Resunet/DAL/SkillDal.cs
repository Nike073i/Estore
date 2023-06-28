using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class SkillDal : ISkillDal
    {
        public async Task<int> Create(string skillName)
        {
            string sql = @"
                INSERT INTO Skill (SkillName)
                SELECT @skillName
                WHERE NOT EXISTS (
                    SELECT 1 
                    FROM Skill
                    WHERE SkillName = @skillName
                ) returning SkillId";
            return await DbHelper.QueryScalarAsync<int>(sql, new { skillName });
        }

        public async Task<IEnumerable<SkillModel>> Search(int count, string filter)
        {
            string sql = @"
                SELECT SkillId, SkillName
                FROM Skill
                WHERE SkillName LIKE @filter
                LIMIT @count";
            return await DbHelper.QueryAsync<SkillModel>(sql, new { filter = string.Concat("%", filter, "%"), count });
        }
        public async Task<SkillModel?> Get(string skillName)
        {
            string sql = @"
                SELECT SkillId, SkillName
                FROM Skill
                WHERE SkillName = @skillName";
            return await DbHelper.QueryScalarAsync<SkillModel>(sql, new { skillName });
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            string sql = @"
                SELECT ps.ProfileSkillId, ps.ProfileId, ps.SkillId, s.SkillName, ps.Level
                FROM ProfileSkill ps 
                    JOIN Skill s ON ps.SkillId = s.SkillId
                WHERE ps.ProfileId = @profileId";
            return await DbHelper.QueryAsync<ProfileSkillModel>(sql, new { profileId });
        }

        public async Task<int> AddProfileSkill(ProfileSkillModel model)
        {
            string sql = @"
                INSERT INTO ProfileSkill (ProfileId, SkillId, Level)
                VALUES (@ProfileId, @SkillId, @Level) returning ProfileSkillId";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}
