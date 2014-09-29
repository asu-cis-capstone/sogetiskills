UserManagerRegisterNewUserAsyncT Method
=======================================
Registers and inserts a new user.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Task<T> RegisterNewUserAsync<T>(
	string emailAddress,
	string plainTextPassword,
	string firstName,
	string lastName,
	string phoneNumber
)
where T : User

```

### Parameters

#### *emailAddress*
Type: [SystemString][2]  
The email address for the new user. Also used to log in.

#### *plainTextPassword*
Type: [SystemString][2]  
The password for the new user.

#### *firstName*
Type: [SystemString][2]  
The new user's first name.

#### *lastName*
Type: [SystemString][2]  
The new user's last name.

#### *phoneNumber*
Type: [SystemString][2]  
The new user's phone number.

### Type Parameters

#### *T*
The type of user to insert - either a Consultant or Account Executive.

### Return Value
Type: [Task][3]**T**  
The new user with its id populated.
### Implements
[IUserManagerRegisterNewUserAsyncT(String, String, String, String, String)][4]  


See Also
--------

### Reference
[UserManager Class][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/dd321424
[4]: ../IUserManager/RegisterNewUserAsync__1.md
[5]: README.md