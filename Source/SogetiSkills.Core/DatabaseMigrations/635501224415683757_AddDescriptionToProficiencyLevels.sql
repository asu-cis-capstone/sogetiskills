ALTER TABLE ProficiencyLevels
ADD SecondPersonDescription nvarchar(MAX) NULL
GO

ALTER TABLE ProficiencyLevels
ADD ThirdPersonDescription nvarchar(MAX) NULL
GO

-- Descriptions taken from http://hr.od.nih.gov/workingatnih/competencies/proficiencyscale.htm
-- 1- Fundamental Awareness
UPDATE 
	ProficiencyLevels
SET 
	SecondPersonDescription = 'You have a common knowledge or an understanding of basic techniques and concepts.',
	ThirdPersonDescription = 'The consultant has a common knowledge or an understanding of basic techniques and concepts.'
WHERE 
	[Level] = 1;

-- 2 - Novice
UPDATE 
	ProficiencyLevels
SET 
	SecondPersonDescription = 'You have the level of experience gained in a classroom and/or experimental scenarios or as a trainee on-the-job. You are expected to need help when performing this skill.',
	ThirdPersonDescription = 'The consultant has the level of experience gained in a classroom and/or experimental scenarios or as a trainee on-the-job. He/she is expected to need help when performing this skill.'
WHERE 
	[Level] = 2;

-- 3 - Intermediate
UPDATE 
	ProficiencyLevels
SET 
	SecondPersonDescription = 'You are able to successfully complete tasks in this competency as requested. Help from an expert may be required from time to time, but you can usually perform the skill independently.',
	ThirdPersonDescription = 'The consultant is able to successfully complete tasks in this competency as requested. Help from an expert may be required from time to time, but he/she can usually perform the skill independently.'
WHERE 
	[Level] = 3;

-- 4 - Advanced
UPDATE 
	ProficiencyLevels
SET 
	SecondPersonDescription = 'You can perform the actions associated with this skill without assistance. You are certainly recognized within your immediate organization as "a person to ask" when difficult questions arise regarding this skill.',
	ThirdPersonDescription = 'The consultant can perform the actions associated with this skill without assistance. He/she is certainly recognized within the immediate organization as "a person to ask" when difficult questions arise regarding this skill.'
WHERE 
	[Level] = 4;

-- 5 - Expert
UPDATE 
	ProficiencyLevels
SET 
	SecondPersonDescription = 'You are known as an expert in this area. You can provide guidance, troubleshoot and answer questions related to this area of expertise and the field where the skill is used.',
	ThirdPersonDescription = 'The consultant is known as an expert in this area. He/she can provide guidance, troubleshoot and answer questions related to this area of expertise and the field where the skill is used.'
WHERE 
	[Level] = 5;
GO

-- Select the new descriptions in ProficiencyLevels_SelectAll.
ALTER PROCEDURE ProficiencyLevels_SelectAll

AS

SELECT
	[Level],
	Name,
	SecondPersonDescription,
	ThirdPersonDescription
FROM ProficiencyLevels
ORDER BY [Level]
GO