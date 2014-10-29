-- This script removes the Skill.Description column and replaces it with a Proficiency column that
-- represents the consultant's proficiency with the skill.  The proficiency is constrained by the 
-- new ProficiencyLevels lookup table.

CREATE TABLE ProficiencyLevels
(
	[Level] int NOT NULL PRIMARY KEY,
	Name nvarchar(MAX) NOT NULL
)
GO

-- Taken from http://hr.od.nih.gov/workingatnih/competencies/proficiencyscale.htm
INSERT INTO ProficiencyLevels ([Level], Name) VALUES (1, 'Fundamental Awareness');
INSERT INTO ProficiencyLevels ([Level], Name) VALUES (2, 'Novice');
INSERT INTO ProficiencyLevels ([Level], Name) VALUES (3, 'Intermediate');
INSERT INTO ProficiencyLevels ([Level], Name) VALUES (4, 'Advanced');
INSERT INTO ProficiencyLevels ([Level], Name) VALUES (5, 'Expert');
GO

-- Rename the Consultant_Skill table to ConsultantSkill since it will hold proficiency instead
-- of just two foreign keys.
EXEC sp_rename 'FK_Consultant_Skill_Users', 'FK_ConsultantSkill_Users', 'OBJECT';
EXEC sp_rename 'FK_Consultant_Skill_Skills', 'FK_ConsultantSkill_Skills', 'OBJECT';
EXEC sp_rename 'Consultant_Skill', 'ConsultantSkill';
GO

-- Add the new proficiency column to ConsultantSkill. 
ALTER TABLE ConsultantSkill 
ADD ProficiencyLevel int NOT NULL DEFAULT(3)
GO

ALTER TABLE ConsultantSkill 
ADD CONSTRAINT FK_ConsultantSkill_ProficiencyLevel FOREIGN KEY (ProficiencyLevel) REFERENCES ProficiencyLevels([Level])
GO

-- Update the stored procedures for ConsultantSkill to use the new proficiency column.
CREATE PROCEDURE ProficiencyLevels_SelectAll

AS

SELECT
	[Level],
	Name
FROM ProficiencyLevels
ORDER BY [Level]
GO

-- Skill_SelectByConsultantId
ALTER PROCEDURE Skill_SelectByConsultantId
(
	@consultantId int
)
AS

SELECT
	CS.ConsultantId,
	CS.SkillId,
	S.Name AS SkillName,
	S.IsCanonical,
	P.[Level] AS ProficiencyLevel,
	P.Name AS ProficiencyLevelName
FROM
	ConsultantSkill CS
INNER JOIN
	Skills S ON CS.SkillId = S.Id
INNER JOIN
	ProficiencyLevels P ON CS.ProficiencyLevel = P.[Level]
WHERE
	CS.ConsultantId = @consultantId
GO

-- Skill_InsertNonCanonicalForConsultant
ALTER PROCEDURE Skill_InsertNonCanonicalForConsultant
(
	@name nvarchar(450),
	@consultantId int,
	@proficiencyLevel int
)
AS

INSERT INTO Skills (Name, IsCanonical)
VALUES (@name, 0);

DECLARE @skillId int;
SELECT @skillId = SCOPE_IDENTITY();

INSERT INTO ConsultantSkill (ConsultantId, SkillId, ProficiencyLevel)
VALUES (@consultantId, @skillId, @proficiencyLevel);

SELECT @skillId;
GO

-- Skill_AddToConsultant
ALTER PROCEDURE Skill_AddToConsultant
(
	@skillId int,
	@consultantId int,
	@proficiencyLevel int
)
AS

-- Only insert the many-to-many record if it doesn't already exist.
IF NOT EXISTS (SELECT ConsultantId FROM ConsultantSkill WHERE SkillId = @skillId)
	BEGIN
		INSERT INTO ConsultantSkill (ConsultantId, SkillId, ProficiencyLevel)
		VALUES (@consultantId, @skillId, @proficiencyLevel);
	END
GO

ALTER PROCEDURE Skill_RemoveFromConsultant
(
	@consultantid int,
	@skillId int
)
AS

-- Delete the many-to-many entry between the consultant and skill.
DELETE 
	CS
FROM 
	ConsultantSkill CS
WHERE 
	CS.SkillId = @skillId 
	AND CS.ConsultantId = @consultantid

-- Delete the skill itself if it is non-canonical and no longer tied
-- to any consultants.
DELETE 
	S
FROM 
	SkillS S
LEFT OUTER JOIN 
	ConsultantSkill CS ON CS.SkillId = S.Id
WHERE
	S.Id = @skillId 
	AND S.IsCanonical = 0
	AND CS.SkillId IS NULL
GO

-- Skill_SelectConsultantSkill
CREATE PROCEDURE Skill_SelectConsultantSkill
(
	@consultantId int,
	@skillId int
)
AS

SELECT TOP 1
	CS.ConsultantId,
	CS.SkillId,
	S.Name AS SkillName,
	S.IsCanonical,
	P.[Level] AS ProficiencyLevel,
	P.Name AS ProficiencyLevelName
FROM
	ConsultantSkill CS
INNER JOIN
	Skills S ON CS.SkillId = S.Id
INNER JOIN
	ProficiencyLevels P ON CS.ProficiencyLevel = P.[Level]
WHERE
	CS.ConsultantId = @consultantId AND CS.SkillId = @skillId
GO
