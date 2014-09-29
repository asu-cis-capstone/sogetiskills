IResumeManager Interface
========================
Provides data access for consultants' resumes.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
publicinterfaceIResumeManager
```

The **IResumeManager** type exposes the following members.


Methods
-------

                 | Name                                 | Description                                                                                                     
---------------- | ------------------------------------ | --------------------------------------------------------------------------------------------------------------- 
![Public method] | [LoadResumeByUserId][2]              | Loads the actual resume (including file contents) for the user.                                                 
![Public method] | [LoadResumeMetadataByUserIdAsync][3] | Load just the resume metadata for a consultant, if they have one.                                               
![Public method] | [UploadResumeAsync][4]               | Upload a new resume for a consultant. If the consultant already had a resume then it is replaced with this one. 


See Also
--------

### Reference
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: LoadResumeByUserId.md
[3]: LoadResumeMetadataByUserIdAsync.md
[4]: UploadResumeAsync.md
[Public method]: ../../_icons/pubmethod.gif "Public method"