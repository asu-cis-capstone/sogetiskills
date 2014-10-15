CREATE PROCEDURE Tag_SelectByKeyword
(
	@keyword nvarchar(450)
)
AS

SELECT TOP 1
	Id,
	Keyword,
	SkillDescription,
	IsCanonical
FROM
	Tags
WHERE
	Keyword = @keyword;