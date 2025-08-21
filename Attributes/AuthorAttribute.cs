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
    ///    Specifies the author of the code.
    ///  
    ///  BG: 
    ///    Указва автора на кода.
    /// 
    /// </summary>
    [Description("Specifies the author of that part of the code")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class AuthorAttribute : Attribute
    {
        //
        // The name of the author
        //
        // Името на автора
        //
        private string? _name;


        /// <summary>
        /// 
        ///  EN: Get or set the name of the author.
        /// 
        ///  BG: Достъпва името на автора.
        /// 
        /// </summary>
        public string Name
        {
            get => _name ??= string.Empty;
            set
            {
                ArgumentNullException.ThrowIfNullOrEmpty(value);
                ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

                // EN:
                // Pattern for validating the name.
                // The name should start with uppercase letter and every next should be lowercase and at all should
                // contains only valid letters - no other symbols are allowed.
                // Maximum three names are allowed, and which name should match the definition above.
                // 
                // BG:
                // Шаблон (регулярен израз) за валидиране на името на автора.
                // Името трябва да започва с главна буква и всяка следваща трябва да е малка. Трябва
                // да съдържа само валидни символи(букви ..).
                // Най - много три имена са допустими, като всяко трябва да отгораря на условията
                // и да са разделени с интервал.
                string validNamePattern = @"(?:(?:[A-Z][a-z]+)(?: (?:[A-Z][a-z]+))?(?:[A-Z][a-z]+)*)";

                bool isValidName = Regex.IsMatch(value, validNamePattern);

                if (isValidName)
                {
                    _name = value;
                }
                else
                {
                    throw new SyntaxException("Invalid syntax of the name.");
                }
            }
        }


        /// <summary>
        /// 
        ///  EN: Creates new Author attribute
        ///  
        ///  BG: Създава нов Author атрибут.
        /// 
        /// </summary>
        /// 
        /// <param name="name">
        /// EN: The name of the author.
        /// BG: Името на автора.
        /// </param>
        public AuthorAttribute(string name)
            => Name = name;
    }
}