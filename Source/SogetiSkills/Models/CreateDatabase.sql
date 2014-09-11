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
	EmailAddress nvarchar(450) NOT NULL,
	FirstName nvarchar(max) NOT NULL,
	LastName nvarchar(max) NOT NULL,
	PhoneNumber nvarchar(max) NULL,
	Password_Hash nvarchar(max) NOT NULL,
	Password_Salt nvarchar(max) NOT NULL,
	ResumeId int NULL,
	CONSTRAINT FK_Users_Resume FOREIGN KEY (ResumeId) REFERENCES Resumes(Id)
);
CREATE UNIQUE INDEX IX_Users_EmailAddress ON Users(EmailAddress);

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