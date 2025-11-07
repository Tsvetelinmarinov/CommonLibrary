// CommonLibrary - library for common usage.

namespace CommonLibrary.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using CommonLibrary.Attributes;

    /// <summary>
    ///  Provides set of extension methods for string.
    /// </summary>
    [Usage("Use the extension methods from an instance of a string")]
    public static class StringExtensions
    {
        /// <summary>
        ///  Checks of the specified string is empty.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be checked
        /// </param>
        /// 
        /// <returns>
        ///  True if the text has no characters, otherwise False.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this string text)
            => text != null && text.Length == 0;
    }
}
