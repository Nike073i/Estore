CREATE TABLE IF NOT EXISTS DbSession (
	DbSessionId UUID PRIMARY KEY,
	UserId INT,
	SessionData TEXT,
	Created TIMESTAMP,
	LastAccessed TIMESTAMP
)