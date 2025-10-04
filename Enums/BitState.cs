// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Enums
{
    /// <summary>
    /// 
    /// EN:
    ///   Provides flags that indicate the state of the bit at the given position 
    ///   in the "ChangeBitAt()" method in the BinaryHelper class.
    ///   
    /// BG:
    ///   Предоставя флагове за състоянието на даден бит за "ChangeBitAt()" метода
    ///   в класа BinaryHelper.
    /// 
    /// </summary>
    [Description("Provides options for the state of a bit")]
    [Usage("Used by the ChangeBitAt() method in the BinaryHelper class")]
    public enum BitState
    {
        /// <summary>
        /// 
        /// EN:
        ///   Indicates that the bit should be switched on.
        ///   
        /// BG:
        ///   Индикира, че бита трябва да бъде включен, 
        ///   тоест да присвой стойност 1.
        /// 
        /// </summary>
        SwitchOn,

        /// <summary>
        /// 
        /// EN:
        ///   Indicates that the bit should be switched off.
        ///   
        /// BG:
        ///   Индикира, че бита трябва да бъде изключен, 
        ///   тоест да присвой стойност 0.
        /// 
        /// </summary>
        SwitchOff
    }
}