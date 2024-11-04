namespace RCi.ErrorAsValue.Tests
{
    public static class NullableValueTypeDeconstructTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Explicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.NullableValueType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnNull()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Explicit.ReturnNull();
            Assert.That(val, Is.Null);
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Explicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.NullableValueType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Implicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.NullableValueType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnNull()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Implicit.ReturnNull();
            Assert.That(val, Is.Null);
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            (int? val, Error? err) = Funcs.NullableValueType.Implicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.NullableValueType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }
    }
}
