SqlDatabaseMigrator Constructor
===============================
Instantiates a new instance of the SqlDatabaseMigrator class.

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


See Also
--------

### Reference
[SqlDatabaseMigrator Class][4]  
[SogetiSkills.Core.DatabaseMigrations Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/xbe1wdx9
[4]: README.md