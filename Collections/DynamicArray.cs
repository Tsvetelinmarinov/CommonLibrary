// CommonLibrary - library for common usage
// CommonLibrary - библиотека с общо предназначение

using System;
using System.Collections;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Interfaces;
using CommonLibrary.Attributes;
using System.Collections.Generic;
using CommonLibrary.Helpers;
namespace CommonLibrary.Collections
{
    /// <summary>
    /// 
    ///  EN:
    ///    Dynamic array of elements.
    ///    The default capacity of the array is 4 elements, and is
    ///    multiplied when is reached. Maximum capacity is 1 000 000 000 elements.
    ///    The DynamicArray is implemented by internal collection of elements and is based on the 
    ///    IDynamicArray interface, witch direct implementation is.
    /// 
    ///  BG:
    ///    Динамичен масив от елементи с капацитед по подразбиране от 4 елемента и 
    ///    максимален капацитет от един милиард елемента. Капацитета се удвоява когато е достигнат.
    ///    DynamicArray вътрешно работи с колекция от елементи. 
    ///    Базиран е на IDynamicArray интерфейса и е негова имплементация.
    /// 
    /// </summary>
    /// 
    /// <typeparam name="Type">
    ///  EN: The data type for the elements in the array.
    ///  BG: Типа данни на елементите в масива.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Description("Dynamic array of elements")]
    public class DynamicArray<Type> : IDynamicArray<Type>, IEnumerable<Type>, IEnumerable, ICloneable
    {
        //
        //Array with the elements of the dynamic array.
        //
        //Масив с елементите на динамичния масив.
        //
        private Collection<Type> _elements;

        //
        //The default capacity of 4 elements.
        //
        //Капацитета по подразбиране от 4 елемента.
        //
        private const int DefaultCapacity = 4;

        //
        //The maximum capacity of one billion element.
        //
        //Максимален капацитет от един милиард елемента.
        //
        private const int MaxCapacity = 1_000_000_000;


        /// <summary>
        ///  
        ///  EN:
        ///     Indexer.
        ///  
        ///  BG:
        ///     Индексатор.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        ///  EN: The index in the array.
        ///  BG: Индекса в масива.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The element at that index.
        ///  BG: Елемента на този индекс.
        /// </returns>
        public Type this[int index] 
        { 
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _elements.Count - 1);
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(_elements.Count);

                return _elements[index];                      
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _elements.Count - 1);
                ArgumentNullException.ThrowIfNull(value);

