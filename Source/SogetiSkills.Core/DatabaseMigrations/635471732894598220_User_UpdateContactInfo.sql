CREATE PROCEDURE User_UpdateContactInfo
(
	@userId int,
	@firstName nvarchar(max),
	@lastName nvarchar(max),
	@emailAddress nvarchar(450),
	@phoneNumber nvarchar(max)
)
AS

UPDATE 
	Users
SET 
	Firstname = @firstName,
	LastName = @lastName,
	EmailAddress = @emailAddress,
	PhoneNumber = @phoneNumber
WHERE
	Id = @userId