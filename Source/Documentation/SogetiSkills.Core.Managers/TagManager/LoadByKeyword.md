TagManagerLoadByKeyword Method
==============================
Loads a tag by its keyword.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Tag LoadByKeyword(
	string keyword
)
```

### Parameters

#### *keyword*
Type: [SystemString][2]  
The keyword to search for.

### Return Value
Type: [Tag][3]  
The tag with the given keyword.
### Implements
[ITagManagerLoadByKeyword(String)][4]  


Remarks
-------
 There is a unique index on keyword so only one tag will be returned. 

See Also
--------

### Reference
[TagManager Class][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: ../../SogetiSkills.Core.Models/Tag/README.md
[4]: ../ITagManager/LoadByKeyword.md
[5]: README.md