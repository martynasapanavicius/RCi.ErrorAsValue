namespace RCi.ErrorAsValue.Tests
{
    public static class ValueTypeTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            Ve<int> actual = Funcs.ValueType.Explicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ValueType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            Ve<int> actual = Funcs.ValueType.Explicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ValueType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            Ve<int> actual = Funcs.ValueType.Implicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ValueType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            Ve<int> actual = Funcs.ValueType.Implicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ValueType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }
    }
}
