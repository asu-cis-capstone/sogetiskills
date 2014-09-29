IResumeManagerUploadResumeAsync Method
======================================
Upload a new resume for a consultant. If the consultant already had a resume then it is replaced with this one.

**Namespace:** [SogetiSkills.Core.Managers][1]  
**Assembly:**

Syntax
------

```csharp
Task UploadResumeAsync(
	int userId,
	string fileName,
	string mimeType,
	byte[] fileData
)
```

### Parameters

#### *userId*
Type: [SystemInt32][2]  
The id of the consultant that owns the resume.

#### *fileName*
Type: [SystemString][3]  
The name of the resume file.

#### *mimeType*
Type: [SystemString][3]  
The mime type of the resume file.

#### *fileData*
Type: [SystemByte][4]  
The actual binary contents of the resume file.

### Return Value
Type: [Task][5]

See Also
--------

### Reference
[IResumeManager Interface][6]  
[SogetiSkills.Core.Managers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/td2s409d
[3]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[4]: http://msdn.microsoft.com/en-us/library/yyb1w04y
[5]: http://msdn.microsoft.com/en-us/library/dd235678
[6]: README.md