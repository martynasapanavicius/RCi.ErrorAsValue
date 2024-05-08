using System.Collections.Immutable;

namespace RCi.ErrorAsValue
{
    public sealed record ErrorDump
    (
        string Kind,
        string Message,
        string ThreadContext,
        ImmutableArray<string> StackTrace,
        ImmutableArray<ErrorArg> Args
    );
}
