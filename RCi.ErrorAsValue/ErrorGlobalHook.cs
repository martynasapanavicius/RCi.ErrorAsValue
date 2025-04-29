using System;

namespace RCi.ErrorAsValue
{
    public static class ErrorGlobalHook
    {
        /// <summary>
        /// Fires when a new error is created (not wrapped).
        /// </summary>
        public static event EventHandler<Error>? OnError;

        internal static void InvokeOnError(Error err) => OnError?.Invoke(null, err);
    }
}
