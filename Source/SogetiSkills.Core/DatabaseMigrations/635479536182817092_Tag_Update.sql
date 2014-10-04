CREATE PROCEDURE Tag_Update
(
	@id int,
	@keyword nvarchar(450),
	@skillDescription nvarchar(MAX),
	@isCanonical bit
)
AS

UPDATE
	Tags
SET 
	Keyword = @keyword,
	SkillDescription = @skillDescription,
	IsCanonical = @isCanonical
WHERE
	Id = @Id;