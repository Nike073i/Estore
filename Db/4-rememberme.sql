CREATE TABLE IF NOT EXISTS UserToken (
	UserTokenId UUID PRIMARY KEY,
	UserId int,
	Created TIMESTAMP
)