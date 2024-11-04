namespace RCi.ErrorAsValue.Tests
{
    public static class ValueTypeDeconstructTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            (int val, Error? err) = Funcs.ValueType.Explicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.ValueType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            (int val, Error? err) = Funcs.ValueType.Explicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.ValueType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            (int val, Error? err) = Funcs.ValueType.Implicit.ReturnValue();
            Assert.That(val, Is.EqualTo(Funcs.ValueType.SomeValue));
            Assert.That(err, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            (int val, Error? err) = Funcs.ValueType.Implicit.ReturnError();
            Assert.That(val, Is.EqualTo(Funcs.ValueType.DefaultValue));
            Assert.That(err, Is.Not.Null);
        }
    }
}
