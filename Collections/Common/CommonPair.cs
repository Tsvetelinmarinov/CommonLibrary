// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение..

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Collections.Common
{
    /// <summary>
    /// 
    /// EN:
    ///   CommonPair defines a key value pair where the key and the value are objects(data type System.Object).
    ///   
    ///  БГ:
    ///    CommonPair представлява обща двойка ключ-стойност, като под обща се
    ///    има в предвид, че типа данни на ключа и на стойността е System.Object, тоест те
    ///    са обекти и това позволява гъвкавост на двойката ключ-стойност и на кода като цяло, но
    ///    за сметка на сигурността.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Common key-value pair")]
    [Usage("Used when the user needs a key-value pair, but dont know the data type of the key and the value")]
    public class CommonPair : ICommonPair, ICloneable
    {
        //
        // Holds the key of the key-value pair.
        //
        // Съдържа ключа на двойката ключ-стойност.
        //
        private object? _key;

        //
        // Holds the value of the key-value pair.
        //
        // Съдържа стойността на двойката ключ-стойност.
        //
        private object? _value;


        /// <summary>
        /// 
        /// EN:
        ///   Gets the key of the key value pair.
        ///   
        /// BG:
        ///   Достъпва ключа на дойката ключ-стойност.
        /// 
        /// </summary>
        public object? Key
        {
            get => _key;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the value of the key value pair.
        ///   
        /// BG:
        ///   Достъпва стойността на дойката ключ-стойност.
        /// 
        public object? Value
        {
            get => _value;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the key and the value of the key value pair
        ///   as a tuple.
        ///   
        /// BG:
        ///   Достъпва ключа и стойността на дойката ключ-стойност
        ///   като дойка от тип данни Tuple.
        /// 
        public (object?, object?) KeyAndValue
        {
            get => (_key, _value);
        }

        /// <summary>
        /// 
        /// EN:
        ///   Concatenate the key and the value of the key-value pair
        ///   in a string and returns it.
        ///   
        /// BG:
        ///   Конкатенира ключа и стойността на двойката ключ-стойност
        ///   във стринг.
        ///   
        /// <summary/>
        public string ReturnAsString()
            => $"{_key} - {_value}";

        /// <summary>
        /// 
        /// EN:
        ///   Returns the key-value pair as object, which can be casted to
        ///   pair data type.
        ///   
        /// BG:
        ///   Връща двойката ключ-стойност като обект, който да бъде конвертиран 
        ///   към типа данни на двойката (така работи клонирането).
        /// 
        /// </summary>
        public object Clone()
            => this;
    }
}
