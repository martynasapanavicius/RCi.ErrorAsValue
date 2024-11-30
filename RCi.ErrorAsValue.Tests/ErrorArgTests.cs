namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ErrorArgTests
    {
        [Test]
        public static void ImplicitOperatorTupleToErrorArg()
        {
            ErrorArg actual = ("name", "value");
            Assert.That(actual.Name, Is.EqualTo("name"));
            Assert.That(actual.Value, Is.EqualTo("value"));
        }

        [Test]
        public static void ImplicitOperatorErrorArgToTuple()
        {
            (string Name, object? Value) actual = new ErrorArg("name", "value");
            Assert.That(actual.Name, Is.EqualTo("name"));
            Assert.That(actual.Value, Is.EqualTo("value"));
        }
    }
}
