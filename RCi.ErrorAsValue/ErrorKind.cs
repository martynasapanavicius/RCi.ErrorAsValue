namespace RCi.ErrorAsValue
{
    public static class ErrorKind
    {
        public static readonly string Undefined = nameof(Undefined);
        public static readonly string Exception = nameof(Exception);
        public static readonly string NotImplemented = nameof(NotImplemented);
        public static readonly string NotSupported = nameof(NotSupported);
        public static readonly string Obsolete = nameof(Obsolete);
        public static readonly string Deprecated = nameof(Deprecated);
        public static readonly string Cancelled = nameof(Cancelled);
        public static readonly string Uninitialized = nameof(Uninitialized);
        public static readonly string Disposing = nameof(Disposing);
        public static readonly string Disposed = nameof(Disposed);
        public static readonly string Argument = nameof(Argument);
        public static readonly string Internal = nameof(Internal);
        public static readonly string NotFound = nameof(NotFound);
        public static readonly string Upstream = nameof(Upstream);
    }
}
