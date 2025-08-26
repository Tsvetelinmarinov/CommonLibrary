//  CommonLibrary - library for common usage.
//  CommonLibrary - библиотека с общо предназначение.

using System;
using CommonLibrary.Attributes;
using System.Text.RegularExpressions;

namespace CommonLibrary.Exceptions
{
    /// <summary>
    /// 
    /// EN:
    ///   Defines common error. The default exception for everything.
    /// 
    /// BG:
    ///   Представлява обща грешка. Може да се третира като
    ///   грешка по подразбиране и да се използва по всякакви поводи.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Usage("Common error")]
    public class Error : Exception
    {
        //
        // Holds the error message.
        //
        // Съхранява съобщението за грешка.
        //
        private string? _error;


        /// <summary>
        /// 
        /// EN:
        ///   Creates new Error with the specified error message.
        ///   
        /// BG:
        ///   Създава нов Error със указаното съобщение за грешка.
        /// 
        /// </summary>
        /// 
        /// <param name="message">
        ///  EN: The message.
        ///  BG: Съобщението.
        /// </param>
        public Error(string message)
            => ValidateAndStore(ref message);


        //
        // Validates ans stores the message.
        //
        // Валидира съобщението и го съхранява.
        //
        private void ValidateAndStore(ref string message)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);
            ArgumentNullException.ThrowIfNullOrEmpty(message);

            //
            // Pattern for validating the message syntax.
            //
            // Шаблон (регулярен израз) за валидиране на синтаксиса на съобщението.
            // Трябва да започва с главна буква и може да има всякакви знаци накрая.
            //
            string validationPattern = @"(?:[A-Z][a-z]+)(?: [A-Za-z]+)*(?:[\W])?";

            bool isValid = Regex.IsMatch(message, validationPattern);

            if (isValid)
            {
                _error = message;
            }
            else
            {
                throw new SyntaxException("Invalid syntax of the message.");
            }
        }
    }
}
