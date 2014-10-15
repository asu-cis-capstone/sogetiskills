TagManagerUpdateTagAsync Method
===============================
Updates a tag.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Task UpdateTagAsync(
	int tagId,
	string keyword,
	string skillDescription,
	bool isCanonical
)
```

### Parameters

#### *tagId*
Type: [SystemInt32][2]  
The id of the tag to update.

#### *keyword*
Type: [SystemString][3]  
The new keyword for the tag.

#### *skillDescription*
Type: [SystemString][3]  
The new skill description for the tag.

#### *isCanonical*
Type: [SystemBoolean][4]  
Whether or not the tag is canonical.

### Return Value
Type: [Task][5]
### Implements
[ITagManagerUpdateTagAsync(Int32, String, String, Boolean)][6]  


See Also
--------

### Reference
[TagManager Class][7]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[4]: http://msdn.microsoft.com/en-us/library/a28wyd50
[5]: http://msdn.microsoft.com/en-us/library/dd235678
[6]: ../ITagManager/UpdateTagAsync.md
[7]: README.md