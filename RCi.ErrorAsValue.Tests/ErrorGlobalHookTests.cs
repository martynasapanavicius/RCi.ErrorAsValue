namespace RCi.ErrorAsValue.Tests
{
    [NonParallelizable]
    public static class ErrorGlobalHookTests
    {
        [Test]
        public static void Integration()
        {
            var collected = new List<Error>();

            Error.NewNotSupported();
            Assert.That(collected, Is.Empty);

            ErrorGlobalHook.OnError += ErrorGlobalHookOnOnError;

            Error.NewNotSupported("err 1");
            Assert.That(collected.Count, Is.EqualTo(1));
            Assert.That(collected[^1].Message, Is.EqualTo("err 1"));

            var exception = new NotSupportedException("ex 2");
            Error.NewException(exception);
            Assert.That(collected.Count, Is.EqualTo(2));
            Assert.That(collected[^1].Message, Is.EqualTo("(NotSupportedException) ex 2"));

            ErrorGlobalHook.OnError -= ErrorGlobalHookOnOnError;

            Error.NewNotSupported("err 3");
            Assert.That(collected.Count, Is.EqualTo(2));
            Assert.That(collected[^1].Message, Is.EqualTo("(NotSupportedException) ex 2"));

            return;

            void ErrorGlobalHookOnOnError(object? sender, Error e)
            {
                collected.Add(e);
            }
        }
    }
}
