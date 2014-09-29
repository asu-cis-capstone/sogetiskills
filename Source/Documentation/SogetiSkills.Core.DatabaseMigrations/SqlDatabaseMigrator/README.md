SqlDatabaseMigrator Class
=========================
Migrates a SQL Server database using SQL scripts embedded as resources in the assembly. The database will be created if it does not already exist. Based loosely on Rails' Active Record Migrations.


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
---------------- | ------------------------ | ------------------------------------------------------------- 
![Public method] | [SqlDatabaseMigrator][3] | Instantiates a new instance of the SqlDatabaseMigrator class. 


Methods
-------

                 | Name         | Description                                                                            
---------------- | ------------ | -------------------------------------------------------------------------------------- 
![Public method] | [Migrate][4] | Migrate the database to the latest version by executing all pending migration scripts. 


Remarks
-------
 The SQL scripts are expected to be named in the form [UTC Now Ticks]_[Migration Name].sql. By using UTC now ticks as the migration id, we can ensure that migrations are run in the order in which they were created even if multiple developers are committing changes. The idea is that these migration scripts can be used for the local development database, unit tests, and production. By embedding the scripts with the code that actually uses them we can ensure that the database schema is always in a state that the code is expecting. 

See Also
--------

### Reference
[SogetiSkills.Core.DatabaseMigrations Namespace][2]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../README.md
[3]: _ctor.md
[4]: Migrate.md
[Public method]: ../../_icons/pubmethod.gif "Public method"