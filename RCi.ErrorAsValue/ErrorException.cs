using System;
using System.Collections;
using System.Collections.Generic;

namespace RCi.ErrorAsValue
{
    public sealed class ErrorException(Error err) :
        Exception
    {
        public override string Message => err.Message;

        public override string StackTrace => err.StackTrace;

        private IDictionary? _data;
        public override IDictionary Data => _data ??= GetData(err.Args);

        public override string ToString() => err.ToString();

        private static IDictionary GetData(IEnumerable<ErrorArg> args)
        {
            var data = new Dictionary<object, object?>();
            foreach (var (name, value) in args)
            {
                if (data.TryAdd(name, value))
                {
                    continue;
                }

                var postfix = 1;
                while (!data.TryAdd(name + "_" + postfix, value))
                {
                    postfix++;
                }
            }
            return data;
        }
    }
}
