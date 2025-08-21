//  CommonLibrary - library for common usage.
//  CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Exceptions
{
    /// <summary>
    /// 
    ///  EN:
    ///    Occurs when the data type of some class member(field, property..) is not in the correct
    ///    format that is needed.
    ///  
    /// BG:
    ///   Индикира, когато типа данни на даден член на класа (поле, свойство ..) не е този който е необходим
    ///   в текущата ситуация.
    ///  
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Occurs when the data tpe of the given value is not in the correct(needed) format")]
    public class TypeNotSupportedException : Exception
    {
        //The message that exception shows, when is thrown.
        //
        //Съобщението за грешка, което се показва.
        private string? _message;

        //The default message, that is shown,
        //when the same is not defined.
        //
        //Съобщение по подразбиране, което да се покаже ако съобщението не е зададено.
        private const string DefaultErrorMessage = "This data type is not supported for the current operation.";


        /// <summary>
        /// 
        ///  EN: Get or set the message that exception shows.
        ///  
        ///  BG: Достъпва съобщението за грешка.
        /// 
        /// </summary>
        public string ErrorMessage
        {
            get => _message!;
            set
            {
                ArgumentNullException.ThrowIfNullOrEmpty(value);
                _message = value;
            }
        }


        /// <summary>
        /// 
        ///  EN: Create new TypeNotSupportedException with default error message.
        ///  
        ///  BG: Създава нов TypeNotSupportedException със съобщение по подразбиране.
        /// 
        /// </summary>
        public TypeNotSupportedException()
            => _message = DefaultErrorMessage;

        /// <summary>
        /// 
        ///  EN: Create new TypeNotSupportedException with specified error message
        ///  
        ///  BG: Създава нов TypeNotSupportedException със зададено съобщение за грешка.
        /// 
        /// </summary>
        /// 
        /// <param name="message">
        /// EN: The error message that the exception will show.
        /// BG: Съобщението за грешка, което да се покаже.
        /// </param>
        public TypeNotSupportedException(string message)
            => _message = message;
    }
}
