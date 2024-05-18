using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using ErrorArgTuple = (string Name, object Value);

namespace RCi.ErrorAsValue
{
    public class Error
    (
        Error inner,
        string kind,
        string message,
        ErrorThreadContext threadContext,
        string stackTrace,
        ImmutableArray<ErrorArg> args
    )
    {
        private readonly Error _inner = inner;
        private readonly string _kind = kind ?? ErrorKind.Undefined;
        private readonly string _message = message; // can be null if wrapping only with arguments
        private readonly ImmutableArray<ErrorArg> _args = args.IsDefaultOrEmpty ? [] : args;
        private readonly ErrorThreadContext _threadContext = threadContext ?? ErrorThreadContext.GetCurrent();
        private readonly string _stackTrace = stackTrace ?? Environment.StackTrace;

        public virtual string Kind => _kind;

        public virtual string Message
        {
            get
            {
                if (_inner is null)
                {
                    return _message ?? string.Empty;
                }

                var sb = new StringBuilder();
                var node = this;
                while (node is not null)
                {
                    if (node._message is not null)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(':');
                            sb.Append(' ');
                        }
                        sb.Append(node._message);
                    }
                    node = node._inner;
                }
                return sb.ToString();
            }
        }

        public virtual IEnumerable<ErrorArg> Args
        {
            get
            {
                var node = this;
                while (node is not null)
                {
                    if (!node._args.IsEmpty)
                    {
                        foreach (var arg in node._args)
                        {
                            yield return arg;
                        }
                    }
                    node = node._inner;
                }
            }
        }

        public virtual ErrorThreadContext ThreadContext
        {
            get
            {
                var node = this;
                while (node._inner is not null)
                {
                    node = node._inner;
                }
                return node._threadContext;
            }
        }

        public virtual string StackTrace
        {
            get
            {
                var node = this;
                while (node._inner is not null)
                {
                    node = node._inner;
                }
                return node._stackTrace;
            }
        }

        public override string ToString() => $"{Kind}: {Message}";

        public static implicit operator bool(Error err) => err is not null;

        // factory

        public static Error New(Error inner, string kind, string message, params ErrorArgTuple[] args) =>
            new(inner, kind, message, ErrorThreadContext.GetCurrent(), Environment.StackTrace,
                args.ToErrorArg().CastToImmutableArray());

        public static Error NewException(Exception exception, params ErrorArgTuple[] args) =>
            new(default, ErrorKind.Exception, exception.GetType().Name + ": " + exception.Message,
                ErrorThreadContext.GetCurrent(), exception.StackTrace, args.ToErrorArg().CastToImmutableArray());

        public static Error NewNotImplemented(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.NotImplemented, message, args);

        public static Error NewNotImplemented(params ErrorArgTuple[] args) =>
            NewNotImplemented("not implemented", args);

        public static Error NewNotSupported(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.NotSupported, message, args);

        public static Error NewNotSupported(params ErrorArgTuple[] args) =>
            NewNotSupported("not supported", args);

        public static Error NewObsolete(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Obsolete, message, args);

        public static Error NewObsolete(params ErrorArgTuple[] args) =>
            NewObsolete("obsolete", args);

        public static Error NewDeprecated(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Deprecated, message, args);

        public static Error NewDeprecated(params ErrorArgTuple[] args) =>
            NewDeprecated("deprecated", args);

        public static Error NewCancelled(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Cancelled, message, args);

        public static Error NewCancelled(params ErrorArgTuple[] args) =>
            NewCancelled("cancelled", args);

        public static Error NewUninitialized(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Uninitialized, message, args);

        public static Error NewDisposing(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Disposing, message, args);

        public static Error NewDisposed(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Disposed, message, args);

        public static Error NewArgument(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Argument, message, args);

        public static Error NewInternal(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Internal, message, args);

        public static Error NewNotFound(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.NotFound, message, args);

        public static Error NewUpstream(string message, params ErrorArgTuple[] args) =>
            New(default, ErrorKind.Upstream, message, args);
    }
}
