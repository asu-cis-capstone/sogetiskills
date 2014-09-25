CREATE PROCEDURE User_SelectById
(
	@userId int
)
AS

SELECT TOP 1
	U.Id,
	U.UserType,
	U.EmailAddress,
	U.FirstName,
	U.LastName,
	U.PhoneNumber,
	U.Password_Hash,
	U.Password_Salt,
	U.IsOnBeach
FROM
	Users U
WHERE
	U.Id = @userId