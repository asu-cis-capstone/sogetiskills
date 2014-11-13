CREATE PROCEDURE Consultant_SelectAllWithSkills
AS

-- Select consultants themselves
SELECT
	C.Id,
	C.UserType,
	C.EmailAddress,
	C.FirstName,
	C.LastName,
	C.PhoneNumber,
	C.IsOnBeach
FROM
	Users C
WHERE
	C.UserType = 'Consultant'

-- Select consultant skills
SELECT
	CS.ConsultantId,
	CS.SkillId,
	CS.ProficiencyLevel,
	S.Name AS SkillName,
	S.IsCanonical,
	P.Name AS ProficiencyLevelName
FROM
	Users C
INNER JOIN
	ConsultantSkill CS ON CS.ConsultantId = C.Id
INNER JOIN
	Skills S ON CS.SkillId = S.Id
INNER JOIN
	ProficiencyLevels P ON CS.ProficiencyLevel = P.[Level]
WHERE
	C.UserType = 'Consultant'
GO