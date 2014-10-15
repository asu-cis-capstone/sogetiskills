IUserManagerLoadUserById Method
===============================
Loads a single user by their id.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
User LoadUserById(
	int userId
)
```

### Parameters

#### *userId*
Type: [SystemInt32][2]  
The id of the user to load.

### Return Value
Type: [User][3]  
The user. Null if there are no matching users.

Remarks
-------
 An synchronous version is provided so that it can be used in an action filter attribute. Async action filter attributes are not yet supported. 

See Also
--------

### Reference
[IUserManager Interface][4]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: ../../SogetiSkills.Core.Models/User/README.md
[4]: README.md