// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using CommonLibrary.AbstractDataTypes;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using CommonLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// 
    /// EN:
    ///   Base interface for associative collection of objects of type 
    ///   CommonLibrary.AbstractDataTypes.Pair. Each pair has key and associated value.
    /// 
    /// BG:
    ///   Базов интерфейс за асициативен масив от обекти от тип данни
    ///   CommonLibrary.AbstractDataTypes.Pair. Всяка двойка има ключ
    ///   и асоцийрана с него стойност. Както при всеки асоциативен масив, така и този
    ///   достъпва дадена стойност по неиния ключ.
    /// 
    /// </summary>
    /// 
    /// <typeparam name="KeyType">
    ///  EN: The data type of the key.
    ///  BG: Типът данни на ключа.
    /// </typeparam>
    /// 
    /// <typeparam name="ValueType">
    ///  EN: The data type of the value.
    ///  BG: Типът данни на стойността.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Description("Associative array of pairs")]
    public interface IPairList<KeyType, ValueType>
    {
        /// <summary>
        /// 
        /// EN:
        ///   Indexer.
        ///   
        /// BG:
        ///   Индексатор.
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        ///  EN: The key of the pair.
        ///  BG: Ключа на двойката ключ-стойност.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The value associated with that key.
        ///  BG: Стойността асоцийрана с този ключ.
        /// </returns>
        public abstract ValueType this[KeyType key]
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the keys from the collection in a collection of keys.
        ///   
        /// BG:
        ///   Извлича ключовете от двойките ключ-стойност в колекция от ключове.
        /// 
        /// </summary>
        public abstract Collection<KeyType> Keys
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the values from the collection in a collection of values.
        ///   
        /// BG:
        ///   Извлича стойностите от двойките ключ-стойност в колекция от стойности.
        /// 
        /// </summary>
        public abstract Collection<ValueType> Values
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Get or set the capacity of the collection.
        /// 
        /// BG:
        ///   Достъпва капацитета на асоциативната колекция.
        /// 
        /// </summary>
        public abstract int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the count of the pairs in the collection.
        ///   
        /// BG:
        ///   Достъпва бройката на двойките ключ-стойност в колецията.
        /// 
        /// </summary>
        public abstract int Count
        {
            get;
        }


        /// <summary>
        /// Добавя единичен елемент (ключ–стойност) към колекцията.  
        /// Adds a single (key–value) pair to the collection.
        /// </summary>
        /// <param name="pair">
        /// Елементът за добавяне.  
        /// The pair to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Аргументът <paramref name="pair"/> е null.  
        /// The <paramref name="pair"/> argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В колекцията вече съществува елемент със същия ключ.  
        /// An element with the same key already exists in the collection.
        /// </exception>
        public abstract void Add(Pair<KeyType, ValueType> pair);

        /// <summary>
        /// Добавя единичен елемент (ключ–стойност) към колекцията.  
        /// Adds a single (key–value) pair to the collection.
        /// </summary>
        /// <param name="key">
        /// Ключът на елемента.  
        /// The key of the element.
        /// </param>
        /// <param name="value">
        /// Стойността, асоциирана с ключа.  
        /// The value associated with the key.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="key"/> е null (ако <typeparamref name="KeyType"/> допуска null).  
        /// <paramref name="key"/> is null (if <typeparamref name="KeyType"/> allows null).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В колекцията вече съществува елемент със същия ключ.  
        /// An element with the same key already exists in the collection.
        /// </exception>
        public abstract void Add(KeyType key, ValueType value);

        /// <summary>
        /// Добавя множество елементи (ключ–стойност) към колекцията.  
        /// Adds multiple (key–value) pairs to the collection.
        /// </summary>
        /// <param name="collection">
        /// Последователност от елементи за добавяне.  
        /// The sequence of pairs to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection"/> е null.  
        /// <paramref name="collection"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Някой от елементите е невалиден или съдържа дублиращ се ключ.  
        /// One or more items are invalid or contain duplicate keys.
        /// </exception>
        public abstract void AddMany(IEnumerable<Pair<KeyType, ValueType>> collection);

        /// <summary>
        /// Премахва елемента на указан индекс.  
        /// Removes the element at the specified index.
        /// </summary>
        /// <param name="index">
        /// Нулево-базиран индекс на елемента за премахване.  
        /// The zero-based index of the element to remove.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> е извън диапазона [0, Count - 1].  
        /// <paramref name="index"/> is outside the range [0, Count - 1].
        /// </exception>
        public abstract void Remove(int index);

        /// <summary>
        /// Премахва елемента с посочения ключ, ако съществува.  
        /// Removes the element with the specified key, if it exists.
        /// </summary>
        /// <param name="key">
        /// Ключът на елемента за премахване.  
        /// The key of the element to remove.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="key"/> е null (ако <typeparamref name="KeyType"/> допуска null).  
        /// <paramref name="key"/> is null (if <typeparamref name="KeyType"/> allows null).
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Не е намерен елемент с дадения ключ.  
        /// No element with the specified key was found.
        /// </exception>
        public abstract void Remove(KeyType key);

        /// <summary>
        /// Премахва елементи в зададен индексен диапазон (включително).  
        /// Removes elements within the specified index range (inclusive).
        /// </summary>
        /// <param name="startIndex">
        /// Начален индекс (включително).  
        /// The starting index (inclusive).
        /// </param>
        /// <param name="endIndex">
        /// Краен индекс (включително).  
        /// The ending index (inclusive).
        /// </param>
        /// <remarks>
        /// Премахва всички елементи в интервала [<paramref name="startIndex"/>, <paramref name="endIndex"/>].  
        /// Removes all elements within the range [<paramref name="startIndex"/>, <paramref name="endIndex"/>].
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Някой от индексите е извън диапазона.  
        /// One or both indices are out of range.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="startIndex"/> е по-голям от <paramref name="endIndex"/>.  
        /// <paramref name="startIndex"/> is greater than <paramref name="endIndex"/>.
        public abstract void RemoveInRange(int startIndex, int endIndex);

        /// <summary>
        /// Проверява дали в колекцията съществува елемент с дадения ключ.  
        /// Checks whether the collection contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// Ключът за търсене.  
        /// The key to locate.
        /// </param>
        /// <returns>
        /// <see langword="true"/>, ако съществува елемент с този ключ; иначе <see langword="false"/>.  
        /// <see langword="true"/> if an element with the specified key exists; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="key"/> е null (ако <typeparamref name="KeyType"/> допуска null).  
        /// <paramref name="key"/> is null (if <typeparamref name="KeyType"/> allows null).
        /// </exception>
        public abstract bool ContainsKey(KeyType key);

        /// <summary>
        /// Проверява дали в колекцията съществува дадената стойност.  
        /// Checks whether the collection contains the specified value.
        /// </summary>
        /// <param name="value">
        /// Стойността за търсене.  
        /// The value to locate.
        /// </param>
        /// <returns>
        /// <see langword="true"/>, ако съществува елемент с тази стойност; иначе <see langword="false"/>.  
        /// <see langword="true"/> if an element with the specified value exists; otherwise, <see langword="false"/>.
        /// </returns>
        public abstract bool ContainsValue(ValueType value);

        /// <summary>
        /// Вмъква елемент на указан индекс, измествaйки следващите елементи надясно.  
        /// Inserts an element at the specified index, shifting subsequent elements to the right.
        /// </summary>
        /// <param name="index">
        /// Нулево-базиран индекс. Допустим диапазон: [0, Count].  
        /// The zero-based index. Allowed range: [0, Count].
        /// </param>
        /// <param name="pair">
        /// Елементът за вмъкване.  
        /// The pair to insert.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> е извън допустимия диапазон.  
        /// <paramref name="index"/> is outside the allowed range.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="pair"/> е null.  
        /// <paramref name="pair"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Вече съществува елемент със същия ключ.  
        /// An element with the same key already exists.
        /// </exception>
        public abstract void Insert(int index, Pair<KeyType, ValueType> pair);

        /// <summary>
        /// Вмъква елемент (ключ–стойност) на указан индекс.  
        /// Inserts a (key–value) pair at the specified index.
        /// </summary>
        /// <param name="index">
        /// Нулево-базиран индекс. Допустим диапазон: [0, Count].  
        /// The zero-based index. Allowed range: [0, Count].
        /// </param>
        /// <param name="key">
        /// Ключът на елемента.  
        /// The key of the element.
        /// </param>
        /// <param name="value">
        /// Стойността, асоциирана с ключа.  
        /// The value associated with the key.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> е извън допустимия диапазон.  
        /// <paramref name="index"/> is outside the allowed range.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="key"/> е null (ако <typeparamref name="KeyType"/> допуска null).  
        /// <paramref name="key"/> is null (if <typeparamref name="KeyType"/> allows null).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Вече съществува елемент със същия ключ.  
        /// An element with the same key already exists.
        /// </exception>
        public abstract void Insert(int index, KeyType key, ValueType value);

        /// <summary>
        /// Вмъква колекция от елементи, започвайки от указан индекс.  
        /// Inserts a collection of elements starting at the specified index.
        /// </summary>
        /// <param name="index">
        /// Нулево-базиран начален индекс. Допустим диапазон: [0, Count].  
        /// The zero-based starting index. Allowed range: [0, Count].
        /// </param>
        /// <param name="collection">
        /// Последователност от елементи за вмъкване.  
        /// The sequence of elements to insert.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> е извън допустимия диапазон.  
        /// <paramref name="index"/> is outside the allowed range.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection"/> е null.  
        /// <paramref name="collection"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Някой от елементите е невалиден или съдържа дублиращ се ключ.  
        /// One or more items are invalid or contain duplicate keys.
        /// </exception>
        public abstract void Insert(int index, IEnumerable<Pair<KeyType, ValueType>> collection);

        /// <summary>
        /// Премахва всички елементи от колекцията.  
        /// Removes all elements from the collection.
        /// </summary>
        /// <remarks>
        /// След извикване <c>Count</c> трябва да е 0.  
        /// After calling, <c>Count</c> should be 0.
        /// </remarks>
        public abstract void Clear();

        /// <summary>
        /// Сортира колекцията според подадените опции.  
        /// Sorts the collection according to the given options.
        /// </summary>
        /// <param name="options">
        /// Опции за сортиране (например по ключ/стойност, възходящо/низходящо).  
        /// Sorting options (e.g., by key/value, ascending/descending).
        /// </param>
        /// <exception cref="ArgumentException">
        /// Подадени са невалидни или несъвместими опции.  
        /// Invalid or incompatible sorting options were provided.
        /// </exception>
        public abstract void SortCollection(SortingOptions options);

        /// <summary>
        /// Връща представяне на колекцията като масив.  
        /// Returns the collection as an array.
        /// </summary>
        /// <remarks>
        /// Текущата сигнатура на метода е <c>void</c>. Ако трябва да връща масив,  
        /// прецизирайте сигнатурата например като:  
        /// The current method signature is <c>void</c>. To return an array,  
        /// consider changing it to:  
        /// <code>public abstract Pair&lt;KeyType, ValueType&gt;[] ReturnAsArray();</code>
        /// </remarks>
        public abstract void ReturnAsArray();
    }
}