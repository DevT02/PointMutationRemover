# PointMutationRemover
This remover removes 2d point mutations. It's very basic so there is a slight possibility of it not working with some. Just a warning, BIN/DEBUG is NOT UPDATED by me anymore! Head on over to releases if you really want the new stuff m8

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

# WARNINGS
You might have to re-drag the file into the fixer until it becomes 0. I'm still working on a fix for this.
