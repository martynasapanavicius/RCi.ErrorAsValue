namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class VeTests
    {
        [Test]
        public static void ImplicitOperatorVeToBool_False()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            bool actual = ve;
            Assert.That(actual, Is.False);
        }

        [Test]
        public static void ImplicitOperatorVeToBool_True()
        {
            var ve = 42.ToVe();
            bool actual = ve;
            Assert.That(actual, Is.True);
        }

        [Test]
        public static void ImplicitOperatorTupleToVe()
        {
            var tuple = (value: 42, err: Error.NewInternal("test"));
            Ve<int> actual = tuple;
            Assert.That(actual, Is.EqualTo(new Ve<int>(tuple.value, tuple.err)));
        }

        [Test]
        public static void ImplicitOperatorValueToVe()
        {
            const int value = 42;
            Ve<int> actual = value;
            Assert.That(actual, Is.EqualTo(new Ve<int>(value, null)));
        }

        [Test]
        public static void ImplicitOperatorErrorToVe()
        {
            var err = Error.NewInternal("test");
            Ve<int> actual = err;
            Assert.That(actual, Is.EqualTo(new Ve<int>(0, err)));
        }
    }
}
