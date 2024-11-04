namespace RCi.ErrorAsValue.Tests
{
    public static class NullableReferenceTypeDeconstructTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Explicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.NullableReferenceType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnNull()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Explicit.ReturnNull();
            Assert.That(val, Is.Null);
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Explicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.NullableReferenceType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Implicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.NullableReferenceType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnNull()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Implicit.ReturnNull();
            Assert.That(val, Is.Null);
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            (string? val, Error? err) = Funcs.NullableReferenceType.Implicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.NullableReferenceType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }
    }
}
