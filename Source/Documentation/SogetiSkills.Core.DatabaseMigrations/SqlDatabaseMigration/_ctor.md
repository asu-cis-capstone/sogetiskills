SqlDatabaseMigration Constructor
================================
Represents a single migration to be made to the database.

**Namespace:** [SogetiSkills.Core.DatabaseMigrations][1]  
**Assembly:**

Syntax
------

```csharp
public SqlDatabaseMigration(
	long migrationId,
	string name,
	string script
)
```

### Parameters

#### *migrationId*
Type: [SystemInt64][2]  
The id of the migration. It should be the DateTime.UtcNow.Ticks of when the migration script was created.

#### *name*
Type: [SystemString][3]  
The friendly name of the migration script.

#### *script*
Type: [SystemString][3]  
The actual SQL script to execute when the migration is run.


See Also
--------

### Reference
[SqlDatabaseMigration Class][4]  
[SogetiSkills.Core.DatabaseMigrations Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/6yy583ek
[3]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[4]: README.md