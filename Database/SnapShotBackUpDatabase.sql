-- ===========================
-- Backup Database 
-- ===========================
BACKUP DATABASE <Database_Name, sysname, Database_Name> 
	TO  DISK = N'<Backup_Path,,C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\><Database_Name, sysname, Database_Name>.bak' 
WITH 
	NOFORMAT, 
	COMPRESSION,
	NOINIT,  
	NAME = N'<Database_Name, sysname, Database_Name>-Full Database Backup', 
	SKIP, 
	STATS = 10;
GO


-- =============================================
-- Create Database Snapshot
-- =============================================
USE master
GO

-- Drop database snapshot if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'<Database_Name, sysname, Database_Name>_<Snapshot_Id,,Snapshot_ID>'
)
DROP DATABASE <Database_Name, sysname, Database_Name>_<Snapshot_Id,,Snapshot_ID>
GO

-- Create the database snapshot
CREATE DATABASE <Database_Name, sysname, Database_Name>_<Snapshot_Id,,Snapshot_ID> ON
( NAME = <Database_Name, sysname, Database_Name>, FILENAME = 
'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Data\<Database_Name, sysname, Database_Name>_<Snapshot_Id,,Snapshot_ID>.ss' )
AS SNAPSHOT OF <Database_Name, sysname, Database_Name>;
GO

