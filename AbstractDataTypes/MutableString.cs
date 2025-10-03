// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Linq;
using System.Collections;
using CommonLibrary.Enums;
using CommonLibrary.Helpers;
using CommonLibrary.Exceptions;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using System.Collections.Generic;

namespace CommonLibrary.AbstractDataTypes
{
    /// <summary>
    /// 
    /// EN:
    ///   Defines mutable string, represented as array of characters with dynamic size.
    ///   Provides methods for manipulating MutableString.
    ///   
    /// BG:
    ///   Променяем стринг. Представен е на системно ниво като масив от символи с динамичен
    ///   размер. Предоставя набор от методи за манипулиране на MutableString.
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Usage("Used in case that the user needs to manipulates a string too many times.")]
    public class MutableString : IEnumerable<char>, ICloneable
    {
        //
        // The array of the string characters.
        //
        // Масива със сиволите на стринга.
        //
        private DynamicArray<char> _symbols;

        //
        // Holds the complete value of the mutable string. This is the sequence of
        // the symbols from the source array as string.
        //
        // Съхранява цялостната стойност на стринга. Това са символите от масива
        // на стринга представени като стринг.
        //
        private readonly string _completeString;

        //
        // The default symbol that the indexer's accessor will show if the string is empty.
        //
        // Символа по подразбиране, който аксесора на индексатора ще върне, когато стринга е празен.
        //
        private const char DefaultSymbol = '\0';

        //
        // The default capacity of the mutable string is ten symbols.
        //
        // Капацитета по подразбиране на променяемия стринг е десет символа.
        // Капацитета се удвоява, когато е достигнат.
        //
        private const int DefaultCapacity = 10;

        //
        // Maximum capacity of the string is one bilion characters.
        //
        // Максимален капацитет на стринга от един милиард символа.
        //
        private const int MaxCapacity = 1_000_000_000;


        /// <summary>
        ///  
        /// EN:
        ///    Indexer
        ///    
        /// BG:
        ///   Индексатор.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        ///  EN: The index of the element in the string.
        ///  BG: Индекса на символа в стринга.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The symbol at that index.
        ///  BG: Символа на този индекс.
        /// </returns>
        public char this[int index]
        {
            get => _symbols!.Count == 0 ? DefaultSymbol : _symbols[index];
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);

                if (_symbols!.Count == 0)
                {
                    _symbols.Add(value);
                }
                else
                {
                    _symbols[index] = value;
                }
            }
        }

        /// <summary>
        /// 
        /// EN:
        ///    Get the length of the string
        ///    
        /// BG:
        ///   Достъпва дължината на стринга - броя на символите в него.
        /// 
        /// </summary>
        public int Length
        {
            get => _symbols!.Count;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets or sets the capacity of the mutable string.
        ///   
        /// BG:
        ///   Достъпва капацитета на променяемия стринг.
        /// 
        /// </summary>
        public int Capacity
        {
            get => _symbols.Capacity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);

                if (_symbols.Count == 0)
                {
                    if (value > DefaultCapacity)
                    {
                        _symbols.Capacity = value;
                    }
                    else
                    {
                        _symbols.Capacity = DefaultCapacity;
                    }
                }
                else
                {
                    if (value < _symbols.Count)
                    {
                        throw new Error("The capacity can not be less that the actual count of the elements.");
                    }
                    else if (value <= DefaultCapacity)
                    {
                        _symbols.Capacity = DefaultCapacity;
                    }
                                        
                    _symbols.Capacity = value;                  
                }
            }
        }

        //
        // Get the value of the string.
        // 
        // Достъпва стойността на стринга.
        //
        private string Text
            => BuildString();
       


        /// <summary>
        /// 
        /// EN:
        ///   Creates new empty MutableString with the default capacity.
        ///   
        /// BG:
        ///   Създава нов празен променяем стринг със капацитет по подразбиране.
        /// 
        /// </summary>
        public MutableString()
        {
            _symbols = new(DefaultCapacity);
            _completeString = BuildString();
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new MutableString with the text from another string.
        ///   
        /// BG:
        ///   Създава нов променяем стринг със символите от указания стринг.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text.
        ///  BG: Текста.
        /// </param>
        public MutableString(string text)
        {
            _symbols = CreateFromExtern(text);
            _completeString = BuildString();
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new MutableString with the specified capacity.
        ///   
        /// BG:
        ///   Създава нов променяем стринг с указания капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        ///  EN: The capacity.
        ///  BG: Капацитета.
        /// </param>
        public MutableString(int capacity)
            : this()
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new MutableString with the maximum capacity.
        ///   If the boolean variable "MaxCapacity" is false, the string
        ///   will be created with the default capacity.
        ///   
        /// BG:
        ///   Създава нов променяем стринг с максимален капацитет.
        ///   Когато стойността на булевата променлива "MaxCapacity"
        ///   е false, стринга ще се създаде с капацитет по подразбиране.
        /// 
        /// </summary>
        /// 
        /// <param name="maxCapacity">
        ///  EN: Indicates when to use the maximum capacity.
        ///  BG: Индикира кога да се използва максималния капацитет.
        /// </param>
        public MutableString(bool maxCapacity)
            : this()
        {
            if (maxCapacity)
            {
                Capacity = MaxCapacity;
            }
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new MutableString with the specified text and capacity.
        ///   
        /// BG:
        ///   Създава нов променяем стринг с указаните текст и капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text.
        ///  BG: Текста.
        /// </param>
        /// 
        /// <param name="capacity">
        ///  EN: The capacity.
        ///  BG: Капацитета.
        /// </param>
        public MutableString(string text, int capacity)
            : this(text)
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new MutableString with the specified text and 
        ///   maximum capacity.
        ///   
        /// BG:
        ///   Създава нов променяем стринг с указаните текст 
        ///   и максимален капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text.
        ///  BG: Текста.
        /// </param>
        /// 
        /// <param name="maxCapacity">
        ///  EN: Indicates when to use the maximum capacity.
        ///  BG: Индикира кога да се използва максималния капацитет.
        /// </param>
        public MutableString(string text, bool maxCapacity)
            : this(text)
        {
            if (maxCapacity)
            {
                Capacity = MaxCapacity;
            }
        }


        /// <summary>
        /// 
        ///  EN:
        ///    Adds the symbols to the array of symbols of the string.
        ///    
        /// BG:
        ///   Добавя текущия символ в масива от символи на стринга.
        /// 
        /// </summary>
        /// 
        /// <param name="symbol">
        ///  EN: The symbol to be added.
        ///  BG: Символа, който да се добави в колекцията.
        /// </param>
        public void Concatenate(char symbol)
            => _symbols.Add(symbol);

        /// <summary>
        /// 
        /// EN:
        ///   Adds the array to the end of the symbols array of the string.
        ///   
        /// BG:
        ///   Добавя масива на края на системния масив от символи на стринга.
        /// 
        /// </summary>
        /// 
        /// <param name="array">
        ///  EN: The array to be added.
        ///  BG: Масива, който да бъде добавен.
        /// </param>
        public void Concatenate(IEnumerable<char> array)
            => _symbols.AddMany(array);

        /// <summary>
        /// 
        /// EN:
        ///   Adds multiple symbols to the end of the symbols array 
        ///   of the string.
        ///   
        /// BG:
        ///   Добавя множество символи на края на системния масив от 
        ///   символи на стринга.
        /// 
        /// </summary>
        /// 
        /// <param name="chars">
        ///  EN: The array to be added.
        ///  BG: Масива, който да бъде добавен.
        /// </param>
        public void Concatenate(params char[] chars)
            => _symbols.AddMany(chars);

        /// <summary>
        /// 
        /// EN:
        ///   Removes the text from the string.
        ///   
        /// BG:
        ///   Премахва текста от стринга, ако се съдържа в него.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        ///  EN: The text to be removed.
        ///  BG: Текста, който да бъде премахнат.
        /// </param>
        public void Remove(string value)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(value);

            if (Text.Contains(value))
            {
                int start = Text.IndexOf(value);
                int end = start + value.Length - 1;

                _symbols.RemoveInDiapason(start, end);
            }
        }
        
        /// <summary>
        /// 
        /// EN:
        ///   Removes a symbol from the string by his index.
        ///   
        /// BG:
        ///   Премахва символ от стринга по неговия индекс.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        ///  EN: The index.
        ///  BG: Индекса.
        /// </param>
        public void RemoveAt(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index > _symbols.Count)
            {
                throw new Error("The index can not be greater than the elements count.");
            }

            _symbols.RemoveByIndex(index);
        }

        /// <summary>
        /// 
        /// EN:
        ///   Removes all the symbols in the given diapason in the array.
        ///   
        /// BG:
        ///   Премахва всички символи в указания диапазон в масива на стринга.
        /// 
        /// </summary>
        /// 
        /// <param name="startIndex">
        ///  EN: The beginning of the diapason.
        ///  BG: Началото на диапазона.
        /// </param>
        /// 
        /// <param name="endIndex">
        /// EN: The end of the diapason.
        /// BG: Края на диапазона.
        /// </param>
        public void RemoveInRange(int startIndex, int endIndex)
            => _symbols.RemoveInDiapason(startIndex, endIndex);

        /// <summary>
        /// 
        /// EN:
        ///   Removes all the symbols from the string.
        ///   
        /// BG:
        ///   Премахва всички символи от стринга.
        /// 
        /// </summary>
        public void Clear()
            => _symbols.RemoveAll();

        /// <summary>
        /// 
        /// EN:
        ///   Inserts the text at the given index in the string.
        ///   
        /// BG:
        ///   Вмъква текста на указания индекс в стринга.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        ///  EN: The index of the symbol.
        ///  BG: Индекса на символа.
        /// </param>
        /// 
        /// <param name="value">
        ///  EN: The symbol to be inserted.
        ///  BG: Символа, който да бъде вмъкнат.
        /// </param>
        public void InsertAt(int index, string value)
            => _symbols.InsertMany(index, value);

        /// <summary>
        /// 
        /// EN: 
        ///   Splits the text by the given separator in array of strings by the
        ///   specified split options.
        ///   Use SplitType.NoEmptyString to remove all the empty entries that occur 
        ///   somethimes when splittin with complicated separator.
        /// 
        /// BG:
        ///   Разделя указания текст по указания разделител в масив
        ///   от стрингове според указаната опция за разделяне.
        ///   Използвай флага NoEmptyStrings на еномерациятя SplitType за да
        ///   премахнеш всички празни стрингове, който се получават понякога при
        ///   разделяне на стринга със по-сложен разделител.
        ///   Използвай флага KeepEmptyString на еномерацията SplitType за да
        ///   запазиш празните стрингове в резултата.
        /// 
        /// </summary>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  EN: Specifies the options for splitting.
        ///  BG: Указва опцийте за разделяне.
        /// </param>
        public string[] SplitBy(string separator, SplitType splitType)
            => SplitByInstance(Text, separator, splitType);

        /// <summary>
        /// 
        /// EN: 
        ///   Splits the text by the given separator in array of mutable strings 
        ///   by the specified split options.
        ///   Use SplitType.NoEmptyString to remove all the empty entries that occur 
        ///   somethimes when splittin with complicated separator.
        /// 
        /// BG:
        ///   Разделя указания текст по указания разделител в масив
        ///   от променяеми стрингове според указаната опция за разделяне.
        ///   Използвай флага NoEmptyStrings на еномерациятя SplitType за да
        ///   премахнеш всички празни стрингове, който се получават понякога при
        ///   разделяне на стринга със по-сложен разделител.
        ///   Използвай флага KeepEmptyString на еномерацията SplitType за да
        ///   запазиш празните стрингове в резултата.
        /// 
        /// </summary>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  EN: Specifies the options for splitting.
        ///  BG: Указва опцийте за разделяне.
        /// </param>
        public MutableString[] SplitAsMutable(string separator, SplitType splitType)
        {
            string[] pieces = SplitByInstance(Text, separator, splitType);
            MutableString[] result = new MutableString[pieces.Length];
            int index = default;

            foreach (string piece in pieces)
            {
                result[index++] = piece;
            }

            return result;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Concatenates collection of strings by the specified separator.
        /// 
        /// BG:
        ///   Обединява колекция от стрингове в един промеяем стринг, като ги разделя
        ///   с указания разделител.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        ///  EN: The collection / array with the strings.
        ///  BG: Колекцията / масива със стринговете..
        /// </param>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The string joined with the separator in one mutable string.
        ///  BG: Стринговете обединени със разделителя в един променяем стринг.
        /// </returns>
        public static MutableString JoinBy(string separator, IEnumerable<string> collection)
            => JoinBySeparator(collection, separator);

        /// <summary>
        /// 
        /// EN:
        ///   Concatenates collection of strings by the specified separator.
        /// 
        /// BG:
        ///   Обединява колекция от стрингове в един промеяем стринг, като ги разделя
        ///   с указания разделител.
        /// 
        /// </summary>
        /// 
        /// <param name="values">
        ///  EN: The collection / array with the strings.
        ///  BG: Колекцията / масива със стринговете..
        /// </param>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The string joined with the separator in one mutable string.
        ///  BG: Стринговете обединени със разделителя в един променяем стринг.
        /// </returns>
        public static MutableString JoinBy(string separator, params string?[] values)
            => JoinBySeparator(values!, separator);

        /// <summary>
        ///  
        /// EN:
        ///   Splits the specified text by the specified separator
        ///   by a specified split option in a array of strings.
        /// 
        /// BG:
        ///   Разделя указания текст по указания сепаратор по указаните
        ///   опций в масив от стрингове.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text.
        ///  BG: Текста.
        /// </param>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <param name="option">
        ///  EN: The split option:
        ///  BG: Опцийте за разделяне.
        /// </param>
        public static string[] SplitBy(string text, string separator, SplitType option)
            => SplitByStatic(text, separator, option);

        /// <summary>
        ///  
        /// EN:
        ///   Splits the specified text by the specified separator
        ///   by a specified split option in a array of mutable strings.
        /// 
        /// BG:
        ///   Разделя указания текст по указания сепаратор по указаните
        ///   опций в масив от променяеми стрингове.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The text.
        ///  BG: Текста.
        /// </param>
        /// 
        /// <param name="separator">
        ///  EN: The separator.
        ///  BG: Разделителя.
        /// </param>
        /// 
        /// <param name="option">
        ///  EN: The split option:
        ///  BG: Опцийте за разделяне.
        /// </param>
        public static MutableString[] SplitAsMutable(string text, string separator, SplitType option)
        {
            string[] pieces = SplitByStatic(text, separator, option);
            MutableString[] piecesAsMutable = new MutableString[pieces.Length];
            int index = default;

            foreach (string piece in pieces)
            {
                piecesAsMutable[index++] = piece; 
            }

            return piecesAsMutable;
        }

        /// <summary>
        ///  
        /// EN:
        ///   Converts the array of symbols to a string.
        ///  
        /// BG: 
        ///   Преобразува масива от символи в стринг.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: The string representation of the class.
        /// BG: Стринга.
        /// </returns>
        public override string ToString()
            => BuildString();

        /// <summary>
        /// 
        /// EN:
        ///   Converts the mutable string to array of characters.
        ///   
        /// BG:
        ///   Преобразува променяемия стринг в масив от символи.
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: Array with the symbols of the string.
        ///  BG: Масив със символите от стринга.
        /// </returns>
        public char[] ReturnAsArray()
            => [.. Text];

        /// <summary>
        /// 
        /// EN:
        ///   Compare the mutable string with other object.
        ///   
        /// BG:
        ///   Сравнява променяемия стринг с друг обект.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        ///  EN: The other object to compare the string with.
        ///  BG: Другия обект, с който да се сравни стринга.
        /// </param>
        /// 
        /// <returns>
        ///  EN: True of the string and the object are equals, otherwise false.
        ///  BG: True ако стринга и обекта са равни, ако не false.
        /// </returns>
        public override bool Equals(object? value)
        {
            if (ReferenceEquals(this, value))
            {
                return true;
            }

            if (value is null || this is null)
            {
                return false;
            }

            return value is MutableString other && this == other;
        }

        /// <summary>
        ///  
        ///  EN:
        ///    Returns the hash code of the instance.
        ///    
        /// BG:
        ///   Връща хеш кода на инстанцията.
        /// 
        /// </summary>
        public override int GetHashCode()
            => BuildString().GetHashCode();

        /// <summary>
        ///  
        /// EN:
        ///   Clones the mutable string to another.
        ///   
        /// BG:
        ///   Клонира променяемия сринг, като това става възможно като 
        ///   текущия стринг (този клас) се върне като обект, който в по
        ///   късен етап да бъде кастнат (изрично конвертиран) към MutableString.
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: The mutable string as object.
        ///  BG: Връща променяемия стринг като обект.
        /// </returns>
        public object Clone()
            => this;


        //
        // Builds the string.
        //
        // Изгражда стринга от символите му.
        //
        private string BuildString()
            => new(_symbols!.ToArray());

        //
        // Copys symbols of the extern string in the local array of symbols.
        //
        // Копира символите на указания текст в локалния масив от символи на 
        // стринга.
        //
        private DynamicArray<char> CreateFromExtern(string externString)
            => _symbols = [.. externString];

        //
        // Splits the specified text by the specified separator
        // in a array of strings.
        //
        // Разделя указания текст по указания сепаратор в масив
        // от стрингове.
        //
        private string[] SplitByInstance(string text, string separator, SplitType type)
        { 
            ArgumentNullException.ThrowIfNullOrEmpty(text);
            ArgumentNullException.ThrowIfNull(separator);

            Collection<string> pieces = [];

            if (separator == string.Empty)
            {
                CollectionHelper.ExecuteOnEachElement(symbol => pieces.Add(symbol.ToString()), text);
                return [.. pieces];
            }

            while (text != string.Empty)
            {
                bool hasSeparator = text.Contains(separator);

                if (hasSeparator)
                {
                    string piece = text.Substring(0, text.IndexOf(separator));
                    pieces.Add(piece);
                    text = text.Remove(0, text.IndexOf(separator) + separator.Length);
                }
                else
                {
                    if (text != string.Empty)
                    {
                        pieces.Add(text);
                        text = string.Empty;
                    }
                }
            }

            if (type == SplitType.KeepEmptyStrings)
            {
                return [.. pieces];
            }
            else if (type == SplitType.NoEmptyStrings)
            {
                while (pieces.ContainsElement(string.Empty))
                {
                    pieces.RemoveElement(string.Empty);
                }
            }

            return [.. pieces];
        }

        //
        // Splits the specified text by the specified separator
        // in a array of strings. This is base algorithm for the static method SpltBy().
        //
        // Разделя указания текст по указания сепаратор в масив
        // от стрингове. Това е базов алгоритъм за статичния метотд SplitBy().
        //
        private static string[] SplitByStatic(string text, string separator, SplitType option)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(text);
            ArgumentNullException.ThrowIfNull(separator);

            Collection<string> pieces = [];

            while (text != string.Empty)
            {
                bool hasSeparator = text.Contains(separator);

                if (hasSeparator)
                {
                    string piece = text.Substring(0, text.IndexOf(separator));
                    pieces.Add(piece);
                    text = text.Remove(0, text.IndexOf(separator) + separator.Length);
                }
                else
                {
                    if (text != string.Empty)
                    {
                        pieces.Add(text);
                        text = string.Empty;
                    }
                }
            }

            if (option == SplitType.KeepEmptyStrings)
            {
                return [.. pieces];
            }
            else if (option == SplitType.NoEmptyStrings)
            {
                while (pieces.ContainsElement(string.Empty))
                {
                    pieces.RemoveElement(string.Empty);
                }
            }

            return [.. pieces];
        }

        // 
        // Concatenates collection of strings by the specified separator.
        //
        // Обединява колекция от стрингове в един промеяем стринг, като ги разделя
        // с указания разделител.
        //
        private static MutableString JoinBySeparator(IEnumerable<string> collection, string separator)
        {
            MutableString result = new();
            int counter = default;
            
            foreach (string value in collection)
            {
                ++counter;
                result.Concatenate(value);
                
                if (!(counter >= collection.Count()))
                {
                    result.Concatenate(separator);
                }
            }

            return result;
        }


        /// <inheritdoc/>
        public IEnumerator<char> GetEnumerator()
            => (IEnumerator<char>)_symbols.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();


        /// <summary>
        /// 
        ///  EN:
        ///    The implicit conversion allows the user to enter a text and 
        ///    automatically to be converted to a MutableString. That's means
        ///    the MutableString can accept values by operator "=" (equals).
        ///    
        /// BG:
        ///   Вътрешната конверсия позволява на потребител да присвой стойност на 
        ///   променяемия стринг директо със оператор "=" (равно). Компилатора
        ///   преубразува автоматично нормалия стринг в променяем стринг.
        /// 
        /// </summary>
        /// 
        /// <param name="text">
        ///  EN: The normal string.
        ///  BG: Стандартния стринг (текста).
        /// </param>
        public static implicit operator MutableString(string text)
            => new(text);

        /// <summary>
        /// 
        ///  EN:
        ///    Comparsion operators. Allows the user to compare two MutableStrings.
        ///    
        ///  BG: 
        ///    Оператори за сравнение. Позволяват на потребител да сравни два
        ///    променяеми стринга.
        ///    
        /// </summary>
        public static bool operator ==(MutableString left, MutableString right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.ToString() == right.ToString();
        }

        public static bool operator !=(MutableString left, MutableString right)
            => !left.Equals(right);

        public static bool operator <(MutableString left, MutableString right)
            => string.Compare(left.ToString(), right.ToString()) < 0;

        public static bool operator >(MutableString left, MutableString right)
            => string.Compare(left.ToString(), right.ToString()) > 0;

        public static bool operator <=(MutableString left, MutableString right)
           => string.Compare(left.ToString(), right.ToString()) <= 0;

        public static bool operator >=(MutableString left, MutableString right)
           => string.Compare(left.ToString(), right.ToString()) >= 0;
    }
}