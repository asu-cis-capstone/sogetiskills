ManagerBase Class
=================
Base class for all manager classes. Provides and manages a database connection.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.ManagersManagerBase**  
    [SogetiSkills.Core.ManagersResumeManager][2]  
    [SogetiSkills.Core.ManagersTagManager][3]  
    [SogetiSkills.Core.ManagersUserManager][4]  

**Namespace:** [SogetiSkills.Core.Managers][5]  
**Assembly:**

Syntax
------

```csharp
public abstract class ManagerBase : IDisposable
```

The **ManagerBase** type exposes the following members.


Constructors
------------

                    | Name             | Description 
------------------- | ---------------- | ----------- 
![Protected method] | [ManagerBase][6] |             


Methods
-------

                    | Name                        | Description                              
------------------- | --------------------------- | ---------------------------------------- 
![Public method]    | [Dispose][7]                |                                          
![Protected method] | [GetOpenConnection][8]      | Gets an open connection to the database. 
![Protected method] | [GetOpenConnectionAsync][9] | Gets an open connection to the database. 


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][5]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../ResumeManager/README.md
[3]: ../TagManager/README.md
[4]: ../UserManager/README.md
[5]: ../README.md
[6]: _ctor.md
[7]: Dispose.md
[8]: GetOpenConnection.md
[9]: GetOpenConnectionAsync.md
[Protected method]: ../../_icons/protmethod.gif "Protected method"
[Public method]: ../../_icons/pubmethod.gif "Public method"