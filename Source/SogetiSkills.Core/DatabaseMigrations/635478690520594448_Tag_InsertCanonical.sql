CREATE PROCEDURE Tag_InsertCanonical
(
	@keyword nvarchar(450),
	@skillDescription nvarchar(MAX)
)
AS

INSERT INTO Tags (Keyword, SkillDescription, IsCanonical)
VALUES (@keyword, @skillDescription, 1)

SELECT SCOPE_IDENTITY();