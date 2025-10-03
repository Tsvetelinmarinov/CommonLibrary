// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Base.Interfaces
{
    /// <summary>
    /// 
    /// EN:
    ///   Base interface for a common key-value pair.
    ///   
    /// БГ:
    ///   Базов интерфейс за обща двойка ключ-стойност.
    /// 
    /// </summary>
    [Description("Base interface for a commom key-value pair")]
    public interface ICommonPair
    {
        /// <summary>
        /// 
        /// EN:
        ///   Gets the key of the key-value pair.
        ///   
        /// BG:
        ///   Достъпва ключа на двойката ключ-стойност.
        /// 
        /// </summary>
        object? Key
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the value of the key-value pair.
        ///   
        /// BG:
        ///   Достъпва стойността на двойката ключ-стойност.
        /// 
        /// </summary>
        public object? Value
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Returns the key and the value as tuple.
        /// 
        /// BG:
        ///   Връща ключа и стойността на двойката като двойка (Tuple).
        ///   
        /// </summary>
        public (object?, object?) KeyAndValue
        {
            get;
        }


        /// <summary>
        /// 
        /// EN:
        ///   Returns the key and the value of the pair concatenated with
        ///   separator as string.
        ///   
        /// BG:
        ///   Връща ключа и стойността на двойката ключ-стойност конкатенирани
        ///   с разделител като текст (string).
        /// 
        /// </summary>
        public string ReturnAsString();
    }
}