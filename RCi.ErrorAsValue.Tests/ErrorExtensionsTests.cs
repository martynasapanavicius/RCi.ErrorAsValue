using System.Collections;
using ErrorArgTuple = (string Name, object? Value);

namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ErrorExtensionsTests
    {
        [Test]
        public static void UnsafeAsImmutableArray()
        {
            var array = new[] { 1, 2, 3 };

            var immutableArray = array.UnsafeAsImmutableArray();
            Assert.That(immutableArray.SequenceEqual([1, 2, 3]));

            // modify original array, which changes immutable array (because it points to the original array)
            array[1] = 42;
            Assert.That(immutableArray.SequenceEqual([1, 42, 3]));
        }

        [Test]
        public static void ToErrorArg()
        {
            var expected = new ErrorArg("name", "value");
            var actual = ("name", "value").ToErrorArg();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void ToErrorArgArray_Empty()
        {
            var expected = Array.Empty<ErrorArg>();
            var actual = Array.Empty<ErrorArgTuple>().ToErrorArg();
            Assert.That(actual.SequenceEqual(expected));
        }

        [Test]
        public static void ToErrorArgArray()
        {
            var expected = new ErrorArg[] { ("arg0", "value0"), ("arg1", "value1") };
            var actual = new ErrorArgTuple[]
            {
                ("arg0", "value0"),
                ("arg1", "value1"),
            }.ToErrorArg();
            Assert.That(actual.SequenceEqual(expected));
        }

        [Test]
        public static void ToException()
        {
            var actual = Error
                .NewNotImplemented(
                    "message",
                    ("arg0", "value0"),
                    ("arg1", "value1"),
                    ("arg1", "value2"),
                    ("arg1", "value3")
                )
                .ToException();

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.GetType(), Is.EqualTo(typeof(ErrorException)));
            Assert.That(actual.Message, Is.EqualTo("message"));
            Assert.That(actual.StackTrace, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo("NotImplemented: message"));

            var actualData = new Dictionary<object, object?>();
            foreach (DictionaryEntry entry in actual.Data)
            {
                actualData.Add(entry.Key, entry.Value);
            }
            var expectedData = new Dictionary<object, object?>()
            {
                { "arg0", "value0" },
                { "arg1", "value1" },
                { "arg1_1", "value2" },
                { "arg1_2", "value3" },
            };
            Assert.That(actualData, Is.EquivalentTo(expectedData));
        }
    }
}
