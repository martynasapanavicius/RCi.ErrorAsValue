using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ErrorArgTuple = (string Name, object? Value);

namespace RCi.ErrorAsValue
{
    public class Error
    {
        private readonly Error? _inner;
        private readonly string? _kind;
        private readonly string? _message;
        private readonly ErrorThreadContext? _threadContext;
        private readonly string? _stackTrace;
        private readonly ImmutableArray<ErrorArg> _args;

        /// <summary>
        /// Constructor for new error.
        /// </summary>
        internal Error
        (
            string kind,
            string message,
            ErrorThreadContext threadContext,
            string stackTrace,
            ImmutableArray<ErrorArg> args
        )
        {
            _inner = default;
            _kind = kind;
            _message = message;
            _threadContext = threadContext;
            _stackTrace = stackTrace;
            _args = args;
        }

        /// <summary>
        /// Constructor for wrapping an existing error.
        /// </summary>
        internal Error
        (
            Error inner,
            string? kindOverride,
            string? messageOverride,
            ImmutableArray<ErrorArg> args
        )
        {
            _inner = inner;
            _kind = kindOverride;
            _message = messageOverride;
            _args = args;
        }

        protected IEnumerable<Error> EnumerateErrorChain()
        {
            var node = this;
            while (node is not null)
            {
                yield return node;
                node = node._inner;
            }
        }

        public virtual string Kind
        {
            get
            {
                // we should always find kind at root level
                return EnumerateErrorChain()
                    .First(e => e._kind is not null)
                    ._kind!;
            }
        }

        public virtual string Message
        {
            get
            {
                var allMessages = EnumerateErrorChain()
                    .Select(e => e._message)
                    .Where(m => m is not null);
                return string.Join(": ", allMessages);
            }
        }

        public virtual ErrorThreadContext ThreadContext
        {
            get
            {
                // we should always find thread context at root level
                return EnumerateErrorChain()
                    .Last(e => e._threadContext is not null)
                    ._threadContext!;
            }
        }

        public virtual string StackTrace
        {
            get
            {
                // we should always find stack trace at root level
                return EnumerateErrorChain()
                    .Last(e => e._stackTrace is not null)
                    ._stackTrace!;
            }
        }

        public virtual IEnumerable<ErrorArg> Args => EnumerateErrorChain()
            .Where(err => !err._args.IsDefaultOrEmpty)
            .SelectMany(err => err._args);

        public override string ToString() => $"{Kind}: {Message}";

        public static implicit operator bool(Error? err) => err is not null;

        // factory (generic)

        public static Error New(string kind, string message, params ErrorArgTuple[] args) =>
            new(kind, message, ErrorThreadContext.GetCurrent(), Environment.StackTrace, args.ToErrorArg().UnsafeAsImmutableArray());

        // factory (explicit)

        public static Error NewException(Exception exception, params ErrorArgTuple[] args)
        {
            return new Error
            (
                ErrorKind.Exception,
                exception.GetType().Name + ": " + exception.Message,
                ErrorThreadContext.GetCurrent(),
                exception.StackTrace ?? Environment.StackTrace,
                [.. GetArgs()]
            );

            IEnumerable<ErrorArg> GetArgs()
            {
                foreach (DictionaryEntry entry in exception.Data)
                {
                    yield return new ErrorArg(entry.Key.ToString() ?? string.Empty, entry.Value);
                }
                foreach (var tuple in args)
                {
                    yield return tuple;
                }
            }
        }

        public static Error NewNotImplemented(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.NotImplemented, message, args);

        public static Error NewNotImplemented(params ErrorArgTuple[] args) =>
            NewNotImplemented("not implemented", args);

        public static Error NewNotSupported(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.NotSupported, message, args);

        public static Error NewNotSupported(params ErrorArgTuple[] args) =>
            NewNotSupported("not supported", args);

        public static Error NewObsolete(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Obsolete, message, args);

        public static Error NewObsolete(params ErrorArgTuple[] args) =>
            NewObsolete("obsolete", args);

        public static Error NewDeprecated(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Deprecated, message, args);

        public static Error NewDeprecated(params ErrorArgTuple[] args) =>
            NewDeprecated("deprecated", args);

        public static Error NewCancelled(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Cancelled, message, args);

        public static Error NewCancelled(params ErrorArgTuple[] args) =>
            NewCancelled("cancelled", args);

        public static Error NewUninitialized(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Uninitialized, message, args);

        public static Error NewDisposing(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Disposing, message, args);

        public static Error NewDisposed(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Disposed, message, args);

        public static Error NewArgument(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Argument, message, args);

        public static Error NewInternal(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Internal, message, args);

        public static Error NewNotFound(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.NotFound, message, args);

        public static Error NewUpstream(string message, params ErrorArgTuple[] args) =>
            New(ErrorKind.Upstream, message, args);
    }
}
