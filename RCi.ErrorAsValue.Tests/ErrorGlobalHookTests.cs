namespace RCi.ErrorAsValue.Tests
{
    [NonParallelizable]
    public static class ErrorGlobalHookTests
    {
        [Test]
        public static void Integration()
        {
            var collected = new List<Error>();

            // unhooked
            Error.NewNotSupported();
            Assert.That(collected, Is.Empty);

            ErrorGlobalHook.OnError += ErrorGlobalHookOnOnError;

            // new error, should fie event
            var err = Error.NewNotSupported("err 1");
            Assert.That(collected.Count, Is.EqualTo(1));
            Assert.That(collected[^1].Message, Is.EqualTo("err 1"));

            // wrapping does not invoke event
            err.Wrap("wrap 1");
            Assert.That(collected.Count, Is.EqualTo(1));
            Assert.That(collected[^1].Message, Is.EqualTo("err 1"));

            // only 1 event after nested exceptions are unwrapped into one nested error
            var exception = new NotSupportedException("ex 2", new Exception("inner ex 2"));
            Error.NewException(exception);
            Assert.That(collected.Count, Is.EqualTo(2));
            Assert.That(
                collected[^1].Message,
                Is.EqualTo("(NotSupportedException) ex 2: (Exception) inner ex 2")
            );

            ErrorGlobalHook.OnError -= ErrorGlobalHookOnOnError;

            // unhooked
            Error.NewNotSupported("err 3");
            Assert.That(collected.Count, Is.EqualTo(2));
            Assert.That(
                collected[^1].Message,
                Is.EqualTo("(NotSupportedException) ex 2: (Exception) inner ex 2")
            );

            return;

            void ErrorGlobalHookOnOnError(object? sender, Error e) => collected.Add(e);
        }
    }
}
