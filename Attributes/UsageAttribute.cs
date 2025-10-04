// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using System.Text.RegularExpressions;

namespace CommonLibrary.Attributes
{
    /// <summary>
    ///  
    ///  EN: 
    ///    Specifies the usage of that member/data type.
    ///    
    ///  BG:
    ///    Указва за какво се използва този член или тип данни.
    ///  
    /// </summary>
    [Description("Provides information about the usage of that part of the code")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class UsageAttribute : Attribute
    {
        //
        // Holds the text of the attribute.
        //
        // Съхранява текста на атрибута.
        // Описанието на члена/типа данни.
        //
        private string? _msg;


        /// <summary>
        /// 
        /// EN:
        ///   Creates new UsageAttribute with the specified definition.
        ///   
        /// BG:
        ///   Създава нов UsageAttribute с указаната дефиниция.
        /// 
        /// </summary>
        /// 
        /// <param name="definition">
        ///  EN: The definition.
        ///  BG: Дефиницията.
        /// 
        /// </param>
        public UsageAttribute(string definition)
            => ValidateDefinition(ref definition);


        //
        // Validates the definition before updating the private field.
        //
        // Валидира дефиницията с регулярен израз преди да обнови стойността на 
        // частното поле.
        //
        private void ValidateDefinition(ref string def)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(def);
            ArgumentNullException.ThrowIfNullOrEmpty(def);

            //
            // Pattern for validating the definition syntax.
            //
            // Шаблон (регулярен израз) за валидиране на синтаксиса на дефиницията.
            // Трябва да започва с главна буква и може да има или . или ! накрая.
            //
            string pattern = @"(?:[A-Z][a-z]+)(?: [A-Za-z]+)*(?:[!.])?";

            bool isValid = Regex.IsMatch(def, pattern);

            if (isValid)
            {
                _msg = def;
            }
            else
            {
                throw new SyntaxException("Invalid syntax of the member/data type definition.");
            }
        }
    }
}
