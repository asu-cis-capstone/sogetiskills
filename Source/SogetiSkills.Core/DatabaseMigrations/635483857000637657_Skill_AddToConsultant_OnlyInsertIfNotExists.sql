ALTER PROCEDURE Skill_AddToConsultant
(
	@skillId int,
	@consultantId int
)
AS

-- Only insert the many-to-many record if it doesn't already exist.
IF NOT EXISTS (SELECT ConsultantId FROM Consultant_Skill WHERE SkillId = @skillId)
	BEGIN
		INSERT INTO Consultant_Skill (ConsultantId, SkillId)
		VALUES (@consultantId, @skillId);
	END