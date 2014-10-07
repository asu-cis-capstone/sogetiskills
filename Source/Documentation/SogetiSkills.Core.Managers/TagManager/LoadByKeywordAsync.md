TagManagerLoadByKeywordAsync Method
===================================
Loads a tag by its keyword.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Task<Tag> LoadByKeywordAsync(
	string keyword
)
```

### Parameters

#### *keyword*
Type: [SystemString][2]  
The keyword to search for.

### Return Value
Type: [Task][3][Tag][4]  
The tag with the given keyword.
### Implements
[ITagManagerLoadByKeywordAsync(String)][5]  


Remarks
-------
 There is a unique index on keyword so only one tag will be returned. 

See Also
--------

### Reference
[TagManager Class][6]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../../SogetiSkills.Core.Models/Tag/README.md
[5]: ../ITagManager/LoadByKeywordAsync.md
[6]: README.md