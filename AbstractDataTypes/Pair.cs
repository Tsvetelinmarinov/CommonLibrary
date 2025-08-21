// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Interfaces;

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
    [Author("Tsvetelin Marinov")]
    [Description("Key-value pair")]
    public class Pair<KeyType, ValueType> : IPair<KeyType, ValueType>, ICloneable
    {
        //
        // Holds the key of the pair.
        //
        // Съхранява ключа на двойката.
        //
        private KeyType? _key;

        //
        // Holds the value of the pair.
        //
        // Съхранява стойността на двойката.
        //
        private ValueType? _value;


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
            get => _key ??= default!;
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
            get => _value ??= default!;
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
            => (Key, Value);


#pragma warning disable IDE0290
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
            _key = key;
            _value = value;
        }
#pragma warning restore IDE0290


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
        public string ReturnAsString()
            => $"{Key} - {Value}";

        /// <summary>
        /// 
        ///  EN:
        ///     Clones the pair. Returns it like an object;
        ///     
        ///  BG:
        ///    Клонира двойката ключ-стойност, като я връща като обект,
        ///    който да бъде конвертиран (кастнат).
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: The pair as object.
        ///  BG: Дойката ключ-стойност като обект.
        /// </returns>
        public object Clone()
            => this;
    }
}