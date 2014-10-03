CREATE PROCEDURE Tags_SelectCanonical
AS

SELECT T.Id,
	T.Keyword,
	T.SkillDescription,
	T.IsCanonical
FROM
	Tags T
WHERE
	T.IsCanonical = 1
ORDER BY 
	T.Keyword