                _elements[index] = value;
            }
        }

        /// <summary>
        ///  
        ///  EN:
        ///     Get the count of the elements in the array.
        ///  
        ///  BG:
        ///     Достъпва бройката на елементите в масива.
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: The count of the elements.
        ///  BG: Бвойката на елементите.
        /// </returns>
        public int Count 
            => _elements.Count;

        /// <summary>
        ///  
        ///  EN:
        ///     Get or set the capacity of the array.
        ///  
        ///  BG:
        ///     Достъпва капацитета на масива.
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: The capacity.
        ///  BG: Капацитета на масива.
        /// </returns>
        public int Capacity 
        { 
            get => _elements.Capacity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
                ArgumentOutOfRangeException.ThrowIfLessThan(value, Count);

                _elements.Capacity = value; //The validation is maded in the Collection class property "Capacity".
                                            //
                                            //Валидацията на капацитета се прави в свойството "Capacity" на класа
                                            //Collection
            }
        }


        /// <summary>
        ///  
        ///  EN:
        ///     Creates new empty DynamicArray with the default capacity.
        ///   
        ///  BG:
        ///    Създава нов празен DynamicArray с капацитет по подразбиране.
        /// 
        /// </summary>
        public DynamicArray() 
            =>  _elements = new(DefaultCapacity);

        /// <summary>
        ///  
        ///  EN:
        ///     Creates new empty DynamicArray with the specified capacity.
        ///   
        ///  BG:
        ///    Създава нов празен DynamicArray с указания капацитет.
        /// 
        /// </summary>
        /// 
        ///<param name="capacity">
        /// EN: The capacity of the array.
        /// BG: Капацитетът на масива.
        /// </param>
        public DynamicArray(int capacity)
            => _elements = new(capacity);

        /// <summary>
        ///  
        ///  EN:
        ///     Creates new empty DynamicArray with maximum capacity.
        ///     To create the array with maximum capacity the value of the maxCapacity boolean
        ///     should be true. If it false (by default) the array will be created with the default
        ///     capacity.
        ///   
        ///  BG:
        ///    Създава нов празен DynamicArray с максимален капацитет.
        ///    За да се създаде масива с максимален капацитет, стойноста на булевата променлива
        ///    maxCapacity трябва да е true. Ако е false (по подразбиране) масива ще се създаде с
        ///    капацитет по подразбиране.
        /// 
        /// </summary>
        /// 
        ///<param name="maxCapacity">
        /// EN: Indicates when to use maximum capacity.
        /// BG: Индикира кога да се използва маскималния капацитет.
        /// </param>
        public DynamicArray(bool maxCapacity) 
        {
            if (maxCapacity)
            {
                _elements = new(MaxCapacity);
            }
                     
           _elements = new(DefaultCapacity); 
        }


        /// <summary>
        ///  
        /// EN:
        ///    Adds an element to the array.
        /// 
        /// BG:
        ///    Добадя елемент към масива.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The element to be added.
        /// BG: Елемента, който да бъде добавен.
        /// </param>
        public void Add(Type value)
            => _elements.Add(value);

        /// <summary>
        /// 
        /// EN:
        ///    Adds array of elemets to the array.
        ///
        /// BG:
        ///    Добавя масив от елементи в масива.
        /// 
        /// </summary>
        /// 
        /// <param name="valuesArray">
        /// EN: The array with values to be added.
        /// BG: Масива от елементи, който да бъде добавен.
        /// </param>
        public void AddMany(IEnumerable<Type> valuesArray)
            => _elements.AddCollection(valuesArray);

        /// <summary>
        /// 
        /// EN:
        ///    Removes an element from the collection by his value.
        /// 
        /// BG:
        ///   Премахва елемент от кекцията по неговата стойност.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The element value.
        /// BG: Елемента - стойността му.
        /// </param>
        /// 
        /// <returns>
        /// EN: True if the element is successfully removed, otherwise false.
        /// BG: True ако елемента е премахнат успешно, в противен случай - false.
        /// </returns>
        public bool RemoveByValue(Type value)
            => _elements.RemoveElement(value);

        /// <summary>
        /// 
        /// EN:
        ///    Removes an element from the collection by his index.
        /// 
        /// BG:
        ///   Премахва елемент от колекцията по неговия индекс.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the element in the collection.
        /// BG:
        /// </param>
        /// 
        /// <returns>
        /// EN: None
        /// BG: Не връща стойност.
        /// </returns>
        public void RemoveByIndex(int index)
            => _elements.RemoveElementAt(index);

        /// <summary>
        /// 
        /// EN:
        ///    Remove all elements in the specified diapason in the collection.
        /// 
        /// BG:
        ///   Премахва всички елементи в указания диапазон в колецията.
        ///   Стартовия индекс и крайния индекс се премахват също.
        /// 
        /// </summary>
        /// 
        /// <param name="startIndex">
        /// EN: The starting index.
        /// BG: Стартовия индекс.
        /// </param>
        /// 
        /// <param name="endIndex">
        /// EN: The ending index.
        /// BG: Крайния индекс.
        /// </param>
        /// 
        /// <returns>
        /// EN: None.
        /// BG: Не връща стойност.
        /// </returns>
        public void RemoveInDiapason(int startIndex, int endIndex)
            => _elements.RemoveInDiapason(startIndex, endIndex);

        /// <summary>
        /// 
        /// EN:
        ///    Removes all elements from the array, and sets the default capacity.
        /// 
        /// BG:
        ///   Премахва всички елементи от масива и връща капацитета по подразбиране.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: None. 
        /// BG: Не връща стойност.
        /// </returns>
        public void RemoveAll()
            => _elements.TruncateCollection();

        /// <summary>
        /// 
        /// EN:
        ///    Inserts an element at given index in the array.
        /// 
        /// BG:
        ///   Вмъква елемент на указан индекс в масива.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index in the array.
        /// BG: Индекса в масива.
        /// </param>
        /// 
        /// <param name="value">
        /// EN: The value to be inserted.
        /// BG: Стойността, която да бъде вмъкната.
        /// </param>
        /// 
        /// <returns>
        /// EN: None.
        /// BG: Не връща стойност.
        /// </returns>
        public void InsertAt(int index, Type value)
            => _elements.InsertElementAt(index, value);

        /// <summary>
        /// 
        /// EN:
        ///    Inserts array of elements at the specified index in the array.
        /// 
        /// BG:
        ///   Вмъква масив от елементи на указан индекс в масива.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index in the array.
        /// BG: Индекса в масива.
        /// </param>
        /// 
        /// <param name="valuesArray">
        /// EN: The array with values to be inserted.
        /// BG: Масива със стойности, който да бъдат вмъкнати.
        /// </param>
        /// 
        /// <returns>
        /// EN: None.
        /// BG: Не връща стойност.
        /// </returns>
        public void InsertMany(int index, IEnumerable<Type> valuesArray)
            => _elements.InsertCollectionAt(index, valuesArray);

        /// <summary>
        /// 
        /// EN:
        ///    Checks if the specified element is in the array.
        /// 
        /// BG:
        ///   Проверява, дали указания елемент се съдържа в масива.
        /// 
        /// </summary>
        /// 
        /// <param name="value">
        /// EN: The value.
        /// BG: Стойността, която да се провери в масива.
        /// </param>
        /// 
        /// <returns>
        /// EN: True if the array contains that element, otherwise false. 
        /// BG: True ако стойността се съдържа в масива, в противен случай връща false.
        /// </returns>
        public bool HasElement(Type value)
            => _elements.ContainsElement(value);

        /// <summary>
        /// 
        /// EN:
        ///    Execute the method with each element from the array.
        ///    Each element is passed as parameter of the method.
        /// 
        /// BG:
        ///   Изпълнява метода със всеки един елемент от масива.
        ///   Всеки елемент се подава като параметър на метода и той се изпълнява с него.
        /// 
        /// </summary>
        /// 
        /// <param name="command">
        /// EN: The action (method) with one parameter that will be executed with each element.
        /// BG: Метода с един параметър, който ще се изпълява със всеки елемент от масива.
        /// </param>
        /// 
        /// <returns>
        /// EN: None.
        /// BG: Не връща стойност.
        /// </returns>
        public void ExecuteOnEach(Action<Type> command)
            => _elements.ExecuteOnEach(command);

        /// <summary>
        /// 
        /// EN:
        ///    Sorts the array by specified sorting options.
        ///    By default is ascending sorting.
        ///    To make to sorting descending use the SortingOptions.Descending flag to change the
        ///    sorting algorithm.
        /// 
        /// BG:
        ///   Сортира масива според указаната опция за сортиране.
        ///   По подразбиране сортирането е възходящо.
        ///   Ако трябва да се сортира низходящо, се указва изрично с флага
        ///   SortingOptions.Descending подаден като параметър на метода.
        /// 
        /// </summary>
        /// 
        /// <param name="options">
        /// EN: The sorting option.
        /// BG: Опцията за сортиране, по подразбиране е възходящо.
        /// </param>
        /// 
        /// <returns>
        /// EN: None.
        /// BG: Не връща стойност.!
        /// </returns>
        public void SortArray(SortingOptions options = SortingOptions.Ascending)
              => _elements = CollectionHelper.SortCollectionBy(_elements, options);                 

        /// <summary>
        ///  
        ///  EN:
        ///     Retirns the array as object.
        ///  
        ///  BG:
        ///     Връща масива като обект.
        /// 
        /// </summary>
        /// 
        /// <returns>
        ///  EN: The array as object
        ///  BG: Масива като обекст
        /// </returns>
        public object Clone()     
            => this as object;

        /// <summary>
        /// 
        /// EN:
        ///    Iterates each element is the collection and pass it as argument to the predicate.
        /// 
        /// BG:
        ///   Итерита през всеки елемент в масива и го подава като параметър на предиката
        ///   с условието.
        /// 
        /// </summary>
        /// 
        /// <param name="condition">
        /// EN: The Predicate with the condition.
        /// BG: Опцията за сортиране, по подразбиране е възходящо.
        /// </param>
        /// 
        /// <returns>
        /// EN: True if all the elements in the array match the condition, otherwise false.
        /// BG: Връща true когато всички елементи в масива отговарят на условието. В противе
        ///     случай връща false.
        /// </returns>
        public bool IsTrueForAll(Predicate<Type> condition)
            => _elements.IsTrueForAll(condition);


        #region Enumerator

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
            => _elements.GetEnumerator();
        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
            => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}