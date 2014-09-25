CREATE PROCEDURE User_Insert
(
	@userType nvarchar(450),
	@emailAddress nvarchar(450),
	@firstName nvarchar(max),
	@lastName nvarchar(max),
	@phoneNumber nvarchar(max),
	@passwordHash nvarchar(max),
	@passwordSalt nvarchar(max)
)
AS

INSERT INTO Users (UserType, EmailAddress, FirstName, LastName, PhoneNumber, Password_Hash, Password_Salt)
VALUES (@userType, @emailAddress, @firstName, @lastName, @phoneNumber, @passwordHash, @passwordSalt);

SELECT SCOPE_IDENTITY();