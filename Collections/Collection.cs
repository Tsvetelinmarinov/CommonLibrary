// CommonLibrary - library for common usage
// CommonLibrary - библиотека с общо предназначение

using System;
using System.Linq;
using System.Collections;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommonLibrary.Base.Interfaces;

namespace CommonLibrary.Collections
{
    /// <summary>
    /// 
    ///  Defines sizable collection of elements, represented at system level like
    ///  list of the same elements with default capacity of 4 elements in the beginning.
    ///  The collection is resized every time the capacity is reached.
    ///  The collection is strongly typed and the data type of the elements should be
    ///  defined with the defining of the collection. This collection can be indexed by index and can
    ///  be iterated with foreach loop. This is the implementation of the IGenericCollection interface.
    ///  
    ///  Kолекция от елементи с динамичен размер, представена като списък от елементи с капацитет по
    ///  подразбиране от 4 елемента.
    ///  Капацитета се увеличава автоматично при запълване на колекцията.
    ///  Колекцията е строго типизирана, тоест типа данни се указва още в началото със създаването на колекцията.
    ///  Може да се индексира по индекс и да се итерира с цикъл foreach.
    ///  Тази колекция се базира на IGenericCollection интерфейса, и е негова директа имплементация.
    ///  
    /// <typeparam name="DataType">
    ///  The common data type for the elements in this collection
    /// </typeparam>
    ///
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Collection of elements with variable size")]
    public class Collection<DataType> 
        : IGenericCollection<DataType>, IEnumerable<DataType>, IReadOnlyCollection<DataType>, IEnumerable, ICloneable
    {
        #region Private Fileds

        // Holds the elements in the collection.
        // Съдържа елементите на колекцията.
        private readonly List<DataType>? _data;

        // Default capacity.
        // Капацитет по подразбиране.
        private const int DEFCAPACITY = 4;

        // Maximum capacity of the collection.
        // Максимален капацитет на колекцията.
        private const int MAXCAPACITY = 2_000_000_000;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// Indexer
        /// 
        /// Индексатор. Позволява достъп до елемент по неговия индекс в колекцията.
        /// 
        /// <param name="index">
        ///  The index of the element in the collection
        /// </param>
        /// 
        /// <returns>
        ///  The element at that index in the collection
        /// </returns>
        /// 
        /// </summary>
        public DataType? this[int index]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);

                if (index > _data!.Capacity - 1)
                {
                    throw new Error("The index is outside the bounds of the collection");
                }

                return _data[index];
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                bool indexIsInside = index <= _data!.Capacity - 1;

