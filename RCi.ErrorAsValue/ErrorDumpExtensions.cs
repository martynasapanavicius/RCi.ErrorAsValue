using System;
using System.Linq;

namespace RCi.ErrorAsValue
{
    public static class ErrorDumpExtensions
    {
        public static ErrorDump ToErrorDump(this Error err) =>
            new(
                err.Kind,
                err.Message,
                err.ThreadContext.ToString(),
                [
                    .. err
                        .StackTrace.Split(
                            [Environment.NewLine, "\r", "\n"],
                            StringSplitOptions.RemoveEmptyEntries
                        )
                        .Select(s => s.TrimStart(' '))
                        .Where(s => !string.IsNullOrWhiteSpace(s)),
                ],
                [.. err.Args]
            );
    }
}
