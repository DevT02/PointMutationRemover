# PointMutationRemover
This remover removes 2d point mutations. It's very basic so there is a slight possibility of it not working with some.

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
