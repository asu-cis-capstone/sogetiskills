TagManagerLoadTagsForConsultantAsync Method
===========================================
Load all of the tags that have been applied to a consultant.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(
	int consultantId
)
```

### Parameters

#### *consultantId*
Type: [SystemInt32][2]  
The id of the consultant to load tags for.

### Return Value
Type: [Task][3][IEnumerable][4][Tag][5]  
All the tags that have been applied to consultant
### Implements
[ITagManagerLoadTagsForConsultantAsync(Int32)][6]  


See Also
--------

### Reference
[TagManager Class][7]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: http://msdn.microsoft.com/en-us/library/9eekhta0
[5]: ../../SogetiSkills.Core.Models/Tag/README.md
[6]: ../ITagManager/LoadTagsForConsultantAsync.md
[7]: README.md