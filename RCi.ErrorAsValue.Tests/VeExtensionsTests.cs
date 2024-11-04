namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class VeExtensionsTests
    {
        [Test]
        public static void Ok_False()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actualSuccess = ve.Ok(out var actualValue, out var actualErr);
            Assert.That(actualSuccess, Is.False);
            Assert.That(actualValue, Is.EqualTo(ve.Value));
            Assert.That(actualErr, Is.EqualTo(ve.Error));
        }

        [Test]
        public static void Ok_True()
        {
            var ve = 42.ToVe();
            var actualSuccess = ve.Ok(out var actualValue, out var actualErr);
            Assert.That(actualSuccess, Is.True);
            Assert.That(actualValue, Is.EqualTo(ve.Value));
            Assert.That(actualErr, Is.EqualTo(ve.Error));
        }

        [Test]
        public static void Failed_False()
        {
            var ve = 42.ToVe();
            var actualSuccess = ve.Failed(out var actualValue, out var actualErr);
            Assert.That(actualSuccess, Is.False);
            Assert.That(actualValue, Is.EqualTo(ve.Value));
            Assert.That(actualErr, Is.EqualTo(ve.Error));
        }

        [Test]
        public static void Failed_True()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actualSuccess = ve.Failed(out var actualValue, out var actualErr);
            Assert.That(actualSuccess, Is.True);
            Assert.That(actualValue, Is.EqualTo(ve.Value));
            Assert.That(actualErr, Is.EqualTo(ve.Error));
        }
    }
}
