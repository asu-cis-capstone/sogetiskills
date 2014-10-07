ResumeMetadata Class
====================
Metadata for a resume uploaded by a consultant.


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.ModelsResumeMetadata**  

**Namespace:** [SogetiSkills.Core.Models][2]  
**Assembly:**

Syntax
------

```csharp
publicclassResumeMetadata
```

The **ResumeMetadata** type exposes the following members.


Constructors
------------

                 | Name                | Description 
---------------- | ------------------- | ----------- 
![Public method] | [ResumeMetadata][3] |             


Properties
----------

                   | Name          | Description                                  
------------------ | ------------- | -------------------------------------------- 
![Public property] | [FileName][4] | The name of the file that was uploaded.      
![Public property] | [MimeType][5] | The mime type of the file that was uploaded. 


Remarks
-------
 Often we just want to know the file name and mime type of a consultant's resume so that w can display a link. By only returning these two fields we can display the link without having to pull the full binary contents of the resume back from the database. 

See Also
--------

### Reference
[SogetiSkills.Core.Models Namespace][2]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../README.md
[3]: _ctor.md
[4]: FileName.md
[5]: MimeType.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Public property]: ../../_icons/pubproperty.gif "Public property"