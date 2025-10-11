// CommonLibrary - library for common usage.

namespace CommonLibrary.AbstractDataTypes
{
    using System.Collections.Generic;
    using CommonLibrary.Exceptions;
    using System.ComponentModel;
    using CommonLibrary.Enums;
    using System.Diagnostics;
    using System.Collections;
    using System.Text;
    using System.Linq;
    using System;

    /// <summary>
    ///  The Text represent a data type for text with the same characteristics
    ///  as a String but with the difference that the Text is mutable and uses 
    ///  a list for buffer. The Text provides faster operation because there is no
    ///  creation of a new text every time when concatenating, removing, replacing etc.
    /// </summary>
    [Description("Data type for text that is mutable and with fast operations")]
    [DebuggerDisplay("{BuildText().ToString(), nq}")]
    public sealed class Text : IComparable<Text>, IEnumerable<char>, IEnumerable, ICloneable, IDisposable
    {
        // The symbols of the text.
        private List<char> symbols;

        // The default symbol.
        private const char DefaultSymbol = '\0';


        /// <summary>
        ///  Defines empty text.
        /// </summary>
        public static readonly Text EmptyText = "";

        /// <summary>
        ///  Defines a white space.
        /// </summary>
        public static readonly Text WhiteSpace = " ";

        /// <summary>
        ///  Defines a new line character.
        /// </summary>
        public static readonly Text NewLine = "\n";


        /// <summary>
        ///  Gets or sets the symbol at the specified index in the text.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index of the symbol in the text.
        /// </param>
        /// 
        /// <returns>
        ///  The symbol at the specified index in the text.
        /// </returns>
        public char this[int index]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                
                if (index > this.Length - 1)
                {
                    throw new Error("The index can not be greater that the actual count of the elements.");
                }

                if (this.symbols.Count == 0)
                {
                    return DefaultSymbol; // If the text has no symbols the indexer will return the default symbol.
                }

                return this.symbols[index];
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);

                if (index > this.Length - 1)
                {
                    throw new Error("The index can not be greater than the actual count of the elements.");
                }

