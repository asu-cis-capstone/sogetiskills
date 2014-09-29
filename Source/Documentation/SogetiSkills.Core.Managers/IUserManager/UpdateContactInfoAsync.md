IUserManagerUpdateContactInfoAsync Method
=========================================
Updates just the contact info for a user.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
Task UpdateContactInfoAsync(
	int userId,
	string firstName,
	string lastName,
	string emailAddress,
	string phoneNumber
)
```

### Parameters

#### *userId*
Type: [SystemInt32][2]  
The id of the user to update.

#### *firstName*
Type: [SystemString][3]  
The new first name to store for the user.

#### *lastName*
Type: [SystemString][3]  
The new last name to store for the user.

#### *emailAddress*
Type: [SystemString][3]  
The new email address to store for the user.

#### *phoneNumber*
Type: [SystemString][3]  
The new phone number to store for the user.

### Return Value
Type: [Task][4]

See Also
--------

### Reference
[IUserManager Interface][5]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[4]: http://msdn.microsoft.com/en-us/library/dd235678
[5]: README.md