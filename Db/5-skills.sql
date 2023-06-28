CREATE TABLE IF NOT EXISTS Skill (
	SkillId SERIAL PRIMARY KEY,
	SkillName VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS ProfileSkill (
	ProfileSkillId SERIAL PRIMARY KEY,
	ProfileId INT,
	SkillId INT,
	Level INT
);

CREATE INDEX IF NOT EXISTS IX_ProfileSkill_ProfileId on ProfileSkill(ProfileId);