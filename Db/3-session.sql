CREATE TABLE IF NOT EXISTS DbSession (
	DbSessionId UUID PRIMARY KEY,
	SessionData TEXT,
	Created TIMESTAMP,
	LastAccessed TIMESTAMP,
	UserId INT
);