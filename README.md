# PointMutationRemover
This remover removes 2d point mutations. It's very basic so there is a slight possibility of it not working with some. Just a warning, BIN/DEBUG is NOT UPDATED by me anymore! Head on over to releases.

# Example of what it does
**Original Code:**
```csharp
int test = new Point(123, 312).X
Console.WriteLine(test);
```

**Final:**
```csharp
int test = 123;
Console.WriteLine(test);
```

# How to use
Drag and drop exe file protected with point mutations.
Use de4dot to clean the file, then re-use the point remover.

# Help?
You could always open an issue and i will happily look into it. Or you could DM me (check my github homepage)