                if (_data!.Count > 1 && indexIsInside)
                {
                    _data![index] = value!;
                }
                else if (_data!.Count == 0 && indexIsInside)
                {
                    _data![0] = value!;
                }
            }
        }

        /// <summary>
        /// 
        /// Get the count of the elements in the collection.
        ///
        /// Броя на елементите в колекцията.
        /// 
        /// </summary>
        public int Count 
            => _data!.Count;

        /// <summary>
        /// 
        /// Get or set the capacity of the list.
        /// 
        /// Достъпва капацитета на списъка.
        /// 
        /// </summary>
        public int Capacity
        {
            get => _data!.Capacity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
                ArgumentOutOfRangeException.ThrowIfLessThan(value, Count);

                if (value < DEFCAPACITY)
                {
                    _data!.Capacity = DEFCAPACITY;
                }

                _data!.Capacity = value;
            }
        }

        /// <summary>
        /// 
        /// Get or set the first element in the collection
        /// 
        /// Достъпва първия елемент в колекцията.
        /// 
        /// </summary>
        public DataType? FirstElement
        {
            get => _data!.Count == 0 ? default! : _data![0];
            set
            {
                if (_data!.Count == 0)
                {
                    _data!.Add(value!);
                }

                _data![0] = value!;
            }
        }

        /// <summary>
        /// 
        /// Get or set the last element in the collection.
        /// 
        /// Достъпва последния елемент в колекцията.
        /// 
        /// </summary>
        public DataType? LastElement
        {
            get => _data!.Count > 0 ? _data![^1] : default!;
            set
            {
                if (_data!.Count == 0)
                {
                    _data!.Add(value!);
                }
                else
                {
                    _data[^1] = value!;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// Create new empty collection of elements with default capacity.
        /// 
        /// Създава нова празна колекция с капацитет по подразбиране.
        /// 
        /// </summary>
        public Collection()
            => _data = new List<DataType>(DEFCAPACITY);
        
        /// <summary>
        /// 
        /// Create new empty collection with maximum allowed capacity of 2 000 000 000.
        /// 
        /// Създава нова празна колекция с максимален капацитет от два милиарда елемента.
        /// 
        /// <param name="maxCapacity">Indicates when to use the maximum capacity</param>
        /// 
        /// </summary>
        public Collection(bool maxCapacity)
            : this()
        {
            if (maxCapacity)
            {
                _data!.Capacity = MAXCAPACITY;
            }
        }

        /// <summary>
        /// 
        /// Create new empty collection with the given capacity.
        /// 
        /// Създава нова празна колекция със зададения капацитет.
        /// 
        /// <param name="capacity">The capacity for the collection</param>
        /// 
        /// </summary>
        public Collection(int capacity)
            : this()
        {
            _data!.Capacity = capacity;
        }

        /// <summary>
        /// 
        /// Create new collection with the copied elements from the given
        /// collection and with capacity same as the count of the elements;
        /// 
        /// Създава нова колекция с копираните елементи от дадената колекция и капацитет равен на броя им.
        ///
        /// <param name="array">The collection which elements will be copied in the new collection</param>
        ///
        /// </summary>
        public Collection(IEnumerable<DataType> array)
            => _data = [.. array];

        /// <summary>
        /// 
        /// Create a collection with the copied elements of the given collection, and capacity set to the 
        /// given capacity.
        /// 
        /// Създава колекция с копирани елементи от дадената колекция и с капаците равен на зададения капацитет.
        /// 
        /// <param name="array">The collection witch elements will be copied in the new collection</param>
        /// <param name="capacity">The capacity of the new collection</param>
        /// 
        /// </summary>
        public Collection(IEnumerable<DataType> array, int capacity)
            : this(array)
        {
            _data!.Capacity = capacity;
        }

        /// <summary>
        /// 
        /// Create new collection with the copied elements from the given collection
        /// and maximum capacity.
        /// 
        /// Създава нова колекция с копирани елементи от дадената колекция и максимален капацитет.
        /// 
        /// <param name="array">The collection which elements will be copied for the new collection</param>
        /// <param name="maxCapacity">If true - sets the maximum capacity</param>
        /// 
        /// </summary>
        public Collection(IEnumerable<DataType> array, bool maxCapacity)
            : this(array)
        {
            if (maxCapacity)
            {
                _data!.Capacity = MAXCAPACITY;
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// 
        /// Add an element in the collection.
        /// 
        /// Добавя елемент в колекцията.
        /// 
        /// <param name="element">The element to be added.</param>
        /// 
        /// </summary>
        public void Add(DataType element)
            => _data!.Add(element);

        /// <summary>
        /// 
        /// Add multiple elements - no actual count.
        /// 
        /// Добавя множество елементи в колекцията.
        /// 
        /// <param name="elements">The elements to be added</param>
        /// 
        /// </summary>
        public void AddMultipleElements(params DataType?[] elements)
        {
            ArgumentNullException.ThrowIfNull(elements);
            _data!.AddRange(elements!);
        }

        /// <summary>
        /// 
        /// Add collection of elements in the collection
        /// 
        /// Добавя друга колекция от елементи в колекцията.
        /// 
        /// <param name="collection">The collection to be added</param>
        /// 
        /// </summary>
        public void AddCollection(IEnumerable<DataType> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            _data!.AddRange(collection);
        }

        /// <summary>
        /// 
        /// Remove an element from the collection by his value.
        /// Returns true when successfully removed the element and
        /// false when not.
        /// 
        /// Премахва елемент от колекцията по неговата стойност.
        /// Връща true при успешно премахване, в противен случай false.
        /// 
        /// <param name="value">The element to be removed</param>
        /// 
        /// <returns>True if the element is successfully removed, otherwise false</returns>
        /// 
        /// </summary>
        public bool RemoveElement(DataType value)
            => _data!.Remove(value);

        /// <summary>
        /// 
        /// Remove an element from the collection by his index.
        /// 
        /// Премахва елемент от колекцията по неговия индекс.
        /// 
        /// <param name="index">The index of the element in the collection</param>
        /// 
        /// </summary>
        public void RemoveElementAt(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index > _data!.Capacity - 1)
            {
                throw new Error("The index can not be greater than the capacity of the collection.");
            }

            _data!.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// Remove elements form the collection from given index to given index.
        /// 
        /// Премахва елементите от колекцията в зададения диапазон.
        /// 
        /// <param name="startIndex">Starting index in the collection</param>
        /// <param name="endIndex">Ending index in the collection</param>
        /// 
        /// </summary>
        public void RemoveInDiapason(int startIndex, int endIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfNegative(endIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startIndex, endIndex);

            bool isNotEmpty = _data!.Count > 0;

            if (isNotEmpty && startIndex > _data!.Capacity - 1)
            {
                throw new Error("The starting index can not be greater than the capacity of the collection.");
            }
            else if (isNotEmpty && endIndex > _data!.Capacity - 1)
            {
                throw new Error("The ending index can not be greater than the capacity of the collection.");
            }

            _data!.RemoveRange(startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// 
        /// Insert an element at given index in the collection
        /// 
        /// Вмъква елемент на зададения индекс в колекцията.
        /// 
        /// <param name="index">The index in the collection</param>
        /// <param name="element">The element to be inserted at that index</param>
        /// 
        /// </summary>
        public void InsertElementAt(int index, DataType element)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _data!.Capacity - 1);

            _data!.Insert(index, element);
        }

        /// <summary>
        /// 
        /// Insert multiple elements at given index in the collection
        /// 
        /// Вмъква множество елементи на зададения индекс в колекцията.
        /// 
        /// <param name="index">The index in the collection</param>
        /// <param name="elements">The elements</param>
        /// 
        /// </summary>
        public void InsertMultipleElementsAt(int index, params DataType?[] elements)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _data!.Capacity - 1);
            ArgumentNullException.ThrowIfNull(elements);

            _data!.InsertRange(index, elements!);
        }

        /// <summary>
        /// 
        /// Insert another collection at given index in the collection.
        /// 
        /// Вмъква друга колекция на зададения индекс в колекцията.
        /// 
        /// <param name="index">The index in the collection</param>
        /// <param name="collection">The collection to be inserted at that index</param>
        /// 
        /// </summary>
        public void InsertCollectionAt(int index, IEnumerable<DataType> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _data!.Capacity - 1);

            _data!.InsertRange(index, collection);
        }

        /// <summary>
        /// 
        /// Check if the given elements is in the collection and returns true if is in, otherwise false.
        /// 
        /// Проверява дали даден елемент се съдържа в колекцията.
        /// 
        /// <param name="element">The element</param>
        /// 
        /// <returns>True if the element is in the collection, otherwise false</returns>
        /// 
        /// </summary>
        public bool ContainsElement(DataType element)
            => element == null ? false : _data!.Contains(element);

        /// <summary>
        /// 
        /// Get the index of the given element in the collection.
        /// 
        /// Намира индекса на даден елемент в колекцията, ако елемента се съдържа в колекцията, ако не връща -1.
        /// 
        /// <param name="element">The element</param>
        /// 
        /// <returns>The index of the element. If the elements is not in the collection returns -1</returns>
        /// 
        /// </summary>
        public int FindIndexOf(DataType element)
        {
            if (element == null || !_data!.Contains(element))
            {
                return -1;
            }

            return _data.IndexOf(element);
        }

        /// <summary>
        /// 
        /// Get the index of the last occurrence of the element in the collection,
        /// if there are more than one element like this.
        /// 
        /// Връща индекса на последното срещане на елементa в колекцията, ако елемента не се съдържа в колекцията връща -1.
        /// 
        /// <param name="element">The element in the collection</param>
        /// 
        /// <returns>The index of the element. If the element in not in the collection returns -1</returns>
        /// 
        /// </summary>
        public int FindLastIndexOf(DataType element)
        {
            if (element == null || !_data!.Contains(element))
            {
                return -1;
            }

            return _data!.LastIndexOf(element);
        }

        /// <summary>
        /// 
        /// Checks every element in the collection and finds and extracts that one
        /// who match the Predicate.
        /// 
        /// Итерира през всеки един елемент в колекцията и връща първия, който отговаря на 
        /// зададеното условие.
        /// Ако няма съвпадения, връща стойността по подразбиране за дадения тип данни.
        /// 
        /// <param name="condition">The Predicate with the condition</param>
        /// 
        /// <returns>The first element who match the condition. If there is no
        /// matches returns the default value of the data type.</returns>
        /// 
        /// </summary>
        public DataType? FindElementByCondition(Predicate<DataType> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);

            foreach (DataType element in _data!)
            {
                if (condition(element))
                {
                    return element;
                }
            }

            return default!;
        }

        /// <summary>
        /// 
        /// Checks every element in the collection and extracts all elements
        /// who match the Predicate.
        /// 
        /// Итерира през всеки един елемент в колекцията и връща всички, които отговарят на 
        /// зададеното условие.
        /// Ако няма съвпадения, връща масив със стойностите по подразбиране за дадения тип данни.
        /// 
        /// <param name="condition">The Predicate with the condition</param>
        /// 
        /// <returns>All the elements who match the condition. If there is no
        /// matches returns array with the default values of the data type.</returns>
        /// 
        /// </summary>
        public DataType?[] FindElementsByCondition(Predicate<DataType> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);

            Collection<DataType> elements = [];

            foreach (DataType element in _data!)
            {
                if (condition(element))
                {
                    elements.Add(element);
                }
            }

            return [.. elements];
        }

        /// <summary>
        /// 
        /// Sorts the collection ascending by default.
        /// 
        /// Сортира колекцията във възходящ ред. Възходящия ред е по подразбиране.
        /// 
        /// </summary>
        public void SortCollection()
            => _data!.Sort();

        /// <summary>
        /// 
        /// EN:
        ///    Sorts the collection by specified sorting options and
        ///    returns it sortet.
        ///    The options for sorting should be specified with a flag from SprtingOptions enumeration.
        ///    Use SortingOptions.Ascending to sort the collection ascending.
        ///    Use SortingOptions.Descending to sort the collecion descending.
        /// 
        /// BG:
        ///    Сортира указаната колекция с указаните опций за сортиране.
        ///    Режима на сортиране трябва да бъде указан с флаг от еномерацията SortingOptions.
        ///    Използвай SortingOptions.Ascending за да сортираш колекцията възходящо.
        ///    Използвaй SortingOptions.Descending за да сортираш колекцията низходящо.
        /// 
        /// </summary>
        /// 
        /// <param name="options">
        ///  EN: The sorting options.
        ///  BG: Опцийте за сортиране.
        /// </param>
        public void SortCollectionBy(SortingOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            if (options == SortingOptions.Descending)
            {
                _data!.Sort();
                _data!.Reverse();
            }
            else if (options == SortingOptions.Ascending)
            {
                _data!.Sort();
            }
        }

        /// <summary>
        /// 
        /// Reverse the collection.
        /// 
        /// Обръща реда на елементите в колекцията.
        /// 
        /// </summary>
        public void ReverseCollection()
            => _data!.Reverse();

        /// <summary>
        /// 
        /// Checks if all the elements in the collection match given condition.
        /// 
        /// Проверява дали всички елементи в колекцията отговарят на зададеното условие.
        /// 
        /// <returns>Тrue if all elements match the condition, otherwise false.</returns>
        /// 
        /// </summary>
        public bool IsTrueForAll(Predicate<DataType> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);
            return _data!.TrueForAll(condition);
        }

        /// <summary>
        /// 
        /// Iterates over collection and execute the action with each element.
        /// 
        /// Итерира през всеки един елемент в колекцията и го подава като параметър на командата, след което
        /// я изпълява.
        /// 
        /// <param name="command">The command(Method) to be executed with each element</param>
        /// 
        /// </summary>
        public void ExecuteOnEach(Action<DataType> command)
        {
            ArgumentNullException.ThrowIfNull(command);
            _data!.ForEach(command);
        }

        /// <summary>
        /// 
        /// Truncates the collection, and resets the capacity to the default capacity.
        /// 
        /// Изпразва колекцията и върща капацитета по подразбиране.
        /// 
        /// </summary>
        public void TruncateCollection()
        {
            _data!.Clear();
            _data!.Capacity = DEFCAPACITY;
        }

        /// <summary>
        /// 
        /// Clone the collection to another collection.
        /// 
        /// Клонира колекцията в нова колекция, но като обект. Необходимо е преобразуване на типа данни чрез кастинг.
        /// 
        /// <returns>New collection as object.</returns>
        /// 
        /// </summary>
        public object Clone()
            => this;

        /// <summary>
        /// 
        /// Trim the capacity to the elements count.
        /// 
        /// Намалява капацитета на колекцията до броя на елементите.
        /// 
        /// </summary>
        public void RemoveExcessCapacity()
            => _data!.TrimExcess();

        /// <summary>
        /// 
        /// Trim the capacity to given value.
        ///
        /// Намалява капацитета на колекцията до зададения капацитет.
        ///  
        /// <param name="capacity">The new capacity</param>
        /// 
        /// </summary>
        public void ReduceCapacityTo(int capacity)
            => _data!.Capacity = capacity;       

        /// <summary>
        /// 
        /// Return the collection as read only collection.
        /// 
        /// Връща колекцията като защитена колекция.
        /// 
        /// <returns>Safe readonly collection</returns>
        /// 
        /// </summary>
        public ReadOnlyCollection<DataType> AsReadOnly()
            => _data!.AsReadOnly();

        #endregion

        #region Enumerator

        // Implement the IEnumerable interface, so the collection has
        // enumerator and can be iterated with foreach loop.
        //
        // Имплементира интерфейса IEnumerable, за да може колекцията
        // да се обхожда с foreach цикъл.
        /// <inheritdoc/>
        public IEnumerator<DataType> GetEnumerator()
            => _data!.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}