CREATE PROCEDURE Tag_SelectByConsultantId
(
	@consultantId int
)
AS

SELECT
	T.Id,
	T.Keyword,
	T.SkillDescription,
	IsCanonical
FROM
	Consultant_Tag CT
INNER JOIN
	Tags T ON CT.TagId = T.Id
WHERE
	CT.ConsultantId = @consultantId