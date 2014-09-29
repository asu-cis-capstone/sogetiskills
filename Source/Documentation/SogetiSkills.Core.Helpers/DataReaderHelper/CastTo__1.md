DataReaderHelperCastToT Method
==============================
Casts a value returned from the database to T. DBNull.Value is returned as default(T).

**Namespace:** [SogetiSkills.Core.Helpers][1]  
**Assembly:**

Syntax
------

```csharp
public static T CastTo<T>(
	Object value
)

```

### Parameters

#### *value*
Type: [SystemObject][2]  
The value returned from the database that needs to be casted.

### Type Parameters

#### *T*
The type to cast to.

### Return Value
Type: **T**  
The value casted to T or default(T) if the value was null or DBNull.Value.

See Also
--------

### Reference
[DataReaderHelper Class][3]  
[SogetiSkills.Core.Helpers Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[3]: README.md