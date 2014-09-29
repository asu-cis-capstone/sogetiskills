DataReaderHelperFieldT Method
=============================
Reads a value from a SqlDataReader and cast it to T. DBNull.Value is returned as default(T).

**Namespace:** [SogetiSkills.Core.Helpers][1]  
**Assembly:**

Syntax
------

```csharp
public static T Field<T>(
	this SqlDataReader reader,
	string columnName
)

```

### Parameters

#### *reader*
Type: [System.Data.SqlClientSqlDataReader][2]  
The data reader to read from.

#### *columnName*
Type: [SystemString][3]  
The name of the column to read from.

### Type Parameters

#### *T*
The type to cast to.

### Return Value
Type: **T**  
The value casted to T or default(T) if the value was null or DBNull.Value. 
### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type [SqlDataReader][2]. When you use instance method syntax to call this method, omit the first parameter. For more information, see [Extension Methods (Visual Basic)][4] or [Extension Methods (C# Programming Guide)][5].

See Also
--------

### Reference
[DataReaderHelper Class][6]  
[SogetiSkills.Core.Helpers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/w9y9ttex
[3]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[4]: http://msdn.microsoft.com/en-us/library/bb384936.aspx
[5]: http://msdn.microsoft.com/en-us/library/bb383977.aspx
[6]: README.md