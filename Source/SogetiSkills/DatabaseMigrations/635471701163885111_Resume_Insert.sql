CREATE PROCEDURE Resume_Insert
(
	@userid int,
	@fileData varbinary(max),
	@fileName nvarchar(max),
	@mimeType nvarchar(max)
)
AS

IF EXISTS (SELECT COUNT(*) FROM Resumes WHERE UserId = @userId)
	BEGIN
		DELETE Resumes WHERE UserId = @userId;
	END

INSERT INTO Resumes (UserId, FileData, [FileName], MimeType)
VALUES (@userId, @fileData, @fileName, @mimeType);