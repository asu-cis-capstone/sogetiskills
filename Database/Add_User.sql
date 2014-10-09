/*
procedure to register new user
inserts into User, Membership, UserType tables
*/

IF EXISTS (SELECT * FROM sys.objects
			WHERE type = 'P' 
			AND name = 'dbo.Add_User'))
DROP PROCEDURE dbo.Add_User

CREATE PROCEDURE dbo.Add_User
(
	@email 			nvarchar(max),
	@firstName	 	nvarchar(max),
	@lastName 		nvarchar(max),
	@phone 			nvarchar(max),
	@passwordHash 	nvarchar(max),
	@passwordSalt 	nvarchar(max),
	@userType		nvarchar(max)
)	
AS

BEGIN

DECLARE @userId int,
	 	@now date = getutcdate()

INSERT INTO User 
			(
				FirstName, 
				LastName, 
				Email, 
				Phone
			)
			VALUES 
			(
				@firstName,
				@lastName, 
				@email, 
				@phone
			)

SELECT @userId=SCOPE_IDENTITY()

INSERT INTO Membership
			(
				UserId, 
				PasswordHash, 
				PasswordSalt, 
				LastLogin
			)
			VALUES 
			(
				@userId, 
				@passwordHash, 
				@passwordSalt, 
				@now)

IF @usertype = 'consultant'
INSERT INTO UserType
			(
				UserId,
				TypeId,
				DateFrom
			)
			VALUES
			(
				@userId,
				1,
				@now
			)

IF @usertype = 'executive'
INSERT INTO UserType
			(
				UserId,
				TypeId,
				DateFrom
			)
			VALUES
			(
				@userId,
				2,
				@now
			)
END
GO