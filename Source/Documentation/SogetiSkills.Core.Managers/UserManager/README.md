UserManager Class
=================
Provides data access for users.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  [SogetiSkills.Core.ManagersManagerBase][2]  
    **SogetiSkills.Core.ManagersUserManager**  

**Namespace:** [SogetiSkills.Core.Managers][3]  
**Assembly:**

Syntax
------

```csharp
public class UserManager : ManagerBase, 
	IUserManager
```

The **UserManager** type exposes the following members.


Constructors
------------

                 | Name             | Description                                          
---------------- | ---------------- | ---------------------------------------------------- 
![Public method] | [UserManager][4] | Instantiate a new instance of the UserManager class. 


Methods
-------

                    | Name                          | Description                                                                                                      
------------------- | ----------------------------- | ---------------------------------------------------------------------------------------------------------------- 
![Public method]    | [Dispose][5]                  | (Inherited from [ManagerBase][2].)                                                                               
![Protected method] | [GetOpenConnection][6]        | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                                      
![Protected method] | [GetOpenConnectionAsync][7]   | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                                      
![Public method]    | [GetUserIdForEmailAddress][8] | Get the id of the user that is using an email address. If the email address is not in use then null is returned. 
![Public method]    | [LoadUserById][9]             | Loads a single user by their id.                                                                                 
![Public method]    | [LoadUserByIdAsync][10]       | Loads a single user by their id.                                                                                 
![Public method]    | [RegisterNewUserAsyncT][11]   | Registers and inserts a new user.                                                                                
![Public method]    | [UpdateContactInfoAsync][12]  | Updates just the contact info for a user.                                                                        
![Public method]    | [ValidatePasswordAsync][13]   | Validates an email address and password and returns the user if they match.                                      


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][3]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../ManagerBase/README.md
[3]: ../README.md
[4]: _ctor.md
[5]: ../ManagerBase/Dispose.md
[6]: ../ManagerBase/GetOpenConnection.md
[7]: ../ManagerBase/GetOpenConnectionAsync.md
[8]: GetUserIdForEmailAddress.md
[9]: LoadUserById.md
[10]: LoadUserByIdAsync.md
[11]: RegisterNewUserAsync__1.md
[12]: UpdateContactInfoAsync.md
[13]: ValidatePasswordAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Protected method]: ../../_icons/protmethod.gif "Protected method"