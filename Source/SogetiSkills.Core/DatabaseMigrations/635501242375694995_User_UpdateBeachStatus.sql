CREATE PROCEDURE User_UpdateBeachStatus
(
	@consultantId int,
	@beachStatus bit
)
AS

UPDATE
	Users
SET
	IsOnBeach = @beachStatus
WHERE
	Id = @consultantId AND UserType = 'Consultant';
GO