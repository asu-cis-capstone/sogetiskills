Consultant Class
================
A consultant that may be on "the beach" and have tags.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  [SogetiSkills.Core.ModelsUser][2]  
    **SogetiSkills.Core.ModelsConsultant**  

**Namespace:** [SogetiSkills.Core.Models][3]  
**Assembly:**

Syntax
------

```csharp
public class Consultant : User
```

The **Consultant** type exposes the following members.


Constructors
------------

                 | Name            | Description 
---------------- | --------------- | ----------- 
![Public method] | [Consultant][4] |             


Properties
----------

                   | Name              | Description                                                                                          
------------------ | ----------------- | ---------------------------------------------------------------------------------------------------- 
![Public property] | [EmailAddress][5] | The user's email address. This is also used when the user is logging in. (Inherited from [User][2].) 
![Public property] | [FirstName][6]    | The user's first name. (Inherited from [User][2].)                                                   
![Public property] | [Id][7]           | The id of the user. (Inherited from [User][2].)                                                      
![Public property] | [IsOnBeach][8]    | Gets or sets a flag indicating whether or not the consultant is currently on the beach.              
![Public property] | [LastName][9]     | The user's last name. (Inherited from [User][2].)                                                    
![Public property] | [Password][10]    | The user's salted and hashed password. (Inherited from [User][2].)                                   
![Public property] | [PhoneNumber][11] | The user's phone number. (Inherited from [User][2].)                                                 


See Also
--------

### Reference
[SogetiSkills.Core.Models Namespace][3]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../User/README.md
[3]: ../README.md
[4]: _ctor.md
[5]: ../User/EmailAddress.md
[6]: ../User/FirstName.md
[7]: ../User/Id.md
[8]: IsOnBeach.md
[9]: ../User/LastName.md
[10]: ../User/Password.md
[11]: ../User/PhoneNumber.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Public property]: ../../_icons/pubproperty.gif "Public property"