// CommonLibrary - library for common usage.

using System.ComponentModel;

namespace CommonLibrary.Enums
{
    /// <summary>
    ///  Provides positions for modules in a modular array.
    /// </summary>
    [Description("Provides flags that indicates the position of a module in a modular array")]
    public enum ModulePosition
    {
        /// <summary>
        ///  Indicates the first module in the array.
        /// </summary>
        Head,

        /// <summary>
        ///  Indicates the last module in the array.
        /// </summary>
        Tail
    }
}
