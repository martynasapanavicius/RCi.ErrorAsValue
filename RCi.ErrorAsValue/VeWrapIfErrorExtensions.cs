using ErrorArgTuple = (string Name, object Value);

namespace RCi.ErrorAsValue
{
    public delegate string GetMessageDelegate();

    public delegate ErrorArgTuple[] GetArgsDelegate();

    public delegate (string Message, ErrorArgTuple[] args) GetMessageArgsDelegate();

    public delegate (string Kind, string Message, ErrorArgTuple[] args) GetKindMessageArgsDelegate();

    public static class VeWrapIfErrorExtensions
    {
        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetMessageDelegate getMessage) =>
            ve.HasError ? ve with { Error = ve.Error.Wrap(getMessage()) } : ve;

        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetArgsDelegate getArgs) =>
            ve.HasError ? ve with { Error = ve.Error.Wrap(getArgs()) } : ve;

        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetMessageArgsDelegate getMessageArgs)
        {
            if (!ve.HasError)
            {
                return ve;
            }
            var (message, args) = getMessageArgs();
            return ve with
            {
                Error = ve.Error.Wrap(message, args)
            };
        }

        public static Ve<T> WrapIfError<T>(this Ve<T> ve, GetKindMessageArgsDelegate getKindMessageArgs)
        {
            if (!ve.HasError)
            {
                return ve;
            }
            var (kind, message, args) = getKindMessageArgs();
            return ve with
            {
                Error = ve.Error.Wrap(kind, message, args)
            };
        }
    }
}
