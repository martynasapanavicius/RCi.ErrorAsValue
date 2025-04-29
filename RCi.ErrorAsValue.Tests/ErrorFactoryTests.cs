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
                e.Data.Add("innerArg", "innerValue");
                exception = e;
                actual = e.ToError(("wrappedArg", "wrappedValue"));
            }

            Assert.That(exception, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);

            var expectedThreadContext = ErrorThreadContext.GetCurrent();

            Assert.That(actual.Kind, Is.EqualTo(ErrorKind.Exception));
            Assert.That(actual.Message, Is.EqualTo("(ApplicationException) some exception"));
            Assert.That(actual.StackTrace, Is.EqualTo(exception.StackTrace));
            Assert.That(actual.ThreadContext, Is.EqualTo(expectedThreadContext));
            Assert.That(
                actual.ToString(),
                Is.EqualTo("Exception: (ApplicationException) some exception")
            );

            var actualArgs = actual.Args.ToArray();
            var expectedArgs = new ErrorArg[]
            {
                ("wrappedArg", "wrappedValue"),
                ("innerArg", "innerValue"),
            };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }

        [Test]
        public static void NewExceptionNested()
        {
            Exception? exceptionInner = null;
            Exception? exceptionOuter = null;
            Error? actual = null;
            try
            {
                var task = Task.Run(() =>
                {
                    // make condition to always throw
                    if (DateTime.UtcNow.Ticks != 0)
                    {
                        exceptionInner = new ApplicationException("some exception");
                        exceptionInner.Data.Add("innerArg", "innerValue");
                        throw exceptionInner;
                    }
                    // we don't want to reach this
                    return 1;
                });
                //task.GetAwaiter().GetResult();    // this forwards the exception
                _ = task.Result; // this wraps it in AggregateException
            }
            catch (Exception e)
            {
                e.Data.Add("outerArg", "outerValue");
                exceptionOuter = e;
                actual = e.ToError(("wrappedArg", "wrappedValue"));
            }

            Assert.That(exceptionInner, Is.Not.Null);
            Assert.That(exceptionOuter, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);

            var expectedThreadContext = ErrorThreadContext.GetCurrent();

            Assert.That(actual.Kind, Is.EqualTo(ErrorKind.Exception));
            Assert.That(
                actual.Message,
                Is.EqualTo(
                    "(AggregateException) One or more errors occurred. (some exception): (ApplicationException) some exception"
                )
            );
            Assert.That(actual.StackTrace, Is.EqualTo(exceptionInner.StackTrace));
            Assert.That(actual.ThreadContext, Is.EqualTo(expectedThreadContext));
            Assert.That(
                actual.ToString(),
                Is.EqualTo(
                    "Exception: (AggregateException) One or more errors occurred. (some exception): (ApplicationException) some exception"
                )
            );

            var actualArgs = actual.Args.ToArray();
            var expectedArgs = new ErrorArg[]
            {
                ("wrappedArg", "wrappedValue"),
                ("outerArg", "outerValue"),
                ("innerArg", "innerValue"),
            };
            Assert.That(actualArgs.SequenceEqual(expectedArgs));
        }
    }
}
