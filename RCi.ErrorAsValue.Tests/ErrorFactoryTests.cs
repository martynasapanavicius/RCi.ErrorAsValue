namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ErrorFactoryTests
    {
        [Test]
        public static void New()
        {
            var actual = Error.New("kind", "message", ("arg0", "value0"), ("arg1", "value1"));
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Kind, Is.EqualTo("kind"));
            Assert.That(actual.Message, Is.EqualTo("message"));
            var actualArgs = actual.Args.ToArray();
            var expectedArgs = new ErrorArg[] { ("arg0", "value0"), ("arg1", "value1") };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
            var expectedThreadContext = ErrorThreadContext.GetCurrent();
            Assert.That(actual.ThreadContext, Is.EqualTo(expectedThreadContext));
            Assert.That(actual.ToString(), Is.EqualTo("kind: message"));
        }

        [Test]
        public static void NewException()
        {
            Exception exception;
            Error actual;
            try
            {
                throw new ApplicationException("some exception");
            }
            catch (Exception e)
            {
                exception = e;
                e.Data.Add("arg0", "value0");
                actual = e.ToError(("arg1", "value1"));
            }

            Assert.That(actual.Kind, Is.EqualTo(ErrorKind.Exception));
            Assert.That(actual.Message, Is.EqualTo("ApplicationException: some exception"));
            Assert.That(actual.StackTrace, Is.EqualTo(exception.StackTrace));
            var expectedThreadContext = ErrorThreadContext.GetCurrent();
            Assert.That(actual.ThreadContext, Is.EqualTo(expectedThreadContext));
            Assert.That(actual.ToString(), Is.EqualTo("Exception: ApplicationException: some exception"));

            var actualArgs = actual.Args.ToArray();
            var expectedArgs = new ErrorArg[]
            {
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }
    }
}
