ALTER PROCEDURE [dbo].[Skill_AddToConsultant]
(
	@skillId int,
	@consultantId int,
	@proficiencyLevel int
)
AS

-- Only insert the many-to-many record if it doesn't already exist.
IF NOT EXISTS (SELECT ConsultantId FROM ConsultantSkill WHERE SkillId = @skillId AND ConsultantId = @consultantId)
	BEGIN
		INSERT INTO ConsultantSkill (ConsultantId, SkillId, ProficiencyLevel)
		VALUES (@consultantId, @skillId, @proficiencyLevel);
	END