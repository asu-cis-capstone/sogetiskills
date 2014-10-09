/*
* in-progress DDL
*/


/* 
Notes: 
to do-

!!! need to find out NAMING CONVENTIONS at SOGETI
SKILL LEVELS should have separate table as well
indexing
start fleshing out/ figure out how skills will be represented in DDL
*/
/* */
/* */


/* 
Create database snapshot
CREATE DATABASE DataSnapShot
	ON
	(
		NAME = logical_file_name_here,
		FILENAME = 'os_file_name_here',
	)
AS SNAPSHOT OF source_database_name
*/


/* Application accessing DB 
CREATE TABLE Application(
	ApplicationId	int			PRIMARY KEY,
	ApplicationName	text(50),
	Description		text(50)
);
*/


/* Person */
CREATE TABLE User (
	UserId		int	NOT NULL PRIMARY KEY,
	FirstName	char(50)	NOT NULL,
	LastName	char(50)	NOT NULL,
	Email		text(50)	NOT NULL,
	Phone		char(10)
);

/* Authentication */
CREATE TABLE Membership(
	UserId	int		PRIMARY KEY,
	PasswordHash	varchar(128)	NOT NULL,
	PasswordSalt	varchar(128)	NOT NULL,
	LastLogin	date	
);

/* Current Session Info */
CREATE TABLE Session(
	UserId
	StartTime
	EndTime
	PRIMARY KEY (UserId, StartTime)
);

/* Role- Account Executive or Consultant */
CREATE TABLE Type(
	TypeId	int	PRIMARY KEY IDENTITY(1,1),
	TypeName	char(50)	NOT NULL
);


/* Users in Roles */
CREATE TABLE UserType(
	UserId	int	NOT NULL,
	TypeId	int	NOT NULL,
	DateFrom	date	NOT NULL,
	DateTo	date,
	PRIMARY KEY (UserId, TypeId)
	CONSTRAINT FK_UserType_User FOREIGN KEY (UserId) REFERENCES User(UserId),
	CONSTRAINT FK_UserType_Type FOREIGN KEY (TypeId) REFERENCES Type(TypeId)
);

/* User Beach Status */
CREATE TABLE UserStatus(
	UserId	int	NOT NULL,
	IsOnBeach	bit	NOT NULL,
	DateFrom	date	NOT NULL,
	DateTo	date,
	PRIMARY KEY (UserId, IsOnBeach, DateFrom)
	CONSTRAINT FK_UserStatus_User FOREIGN KEY (UserId) REFERENCES User(UserId)
);

/* Resume */
CREATE TABLE Resume(
	ResumeId	int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ResumeBlob	varbinary(max)	NULL,
	[FileName]	nvarchar(max)	NULL,
	UserId	int	NOT NULL,
	CONSTRAINT FK_Resume_User FOREIGN KEY (UserId) REFERENCES User(UserId)
);

/* Skills */
CREATE TABLE Skill(
	SkillId	int	PRIMARY KEY IDENTITY(1,1),
	SkillDescription	varchar(250)
);

/* UserSkill */
CREATE TABLE UserSkill(
	UserId		int		NOT NULL,
	SkillId		int		NOT NULL,
	PRIMARY KEY (UserId, SkillId),
	CONSTRAINT FK_UserSkill_User FOREIGN KEY (UserId) REFERENCES User(UserId),
	CONSTRAINT FK_UserSkill_Skill FOREIGN KEY (SkillId) REFERENCES Skill(SkillId)
);

/*
Proficiency Levels for skills
*/