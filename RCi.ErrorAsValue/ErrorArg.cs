namespace RCi.ErrorAsValue
{
    public readonly record struct ErrorArg(string Name, object? Value)
    {
        public static implicit operator ErrorArg((string Name, object? Value) tuple) =>
            new(tuple.Name, tuple.Value);

        public static implicit operator (string Name, object? Value)(ErrorArg errArg) =>
            (errArg.Name, errArg.Value);
    }
}
