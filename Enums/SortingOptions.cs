// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Enums
{
    /// <summary>
    /// 
    /// EN: Provides options for sorting.
    /// 
    /// BG: Предоставя опций за сортиране.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Provides options for sorting")]
    public enum SortingOptions
    {
        /// <summary>
        /// 
        /// EN: Default sorting - ascending.
        /// 
        /// BG: Възходящо сортиране (по подразбиране).
        /// 
        /// </summary>
        Ascending = 0,

        /// <summary>
        /// 
        /// EN: Descending sorting.
        /// 
        /// BG: Низходящо сортиране.
        /// 
        /// </summary>
        Descending = 1
    }
}
