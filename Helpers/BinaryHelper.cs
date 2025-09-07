// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using CommonLibrary.AbstractDataTypes;

namespace CommonLibrary.Helpers
{
    /// <summary>
    /// 
    /// EN:
    ///   Provides set of static methods for converting strings, numbers and etc. to binary string.
    ///   
    /// BG:
    ///   Предоставя набор от статични методи за конвертиране на текст, числа и т.н. в двойчен стринг.
    ///   Двойчния стринг преставлява двойчно число под формата на стринг.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Provides methods for converting a data type to a binary string")]
    public static class BinaryHelper
    {
        /// <summary>
        /// 
        /// EN:
        ///   Converts the number to a binary string.
        ///   
        /// BG:
        ///   Конвертира числото в двойчен стринг.
        /// 
        /// </summary>
        /// 
        /// <param name="number">
        ///  EN: The number to be converted.
        ///  BG: Числото, което ще се конвертира.
        /// </param>
        /// 
        /// <returns>
        ///  EN: Binary string.
        ///  BG: Двойчен стринг.
        /// </returns>
        public static string ConvertIntToBinary(int number)
        {           
            if (number == 0)
            {
                return "0"; // If the number is already 0, why we should execute the other code?
            }
            else if (number < 0)
            {
                number = Math.Abs(number); // If the number is negative, get the module of the number                                             
            }                             // and work with the module. 

            MutableString binary = string.Empty;

            while (number > 0)
            {
                int rem = number % 2; // get the remainder: 0 or 1.

                binary.InsertAt(0, rem.ToString()); // Insert the bit(the remainder) at the first position in the 
                                                    // binary string.

                number /= 2; // Devide the number with 2.
            }

            return
                 binary.ToString();
        }

        /// <summary>
        /// 
        /// EN:
        ///   Converts the string to a binary string.
        ///   
        /// BG:
        ///   Конвертира стринга в двойчен стринг.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text to be converted.
        ///  BG: Текста, който ще се конвертира.
        /// </param>
        /// 
        /// <returns>
        ///  EN: Binary string.
        ///  BG: Двойчен стринг.
        /// </returns>
        public static string ConvertStringToBinary(string text)
        {
            Collection<int> symbolsBase = [];
            CollectionHelper.ExecuteOnEachElement(symbol => symbolsBase.Add(symbol), text);

            Collection<string> binarys = [];
            CollectionHelper.ExecuteOnEachElement(
                _base => binarys.Add(ConvertIntToBinary(_base)),
                symbolsBase
            );

            MutableString finalBinary = string.Empty;
            CollectionHelper.ExecuteOnEachElement(finalBinary.Concatenate, binarys);

            return
                finalBinary.ToString();
        }
    }
}