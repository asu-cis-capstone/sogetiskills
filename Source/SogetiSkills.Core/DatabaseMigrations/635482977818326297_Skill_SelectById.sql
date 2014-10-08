CREATE PROCEDURE Skill_SelectById
(
	@id int
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
	Id = @id;