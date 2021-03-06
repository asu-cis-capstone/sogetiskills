User Class
==========
Abstract base class for a user of the system. Users are either consultants or account executives. The user class holds contact and login information which are applicable to both user types.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.ModelsUser**  
    [SogetiSkills.Core.ModelsAccountExecutive][2]  
    [SogetiSkills.Core.ModelsConsultant][3]  

**Namespace:** [SogetiSkills.Core.Models][4]  
**Assembly:**

Syntax
------

```csharp
publicabstractclassUser
```

The **User** type exposes the following members.


Constructors
------------

                    | Name      | Description 
------------------- | --------- | ----------- 
![Protected method] | [User][5] |             


Properties
----------

                   | Name              | Description                                                              
------------------ | ----------------- | ------------------------------------------------------------------------ 
![Public property] | [EmailAddress][6] | The user's email address. This is also used when the user is logging in. 
![Public property] | [FirstName][7]    | The user's first name.                                                   
![Public property] | [Id][8]           | The id of the user.                                                      
![Public property] | [LastName][9]     | The user's last name.                                                    
![Public property] | [Password][10]    | The user's salted and hashed password.                                   
![Public property] | [PhoneNumber][11] | The user's phone number.                                                 


See Also
--------

### Reference
[SogetiSkills.Core.Models Namespace][4]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../AccountExecutive/README.md
[3]: ../Consultant/README.md
[4]: ../README.md
[5]: _ctor.md
[6]: EmailAddress.md
[7]: FirstName.md
[8]: Id.md
[9]: LastName.md
[10]: Password.md
[11]: PhoneNumber.md
[Protected method]: ../../_icons/protmethod.gif "Protected method"
[Public property]: ../../_icons/pubproperty.gif "Public property"