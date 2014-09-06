CREATE TABLE Users
(
	Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Username nvarchar(450) NOT NULL,
	FirstName nvarchar(450) NOT NULL,
	LastName nvarchar(450) NOT NULL,
	EmailAddress nvarchar(450) NOT NULL,
	UserType int NOT NULL,
	PasswordHash nvarchar(4000) NULL,
	PasswordSalt nvarchar(4000) NULL
);
CREATE UNIQUE INDEX IX_Users_Username ON Users(Username);