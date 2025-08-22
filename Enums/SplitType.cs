// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using CommonLibrary.Attributes;

namespace CommonLibrary.Enums
{
    /// <summary>
    /// 
    /// EN:
    ///   Specifies the splitting style, when a string is splatted by separator.
    ///   Use NoEmptyStrings to remove the empty entries that can occur when splitting.
    ///   Use KeepEmptyStrings to keep the empty entries.
    /// 
    /// BG:
    ///   Указва начина на разделяне на стринг по разделител.
    ///   Използвай флага NoEmptyStrings за да се премахнат празните стрингове,
    ///   който могат да се получат при разделянето.
    ///   Изплозвай флага KeepEmptyStrings за да запазиш празните стрингове.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Usage("Used in the SplitBy method in the MutableString class to specify the splitting type")]
    public enum SplitType
    {
        /// <summary>
        /// 
        /// EN:
        ///   Indicates that the empty strings, created when splitting should be
        ///   removed.
        ///   
        /// BG:
        ///   Индикира, че празните стрингове, получени при разделяне по разделител,
        ///   трябва да бъдат премахнати от резултата.
        /// 
        /// </summary>
        NoEmptyStrings = 0,

        /// <summary>
        /// 
        /// EN:
        ///   Indicates that the empty strings, created when splitting should be
        ///   saved.
        ///   
        /// BG:
        ///   Индикира, че празните стрингове, получени при разделяне по разделител,
        ///   трябва да бъдат добавени в резултата.
        /// 
        /// </summary>
        KeepEmptyStrings = 1
    }
}
