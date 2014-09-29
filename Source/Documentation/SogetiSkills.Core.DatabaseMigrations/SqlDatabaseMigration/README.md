SqlDatabaseMigration Class
==========================
Represents a single migration to be made to the database.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.DatabaseMigrationsSqlDatabaseMigration**  

**Namespace:** [SogetiSkills.Core.DatabaseMigrations][2]  
**Assembly:**

Syntax
------

```csharp
publicclassSqlDatabaseMigration
```

The **SqlDatabaseMigration** type exposes the following members.


Constructors
------------

                 | Name                      | Description                                                    
---------------- | ------------------------- | -------------------------------------------------------------- 
![Public method] | [SqlDatabaseMigration][3] | Instantiates a new instance of the SqlDatabaseMigration class. 


Methods
-------

                 | Name       | Description                                                                          
---------------- | ---------- | ------------------------------------------------------------------------------------ 
![Public method] | [Apply][4] | Actually apply the migration by executing the migration script against the database. 


Properties
----------

                   | Name             | Description                                                                                             
------------------ | ---------------- | ------------------------------------------------------------------------------------------------------- 
![Public property] | [MigartionId][5] | Gets the migration id. It should be the DateTime.UtcNow.Ticks of when the migration script was created. 
![Public property] | [Name][6]        | Gets the friendly name of the migration script.                                                         
![Public property] | [Script][7]      | Gets the actual SQL script to execute when the migration is run.                                        


See Also
--------

### Reference
[SogetiSkills.Core.DatabaseMigrations Namespace][2]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../README.md
[3]: _ctor.md
[4]: Apply.md
[5]: MigartionId.md
[6]: Name.md
[7]: Script.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Public property]: ../../_icons/pubproperty.gif "Public property"