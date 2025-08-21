// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Collections;
using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Collections.Common
{
    /// <summary>
    /// 
    /// EN:
    /// Common(generic) list of objects that allows null.
    /// The default capacity of the list in 4 elements, and its multiplied both when is
    /// reached. The collection is COMMON. The type of the elements is System.Object, so the
    /// elements in the collection can be from different data types (not strongly typed).
    /// 
    /// BG:
    /// Общ лист от обекти, които позволяват null.
    /// Капацитета по подразбиране е 4 елемента и се удвоява, когато е достигнат.
    /// Колекцията е ОБЩА. Елементите са от тип данни System.Object, тоест те са обекти и
    /// това им позволява да бъдат от всякакъв тип данни. На кратко всеки елемент може да е от различен тип данни,
    /// което прави колекцията обща, а не стого типизирана.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Collection of objects")]
    public class CommonList : IEnumerable, ICloneable
    {
        //The objects of the list.
        //Обектите на списъка (в колекция).
        private readonly Collection<object?> _collection;

        //Maximum capacity of 2 000 000 000 objects.
        //Максимален капацитет от два милиарда обекта.
        private const int MAXCAP = 2_000_000_000;

        //Default capacity of 4 objects.
        //Капацитет по подразбиране от 4 обекта.
        private const int DEFCAP = 4;

        //The index of the last object. For internal use.
        //Индекса на последия обект в колекцията. За системна употреба.
        private int lastIndex;


        /// <summary>
        /// 
        ///  EN: Indexer
        ///  
        ///  BG: Индексатор
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index in the collection
        /// BG: Инекса на обекта в колекцията
        /// </param>
        /// 
        /// <returns>
        /// EN: The object at that index
        /// BG: Обекта на този индекс
        /// </returns>
        public object? this[int index]
        {
            get => _collection![index];
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                _collection![index] = value;
            }
        }

        /// <summary>
        /// 
        ///  EN: Get the count of the elements in the collection.
        ///  
        ///  BG: Достъпва броя на елементите в колекцията
        /// 
        /// </summary>
        public int Count
        {
            get => _collection!.Count;
        }

        /// <summary>
        /// 
        ///  EN: Get or set the capacity of the collection
        ///  
        ///  BG: Достъпва капацитета на колекцията
        /// 
        /// </summary>
        public int Capacity
        {
            get => _collection!.Capacity;
            set => _collection!.Capacity = value;
        }

        /// <summary>
        /// 
        ///  EN: Get or set the first object in the collection
        ///  
        ///  BG: Достъпва първия обект в колекцията
        /// 
        /// </summary>
        public object? FirstObject
        {
            get
            {
                if (_collection!.Count > 0)
                {
                    return _collection![0];
                }

                return default!;
            }
            set
            {
                if (_collection!.Count == 0)
                {
                    _collection.Add(value);
                }
                else
                {
                    _collection![0] = value;
                }
            }
        }

        /// <summary>
        /// 
        ///  EN: Get or set the last object in the collection
        ///  
        ///  BG: Достъпва последния обект в колекцията
        /// 
        /// </summary>
        public object? LastObject
        {
            get
            {
                lastIndex = _collection!.Count - 1;

                if (_collection!.Count > 0)
                {
                    return _collection![lastIndex];
                }

                return default!;
            }
            set
            {
                lastIndex = _collection!.Count - 1;

                if (_collection!.Count == 0)
                {
                    _collection.Add(value);
                }
                else
                {
                    _collection![lastIndex] = value;
                }
            }
        }


        /// <summary>
        /// 
        /// EN: Create new empty common list with default capacity.  
        ///  
        /// BG: Създава нов празен списък с капацитет по подразбиране.
        /// 
        /// </summary>
        public CommonList()
            => _collection = new(DEFCAP);

        /// <summary>
        /// 
        /// EN: Create new empty list with specified capacity.  
        ///  
        /// BG: Създава нов празен списък със зададен капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        /// EN: The capacity.  
        /// BG: Капацитетът.
        /// </param>
        public CommonList(int capacity)
            => _collection = new(capacity);

        /// <summary>
        /// 
        /// EN: Creates new empty collection with maximum capacity if the boolean variable value is true.  
        /// If false, the collection will be created with the default capacity.  
        ///  
        /// BG: Създава нова празна колекция с максимален капацитет, ако булевата стойност е true.  
        /// Ако е false, колекцията се създава с капацитет по подразбиране.
        /// 
        /// </summary>
        /// 
        /// <param name="maxCapacity">
        /// EN: When true, maximum capacity is enabled.  
        /// BG: Ако е true, се задава максимален капацитет.
        /// </param>
        public CommonList(bool maxCapacity)
            : this()
        {
            if (maxCapacity)
            {
                Capacity = MAXCAP;
            }
        }

        /// <summary>
        /// 
        /// EN: Create new common list with copied objects from the array of objects and  
        /// capacity = count of the elements.  
        ///  
        /// BG: Създава нов списък с копирани обекти от масив и капацитет равен на броя на елементите.
        /// 
        /// </summary>
        /// 
        /// <param name="objects">
        /// EN: The object array.  
        /// BG: Масивът от обекти.
        /// </param>
        public CommonList(object?[] objects)
            : this(objects!.Length)
        {
            foreach (object obj in objects!)
            {
                _collection!.Add(obj);
            }
        }

        /// <summary>
        /// 
        /// EN: Create new common list with copied objects from the array of objects and  
        /// capacity = specified capacity.  
        ///  
        /// BG: Създава нов списък с копирани обекти от масив и зададен капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="objects">
        /// EN: The object array.  
        /// BG: Масивът от обекти.
        /// </param>
        /// 
        /// <param name="capacity">
        /// EN: The capacity.  
        /// BG: Капацитетът.
        /// </param>
        public CommonList(object?[] objects, int capacity)
            : this(objects)
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN: Create new common list with copied objects from the array of objects and  
        /// maximum capacity.  
        ///  
        /// BG: Създава нов списък с копирани обекти от масив и максимален капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="objects">
        /// EN: The object array.  
        /// BG: Масивът от обекти.
        /// </param>
        /// 
        /// <param name="maxCapacity">
        /// EN: When true, maximum capacity is enabled.  
        /// BG: Ако е true, се задава максимален капацитет.
        /// </param>
        public CommonList(object?[] objects, bool maxCapacity)
            : this(objects)
        {
            if (maxCapacity)
            {
                Capacity = MAXCAP;
            }
        }


        /// <summary>
        /// 
        /// EN: Adds object to the collection.  
        ///  
        /// BG: Добавя обект към колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The object (value).  
        /// BG: Обектът (стойността).
        /// </param>
        public void Add(object value)
            => _collection!.Add(value);

        /// <summary>
        /// 
        /// EN: Adds multiple objects to the collection.  
        ///  
        /// BG: Добавя множество обекти към колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="objects">
        /// EN: The objects.  
        /// BG: Обектите.
        public void AddManyObjects(params object?[]? objects)
            => _collection!.AddMultipleElements(objects);

        /// <summary>
        /// 
        /// EN: Adds an array of objects to the collection.  
        ///  
        /// BG: Добавя масив от обекти към колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="objectArray">
        /// EN: The array of objects.  
        /// BG: Масивът от обекти.
        public void AddObjectArray(object?[]? objectArray)
            => _collection!.AddCollection(objectArray!);

        /// <summary>
        /// 
        /// EN: Removes an object from the collection.  
        ///  
        /// BG: Премахва обект от колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The object.  
        /// BG: Обектът.
        public void Remove(object value)
            => _collection!.RemoveElement(value);

        /// <summary>
        /// 
        /// EN: Removes an object from the collection by its index.  
        ///  
        /// BG: Премахва обект от колекцията по индекс.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the object in the collection.  
        /// BG: Индексът на обекта в колекцията.
        public void RemoveObjectAt(int index)
            => _collection!.RemoveElementAt(index);

        /// <summary>
        /// 
        /// EN: Removes all objects in the specified range from the collection.  
        ///  
        /// BG: Премахва всички обекти в зададения диапазон от колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="startIndex">
        /// EN: The starting index.  
        /// BG: Началният индекс.
        /// </param>
        /// 
        /// <param name="endIndex">
        /// EN: The ending index.  
        /// BG: Крайният индекс.
        /// </param>
        public void RemoveAllInRange(int startIndex, int endIndex)
            => _collection!.RemoveInDiapason(startIndex, endIndex);

        /// <summary>
        /// 
        /// EN: Clears the list.  
        ///  
        /// BG: Изчиства списъка.
        /// 
        /// </summary>
        public void ClearList()
            => _collection!.TruncateCollection();

        /// <summary>
        /// 
        /// EN: Clones the collection to a new collection.  
        ///  
        /// BG: Клонира колекцията в нова колекция.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: New collection with copied elements.  
        /// BG: Нова колекция с копирани елементи.
        /// </returns>
        public object Clone()
            => _collection!.Clone();

        /// <summary>
        /// 
        /// EN: Checks if the collection contains the object.  
        ///  
        /// BG: Проверява дали колекцията съдържа дадения обект.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The object.  
        /// BG: Обектът.
        /// </param>
        /// 
        /// <returns>
        /// EN: True if the object is in the collection, otherwise false.  
        /// BG: Връща true ако обектът е в колекцията, иначе false.
        /// </returns>
        public bool ContainsObject(object? value)
            => _collection!.ContainsElement(value);

        /// <summary>
        /// 
        /// EN: Inserts an object at the specified index in the collection.  
        ///  
        /// BG: Вмъква обект на зададен индекс в колекцията.
        /// 
        /// </summary>
        public void InsertObjectAt(object? value, int index)
            => _collection!.InsertElementAt(index, value);

        /// <summary>
        /// 
        /// EN: Inserts multiple objects at the specified index in the collection.  
        ///  
        /// BG: Вмъква множество обекти на зададен индекс в колекцията.
        /// 
        /// </summary>
        public void InsertManyObjectsAt(object?[]? values, int index)
            => _collection!.InsertMultipleElementsAt(index, values);

        /// <summary>
        /// 
        /// EN: Converts the common list to an array of objects.  
        ///  
        /// BG: Преобразува списъка в масив от обекти.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: New array with copied objects from the list.  
        /// BG: Нов масив с копирани обекти от списъка.
        /// </returns>
        public object[] ReturnAsArray()
        {
            object?[] objectArray = new object[_collection!.Count];
            int index = default;

            foreach (object value in _collection!)
            {
                objectArray[index++] = value;
            }

            return objectArray!;
        }

        /// <summary>
        /// 
        /// EN: Converts the common list to a Collection of objects.  
        ///  
        /// BG: Преобразува списъка в колекция от обекти.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: New collection with the objects from the list.  
        /// BG: Нова колекция с обектите от списъка.
        /// </returns>
        public Collection<object?> ReturnAsCollection()
            => _collection!;


        #region Enumerator

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
            => _collection!.GetEnumerator();
        

        #endregion  
    }
}