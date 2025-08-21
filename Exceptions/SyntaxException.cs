//  CommonLibrary - library for common usage.
//  CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using System.Text.RegularExpressions;

namespace CommonLibrary.Exceptions
{
    /// <summary>
    /// 
    /// EN:
    ///  Indicates when some member value (Property or Field, Parameter..) is not typed correct.
    ///  The check of the syntax is made with a regular expression.
    ///  
    /// BG: 
    ///  Индикира когато стойността на даден член на класа (свойство, поле, параметър..) е с непозволен синтаксис.
    ///  Извършва се проверка на синтаксиса с регулярен израз.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Indicates incorrect syntax based on check with regular expression")]
    public sealed class SyntaxException : Exception
    {
        //The error message that the exception shows.
        //By default is Invalid syntax.
        //
        //Съобщението, което показва грешката.
        //По подразбиране е "Invalid syntax.".
        private string? _errorMessage;

        //Default error message to be shown, when the same is not specified.
        //
        //Съобщението по подразбиране, което се показва ако съобщението не е зададено.
        private const string DefaultErrorMessage = "Invalid syntax.";


        /// <summary>
        /// 
        ///  EN: Get or set the message that exception shows.
        ///  
        ///  BG: Достъпва съобщението за грешка, което се показва. 
        /// 
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage!;
            set
            {
                ArgumentNullException.ThrowIfNullOrEmpty(value);
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

                //Pattern for valid exception text. Not everything should be typed.
                //
                //Шаблон (регулярен израз) за валидиране на текста. Не може (и не е редно) да се пише всичко.
                string validExceptionPattern = @"[A-Za-z][a-z]*(?:[!?,.])*";

                bool isValidText = Regex.IsMatch(value, validExceptionPattern);

                if (isValidText)
                {
                    _errorMessage = value;
                }
                else
                {
                    throw new SyntaxException("Invalid syntax of the exception message.");
                }
            }
        }


        /// <summary>
        /// 
        ///  EN: Create new SyntaxException with default error message.
        ///  
        ///  BG: Създава нов SyntaxException със съобщение за грешка по подразбиране.
        /// 
        /// </summary>
        public SyntaxException()
            => ErrorMessage = DefaultErrorMessage;

        /// <summary>
        /// 
        ///  EN: Create new SyntaxException with specified error message.
        ///  
        ///  BG: Създава нов SytanxException със зададено съобщение за грешка.
        /// 
        /// </summary>
        /// 
        /// <param name="message">
        /// EN: The message for the exception.
        /// BG: Съобщението за грешка.
        /// </param>
        public SyntaxException(string message)
            => ErrorMessage = message;
    }
}
