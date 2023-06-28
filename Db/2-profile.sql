CREATE TABLE IF NOT EXISTS Profile (
	ProfileId SERIAL PRIMARY KEY,
	UserId INT,
	ProfileName VARCHAR(50),
	FirstName VARCHAR(50),
	LastName VARCHAR(50),
	ProfileImage VARCHAR(100)
)