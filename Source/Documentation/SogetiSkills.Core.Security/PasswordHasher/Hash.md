PasswordHasherHash Method
=========================
Salt and hash the password.

**Namespace:** [SogetiSkills.Core.Security][1]  
**Assembly:**

Syntax
------

```csharp
public string Hash(
	string plainTextPassword,
	string salt
)
```

### Parameters

#### *plainTextPassword*
Type: [SystemString][2]  
The password to salt and hash.

#### *salt*
Type: [SystemString][2]  
The salt to use before hashing. If this is a brand new password then the salt should be generated with an instance of an ISaltGenerator.

### Return Value
Type: [String][2]  
The salted and hashed password.
### Implements
[IPasswordHasherHash(String, String)][3]  


See Also
--------

### Reference
[PasswordHasher Class][4]  
[SogetiSkills.Core.Security Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: ../IPasswordHasher/Hash.md
[4]: README.md