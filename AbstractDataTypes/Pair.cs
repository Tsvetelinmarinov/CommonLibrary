// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;

namespace CommonLibrary.AbstractDataTypes
{
    /// <summary>
    /// 
    /// EN: 
    ///    A key-value pair.
    ///   
    /// BG:
    ///   Двойка ключ-стойност.
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
    [Description("Key-value pair")]
    public sealed class Pair<KeyType, ValueType>
        where KeyType : notnull
        where ValueType : notnull
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
        public KeyType Key
        {
            get;
            private init;
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
            private init;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Returns the key and the value as tuple.
        /// 
        /// BG:
        ///   Връща ключа и стойността на двойката ключ-стойност като двойка (Tuple).
        ///   
        /// </summary>
        public (KeyType, ValueType) KeyAndValue 
            => (this.Key, this.Value);


        /// <summary>
        /// 
        /// EN:
        ///   Creates new key-value pair with the specified key and value.
        ///   
        /// BG:
        ///   Създава нова двойка ключ-стойност с указаните ключ и стойност.
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        ///  EN: The key.
        ///  BG: Ключа.
        /// </param>
        /// 
        /// <param name="value">
        ///  EN: The value.
        ///  BG: Стойността.
        /// </param>
        public Pair(KeyType key, ValueType value)
        {
            this.Key = key;
            this.Value = value;
        }


        /// <summary>
        ///  Converts the key value pair to a string.
        /// </summary>
        public override string ToString()
            => $"{this.Key} - {this.Value}";
    }
}