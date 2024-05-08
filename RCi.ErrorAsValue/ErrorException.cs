using System;
using System.Collections;
using System.Collections.Immutable;

namespace RCi.ErrorAsValue
{
    public sealed class ErrorException(Error err) :
        Exception
    {
        public override string Message => err.Message;

        public override string StackTrace => err.StackTrace;

        private ImmutableDictionary<string, object> _data;
        public override IDictionary Data => _data ??= err.Args.ToImmutableDictionary(a => a.Name, a => a.Value);

        public override string ToString() => err.ToString();
    }
}
