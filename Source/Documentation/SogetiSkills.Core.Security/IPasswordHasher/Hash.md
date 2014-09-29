IPasswordHasherHash Method
==========================
Salt and hash the password.

**Namespace:** [SogetiSkills.Core.Security][1]  
**Assembly:**

Syntax
------

```csharp
string Hash(
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

See Also
--------

### Reference
[IPasswordHasher Interface][3]  
[SogetiSkills.Core.Security Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: README.md