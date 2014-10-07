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

                    | Name                             | Description                                                                                               
------------------- | -------------------------------- | --------------------------------------------------------------------------------------------------------- 
![Public method]    | [AddCanonicalTagAsync][5]        | Inserts a new canonical tag.                                                                              
![Public method]    | [Dispose][6]                     | (Inherited from [ManagerBase][2].)                                                                        
![Protected method] | [GetOpenConnection][7]           | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                               
![Protected method] | [GetOpenConnectionAsync][8]      | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                               
![Public method]    | [LoadByKeyword][9]               | Loads a tag by its keyword.                                                                               
![Public method]    | [LoadByKeywordAsync][10]         | Loads a tag by its keyword.                                                                               
![Public method]    | [LoadCanonicalTagsAsync][11]     | Load all of the canonical tags from the database. Account executives maintain the list of canonical tags. 
![Public method]    | [LoadTagsForConsultantAsync][12] | Load all of the tags that have been applied to a consultant.                                              
![Public method]    | [RemoveCanonicalTagAsync][13]    | Removes a canonical tag by changing it to no longer be canonical.                                         
![Public method]    | [UpdateTagAsync][14]             | Updates a tag.                                                                                            


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][3]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../ManagerBase/README.md
[3]: ../README.md
[4]: _ctor.md
[5]: AddCanonicalTagAsync.md
[6]: ../ManagerBase/Dispose.md
[7]: ../ManagerBase/GetOpenConnection.md
[8]: ../ManagerBase/GetOpenConnectionAsync.md
[9]: LoadByKeyword.md
[10]: LoadByKeywordAsync.md
[11]: LoadCanonicalTagsAsync.md
[12]: LoadTagsForConsultantAsync.md
[13]: RemoveCanonicalTagAsync.md
[14]: UpdateTagAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Protected method]: ../../_icons/protmethod.gif "Protected method"