// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Linq;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using System.Security.Cryptography;

namespace CommonLibrary.Helpers
{
    /// <summary>
    /// 
    ///  EN: 
    ///    Generator of random numbers. Provides methods for generating random number, or
    ///    sequence of random numbers, in diapason of starting number and
    ///    ending number. The range of the diapason shoul be defined with the creation of the
    ///    generator, and can be accessed or changed with the Start and the End properties.
    ///    
    ///  BG:
    ///    Генератор на случайни номера. Предоставя необходимите методи за генериране на 
    ///    случаен номер или поредица от случайни номера, в диапазон от 
    ///    стартово число и крайно число. Началото и края на диапазона за генериране на числа трябва
    ///    да бъдат указани в началото със създаването на генератора. В по късен етап могад да бъдат
    ///    достъпени или промемени чрез свойтвата Start и End на класа.
    ///    
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Generator of random numbers")]
    public class NumberGenerator
    {
        //
        // The start of the diapason for generating number/numbers.
        //
        // Началото на диапазона за генериране на число/числа.
        //
        private int _start;

        //
        // The end of the diapason for generating number/numbers.
        //
        // Края на диапазона за генериране на число/числа.
        //
        private int _end;


        /// <summary>
        /// 
        /// EN:
        ///    Gets or sets the start of the diapason for generating.
        ///    
        /// BG:
        ///   Достъпва началото на диапазона за генериране на числа.
        /// 
        /// </summary>
        public int Start
        {
            get => _start;
            set => _start = value;           
        }

        /// <summary>
        /// 
        /// EN:
        ///    Gets or sets the end of the diapason for generating.
        ///    
        /// BG:
        ///   Достъпва края на диапазона за генериране на числа.
        /// 
        /// </summary>
        public int End
        {
            get => _end;
            set => _end = value;
        }

#pragma warning disable IDE0290
        /// <summary>
        ///  
        /// EN:
        ///   Creates new generator of random numbers, инт the diapason
        ///   for generating from "start" to "end".
        ///   
        /// BG:
        ///   Създава нов генератор на случайни номера в указания диапазон.
        ///   В този случай генератора генерира число или поредица от числа в диапазон
        ///   от "start" до "end". 
        /// 
        /// </summary>
        public NumberGenerator(int start, int end)
        { 
            Start = start;
            End = end;
        }
#pragma warning restore IDE0290


        /// <summary>
        ///  
        /// EN:
        ///   Generates random number in the specified diapason.
        ///   
        /// BG:
        ///   Генерира случаен номер в зададения диапазон.
        /// 
        /// </summary>
        public int Generate()
        {
            // New byte array for 4 radnom bytes.
            // 4 bytes are 32 bits - enough for an integer.
            //
            // Нов масив от байтове с капацитет 4 байта.
            // В него ще се запишат 4 пройзволни байта, от който да се създаде цяло число
            // от тип данни int (Int32).
            // 1 байт е 8 бита , тоест 4 байта са 32 бита - достатъчно памет за едно
            // цяло число (Int32).
            byte[] sourceBytes = new byte[4];

            // Fill the byte array with random bytes.
            //
            // Напълва масива с 4 пройзволни байта.
            RandomNumberGenerator.Fill(sourceBytes);

            // Converts the byte array to an integer of type int (Int32).
            //
            // Конвертира масива от 4-те байта в 32 битово цяло число от тип данни
            // int (Int32).
            int number = BitConverter.ToInt32(sourceBytes, 0);

            // Generate random number.
            //
            // Генерирания случаен номер в желания диапазон.
            int diapason = _end - _start;
            number = _start + Math.Abs(number) % diapason;

            return number;
        }

        /// <summary>
        ///  
        /// EN:
        ///   Generates multiple random numbers in the given diapason and
        ///   store it in array of integers.
        ///   
        /// BG:
        ///   Генерира указано множество от случайни числа и ги записва в масив, който връща
        ///   като стойност.
        /// 
        /// </summary>
        /// 
        /// <param name="count">
        ///  EN: The count of the numbers to be generated.
        ///  BG: Бройката случайни числа, които да се генерират.
        /// </param>
        /// 
        /// <returns>
        ///  EN: Array with random generated numbers.
        ///  BG: Масив от случайно генерирани числа.
        /// </returns>
        public int[] GenerateMultiple(int count)
        {
            DynamicArray<int> numbers = [];

            for (int i = 0; i < count; ++i)
            {
                numbers.Add(Generate());
            }

#pragma warning disable IDE0305 
            return numbers.ToArray();
#pragma warning restore IDE0305 
        }
    }
}