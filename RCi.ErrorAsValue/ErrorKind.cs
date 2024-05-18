namespace RCi.ErrorAsValue
{
    public static class ErrorKind
    {
        public const string Undefined = nameof(Undefined);
        public const string Exception = nameof(Exception);
        public const string NotImplemented = nameof(NotImplemented);
        public const string NotSupported = nameof(NotSupported);
        public const string Obsolete = nameof(Obsolete);
        public const string Deprecated = nameof(Deprecated);
        public const string Cancelled = nameof(Cancelled);
        public const string Uninitialized = nameof(Uninitialized);
        public const string Disposing = nameof(Disposing);
        public const string Disposed = nameof(Disposed);
        public const string Argument = nameof(Argument);
        public const string Internal = nameof(Internal);
        public const string NotFound = nameof(NotFound);
        public const string Upstream = nameof(Upstream);
    }
}
