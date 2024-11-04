namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class VeWrapIfErrorExtensionsTests
    {
        [Test]
        public static void WrapIfError_GetMessageDelegate_Value()
        {
            var ve = 42.ToVe();
            var actual = ve.WrapIfError(() => "wrap message");
            Assert.That(actual, Is.EqualTo(ve));
        }

        [Test]
        public static void WrapIfError_GetMessageDelegate_Error()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actual = ve.WrapIfError(() => "wrap message");
            Assert.That(actual.Error!.Message, Is.EqualTo("wrap message: test"));
        }

        [Test]
        public static void WrapIfError_GetArgsDelegate_Value()
        {
            var ve = 42.ToVe();
            var actual = ve.WrapIfError(() => [("arg0", "value0")]);
            Assert.That(actual, Is.EqualTo(ve));
        }

        [Test]
        public static void WrapIfError_GetArgsDelegate_Error()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actual = ve.WrapIfError(() => [("arg0", "value0")]);
            var actualArgs = actual.Error!.Args.ToArray();
            var expectedArgs = new ErrorArg[] { ("arg0", "value0") };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }

        [Test]
        public static void WrapIfError_GetMessageArgsDelegate_Value()
        {
            var ve = 42.ToVe();
            var actual = ve.WrapIfError(() => ("wrap message", [("arg0", "value0")]));
            Assert.That(actual, Is.EqualTo(ve));
        }

        [Test]
        public static void WrapIfError_GetMessageArgsDelegate_Error()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actual = ve.WrapIfError(() => ("wrap message", [("arg0", "value0")]));
            Assert.That(actual.Error!.Message, Is.EqualTo("wrap message: test"));
            var actualArgs = actual.Error!.Args.ToArray();
            var expectedArgs = new ErrorArg[] { ("arg0", "value0") };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }

        [Test]
        public static void WrapIfError_GetKindMessageArgsDelegate_Value()
        {
            var ve = 42.ToVe();
            var actual = ve.WrapIfError(() => ("kind", "wrap message", [("arg0", "value0")]));
            Assert.That(actual, Is.EqualTo(ve));
        }

        [Test]
        public static void WrapIfError_GetKindMessageArgsDelegate_Error()
        {
            var ve = Error.NewInternal("test").ToVe<int>();
            var actual = ve.WrapIfError(() => ("kind", "wrap message", [("arg0", "value0")]));
            Assert.That(actual.Error!.Kind, Is.EqualTo("kind"));
            Assert.That(actual.Error!.Message, Is.EqualTo("wrap message: test"));
            var actualArgs = actual.Error!.Args.ToArray();
            var expectedArgs = new ErrorArg[] { ("arg0", "value0") };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }
    }
}
