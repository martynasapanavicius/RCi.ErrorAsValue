using System.Diagnostics.CodeAnalysis;

namespace RCi.ErrorAsValue
{
    public static class VeExtensions
    {
        public static Ve<T> ToVe<T>(this T value) => new(value, default);

        public static Ve<T> ToVe<T>(this Error err) => new(default!, err);

        /// <summary>
        /// Unpacks value and error into out parameters and returns: true - no error, or false - error.
        /// </summary>
        public static bool Ok<T>(this Ve<T> ve, out T value, [NotNullWhen(false)] out Error? err)
        {
            (value, err) = ve;
            return err is null;
        }

        /// <summary>
        /// Unpacks value and error into out parameters and returns: false - no error, or true - error.
        /// </summary>
        public static bool Failed<T>(this Ve<T> ve, out T value, [NotNullWhen(true)] out Error? err)
        {
            (value, err) = ve;
            return err is not null;
        }
    }
}
