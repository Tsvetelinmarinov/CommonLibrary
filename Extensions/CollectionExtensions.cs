// CommonLibrary - library for common usage.

namespace CommonLibrary.Extensions
{
    using static System.Console;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    using CommonLibrary.Attributes;
    using System.ComponentModel;
    using System;

    /// <summary>
    ///  Defines set of extension methods for collections.
    /// </summary>
    [Description("Defines set of extensions for all data types that defines collection or array")]
    [Usage("Use the extension methods from the instance of a collection or array")]
    public static class CollectionExtensions
    {
        /// <summary>
        ///  Prints all elements of the collection to the console.
        /// </summary>
        /// 
        /// <typeparam name="Type">
        ///  The type of elements in the collection.
        /// </typeparam>
        /// 
        /// <param name="collection">
        ///  The collection whose elements are to be printed.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrintAll<Type>(this IEnumerable<Type> collection)
            => WriteLine(String.Join('\n', collection));
    }
}
