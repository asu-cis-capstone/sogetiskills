IUserManager Interface
======================
Provides data access for users.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
publicinterfaceIUserManager
```

The **IUserManager** type exposes the following members.


Methods
-------

                 | Name                          | Description                                                                                                      
---------------- | ----------------------------- | ---------------------------------------------------------------------------------------------------------------- 
![Public method] | [GetUserIdForEmailAddress][2] | Get the id of the user that is using an email address. If the email address is not in use then null is returned. 
![Public method] | [LoadUserById][3]             | Loads a single user by their id.                                                                                 
![Public method] | [LoadUserByIdAsync][4]        | Loads a single user by their id.                                                                                 
![Public method] | [RegisterNewUserAsyncT][5]    | Registers and inserts a new user.                                                                                
![Public method] | [UpdateContactInfoAsync][6]   | Updates just the contact info for a user.                                                                        
![Public method] | [ValidatePasswordAsync][7]    | Validates an email address and password and returns the user if they match.                                      


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: GetUserIdForEmailAddress.md
[3]: LoadUserById.md
[4]: LoadUserByIdAsync.md
[5]: RegisterNewUserAsync__1.md
[6]: UpdateContactInfoAsync.md
[7]: ValidatePasswordAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"