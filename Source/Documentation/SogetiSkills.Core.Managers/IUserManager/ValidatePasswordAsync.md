IUserManagerValidatePasswordAsync Method
========================================
Validates an email address and password and returns the user if they match.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
Task<User> ValidatePasswordAsync(
	string emailAddress,
	string plainTextPassword
)
```

### Parameters

#### *emailAddress*
Type: [SystemString][2]  
The login email address.

#### *plainTextPassword*
Type: [SystemString][2]  
The plain text password. It will be salted and hashed inside this method.

### Return Value
Type: [Task][3][User][4]  
The user that matched the email address/password. Null if there was no match.

See Also
--------

### Reference
[IUserManager Interface][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../../SogetiSkills.Core.Models/User/README.md
[5]: README.md