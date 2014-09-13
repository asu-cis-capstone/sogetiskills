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


/* Application accessing DB */
CREATE TABLE Application(
	ApplicationId	int			PRIMARY KEY,
	ApplicationName	text(50),
	Description		text(50)
);

/* Removed UserName, signin based on email */

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


/* Permission Levels for Roles*/
CREATE TABLE Permission(
	PermissionId	int PRIMARY KEY IDENTITY(1,1),
	PermissionDescription	nvarchar(max) NOT NULL
);


/* Role- Account Executive or Consultant */
CREATE TABLE Role(
	RoleId	int	PRIMARY KEY IDENTITY(1,1),
	RoleName	char(50)	NOT NULL,
	PermissionId
	CONSTRAINT FK_Role_Permission FOREIGN KEY (PermissionId) REFERENCES Permission(PermissionId)
);


/* Users in Roles */
CREATE TABLE UserRole(
	UserId	int	NOT NULL,
	RoleId	int	NOT NULL,
	DateFrom	date	NOT NULL,
	DateTo	date,
	PRIMARY KEY (UserId, RoleId)
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES User(UserId),
	CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

/* User Beach Status */
CREATE TABLE UserStatus(
	UserId	int	NOT NULL,
	IsOnBeach	bit	NOT NULL,
	DateFrom	date	NOT NULL,
	DateTo	date,
	PRIMARY KEY (UserId, IsOnBeach, DateFrom)
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES User(UserId)
);

/* Resume */
CREATE TABLE Resume(
	ResumeId	int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ResumeBlob	varbinary
);

/* Skills */
CREATE TABLE Skill(
	SkillId	int	PRIMARY KEY IDENTITY(1,1),
	SkillDescription	varchar(250)
);

/* UserSkill */
CREATE TABLE UserSkill(
	UserId
	SkillId
	PRIMARY KEY (UserId, SkillId)
	CONSTRAINT FK_UserSkill_User FOREIGN KEY (UserId) REFERENCES User(UserId),
	CONSTRAINT FK_UserSkill_Role FOREIGN KEY (SkillId) REFERENCES Skill(SkillId)
);

