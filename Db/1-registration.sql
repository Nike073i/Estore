CREATE TABLE IF NOT EXISTS AppUser (
	UserId SERIAL PRIMARY KEY,
	Email VARCHAR(50),
	Password VARCHAR(100),
	Salt VARCHAR(50),
	Status INT
);

CREATE TABLE IF NOT EXISTS UserSecurity (
	UserSecurityId SERIAL PRIMARY KEY,
	UserId INT,
	VerificationCode VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS EmailQueue (
	EmailQueueId SERIAL PRIMARY KEY,
	EmailTo VARCHAR(200),
	EmailFrom VARCHAR(200),
	EmailSubject VARCHAR(200),
	EmailBody TEXT,
	Created TIME,
	ProcessingId VARCHAR(100),
	Retry INT
);

CREATE INDEX IF NOT EXISTS IX_AppUser_Email on AppUser (
	Email
);

ALTER TABLE AppUser DROP IF EXISTS FirstName;
ALTER TABLE AppUser DROP IF EXISTS LastName;
ALTER TABLE AppUser DROP IF EXISTS ProfileImage;