                if (this.symbols.Count == 0)
                {
                    this.symbols.Add(value);
                }
                else
                {
                    this.symbols[index] = value;
                }
            }
        }

        /// <summary>
        ///  Gets the count of the symbols of the text.
        /// </summary>
        public int Length
            => this.symbols.Count;

        /// <summary>
        ///  Gets the array with the symbols of the text.
        /// </summary>
        public char[] Symbols
            => this.symbols.ToArray();


        /// <summary>
        ///  Creates new text with the symbols from the specified array.
        ///  The constructor is public but it is preferred to use the operator
        ///  '=' to accept value. 
        /// </summary>
        /// 
        /// <param name="array">
        ///  The array with the symbols for the text.
        /// </param>
        public Text(IEnumerable<char> array)
            => this.symbols = [.. array];
        
        /// <summary>
        ///  The finalizer is used to release all the resources
        ///  of the text.
        /// </summary>
        ~Text()
            => this.Dispose();


        /// <summary>
        ///  Concatenates the specified text with the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be appendet to the current text.
        /// </param>
        public void Concatenate(string text)
            => CombineWithString(text);

        /// <summary>
        ///  Concatenates the specified text with the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be appendet to the current text.
        /// </param>
        public void Concatenate(Text text)
            => CombineWithString(text.ToString());

        /// <summary>
        ///  Concatenates the specified symbol with the current text.
        /// </summary>
        /// 
        /// <param name="symbol">
        ///  The symbol to be appendet to the current text.
        /// </param>
        public void Concatenate(char symbol)
            => CombineWithString(symbol.ToString());

        /// <summary>
        ///  Removes the specified text from the current text, if the
        ///  specified text exists in the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be removed.
        /// </param>
        public void Remove(string text)
            => RemoveText(text);

        /// <summary>
        ///  Removes the specified text from the current text, if the
        ///  specified text exists in the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be removed.
        /// </param>
        public void Remove(Text text)
            => RemoveText(text.ToString());

        /// <summary>
        ///  Removes the symbols in the specified range 
        ///  from the text.
        /// </summary>
        /// 
        /// <param name="start">
        ///  The starting index.
        /// </param>
        /// 
        /// <param name="end">
        ///  The ending index.
        /// </param>
        public void Remove(int start, int end)
            => RemoveInDiapason(start, end);

        /// <summary>
        ///  Removes a symbol from the text at the specified index.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index of the symbol to be removed.
        /// </param>
        public void Remove(int index)
            => RemoveSymbolAt(index);

        /// <summary>
        ///  Checks if the specified text exists in the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text to be checked.
        /// </param>
        /// 
        /// <returns>
        ///  True if the current text contains the specified text,
        ///  otherwise False.
        /// </returns>
        public bool ContainsText(string text)
            => CheckFor(text);

        /// <summary>
        ///  Checks if the specified symbol exists in the current text.
        /// </summary>
        /// 
        /// <param name="symbol">
        ///  The symbol to be checked.
        /// </param>
        /// 
        /// <returns>
        ///  True if the current text contains the specified symbol,
        ///  otherwise False.
        /// </returns>
        public bool ContainsText(char symbol)
            => CheckFor(symbol.ToString());

        /// <summary>
        ///  Clears the text.
        /// </summary>
        public void Clear()
            => this.symbols.Clear();

        /// <summary>
        ///  Inserts the specified text at the specified intex in the 
        ///  current text.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index in the current text.
        /// </param>
        /// 
        /// <param name="text">
        ///  The text to be inserted at that index.
        /// </param>
        public void Insert(int index, string text)
            => InsertTextAt(text, index);

        /// <summary>
        ///  Inserts the specified text at the specified intex in the 
        ///  current text.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index in the current text.
        /// </param>
        /// 
        /// <param name="text">
        ///  The text to be inserted at that index.
        /// </param>
        public void Insert(int index, Text text)
            => InsertTextAt(text.ToString(), index);

        /// <summary>
        ///  Inserts the specified collection of symbols at the specified intex in the 
        ///  current text.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index in the current text.
        /// </param>
        /// 
        /// <param name="array">
        ///  The array of symbols to be inserted at that index.
        /// </param>
        public void Insert(int index, IEnumerable<char> array)
            => InsertCollectionAt(array, index);

        /// <summary>
        ///  Concatenates the symbols from the text with the specified
        ///  separator between each of them.
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        public Text JoinBy(string separator)
            => JoinSymbolsBy(separator.ToString());

        /// <summary>
        ///  Concatenates the symbols from the text with the specified
        ///  separator between each of them.
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        public Text JoinBy(Text separator)
            => JoinSymbolsBy(separator);

        /// <summary>
        ///  Concatenates the symbols from the text with the specified
        ///  separator between each of them.
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        public Text JoinBy(char separator)
            => JoinSymbolsBy(separator.ToString());

        /// <summary>
        ///  Splits the text by the given separator and uses the 
        ///  specified splitting type. To specify the split type you
        ///  should use a flag from the SplitType enumeration as follows:
        ///  - Use flag ClearOutput to specify that the the empty strings
        ///  created sometimes when splitting should be removed from the
        ///  output.
        ///  - Use flag KeepAllEntries to specify that the empty string should be
        ///  left in the output.  
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  The type of splitting.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the splatted text.
        /// </returns>
        public Text[] SplitBy(char separator, SplitType splitType = SplitType.ClearOutput)
            => SplitTextBy(separator.ToString(), splitType);

        /// <summary>
        ///  Splits the text by the given separator and uses the 
        ///  specified splitting type. To specify the split type you
        ///  should use a flag from the SplitType enumeration as follows:
        ///  - Use flag ClearOutput to specify that the the empty strings
        ///  created sometimes when splitting should be removed from the
        ///  output.
        ///  - Use flag KeepAllEntries to specify that the empty string should be
        ///  left in the output.  
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  The type of splitting.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the splatted text.
        /// </returns>
        public Text[] SplitBy(string separator, SplitType splitType = SplitType.ClearOutput)
            => SplitTextBy(separator, splitType);

        /// <summary>
        ///  Splits the text by the given separator and uses the 
        ///  specified splitting type. To specify the split type you
        ///  should use a flag from the SplitType enumeration as follows:
        ///  - Use flag ClearOutput to specify that the the empty strings
        ///  created sometimes when splitting should be removed from the
        ///  output.
        ///  - Use flag KeepAllEntries to specify that the empty string should be
        ///  left in the output.  
        /// </summary>
        /// 
        /// <param name="separator">
        ///  The separator.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  The type of splitting.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the splatted text.
        /// </returns>
        public Text[] SplitBy(Text separator, SplitType splitType = SplitType.ClearOutput)
            => SplitTextBy(separator.ToString(), splitType);

        /// <summary>
        ///  Splits the text by the given separators.
        ///  To specify the split type you should use a 
        ///  flag from the SplitType enumeration as follows:
        ///  - Use flag ClearOutput to specify that the the empty strings
        ///  created sometimes when splitting should be removed from the
        ///  output.
        ///  - Use flag KeepAllEntries to specify that the empty string should be
        ///  left in the output.  
        /// </summary>
        /// 
        /// <param name="separators">
        ///  The separators for split.
        /// </param>
        /// 
        /// <param name="splitType">
        ///  The type of the splitting.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the splatted text.
        /// </returns>
        public Text[] SplitBy(IEnumerable<string> separators, SplitType splitType = SplitType.ClearOutput)
            => SplitByMany(separators, splitType);

        /// <summary>
        ///  Cuts a piece from the text.
        /// </summary>
        /// 
        /// <param name="start">
        ///  The starting index.
        /// </param>
        /// 
        /// <param name="end">
        ///  The ending index.
        /// </param>
        public Text Cut(int start, int end)
            => CutInDiapason(start, end);

        /// <summary>
        ///  Replaces the symbol in the specified range in the text with the symbols
        ///  from the specified replacement text.
        /// </summary>
        /// 
        /// <param name="startIndex">
        ///  The start index.
        /// </param>
        /// 
        /// <param name="end">
        ///  The ending index.
        /// </param>
        /// 
        /// <param name="replacement">
        ///  The replacement text.
        /// </param>
        public void Replace(int startIndex, int end, Text replacement)
            => ReplaceWith(startIndex, end, replacement);

        /// <summary>
        ///  Replaces the symbol in the specified range in the text with the symbols
        ///  from the specified replacement text.
        /// </summary>
        /// 
        /// <param name="startIndex">
        ///  The start index.
        /// </param>
        /// 
        /// <param name="end">
        ///  The ending index.
        /// </param>
        /// 
        /// <param name="replacement">
        ///  The replacement text.
        /// </param>
        public void Replace(int startIndex, int end, string replacement)
            => ReplaceWith(startIndex, end, replacement);

        /// <summary>
        ///  Trims the white spaces from the beginning or the end of the text.
        ///  The position for the trim operation should be specified with a flag from 
        ///  the TrimOrigin enumeration.
        ///   Use flag Beginning to specify to trim the beginning of the text.
        ///   Use flag End to specify to trim the end of the text.
        ///  Note that if there are more that one white spaces they all will be removed.
        /// </summary>
        /// 
        /// <param name="origin">
        ///  The position for trim.
        /// </param>
        public void Trim(TrimOrigin origin)
            => TrimText(origin);

        /// <summary>
        ///  Finds the index of the first occurence of the first symbol
        ///  of the specified text in the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text which index need to be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the first occurence of the first symbol 
        ///  of the specified text or -1 if there is no mathces.
        /// </returns>
        public int IndexOf(Text text)
            => FindIndexOfFirst(text);

        /// <summary>
        ///  Finds the index of the first occurence of the first symbol
        ///  of the specified text in the current text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text which index need to be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the first occurence of the first symbol 
        ///  of the specified text or -1 if there is no mathces.
        /// </returns>
        public int IndexOf(string text)
            => FindIndexOfFirst(text);

        /// <summary>
        ///  Finds the index of the first occurence of the symbol
        ///  in the current text.
        /// </summary>
        /// 
        /// <param name="symbol">
        ///  The symbol which index need to be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the first occurence of the symbol 
        ///  in the text or -1 if there is no mathces.
        /// </returns>
        public int IndexOf(char symbol)
            => FindIndexOfFirst(symbol);

        /// <summary>
        ///  Finds the index of the last occurense of the first symbol
        ///  of the specified text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text which index should be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the last occurence of the first symbol of the
        ///  specified text or -1 if there are no matches.
        /// </returns>
        public int LastIndexOf(Text text)
            => FindIndexOfLast(text);

        /// <summary>
        ///  Finds the index of the last occurense of the first symbol
        ///  of the specified text.
        /// </summary>
        /// 
        /// <param name="text">
        ///  The text which index should be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the last occurence of the first symbol of the
        ///  specified text or -1 if there are no matches.
        /// </returns>
        public int LastIndexOf(string text)
            => FindIndexOfLast(text);

        /// <summary>
        ///  Finds the index of the last occurense of the symbol
        ///  in the text.
        /// </summary>
        /// 
        /// <param name="symbol">
        ///  The symbol which index should be found.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the last occurence of the symbol or -1 if there are no matches.
        /// </returns>
        public int LastIndexOf(char symbol)
            => FindIndexOfLast(symbol);

        /// <summary>
        ///  Overrides ToString() method to use the text content when using the variable.
        /// </summary>
        public override string ToString()
            => BuildText().ToString();

        /// <summary>
        ///  Returns the text as a StingBuilder.
        /// </summary>
        public StringBuilder ToBuilder()
            => BuildText();

        /// <summary>
        ///  Returns the text as a list of characters.
        /// </summary>
        public List<char> ToCharList()
            => this.symbols;

        /// <summary>
        ///  Converts the text to data stream. The data stream defines
        ///  an array of bytes of the text.
        /// </summary>
        public byte[] ToDataStream()
            => ConvertToStream(); 

        /// <summary>
        ///  Converts the symbols of the text to upper case symbols.
        /// </summary>
        public void ToUpperCase()
            => ConvertToUpper();

        /// <summary>
        ///  Converts the symbols of the text to lower case symbols.
        /// </summary
        public void ToLowerCase()
            => ConvertToLower();

        /// <summary>
        ///  Compare two texts.
        /// </summary>
        /// 
        /// <param name="other">
        ///  The other text to compare to.
        /// </param>
        /// 
        /// <returns>
        ///  Less than zero - this text precedes the other in the sort order.
        ///  Zero - both texts has same order and symbols.
        ///  Greater than zero - this text follows the other in the sort order.
        /// </returns>
        public int CompareTo(Text? other)
        {
            if (other! == null!)
            {
                return 1;
            }

            return string.Compare(this.ToString(), other.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        ///  Compare the text with another text.
        /// </summary>
        /// 
        /// <param name="obj">
        ///  The other text to compare to.
        /// </param>
        /// 
        /// <returns>
        ///  True if bot text are equal, otherwise False.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Text text)
            {
                return this.ToString() == text.ToString();
            }

            if (obj is string txt)
            {
                return this.ToString() == txt;
            }

            return false; // If the object is not String or Text returns false.
        }

        /// <summary>
        ///  GetHashCode method helps the text acting normal in collections like Dictionary
        ///  and HashSet.
        /// </summary>
        /// 
        /// <returns>
        ///  The hash code of the text.
        /// </returns>
        public override int GetHashCode()
            => this.ToString().GetHashCode();

        /// <summary>
        ///  Returns the text as object, so can be cloned or casted.
        /// </summary>
        public object Clone()
            => this;

        /// <summary>
        ///  Indicates to the Garbage Collector not to call the destructor of
        ///  the text(there are no resources to be disposed) to save time and to
        ///  improve the performance.
        /// </summary>
        public void Dispose()
            => GC.SuppressFinalize(this);


        private StringBuilder BuildText()
        {
            StringBuilder text = new();

            foreach (char symbol in this.symbols)
            {
                text.Append(symbol);
            }

            return text;
        }
        private void CombineWithString(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new Error("The text does not exist or it's empty string.");
            }

            this.symbols.AddRange(text.ToArray());
        }
        private void RemoveText(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new Error("The text is null or empty.");
            }

            bool isInto = this
                .ToString()
                .Contains(text);
            
            if (isInto)
            {
                Text result = this
                    .ToString()
                    .Remove(this.ToString().IndexOf(text), text.Length);

                this.symbols.Clear();
                this.symbols.AddRange(result);
            }
            else
            {
                throw new Error("The text does not contains specified text");
            }
        }
        private void RemoveSymbolAt(int index)
        {
            if (index < 0)
            {
                throw new Error("The index can not be negative.");
            }

            if (index > this.Length - 1)
            {
                throw new Error("The index can not be greater than the actual count of the symbols.");
            }

            this.symbols.RemoveAt(index);
        }
        private void RemoveInDiapason(int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex < 0)
            {
                throw new Error("The start index or the end index can not be negative.");
            }

            if (startIndex >= this.symbols.Count || endIndex >= this.symbols.Count)
            {
                string msg = "The starting or the ending indexes can not be greater that the" +
                    " actual count of the symbols of the text.";

                throw new Error(msg);
            }

            if (startIndex > endIndex)
            {
                throw new Error("The starting index can not be greater that the ending index.");
            }

            this.symbols.RemoveRange(startIndex, (endIndex - startIndex) + 1); // Removes the values at the both indexes too.
        }
        private bool CheckFor(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new Error("The text is null or empty.");
            }

            return this
                .ToString()
                .Contains(text);
        }
        private void InsertTextAt(string text, int index)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(text);
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index > this.Length - 1)
            {
                throw new Error("The index can not be greater than the actual count of the symbols.");
            }

            this.symbols.InsertRange(index, text);
        }
        private void InsertCollectionAt(IEnumerable<char> array, int index)
        {
            if (!array.Any())
            {
                throw new Error("Empty array can not be added.");
            }

            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index > this.Length - 1)
            {
                throw new Error("The index can not be greater than the actual count of the symbols.");
            }

            this.symbols.InsertRange(index, array);
        }
        private Text JoinSymbolsBy(Text separator)
        {
            ArgumentNullException.ThrowIfNull(separator);

            Text result = EmptyText;

            foreach (char symbol in this.symbols)
            {
                result.CombineWithString(symbol.ToString());
                result.CombineWithString(separator.ToString());
            }

            return result;
        }
        private Text[] SplitTextBy(string separator, SplitType splitType)
        {
            // If the separator considers only from white spaces you will see them 
            // only if you use SplitType.KeepAllEntries, otherwise the output
            // collection will be empty.
            if (String.IsNullOrEmpty(separator))
            {
                throw new Error("The separator is null or it's empty.");
            }

            if (this.symbols.Count == 0)
            {
                return Array.Empty<Text>();
            }

            bool hasSeparator = BuildText()
                .ToString()
                .Contains(separator);

            if (!hasSeparator)
            {
                throw new Error("The text does not contains that separator.");
            }

            List<string>? entries = [];

            if (splitType == SplitType.KeepAllEntries)
            {
                entries = BuildText()
                    .ToString()
                    .Split(separator)
                    .ToList();
            }
            else if (splitType == SplitType.ClearOutput)
            {
                entries = BuildText()
                    .ToString()
                    .Split(separator)
                    .ToList();

                for (int i = 0; i < entries.Count; i++)
                {
                    if (entries[i] == EmptyText || entries[i] == WhiteSpace)
                    {
                        entries.RemoveAt(i);
                    }
                }
            }

            Text[] resultEntries = new Text[entries.Count];

            for(int i = 0; i < entries.Count; i++)
            {
                resultEntries[i] = new(entries[i]);
            }

            entries = null; // Not needed anymore. Everything now is in the resultEntries array.
            return resultEntries;
        }
        private Text[] SplitByMany(IEnumerable<string> separators, SplitType splitType)
        {
            ArgumentNullException.ThrowIfNull(separators);
            ArgumentNullException.ThrowIfNull(splitType);

            if (!separators.Any())
            {
                throw new Error("No specified separators.");
            }

            bool allAreNull = Array.TrueForAll(separators.ToArray(), separator => separator == null);

            if (allAreNull)
            {
                throw new Error("All the separators are null.");
            }

            if (this.symbols.Count == 0)
            {
                return Array.Empty<Text>();
            }

            List<string>? separatorsArray = separators as List<string>;
            List<string> result = new();
            string completeText = BuildText().ToString();

            if (splitType == SplitType.ClearOutput)
            { 
                // if the text does not contains all the separators will throw error.
                bool hasAllSeparators =
                    Array.TrueForAll(separators.ToArray(), completeText.Contains);

                if (hasAllSeparators)
                {
                    string entry = String.Empty; // Empty in the beginning - will be formed through the loop.

                    foreach (char symbol in completeText)
                    {
                        if (Array.Exists(separators.ToArray(), separator => separator == symbol.ToString()))
                        {
                            if (entry.Length > 0)
                            {
                                result.Add(entry.Trim());
                                entry = string.Empty;
                            }
                        }
                        else
                        {
                            entry += symbol;
                        }
                    }

                    if (entry.Length > 0)
                    {
                        result.Add(entry.Trim());
                    }
                }
                else
                {
                    throw new Error("The text does not contains all separators.");
                }

                // Filtering the entries. Empty entries are removed.
                result = result
                    .Where(entry => entry != String.Empty)
                    .ToList();
            }
            else if (splitType == SplitType.KeepAllEntries)
            {
                bool hasAllSeparators =
                   Array.TrueForAll(separators.ToArray(), completeText.Contains);

                if (hasAllSeparators)
                {
                    string entry = string.Empty; // Empty in the beginning - will be formed through the loop.

                    foreach (char symbol in completeText)
                    {
                        if (Array.Exists(separators.ToArray(), separator => separator == symbol.ToString()))
                        {
                            if (entry.Length > 0)
                            {
                                result.Add(entry.Trim());
                                entry = string.Empty;
                            }
                        }
                        else
                        {
                            entry += symbol;
                        }
                    }

                    if (entry.Length > 0)
                    {
                        result.Add(entry.Trim());
                    }
                }
                else
                {
                    throw new Error("The text does not contains all separators.");
                }
            }

            Text[] resultTextArray = new Text[result.Count];

            for(int i = 0; i < resultTextArray.Length; i++)
            {
                resultTextArray[i] = result[i];
            }

            return resultTextArray;
        }
        private Text CutInDiapason(int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex < 0)
            {
                throw new Error("The starting or the ending indexes can not be negative.");
            }

            if (startIndex > endIndex)
            {
                throw new Error("The starting index can not be greater that the ending index.");
            }

            if (endIndex >= this.symbols.Count || startIndex >= this.symbols.Count)
            {
                string error = "The ending index or the starting index" +
                    " can not be greater that the actual count of the symbols.";
                
                throw new Error(error);
            }

            List<char> pieceSymbols = this.symbols.Slice(startIndex, endIndex - startIndex);
            return new(pieceSymbols);
        }
        private int FindIndexOfFirst(Text text)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (text == EmptyText)
            {
                throw new Error("The specified text is empty.");
            }

            if (this.symbols.Count == 0)
            {
                throw new Error("The current text is empty.");
            }
            
            for (int i = default; i < this.symbols.Count; i++)
            {
                if (this.symbols[i] == text[0]) // Allways compare with the first symbol of the text.
                {
                    return i;
                }
            }

            return -1; // If there are no matches.
        }
        private int FindIndexOfLast(Text text)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (text == EmptyText)
            {
                throw new Error("The specified text is empty.");
            }

            if (this.symbols.Count == 0)
            {
                throw new Error("The current text is empty.");
            }

            for (int i = this.symbols.Count - 1; i >= default(int); i--)
            {
                if (this.symbols[i] == text[0])
                {
                    return i;
                }
            }

            return -1;
        }
        private void ConvertToUpper()
        {
            for (int i = default; i < this.symbols.Count; ++i)
            {
                this.symbols[i] = char.ToUpper(this.symbols[i]);
            }
        }
        private void ConvertToLower()
        {
            for (int i = default; i < this.symbols.Count; i++)
            {
                this.symbols[i] = char.ToLower(this.symbols[i]);
            }
        }
        private void TrimText(TrimOrigin origin)
        {
            if (origin == TrimOrigin.Beginning)
            {
                do
                {
                    if (this.symbols[0] == WhiteSpace)
                    {
                        this.symbols.RemoveAt(0);
                    }
                }
                while (this.symbols[0] == WhiteSpace);
            }
            else if (origin == TrimOrigin.End)
            {
                do
                {
                    if (this.symbols[^1] == WhiteSpace)
                    {
                        this.symbols.RemoveAt(this.symbols.Count - 1);
                    }
                }
                while (this.symbols[^1] == WhiteSpace);
            }
        }
        private void ReplaceWith(int start, int end, Text replacement)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(start);
            ArgumentOutOfRangeException.ThrowIfNegative(end);
            ArgumentNullException.ThrowIfNull(replacement);

            if (start > this.symbols.Count)
            {
                throw new Error("The starting index can not be greater the actual count of the symbols.");
            }

            if (end >= this.symbols.Count)
            {
                throw new Error("The ending index can not be greater the actual count of the symbols.");
            }

            this.symbols.RemoveRange(start, end - start + 1);
            this.symbols.InsertRange(start, replacement.ToString());
        }
        private byte[] ConvertToStream()
            => Encoding.UTF8.GetBytes(this.BuildText().ToString());


        // IEnumerable
        public IEnumerator<char> GetEnumerator()
            => new Enumerator(this.symbols);
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();


        // The implicit operators (=) allows creating a text with string, char, array of chars etc.
        public static implicit operator Text(string text)
            => new(text);
        public static implicit operator Text(StringBuilder text)
            => new(text.ToString());
        public static implicit operator Text(List<char> symbols)
            => new(symbols); 
        public static implicit operator Text(char[] text)
            => new(text);
        public static implicit operator Text(char symbol)
            => new(new char[] { symbol });


        // The equitable operators allows comparing two texts.
        public static bool operator ==(Text left, Text right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return left.ToString() == right.ToString();
        }
        public static bool operator != (Text left, Text right)
            => !(left == right);
        public static bool operator < (Text left, Text right)
            => left.CompareTo(right) < 0;
        public static bool operator > (Text left, Text right)
            => left.CompareTo(right) > 0;
        public static bool operator <= (Text left, Text right)
            => left.CompareTo(right) <= 0;
        public static bool operator >= (Text left, Text right)
            => left.CompareTo(right) >= 0;


        // The enumerator
        public struct Enumerator(List<char> symbols) : IEnumerator<char>
        {
            // The symbols of the text
            private readonly List<char> symbols = symbols;

            // The current index.
            private int index;


            public char Current
                => this.symbols[index];
            object? IEnumerator.Current
                => this.Current;


            public bool MoveNext()
            {
                this.index++;

                if (this.index >= this.symbols.Count)
                {
                    return false;
                }

                return true;
            }
            public void Reset()
                => this.index = -1;
            public void Dispose()
                => GC.SuppressFinalize(this);
        }
    }
}