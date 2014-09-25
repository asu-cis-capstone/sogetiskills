CREATE PROCEDURE Resume_SelectByUserId
(
	@userId int
)
AS

SELECT TOP 1
    R.Id,
	R.UserId,
	R.FileData,
	R.[FileName], 
	R.MimeType
FROM
	[Resumes] R
WHERE
	UserId = @userId;