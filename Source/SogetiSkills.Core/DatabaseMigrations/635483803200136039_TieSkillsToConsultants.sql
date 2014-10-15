CREATE PROCEDURE Skill_InsertNonCanonicalForConsultant
(
	@name nvarchar(450),
	@consultantId int
)
AS

INSERT INTO Skills (Name, IsCanonical)
VALUES (@name, 0);

DECLARE @skillId int;
SELECT @skillId = SCOPE_IDENTITY();

INSERT INTO Consultant_Skill (ConsultantId, SkillId)
VALUES (@consultantId, @skillId);

SELECT @skillId;
GO

CREATE PROCEDURE Skill_AddToConsultant
(
	@skillId int,
	@consultantId int
)
AS

INSERT INTO Consultant_Skill (ConsultantId, SkillId)
VALUES (@consultantId, @skillId);
GO