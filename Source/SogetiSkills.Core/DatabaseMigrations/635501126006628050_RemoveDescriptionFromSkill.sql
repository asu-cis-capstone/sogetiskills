ALTER TABLE Skills
DROP COLUMN Description;
GO

-- Skill_InsertCanonical
ALTER PROCEDURE Skill_InsertCanonical
(
	@name nvarchar(450)
)
AS

INSERT INTO Skills (Name, IsCanonical)
VALUES (@name, 1)

SELECT SCOPE_IDENTITY();
GO

-- Skill_Update
ALTER PROCEDURE Skill_Update
(
	@id int,
	@name nvarchar(450),
	@isCanonical bit
)
AS

UPDATE
	Skills
SET 
	Name = @name,	
	IsCanonical = @isCanonical
WHERE
	Id = @Id;
GO

-- Skill_SelectByName
ALTER PROCEDURE Skill_SelectByName
(
	@name nvarchar(450)
)
AS

SELECT TOP 1
	Id,
	Name,
	IsCanonical
FROM
	Skills
WHERE
	Name = @name;
GO

-- Skill_SelectById
ALTER PROCEDURE Skill_SelectById
(
	@id int
)
AS

SELECT TOP 1
	Id,
	Name,
	IsCanonical
FROM
	Skills
WHERE
	Id = @id;
GO

-- Skill_Update
ALTER PROCEDURE Skill_Update
(
	@id int,
	@name nvarchar(450),
	@isCanonical bit
)
AS

UPDATE
	Skills
SET 
	Name = @name,
	IsCanonical = @isCanonical
WHERE
	Id = @Id;
GO

-- Skill_SelectCanonical
ALTER PROCEDURE Skill_SelectCanonical
AS

SELECT
	S.Id,
	S.Name,
	S.IsCanonical
FROM
	Skills S
WHERE
	S.IsCanonical = 1
ORDER BY 
	S.Name
GO

-- Skill_DeleteCanonical
ALTER PROCEDURE Skill_DeleteCanonical
(
	@id int
)
AS

-- If the skill is being used by a consultant then change flag it as no longer
-- being canonical.  If it's not being used then just delete it.
IF EXISTS(SELECT SkillId FROM ConsultantSkill WHERE SkillId = @id)
	BEGIN
		UPDATE Skills
		SET IsCanonical = 0
		WHERE ID = @id;
	END
ELSE
	DELETE Skills
	WHERE ID = @id;
GO
