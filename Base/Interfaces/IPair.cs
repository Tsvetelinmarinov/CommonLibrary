// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Base.Interfaces
{
    /// <summary>
    /// 
    /// EN: 
    ///   Defines base interface for a key-value pair.
    ///   
    /// BG:
    ///   Базов интерфейс за двойка ключ-стойност.
    /// 
    /// </summary>
    /// 
    /// <typeparam name="KeyType">
    ///  EN: The data type of the key.
    ///  BG: Типа данни на ключа.
    /// </typeparam>
    /// 
    /// <typeparam name="ValueType">
    ///  EN: The data type of the value.
    ///  BG: Типа данни на стойността.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Description("Base interface for a key-value pair")]
    public interface IPair<KeyType, ValueType>
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
        KeyType Key
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
        public ValueType Value
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
        public (KeyType, ValueType) KeyAndValue
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