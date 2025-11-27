// CommonLibrary - library for common usage.

using System;
using System.IO;
using System.Text;
using CommonLibrary.Enums;
using CommonLibrary.Exceptions;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace CommonLibrary.Helpers
{
    /// <summary>
    ///   Provides set of static methods for converting strings, numbers 
    ///   and etc. to binary string/file.
    /// </summary>
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

            StringBuilder binary = new();

            while (number > 0)
            {
                int rem = number % 2;
                binary.Insert(0, rem.ToString());
                number /= 2;
            }

            return
                 binary.ToString();
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
        ///  BG: Резултата като цяло число.
        /// </returns>
        public static int ChangeBitAt(int number, int index, BitState state = BitState.SwitchOff)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentNullException.ThrowIfNull(state);

            if (index > ConvertIntToBinary(number).Length)
            {
                throw new Error("The index can not be outside of the bounds of the binary");
            }

            int mask; // The mask should be different with the different bit state.

            if (state == BitState.SwitchOn)
            {
                mask = 1 << index;
                return number | mask;
            }

            mask = ~(1 << index);
            return number & mask;
        }

        /// <summary>
        ///  Creates a binary(.bin) file with the given text.
        /// </summary>
        /// 
        /// <param name="content">
        ///  The text of the file.
        /// </param>
        /// 
        /// <param name="binaryDirectory">
        ///  The location of the binary - where to be created.
        /// </param>
        public static void CreateBinary(string content, string binaryDirectory)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(content);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(binaryDirectory);

            using FileStream binary = new(
                binaryDirectory, 
                FileMode.Create, 
                FileAccess.Write, 
                FileShare.None, 
                bufferSize: 666
            );

            byte[] data = Encoding.UTF8.GetBytes(content);

            binary.Write(data, 0, data.Length);
            binary.Flush(); // The "using" directive calls the Close() command wich calls the Flush() command
                            // but i need to be shure.
        }

        /// <summary>
        ///   Splits the file content to several binary files.
        /// </summary>
        /// 
        /// <param name="fileLocation">
        ///  The location of the file.
        /// </param>
        /// 
        /// <param name="outputFolder">
        ///  The folder where the binaries will be saved.
        /// </param>
        /// 
        /// <param name="parts">
        ///  The count of the binaries.
        /// </param>
        public static void SplitToBinaries(string fileLocation, string outputFolder, byte parts)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(fileLocation);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(outputFolder);

            if (parts < 1)
            {
                _ = ShowMessageWindow(0, "The parts can not be less than one.", "Binary Helper", 0);
                parts = 1;
            }

            if (parts > 100)
            {
                _ = ShowMessageWindow(0, "The parts can not be more that a houndred.", "Binary Helper", 0);
                parts = 100;
            }

            bool fileExists = File.Exists(fileLocation);
            if (!fileExists)
            {
                throw new InvalidOperationException("The file does not exist.");
            }

            using FileStream file = new(fileLocation, FileMode.Open, FileAccess.Read);

            int binCapacity = (int)(file.Length / parts);
            byte[] binaryBuffer = new byte[binCapacity];

            for (int i = 1; i <= parts; i++)
            { 
                using FileStream binary = new(
                    $@"{outputFolder}\part{i}.bin", 
                    FileMode.Create, 
                    FileAccess.Write
                );

                file.Read(binaryBuffer, 0, binaryBuffer.Length);
                binary.Write(binaryBuffer, 0, binaryBuffer.Length);

                Array.Clear(binaryBuffer);
            }

            _ = ShowMessageWindow(0, "The files are created successfull.", "Binary Helper", 0);
        }


        /// <summary>
        ///  Shows a message window with the specified
        ///  message and title.
        /// </summary>
        /// 
        /// <param name="hWind"></param>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "MessageBox", CharSet = CharSet.Unicode)]
        private static extern int ShowMessageWindow(int hWind, string text, string title, int type);
    }
}