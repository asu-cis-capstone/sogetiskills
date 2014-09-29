SqlDatabaseMigration Class
==========================


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
---------------- | ------------------------- | --------------------------------------------------------- 
![Public method] | [SqlDatabaseMigration][3] | Represents a single migration to be made to the database. 


Methods
-------

                 | Name       | Description                                                                          
---------------- | ---------- | ------------------------------------------------------------------------------------ 
![Public method] | [Apply][4] | Actually apply the migration by executing the migration script against the database. 


Properties
----------

                   | Name             | Description 
------------------ | ---------------- | ----------- 
![Public property] | [MigartionId][5] |             
![Public property] | [Name][6]        |             
![Public property] | [Script][7]      |             


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