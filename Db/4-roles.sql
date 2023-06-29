CREATE TABLE IF NOT EXISTS AppRole (
	AppRoleId SERIAL PRIMARY KEY,
	Abbreviation VARCHAR (10),
	RoleName VARCHAR (50)
);

CREATE TABLE IF NOT EXISTS AppUserAppRole (
	AppUserId INT,
	AppRoleId INT,
	PRIMARY KEY (AppUserId, AppRoleId)
);

INSERT INTO AppRole(Abbreviation, RoleName)
SELECT 'Admin', 'Administrator'
WHERE NOT EXISTS (
	SELECT 1
	FROM AppRole
	WHERE Abbreviation = 'Admin'
);

INSERT INTO AppUser (Email, Password, Salt, Status)
SELECT 'admin@estore.ru', 
	   'G4rKaDgp72RZkE1VWnOfwC/68UPRrX43XcbXeEvTrSoQdkZyAtUh229xLOKX7K3onp9zHTuHU63DHc1EXIxV6w==', 
	   'd5c1092c-c9a6-41f6-a70f-7e2965d810a3',
	   1
WHERE NOT EXISTS 
	(SELECT 1 FROM AppUser WHERE Email = 'admin@estore.ru');

INSERT INTO AppUserAppRole
SELECT (SELECT UserId
	    FROM AppUser
	    WHERE Email = 'admin@estore.ru'), 
	   (SELECT AppRoleId
		FROM AppRole
	    WHERE Abbreviation='Admin')
WHERE NOT EXISTS (
	SELECT 1
	FROM AppUserAppRole
    WHERE AppUserId = 
		(SELECT UserId
		 FROM AppUser
		 WHERE Email = 'admin@estore.ru')
	);