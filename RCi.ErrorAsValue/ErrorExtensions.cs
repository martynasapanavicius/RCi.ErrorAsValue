﻿using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using ErrorArgTuple = (string Name, object? Value);

namespace RCi.ErrorAsValue
{
    public static class ErrorExtensions
    {
        internal static ImmutableArray<T> MarshalAsImmutableArray<T>(this T[] src) =>
            ImmutableCollectionsMarshal.AsImmutableArray(src);

        internal static ErrorArg ToErrorArg(this ErrorArgTuple arg) => new(arg.Name, arg.Value);

        internal static ErrorArg[] ToErrorArg(this ErrorArgTuple[] args)
        {
            if (args.Length == 0)
            {
                return [];
            }
            var result = new ErrorArg[args.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = args[i].ToErrorArg();
            }
            return result;
        }

        public static Error Wrap(
            this Error err,
            string kind,
            string message,
            params ErrorArgTuple[] args
        ) => new(err, kind, message, args.ToErrorArg().MarshalAsImmutableArray());

        public static Error Wrap(this Error err, string message, params ErrorArgTuple[] args) =>
            new(err, null, message, args.ToErrorArg().MarshalAsImmutableArray());

        public static Error Wrap(this Error err, params ErrorArgTuple[] args) =>
            new(err, null, null, args.ToErrorArg().MarshalAsImmutableArray());

        public static Error ToError(this Exception exception, params ErrorArgTuple[] args) =>
            Error.NewException(exception, args);

        public static Exception ToException(this Error err) => new ErrorException(err);
    }
}
