namespace RCi.ErrorAsValue.Tests
{
    public static class ReferenceTypeDeconstructTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            (string val, Error? err) = Funcs.ReferenceType.Explicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.ReferenceType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            (string val, Error? err) = Funcs.ReferenceType.Explicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.ReferenceType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            (string val, Error? err) = Funcs.ReferenceType.Implicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.ReferenceType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            (string val, Error? err) = Funcs.ReferenceType.Implicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.ReferenceType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }
    }
}
