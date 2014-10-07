ITagManager Interface
=====================
Provides data access for tags (skills).

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
publicinterfaceITagManager
```

The **ITagManager** type exposes the following members.


Methods
-------

                 | Name                            | Description                                                                                               
---------------- | ------------------------------- | --------------------------------------------------------------------------------------------------------- 
![Public method] | [AddCanonicalTagAsync][2]       | Inserts a new canonical tag.                                                                              
![Public method] | [LoadByKeyword][3]              | Loads a tag by its keyword.                                                                               
![Public method] | [LoadByKeywordAsync][4]         | Loads a tag by its keyword.                                                                               
![Public method] | [LoadCanonicalTagsAsync][5]     | Load all of the canonical tags from the database. Account executives maintain the list of canonical tags. 
![Public method] | [LoadTagsForConsultantAsync][6] | Load all of the tags that have been applied to a consultant.                                              
![Public method] | [RemoveCanonicalTagAsync][7]    | Removes a canonical tag by changing it to no longer be canonical.                                         
![Public method] | [UpdateTagAsync][8]             | Updates a tag.                                                                                            


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: AddCanonicalTagAsync.md
[3]: LoadByKeyword.md
[4]: LoadByKeywordAsync.md
[5]: LoadCanonicalTagsAsync.md
[6]: LoadTagsForConsultantAsync.md
[7]: RemoveCanonicalTagAsync.md
[8]: UpdateTagAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"