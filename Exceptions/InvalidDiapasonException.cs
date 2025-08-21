// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using System.Text.RegularExpressions;

namespace CommonLibrary.Exceptions
{
    /// <summary>
    /// 
    /// EN:
    ///   Indicates that the diapason for random generating an number in the
    ///   CommonLibrary.AbstractDataTypes.NumberGenerator class is invalid.
    /// 
    /// BG:
    ///   Индикира, когато диазапона за генериране на случайни числа на 
    ///   класа CommonLibrary.AbstractDataTypes.NumberGenerator е невалиден.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Indicates when the diapason for generating random number/numbers is invalid")]
    public sealed class InvalidDiapasonException : Exception
    {
        //
        // The default message that is shown, when the exception occurs, if the message is not specified.
        //
        // Съобщението по подразбиране, което се показвва, когато грешката се появи
        // и съобщението не е указано изрично.
        //
        private const string DEFMSG = "Invalid diapason for generating an number/numbers.";

        //
        // The message that is shown, when the exception occurs.
        //
        // Съобщението, което се показвва, когато грешката се появи.
        //
        private string? _msg;


        /// <summary>
        ///  
        ///  EN:
        ///    Gets or sets the message of the exception.
        ///    
        ///  BG:
        ///    Достъпва съобщението за грешка.
        /// 
        /// </summary>
        public string ErrorMessage
        {
            get => _msg ??= DEFMSG;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                ArgumentNullException.ThrowIfNullOrEmpty(value);
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

                //
                // Pattern for validating the message syntax and language dependencies.
                //
                // Шаблон (регулярен израз) за валидиране на синтаксиса на съобщението.
                // Трябва да започва с главна буква и може да има или . или ! или ? накрая.
                //
                string validationPattern = @"(?:[A-Z][a-z]+)(?: [A-Za-z]+)*(?:[!?.])?";

                bool isValid = Regex.IsMatch(value, validationPattern);

                if (isValid)
                {
                    _msg = value;
                }
                else
                {
                    throw new SyntaxException("The syntax of the message is invalid.");
                }
            }
        }


        /// <summary>
        /// 
        /// EN:
        ///   Creates new InvalidDiapasonException with the default message for the user.
        /// 
        /// BG:
        ///   Създава нов InvalidDiapasonException със съобщение по подразбиране за потребителя.
        /// 
        /// </summary>
        public InvalidDiapasonException()
            => _msg = DEFMSG;

        /// <summary>
        /// 
        /// EN:
        ///   Creates new InvalidDiapasonException with the specified message for the user.
        /// 
        /// BG:
        ///   Създава нов InvalidDiapasonException със указаното съобщение за потребителя.
        /// 
        /// <param name="message">
        ///  EN: The message to be shown.
        ///  BG: Съобщението, което да бъде показано.
        /// <param/>
        public InvalidDiapasonException(string message)
            => ErrorMessage = message;
    }
}
