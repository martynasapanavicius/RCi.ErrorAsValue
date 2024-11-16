# RCi.ErrorAsValue
.NET Error as value package

## Usage:
Consider this example:
```
public sealed record Employee(string FirstName, string LastName, int Age);

private static readonly Dictionary<int, Employee> _employees = new()
{
    {0, new("Michael", "Smith", 35)},
    {1, new("Kenji", "Tanaka", 25)},
    {2, new("Andrew", "Martinez", 50)},
    {3, new("Emily", "Johnson", 28)},
    {4, new("Jamal", "Washington", 30)},
};

public static Ve<Employee> GetEmployeeById(int id)
{
    if (_employees.TryGetValue(id, out var employee))
    {
        return employee;
    }
    return Error.NewNotFound("cannot find employee by id", (nameof(id), id));
}
```
Try to get existing employee:
```
var (employee, err) = GetEmployeeById(3);
if (err)
{
    Console.WriteLine(err.ToString());
}
else
{
    Console.WriteLine(employee);
}
```
Output:
```
Employee { FirstName = Emily, LastName = Johnson, Age = 28 }
```
Try to get non-existing employee:
```
var (employee, err) = GetEmployeeById(420);
if (err)
{
    Console.WriteLine(err.ToString());
}
```
Output:
```
NotFound: cannot find employee by id
```
We can get more details on the error:
```
var (employee, err) = GetEmployeeById(420);
if (err)
{
    Console.WriteLine(JsonSerializer.Serialize(err.ToErrorDump(), new JsonSerializerOptions { WriteIndented = true }));
}
```
Output:
```
{
  "Kind": "NotFound",
  "Message": "cannot find employee by id",
  "ThreadContext": "ManagedThreadId=1, ApartmentState=MTA, Name=",
  "StackTrace": [
    "at System.Environment.get_StackTrace()",
    "at RCi.ErrorAsValue.Error.New(Error inner, String kind, String message, ValueTuple`2[] args)",
    "at RCi.ErrorAsValue.Error.NewNotFound(String message, ValueTuple`2[] args)",
    "at TestUsage.Program.GetEmployeeById(Int32 id) in D:\\code\\src\\RCi.ErrorAsValue\\TestUsage\\Program.cs:line 52",
    "at TestUsage.Program.Test2() in D:\\code\\src\\RCi.ErrorAsValue\\TestUsage\\Program.cs:line 79",
    "at TestUsage.Program.Main() in D:\\code\\src\\RCi.ErrorAsValue\\TestUsage\\Program.cs:line 12"
  ],
  "Args": [
    {
      "Name": "id",
      "Value": 420
    }
  ]
}
```
