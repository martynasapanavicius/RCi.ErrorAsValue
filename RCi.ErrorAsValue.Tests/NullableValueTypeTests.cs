namespace RCi.ErrorAsValue.Tests
{
    public static class NullableValueTypeTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            Ve<int?> actual = Funcs.NullableValueType.Explicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableValueType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnNull()
        {
            Ve<int?> actual = Funcs.NullableValueType.Explicit.ReturnNull();
            Assert.That(actual.Value, Is.Null);
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            Ve<int?> actual = Funcs.NullableValueType.Explicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableValueType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            Ve<int?> actual = Funcs.NullableValueType.Implicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableValueType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnNull()
        {
            Ve<int?> actual = Funcs.NullableValueType.Implicit.ReturnNull();
            Assert.That(actual.Value, Is.Null);
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            Ve<int?> actual = Funcs.NullableValueType.Implicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableValueType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }
    }
}
