IResumeManagerLoadResumeByUserIdAsync Method
============================================
Loads the actual resume (including file contents) for the user.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
Task<Resume> LoadResumeByUserIdAsync(
	int userId
)
```

### Parameters

#### *userId*
Type: [SystemInt32][2]  
The user id of the consultant whose resume we are loading.

### Return Value
Type: [Task][3][Resume][4]  
The consultant's resume.

See Also
--------

### Reference
[IResumeManager Interface][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../../SogetiSkills.Core.Models/Resume/README.md
[5]: README.md