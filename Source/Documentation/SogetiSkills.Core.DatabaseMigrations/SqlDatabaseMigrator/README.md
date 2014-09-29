SqlDatabaseMigrator Class
=========================


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.DatabaseMigrationsSqlDatabaseMigrator**  

**Namespace:** [SogetiSkills.Core.DatabaseMigrations][2]  
**Assembly:**

Syntax
------

```csharp
publicclassSqlDatabaseMigrator
```

The **SqlDatabaseMigrator** type exposes the following members.


Constructors
------------

                 | Name                     | Description                                                                                                                                                                                          
---------------- | ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
![Public method] | [SqlDatabaseMigrator][3] | Migrates a SQL Server database using SQL scripts embedded as resources in the assembly. The database will be created if it does not already exist. Based loosely on Rails' Active Record Migrations. 


Methods
-------

                 | Name         | Description                                                                            
---------------- | ------------ | -------------------------------------------------------------------------------------- 
![Public method] | [Migrate][4] | Migrate the database to the latest version by executing all pending migration scripts. 


See Also
--------

### Reference
[SogetiSkills.Core.DatabaseMigrations Namespace][2]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../README.md
[3]: _ctor.md
[4]: Migrate.md
[Public method]: ../../_icons/pubmethod.gif "Public method"