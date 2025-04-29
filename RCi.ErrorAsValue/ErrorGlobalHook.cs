using System;

namespace RCi.ErrorAsValue
{
    public static class ErrorGlobalHook
    {
        public static event EventHandler<Error>? OnError;

        internal static void InvokeOnError(Error err) => OnError?.Invoke(null, err);
    }
}
