namespace RCi.ErrorAsValue.Tests
{
    [Parallelizable]
    public static class ErrorTests
    {
        private const string _testStackTrace = "root stacktrace";
        private static readonly ErrorThreadContext _testThreadContext = new(123, ApartmentState.MTA, "some thread name");
        private static readonly Error _testErr0 = new
        (
            "RootKind",
            "root message",
            _testThreadContext,
            _testStackTrace,
            [
                ("arg0", "value0"),
                ("arg1", "value1"),
            ]
        );
        private static readonly Error _testErr1 = _testErr0.Wrap();
        private static readonly Error _testErr2 = _testErr1.Wrap("WrappedKind", "wrapped message", ("arg2", "value2"));
        private static readonly Error _testErr3 = _testErr2.Wrap(("arg3", "value3"));
        private static readonly Error _testErr4 = _testErr3.Wrap("TopKind", "top message");
        private static readonly Error _testErr5 = _testErr4.Wrap("surface message");
        private static readonly Error[] _testErrors =
        [
            _testErr0,
            _testErr1,
            _testErr2,
            _testErr3,
            _testErr4,
            _testErr5,
        ];

        [TestCase(0, "RootKind")]
        [TestCase(1, "RootKind")]
        [TestCase(2, "WrappedKind")]
        [TestCase(3, "WrappedKind")]
        [TestCase(4, "TopKind")]
        [TestCase(5, "TopKind")]
        public static void Kind(int errorIndex, string expected)
        {
            Assert.That(_testErrors[errorIndex].Kind, Is.EqualTo(expected));
        }

        [TestCase(0, "root message")]
        [TestCase(1, "root message")]
        [TestCase(2, "wrapped message: root message")]
        [TestCase(3, "wrapped message: root message")]
        [TestCase(4, "top message: wrapped message: root message")]
        [TestCase(5, "surface message: top message: wrapped message: root message")]
        public static void Message(int errorIndex, string expected)
        {
            Assert.That(_testErrors[errorIndex].Message, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public static void ThreadContext(int errorIndex)
        {
            Assert.That(_testErrors[errorIndex].ThreadContext, Is.EqualTo(_testThreadContext));
        }

        [TestCase(0, _testStackTrace)]
        [TestCase(1, _testStackTrace)]
        [TestCase(2, _testStackTrace)]
        [TestCase(3, _testStackTrace)]
        [TestCase(4, _testStackTrace)]
        [TestCase(5, _testStackTrace)]
        public static void StackTrace(int errorIndex, string expected)
        {
            Assert.That(_testErrors[errorIndex].StackTrace, Is.EqualTo(expected));
        }

        [Test]
        public static void Args_0()
        {
            var expected = new ErrorArg[]
            {
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[0].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [Test]
        public static void Args_1()
        {
            var expected = new ErrorArg[]
            {
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[1].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [Test]
        public static void Args_2()
        {
            var expected = new ErrorArg[]
            {
                ("arg2", "value2"),
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[2].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [Test]
        public static void Args_3()
        {
            var expected = new ErrorArg[]
            {
                ("arg3", "value3"),
                ("arg2", "value2"),
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[3].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [Test]
        public static void Args_4()
        {
            var expected = new ErrorArg[]
            {
                ("arg3", "value3"),
                ("arg2", "value2"),
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[4].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [Test]
        public static void Args_5()
        {
            var expected = new ErrorArg[]
            {
                ("arg3", "value3"),
                ("arg2", "value2"),
                ("arg0", "value0"),
                ("arg1", "value1"),
            };
            var actual = _testErrors[5].Args.ToArray();
            Assert.That(actual.SequenceEqual(expected), Is.True);
        }

        [TestCase(0, "RootKind: root message")]
        [TestCase(1, "RootKind: root message")]
        [TestCase(2, "WrappedKind: wrapped message: root message")]
        [TestCase(3, "WrappedKind: wrapped message: root message")]
        [TestCase(4, "TopKind: top message: wrapped message: root message")]
        [TestCase(5, "TopKind: surface message: top message: wrapped message: root message")]
        public static void ToString(int errorIndex, string expected)
        {
            Assert.That(_testErrors[errorIndex].ToString(), Is.EqualTo(expected));
        }

        [Test]
        public static void ImplicitOperatorBool_False()
        {
            Error? err = null;
            bool actual = err;
            Assert.That(actual, Is.False);
        }

        [Test]
        public static void ImplicitOperatorBool_True()
        {
            var err = _testErr1;
            bool actual = err;
            Assert.That(actual, Is.True);
        }

        [Test]
        public static void ImplicitOperatorBool_NotNullWhenAttribute()
        {
            var err = GetError();
            if (err)
            {
                // this doesn't compile without [NotNullWhen(true)] attribute
                // compiler err: "Dereference of a possibly null reference."
                // since we do have this attribute, it should compile without errors
                var msg = err/*!*/.Message; // <--- no need for bang operator

                Assert.That(msg, Is.Not.Null);
            }

            return;

            // this always returns non-null error (although return type is nullable reference type)
            static Error? GetError() =>
                DateTime.UtcNow.Ticks == 0
                    ? null
                    : Error.NewInternal("some error");
        }
    }
}
