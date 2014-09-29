DataReaderHelper Class
======================


Inheritance Hierarchy
---------------------
[SystemObject][1]  
  **SogetiSkills.Core.HelpersDataReaderHelper**  

**Namespace:** [SogetiSkills.Core.Helpers][2]  
**Assembly:**

Syntax
------

```csharp
publicstaticclassDataReaderHelper
```

The **DataReaderHelper** type exposes the following members.


Methods
-------

                                 | Name         | Description                                                                                  
-------------------------------- | ------------ | -------------------------------------------------------------------------------------------- 
![Public method]![Static member] | [CastToT][3] | Casts a value returned from the database to T. DBNull.Value is returned as default(T).       
![Public method]![Static member] | [FieldT][4]  | Reads a value from a SqlDataReader and cast it to T. DBNull.Value is returned as default(T). 


See Also
--------

### Reference
[SogetiSkills.Core.Helpers Namespace][2]  

[1]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[2]: ../README.md
[3]: CastTo__1.md
[4]: Field__1.md
[Public method]: ../../_icons/pubmethod.gif "Public method"
[Static member]: ../../_icons/static.gif "Static member"