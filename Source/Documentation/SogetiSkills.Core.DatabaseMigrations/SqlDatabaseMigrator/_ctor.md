SqlDatabaseMigrator Constructor
===============================
Migrates a SQL Server database using SQL scripts embedded as resources in the assembly. The database will be created if it does not already exist. Based loosely on Rails' Active Record Migrations.

**Namespace:** [SogetiSkills.Core.DatabaseMigrations][1]  
**Assembly:**

Syntax
------

```csharp
public SqlDatabaseMigrator(
	string connectionString,
	Assembly migrationScriptsAssembly,
	string migrationScriptsNamespace
)
```

### Parameters

#### *connectionString*
Type: [SystemString][2]  
The connection string for the database to be migrated.

#### *migrationScriptsAssembly*
Type: [System.ReflectionAssembly][3]  
The assembly containing the migration SQL scripts.

#### *migrationScriptsNamespace*
Type: [SystemString][2]  
The namespace where the migration scripts are embedded.


Remarks
-------
 The SQL scripts are expected to be named in the form [UTC Now Ticks]_[Migration Name].sql. By using UTC now ticks as the migration id, we can ensure that migrations are run in the order in which they were created even if multiple developers are committing changes. The idea is that these migration scripts can be used for the local development database, unit tests, and production. By embedding the scripts with the code that actually uses them we can ensure that the database schema is always in a state that the code is expecting. 

See Also
--------

### Reference
[SqlDatabaseMigrator Class][4]  
[SogetiSkills.Core.DatabaseMigrations Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/xbe1wdx9
[4]: README.md