UserManagerGetUserIdForEmailAddress Method
==========================================
Get the id of the user that is using an email address. If the email address is not in use then null is returned.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public Nullable<int> GetUserIdForEmailAddress(
	string emailAddress
)
```

### Parameters

#### *emailAddress*
Type: [SystemString][2]  
The email address to search for.

### Return Value
Type: [Nullable][3][Int32][4]  
The id of the user that is using the email address.
### Implements
[IUserManagerGetUserIdForEmailAddress(String)][5]  


Remarks
-------
 This is generally used for validating that an email address is not in use during registration. Because FluentValidtion does not work with async/await yet, this method is synchronous. 

See Also
--------

### Reference
[UserManager Class][6]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/b3h38hb0
[4]: http://msdn.microsoft.com/en-us/library/td2s409d
[5]: ../IUserManager/GetUserIdForEmailAddress.md
[6]: README.md