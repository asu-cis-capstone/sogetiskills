CREATE PROCEDURE User_SelectIdByEmailAddress
(
	@emailAddress nvarchar(max)
)
AS

SELECT TOP 1
	U.Id
FROM
	Users U
WHERE
	U.EmailAddress = @emailAddress