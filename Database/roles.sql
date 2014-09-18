/*Principal is an entity that can request SQL Server resources. Can be at server or database level.
	Server level can have 2 levels: Windows or SQL Server
	Windows can be local login or domain login (domain is preferred)
-Microsoft Best Practice is to use Window level principals only
Database-level Principals:
	Database User
	Database Role
	Application Role
*/

--2 find server_principals

select * from sys.server_principals
where type in ('s', 'u')


/*Collectively assign permissions with roles

Contained database accepts contained users that are not associated with login but have password
*/

EXECUTE  AS USER = 'app_user'
SELECT USER_NAME()

GO
REVERT
GO 
SELECT USER_NAME()
GO

--add windows group to database role
EXEC sp_addrolemember 'db_ddladmin', [_db\Developers]
GO

-- add sql user to database role
EXEC sp_addrolemember 'db_role', [databaserole]
GO

-- create flexible database role
CREATE ROLE [db_devs]
GO

--add database user(s) to role
ALTER ROLE [db_devs] ADD MEMBER [_db\Developers]
GO

--grant permission to view and change table definitions
GRANT ALTER ON [dbo].[Sales_Archive] TO [db_devs]
GRANT VIEW DEFINITION ON [dbo].[Sales_Archive] TO [db_devs]

USE _db
GO

--activate application role w/password
CREATE APPLICATION ROLE [web_app] WITH PASSWORD = '-p@ssw@rd-'
GO

--assign permissions to application role
GRANT SELECT ON [dbo].[Employees] TO [web_app]
GRANT INSERT ON [dbo].[Employees] TO [web_app]
GRANT UPDATE ON [dbo].[Employees] TO [web_app]
DENY DELETE ON [dbo].[Employees] TO [web_app]
GO

--activate application role
EXEC sp_setapprole 'app', '-p@ssw@rd-'

SELECT USER_NAME()
GO
share point is ad-hoc analytics
ssrs reporting services

full text indexing



? for james
naming conventions
