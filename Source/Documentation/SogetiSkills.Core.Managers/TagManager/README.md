TagManager Class
================
Provides data access for tags (skills).


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  [SogetiSkills.Core.ManagersManagerBase][2]  
    **SogetiSkills.Core.ManagersTagManager**  

**Namespace:** [SogetiSkills.Core.Managers][3]  
**Assembly:**

Syntax
------

```csharp
public class TagManager : ManagerBase, ITagManager
```

The **TagManager** type exposes the following members.


Constructors
------------

                 | Name            | Description 
---------------- | --------------- | ----------- 
![Public method] | [TagManager][4] |             


Methods
-------

                    | Name                            | Description                                                                 
------------------- | ------------------------------- | --------------------------------------------------------------------------- 
![Public method]    | [Dispose][5]                    | (Inherited from [ManagerBase][2].)                                          
![Protected method] | [GetOpenConnection][6]          | Gets an open connection to the database. (Inherited from [ManagerBase][2].) 
![Protected method] | [GetOpenConnectionAsync][7]     | Gets an open connection to the database. (Inherited from [ManagerBase][2].) 
![Public method]    | [LoadTagsForConsultantAsync][8] | Load all of the tags that have been applied to a consultant.                


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][3]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../ManagerBase/README.md
[3]: ../README.md
[4]: _ctor.md
[5]: ../ManagerBase/Dispose.md
[6]: ../ManagerBase/GetOpenConnection.md
[7]: ../ManagerBase/GetOpenConnectionAsync.md
[8]: LoadTagsForConsultantAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Protected method]: ../../_icons/protmethod.gif "Protected method"