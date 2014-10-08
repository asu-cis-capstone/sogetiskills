CREATE PROCEDURE Skill_RemoveFromConsultant
(
	@consultantid int,
	@skillId int
)
AS

-- Delete the many-to-many entry between the consultant and skill.
DELETE 
	CS
FROM 
	Consultant_Skill CS
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
	Consultant_Skill CS ON CS.SkillId = S.Id
WHERE
	S.Id = @skillId 
	AND S.IsCanonical = 0
	AND CS.SkillId IS NULL