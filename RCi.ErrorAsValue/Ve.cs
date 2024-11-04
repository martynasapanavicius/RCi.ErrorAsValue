namespace RCi.ErrorAsValue
{
    public readonly record struct Ve<T>(T Value, Error? Error)
    {
        public static implicit operator bool(Ve<T> ve) => ve.Error is null;

        public static implicit operator Ve<T>((T Value, Error? Error) tuple) => new(tuple.Value, tuple.Error);

        public static implicit operator Ve<T>(T value) => new(value, default);

        public static implicit operator Ve<T>(Error err) => new(default!, err);
    }
}
