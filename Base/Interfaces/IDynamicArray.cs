//CommonLibrary - library for common usage.
//CommonLibrary - библиотека с общо предназначение.

using System;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Exceptions;
using System.Collections.Generic;

namespace CommonLibrary.Base.Interfaces
{
    /// <summary>
    /// 
    /// EN: Base interface for dynamic array of objects.
    /// 
    /// BG: Базов интерфейс за динамичен масив от обекти.
    ///     Под динамичен се има в предвид с променлив капацитет.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Array of objects with dynamic size")]
    public interface IDynamicArray<Type>
    {
        /// <summary>
        ///  
        /// EN: Get the object at that index in the collection.
        /// 
        /// BG: Достъпва обекта на този индекс в колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the element.
        /// BG: Индекса на елемента.
        /// </param>
        /// 
        /// <returns>
        /// EN: The object at that index in the collection.
        /// BG: Обекта на този индекс в колекцията.
        /// </returns>
        Type this[int index] 
        { 
            get; 
            set;
        }

        /// <summary>
        ///  
        /// EN: Get the count of the elements in the array.
        /// 
        /// BG: Достъпва бройката на елементите в колекцията.
        /// 
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        ///  
        /// EN: Gets or sets the capacity of the array.
        /// 
        /// BG: Достъпва капацитета на масива.
        /// 
        /// </summary>
        int Capacity
        {
            get;
            set;
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
        void Add(Type value);

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
        void AddMany(IEnumerable<Type> valuesArray);

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
        bool RemoveByValue(Type value);

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
        void RemoveByIndex(int index);

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
        void RemoveInDiapason(int startIndex, int endIndex);

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
        void RemoveAll();

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
        void InsertAt(int index, Type value);

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
        void InsertMany(int index, IEnumerable<Type> valuesArray);

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
        bool HasElement(Type value);

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
        void ExecuteOnEach(Action<Type> command);

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
        void SortArray(SortingOptions options = SortingOptions.Ascending);

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
        bool IsTrueForAll(Predicate<Type> condition);
    }
}
