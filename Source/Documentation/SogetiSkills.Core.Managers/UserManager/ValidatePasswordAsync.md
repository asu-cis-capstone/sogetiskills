UserManagerValidatePasswordAsync Method
=======================================
Validates an email address and password and returns the user if they match.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Task<User> ValidatePasswordAsync(
	string emailAddress,
	string password
)
```

### Parameters

#### *emailAddress*
Type: [SystemString][2]  
The login email address.

#### *password*
Type: [SystemString][2]  


### Return Value
Type: [Task][3][User][4]  
The user that matched the email address/password. Null if there was no match.
### Implements
[IUserManagerValidatePasswordAsync(String, String)][5]  


See Also
--------

### Reference
[UserManager Class][6]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../../SogetiSkills.Core.Models/User/README.md
[5]: ../IUserManager/ValidatePasswordAsync.md
[6]: README.md