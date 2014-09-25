CREATE PROCEDURE Resume_SelectMetadataByUserId
(
	@userId int
)
AS

SELECT TOP 1
	R.[FileName], 
	R.MimeType
FROM
	[Resumes] R
WHERE 
	UserId = @userId;