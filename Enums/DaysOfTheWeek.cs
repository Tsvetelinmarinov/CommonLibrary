// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Enums
{ 
    /// <summary>
    /// 
    /// EN: Specifies which day of the week it is.
    /// 
    /// BG: Указва кой ден от седмицата е.
    /// 
    /// </summary>
    [Description("Specifies which day of the week it is")]
    public enum Day
    {
        /// <summary>
        /// 
        /// EN: Specifies the first day of the week  
        /// 
        /// BG: Указва първия ден от седмицата
        /// 
        /// </summary>
        Monday = 0,

        /// <summary>
        /// 
        /// EN: Specifies the second day of the week  
        /// 
        /// BG: Указва втория ден от седмицата
        /// 
        /// </summary>
        Tuesday = 1,

        /// <summary>
        /// 
        /// EN: Specifies the third day of the week  
        /// 
        /// BG: Указва третия ден от седмицата
        /// 
        /// </summary>
        Wednesday = 2,

        /// <summary>
        /// 
        /// EN: Specifies the fourth day of the week  
        /// 
        /// BG: Указва четвъртия ден от седмицата
        /// 
        /// </summary>
        Thursday = 3,

        /// <summary>
        /// 
        /// EN: Specifies the fifth day of the week  
        /// 
        /// BG: Указва петия ден от седмицата
        /// 
        /// </summary>
        Friday = 4,

        /// <summary>
        /// 
        /// EN: Specifies the sixth day of the week 
        /// 
        /// BG: Указва шестия ден от седмицата
        /// 
        /// </summary>
        Saturday = 5,

        /// <summary>
        /// 
        /// EN: Specifies the seventh day of the week  
        /// 
        /// BG: Указва седмия ден от седмицата
        /// 
        /// </summary>
        Sunday = 6
    }

}
