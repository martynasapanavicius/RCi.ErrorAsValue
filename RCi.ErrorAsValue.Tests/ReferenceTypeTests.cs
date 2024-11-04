namespace RCi.ErrorAsValue.Tests
{
    public static class ReferenceTypeTests
    {
        [Test]
        public static void Explicit_ReturnValue()
        {
            Ve<string> actual = Funcs.ReferenceType.Explicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ReferenceType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Explicit_ReturnError()
        {
            Ve<string> actual = Funcs.ReferenceType.Explicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ReferenceType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }

        [Test]
        public static void Implicit_ReturnValue()
        {
            Ve<string> actual = Funcs.ReferenceType.Implicit.ReturnValue();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ReferenceType.SomeValue));
            Assert.That(actual.Error, Is.Null);
        }

        [Test]
        public static void Implicit_ReturnError()
        {
            Ve<string> actual = Funcs.ReferenceType.Implicit.ReturnError();
            Assert.That(actual.Value, Is.EqualTo(Funcs.ReferenceType.DefaultValue));
            Assert.That(actual.Error, Is.Not.Null);
        }
    }
}
