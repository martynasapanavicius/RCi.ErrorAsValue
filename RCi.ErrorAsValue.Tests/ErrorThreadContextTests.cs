namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ErrorThreadContextTests
    {
        [Test]
        public static void ErrorThreadContextToString()
        {
            var threadContext = new ErrorThreadContext(42, ApartmentState.MTA, "name");
            var actual = threadContext.ToString();
            Assert.That(actual, Is.EqualTo("ManagedThreadId=42, ApartmentState=MTA, Name=name"));
        }
    }
}
