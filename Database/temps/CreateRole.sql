-- ================================
-- Create Database Role template
-- ================================
USE dbSogetiBeach
GO

-- Create the database role
CREATE ROLE Production_Owner AUTHORIZATION [dbo]
GO

-- Grant access rights to a specific schema in the database
GRANT 
	ALTER, 
	CONTROL, 
	DELETE, 
	EXECUTE, 
	INSERT, 
	REFERENCES, 
	SELECT, 
	TAKE OWNERSHIP, 
	UPDATE, 
	VIEW DEFINITION 
ON SCHEMA::Production
	TO Production_Owner
GO

-- Add an existing user to the role
ALTER ROLE Production_Owner ADD MEMBER user_name
GO


 