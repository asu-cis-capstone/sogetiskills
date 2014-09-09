CREATE TABLE Passwords
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Hash] varbinary(max) NOT NULL,
	Salt varbinary(max) NOT NULL
);

CREATE TABLE Resumes
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	FileData varbinary(max) NULL,
	[FileName] nvarchar(max) NULL,
	MimeType nvarchar(max) NULL
);

CREATE TABLE Users
(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	UserType nvarchar(450) NOT NULL,
	Username nvarchar(450) NOT NULL,
	FirstName nvarchar(max) NOT NULL,
	LastName nvarchar(max) NOT NULL,
	EmailAddress nvarchar(max) NOT NULL,
	PasswordId int NOT NULL,
	ResumeId int NULL,
	CONSTRAINT FK_Users_Password FOREIGN KEY (PasswordId) REFERENCES Passwords(Id),
	CONSTRAINT FK_Users_Resume FOREIGN KEY (ResumeId) REFERENCES Resumes(Id)
);
CREATE UNIQUE INDEX IX_Users_Usernme ON Users(Username);

CREATE TABLE Tags
(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Keyword nvarchar(450) NOT NULL,
	SkillDescription nvarchar(2000) NULL,
	IsCanonical bit NOT NULL
);
CREATE INDEX IX_Tag_IsCanonical ON Tags(IsCanonical) INCLUDE(Keyword);

CREATE TABLE Consultant_Tag
(
	ConsultantId int NOT NULL,
	TagId int NOT NULL,
	PRIMARY KEY (ConsultantId, TagId),
	CONSTRAINT FK_Consultant_Tag_Users FOREIGN KEY (ConsultantId) REFERENCES Users(Id),
	CONSTRAINT FK_Consultant_Tag_Tags FOREIGN KEY (TagId) REFERENCES Tags(Id)
);
CREATE INDEX IX_Consultant_Tag_ConsultantId ON Consultant_Tag(ConsultantId) INCLUDE(TagId);