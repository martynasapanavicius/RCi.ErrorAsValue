using System.Text.Json;

namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ReadmeTests
    {
        public sealed record Employee(string FirstName, string LastName, int Age);

        private static readonly Dictionary<int, Employee> _employees = new()
        {
            { 0, new("Michael", "Smith", 35) },
            { 1, new("Kenji", "Tanaka", 25) },
            { 2, new("Andrew", "Martinez", 50) },
            { 3, new("Emily", "Johnson", 28) },
            { 4, new("Jamal", "Washington", 30) },
        };

        private static Ve<Employee> GetEmployeeById(int id)
        {
            if (_employees.TryGetValue(id, out var employee))
            {
                return employee;
            }
            return Error.NewNotFound("cannot find employee by id", (nameof(id), id));
        }

        [Test]
        public static void GetExistingEmployee()
        {
            var (employee, err) = GetEmployeeById(3);
            if (err)
            {
                Console.WriteLine(err.ToString());
            }
            else
            {
                Console.WriteLine(employee);
            }

            Assert.That(err, Is.Null);
            Assert.That(employee, Is.EqualTo(_employees[3]));
        }

        [Test]
        public static void GetNonExistingEmployee()
        {
            var (employee, err) = GetEmployeeById(420);
            if (err)
            {
                Console.WriteLine(err.ToString());
            }

            Assert.That(err, Is.Not.Null);
            Assert.That(err.ToString(), Is.EqualTo("NotFound: cannot find employee by id"));
            Assert.That(employee, Is.Null);
        }

        [Test]
        public static void ErrorDumpJson()
        {
            var (employee, err) = GetEmployeeById(420);
            if (err)
            {
                var errDump = err.ToErrorDump();
                var json = JsonSerializer.Serialize(
                    errDump,
                    new JsonSerializerOptions { WriteIndented = true }
                );
                Console.WriteLine(json);
            }

            Assert.That(err, Is.Not.Null);
            var errDump2 = err.ToErrorDump();
            var json2 = JsonSerializer.Serialize(
                errDump2,
                new JsonSerializerOptions { WriteIndented = true }
            );
            Assert.That(json2, Is.Not.Null);
            Assert.That(employee, Is.Null);
        }
    }
}
