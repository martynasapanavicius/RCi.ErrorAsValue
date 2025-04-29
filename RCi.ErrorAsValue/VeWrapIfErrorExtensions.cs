using ErrorArgTuple = (string Name, object? Value);

namespace RCi.ErrorAsValue
{
    public delegate string GetMessageDelegate();

    public delegate ErrorArgTuple[] GetArgsDelegate();

    public delegate (string Message, ErrorArgTuple[] args) GetMessageArgsDelegate();

    public delegate (
        string Kind,
        string Message,
        ErrorArgTuple[] args
    ) GetKindMessageArgsDelegate();

    public static class VeWrapIfErrorExtensions
    {
        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetMessageDelegate getMessage) =>
            ve.Error is null ? ve : ve with { Error = ve.Error.Wrap(getMessage()) };

        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetArgsDelegate getArgs) =>
            ve.Error is null ? ve : ve with { Error = ve.Error.Wrap(getArgs()) };

        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetMessageArgsDelegate getMessageArgs)
        {
            if (ve.Error is null)
            {
                return ve;
            }
            var (message, args) = getMessageArgs();
            return ve with { Error = ve.Error.Wrap(message, args) };
        }

        public static Ve<T> WrapIfError<T>(
            this Ve<T> ve,
            GetKindMessageArgsDelegate getKindMessageArgs
        )
        {
            if (ve.Error is null)
            {
                return ve;
            }
            var (kind, message, args) = getKindMessageArgs();
            return ve with { Error = ve.Error.Wrap(kind, message, args) };
        }
    }
}
