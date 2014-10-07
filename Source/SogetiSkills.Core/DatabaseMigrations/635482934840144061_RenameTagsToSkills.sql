-- Rename the tags table to skills and change the Keyword column to Name and the SkillDescription column to just Description.
DROP INDEX Tags.IX_Tag_IsCanonical
EXEC sp_rename 'Tags.Keyword', 'Name', 'COLUMN';
EXEC sp_rename 'Tags.SkillDescription', 'Description', 'COLUMN';
EXEC sp_rename 'Tags', 'Skills';
GO

-- Rename the Consultant_Tag table to use Skill instead of Tag.
EXEC sp_rename 'FK_Consultant_Tag_Users', 'FK_Consultant_Skill_Users', 'OBJECT';
EXEC sp_rename 'FK_Consultant_Tag_Tags', 'FK_Consultant_Skill_Skills', 'OBJECT';
EXEC sp_rename 'Consultant_Tag.TagId', 'SkillId', 'COLUMN';
EXEC sp_rename 'Consultant_Tag', 'Consultant_Skill';
GO

-- Update the stored procedures for Skills to work with the new table and column names.
-- Update the Tag_SelectByConsultantId procedure.
EXEC sp_rename 'Tag_SelectByConsultantId', 'Skill_SelectByConsultantId', 'OBJECT';
GO
ALTER PROCEDURE Skill_SelectByConsultantId
(
	@consultantId int
)
AS

SELECT
	S.Id,
	S.Name,
	S.[Description],
	S.IsCanonical
FROM
	Consultant_Skill CS
INNER JOIN
	Skills S ON CS.SkillId = S.Id
WHERE
	CS.ConsultantId = @consultantId
GO

-- Update the Tag_SelectByConsultantId procedure.
EXEC sp_rename 'Tag_SelectCanonical', 'Skill_SelectCanonical', 'OBJECT';
GO
ALTER PROCEDURE Skill_SelectCanonical
AS

SELECT
	S.Id,
	S.Name,
	S.[Description],
	S.IsCanonical
FROM
	Skills S
WHERE
	S.IsCanonical = 1
ORDER BY 
	S.Name
GO

-- Update the Tag_InsertCanonical procedure.
EXEC sp_rename 'Tag_InsertCanonical', 'Skill_InsertCanonical', 'OBJECT';
GO
ALTER PROCEDURE Skill_InsertCanonical
(
	@name nvarchar(450),
	@description nvarchar(MAX)
)
AS

INSERT INTO Skills (Name, Description, IsCanonical)
VALUES (@name, @description, 1)

SELECT SCOPE_IDENTITY();
GO

-- Update the Tag_DeleteCanonical procedure.
EXEC sp_rename 'Tag_DeleteCanonical', 'Skill_DeleteCanonical', 'OBJECT';
GO
ALTER PROCEDURE Skill_DeleteCanonical
(
	@id int
)
AS

-- If the skill is being used by a consultant then change flag it as no longer
-- being canonical.  If it's not being used then just delete it.
IF EXISTS(SELECT SkillId FROM Consultant_Skill WHERE SkillId = @id)
	BEGIN
		UPDATE Skills
		SET IsCanonical = 0
		WHERE ID = @id;
	END
ELSE
	DELETE Skills
	WHERE ID = @id;
GO

-- Update the Tag_Update procedure.
EXEC sp_rename 'Tag_Update', 'Skill_Update', 'OBJECT';
GO
ALTER PROCEDURE Skill_Update
(
	@id int,
	@name nvarchar(450),
	@description nvarchar(MAX),
	@isCanonical bit
)
AS

UPDATE
	Skills
SET 
	Name = @name,
	[Description] = @description,
	IsCanonical = @isCanonical
WHERE
	Id = @Id;
GO

-- Update the Tag_SelectByKeyword procedure.
EXEC sp_rename 'Tag_SelectByKeyword', 'Skill_SelectByName', 'OBJECT';
GO
ALTER PROCEDURE Skill_SelectByName
(
	@name nvarchar(450)
)
AS

SELECT TOP 1
	Id,
	Name,
	[Description],
	IsCanonical
FROM
	Skills
WHERE
	Name = @name;
GO