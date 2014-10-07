UserManagerLoadUserById Method
==============================
Loads a single user by their id.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public User LoadUserById(
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
### Implements
[IUserManagerLoadUserById(Int32)][4]  


See Also
--------

### Reference
[UserManager Class][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: ../../SogetiSkills.Core.Models/User/README.md
[4]: ../IUserManager/LoadUserById.md
[5]: README.md