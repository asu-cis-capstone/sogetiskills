ResumeManager Class
===================
Provides data access for consultants' resumes.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  [SogetiSkills.Core.ManagersManagerBase][2]  
    **SogetiSkills.Core.ManagersResumeManager**  

**Namespace:** [SogetiSkills.Core.Managers][3]  
**Assembly:**

Syntax
------

```csharp
public class ResumeManager : ManagerBase, 
	IResumeManager
```

The **ResumeManager** type exposes the following members.


Constructors
------------

                 | Name               | Description 
---------------- | ------------------ | ----------- 
![Public method] | [ResumeManager][4] |             


Methods
-------

                    | Name                                 | Description                                                                                                     
------------------- | ------------------------------------ | --------------------------------------------------------------------------------------------------------------- 
![Public method]    | [Dispose][5]                         | (Inherited from [ManagerBase][2].)                                                                              
![Protected method] | [GetOpenConnection][6]               | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                                     
![Protected method] | [GetOpenConnectionAsync][7]          | Gets an open connection to the database. (Inherited from [ManagerBase][2].)                                     
![Public method]    | [LoadResumeByUserId][8]              | Loads the actual resume (including file contents) for the user.                                                 
![Public method]    | [LoadResumeMetadataByUserIdAsync][9] | Load just the resume metadata for a consultant, if they have one.                                               
![Public method]    | [UploadResumeAsync][10]              | Upload a new resume for a consultant. If the consultant already had a resume then it is replaced with this one. 


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
[8]: LoadResumeByUserId.md
[9]: LoadResumeMetadataByUserIdAsync.md
[10]: UploadResumeAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Protected method]: ../../_icons/protmethod.gif "Protected method"