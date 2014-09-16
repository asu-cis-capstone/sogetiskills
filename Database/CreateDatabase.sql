-- =============================================
-- Create database
-- =============================================
USE master
GO

-- Drop the database if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'<Database_Name, sysname, Database_Name>'
)
DROP DATABASE dbSogetiBeach, Databases, SogetiBeach
GO

CREATE DATABASE dbSogetiBeach
GO