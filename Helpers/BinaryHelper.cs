// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using CommonLibrary.AbstractDataTypes;
using System.Text.RegularExpressions;
using CommonLibrary.Enums;

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
                number = Math.Abs(number);                                              
            }                             

            MutableString binary = string.Empty;

            while (number > 0)
            {
                int rem = number % 2; 
                binary.InsertAt(0, rem.ToString());                                                    
                number /= 2;
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
            ArgumentNullException.ThrowIfNullOrEmpty(text);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(text);

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

        /// <summary>
        /// 
        /// EN:
        ///   Converts the binary string to a integer.
        ///   
        /// BG:
        ///   Конвертира двойчния стринг в цяло число.
        /// 
        /// </summary>
        /// 
        /// <param name="binary">
        ///  EN: The binary string to be converted.
        ///  BG: Двойчния стринг, който ще се конвертира.
        /// </param>
        /// 
        /// <returns>
        ///  EN: Integer.
        ///  BG: Цяло число, получено от двойчния стринг.
        /// </returns>
        public static int ConvertBinaryToInt(string binary)
        {
            ArgumentNullException.ThrowIfNull(binary);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(binary.Length);

            if (binary.StartsWith("0b"))
            {
                binary = binary.Remove(0, 2);
            }

            if (binary.Length > 32)
            {
                throw new Error("Binary can not be with more than 32 bits.");
            }

            int result = default;
            string validBinPattern = @"(?<Bit>[0-1])+";
            bool isValid = Regex.IsMatch(binary, validBinPattern);

            if (isValid)
            {
                int exponent = default;
                for (int i = binary.Length - 1; i >= 0; i--)
                {
                    result += (int)(int.Parse(binary[i].ToString()) * Math.Pow(2, exponent++));
                }
            }

            return 
                result;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Changes the bit at the given position.
        ///   The first index in the binary is the last bit.
        ///   The index is incremented at every nex bit, but the 
        ///   direction of indexing is from left to rigth.
        ///   The state of the bit is indicated by a flag from the
        ///   enumeration "BitState".
        ///   Use flag "SwitchOn" to switch on the bit(set the value to 1).
        ///   Use flag "SwitchOff" to switch off the bit(set the value to 0).
        ///   
        /// BG:
        ///    Променя състоянието на даден бит на дадена позиция.
        ///    Позициятя на бита е неговия индекс в машинния код.
        ///    Индекирането става от ляво на дясно, тоест на индекс 0
        ///    е последния бит. Състоянието на бита се указва с флаг
        ///    от еномерацията "BitState".
        ///    Използвай флага "SwitchOn" за да вкючиш бита(да му зададеш стойност 1).
        ///    Използвай флага "SwitchOff" за да изкючиш бита(да му зададеш стойност 0).
        /// 
        /// </summary>
        /// 
        /// <param name="number">
        ///  EN: The number.
        ///  BG: Числото.
        /// </param>
        /// 
        /// <param name="index">
        ///  EN: The position of the bit in the number.
        ///  BG: Позицията на бита в числото(в машинния му код). 
        /// </param>
        /// 
        /// <param name="state">
        ///  BG: The state of the bit.
        ///  BG: Съзтоянието на бита. Дали да е включен или не.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The result as new integer.
        ///  BG: Резултата като цяло число.+
        /// </returns>
        public static int ChangeBitAt(int number, int index, BitState state = BitState.SwitchOff)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentNullException.ThrowIfNull(state);

            if (index > ConvertIntToBinary(number).Length)
            {
                throw new Error("The index can not be outside of the bounds of the binary");
            }

            int mask; // The mask should be different.

            if (state == BitState.SwitchOn)
            {
                mask = 1 << index;
                return number | mask;
            }

            mask = ~(1 << index);
            return number & mask;
        }
    }
}