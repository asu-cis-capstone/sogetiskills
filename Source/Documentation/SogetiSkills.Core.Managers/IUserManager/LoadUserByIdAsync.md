IUserManagerLoadUserByIdAsync Method
====================================
Loads a single user by their id.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
Task<User> LoadUserByIdAsync(
	int userId
)
```

### Parameters

#### *userId*
Type: [SystemInt32][2]  
The id of the user to load.

### Return Value
Type: [Task][3][User][4]  
The user. Null if there are no matching users.

See Also
--------

### Reference
[IUserManager Interface][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../../SogetiSkills.Core.Models/User/README.md
[5]: README.md