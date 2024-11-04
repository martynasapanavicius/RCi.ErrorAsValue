namespace RCi.ErrorAsValue.Tests
{
    public static class Funcs
    {
        public static readonly Error Err = Error.NewNotSupported();

        public static class ValueType
        {
            public const int SomeValue = 42;
            public const int DefaultValue = default;

            public static class Explicit
            {
                public static Ve<int> ReturnValue()
                {
                    return new Ve<int>(SomeValue, null);
                }

                public static Ve<int> ReturnError()
                {
                    return new Ve<int>(DefaultValue, Err);
                }
            }

            public static class Implicit
            {
                public static Ve<int> ReturnValue()
                {
                    return SomeValue;
                }

                public static Ve<int> ReturnError()
                {
                    return Err;
                }
            }
        }

        public static class NullableValueType
        {
            public static readonly int? SomeValue = 1337;
            public static readonly int? DefaultValue = null;

            public static class Explicit
            {
                public static Ve<int?> ReturnValue()
                {
                    return new Ve<int?>(SomeValue, null);
                }

                public static Ve<int?> ReturnNull()
                {
                    return new Ve<int?>(null, null);
                }

                public static Ve<int?> ReturnError()
                {
                    return new Ve<int?>(DefaultValue, Err);
                }
            }

            public static class Implicit
            {
                public static Ve<int?> ReturnValue()
                {
                    return SomeValue;
                }

                public static Ve<int?> ReturnNull()
                {
                    return DefaultValue;
                }

                public static Ve<int?> ReturnError()
                {
                    return Err;
                }
            }
        }

        public static class ReferenceType
        {
            public static readonly string SomeValue = "hello";
            public static readonly string DefaultValue = null!;

            public static class Explicit
            {
                public static Ve<string> ReturnValue()
                {
                    return new Ve<string>(SomeValue, null);
                }

                public static Ve<string> ReturnError()
                {
                    return new Ve<string>(DefaultValue, Err);
                }
            }

            public static class Implicit
            {
                public static Ve<string> ReturnValue()
                {
                    return SomeValue;
                }

                public static Ve<string> ReturnError()
                {
                    return Err;
                }
            }
        }

        public static class NullableReferenceType
        {
            public static readonly string? SomeValue = "world";
            public static readonly string? DefaultValue = null;

            public static class Explicit
            {
                public static Ve<string?> ReturnValue()
                {
                    return new Ve<string?>(SomeValue, null);
                }

                public static Ve<string?> ReturnNull()
                {
                    return new Ve<string?>(null, null);
                }

                public static Ve<string?> ReturnError()
                {
                    return new Ve<string?>(DefaultValue, Err);
                }
            }

            public static class Implicit
            {
                public static Ve<string?> ReturnValue()
                {
                    return SomeValue;
                }

                public static Ve<string?> ReturnNull()
                {
                    return DefaultValue;
                }

                public static Ve<string?> ReturnError()
                {
                    return Err;
                }
            }
        }
    }
}
