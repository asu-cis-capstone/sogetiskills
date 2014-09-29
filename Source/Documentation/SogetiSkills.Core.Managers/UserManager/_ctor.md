UserManager Constructor
=======================
Instantiate a new instance of the UserManager class.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
public UserManager(
	ISaltGenerator saltGenerator,
	IPasswordHasher passwordHasher
)
```

### Parameters

#### *saltGenerator*
Type: [SogetiSkills.Core.SecurityISaltGenerator][2]  
Salt generator used to salt user passwords.

#### *passwordHasher*
Type: [SogetiSkills.Core.SecurityIPasswordHasher][3]  
Password hasher used to salt and hash user passwords.


See Also
--------

### Reference
[UserManager Class][4]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: ../../SogetiSkills.Core.Security/ISaltGenerator/README.md
[3]: ../../SogetiSkills.Core.Security/IPasswordHasher/README.md
[4]: README.md