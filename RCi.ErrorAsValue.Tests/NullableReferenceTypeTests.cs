namespace RCi.ErrorAsValue.Tests
{
    public static class NullableReferenceTypeTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Explicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableReferenceType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnNull()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Explicit.ReturnNull();
            Assert.That(actual.Value, Is.Null);
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Explicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableReferenceType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Implicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableReferenceType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnNull()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Implicit.ReturnNull();
            Assert.That(actual.Value, Is.Null);
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            Ve<string?> actual = Funcs.NullableReferenceType.Implicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.NullableReferenceType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }
    }
}
