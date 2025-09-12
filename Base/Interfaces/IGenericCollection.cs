//  CommonLibrary - library for common usage.
//  CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Attributes;
using System.Collections.Generic;

namespace CommonLibrary.Base.Interfaces
{
    /// <summary>
    /// 
    /// EN: Base interface for generic collections of elements.  
    /// 
    /// BG: Базов интерфейс за общи колекции от елементи.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Describes generic collection of elements")]
    public interface IGenericCollection<Type>
    {
        /// <summary>
        /// 
        /// EN: Indexer  
        /// 
        /// BG: Индексатор
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the element in the collection  
        /// BG: Индексът на елемента в колекцията
        /// </param>
        /// 
        /// <returns>
        /// EN: The element at that index in the collection  
        /// BG: Елементът на този индекс в колекцията
        /// </returns>
        Type? this[int index] { get; set; }

        /// <summary>
        /// 
        /// EN: The count of the elements in the collection  
        /// 
        /// BG: Броят на елементите в колекцията
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// EN: The capacity of the collection  
        /// 
        /// BG: Капацитетът на колекцията
        /// 
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// 
        /// EN: Get or set the first element in the collection  
        /// 
        /// BG: Взема или задава първия елемент в колекцията
        /// 
        /// </summary>
        Type? FirstElement { get; set; }

        /// <summary>
        /// 
        /// EN: Get or set the last element in the collection  
        /// 
        /// BG: Взема или задава последния елемент в колекцията
        /// 
        /// </summary>
        Type? LastElement { get; set; }


        /// <summary>
        /// 
        /// EN: Adds an element to the collection  
        /// 
        /// BG: Добавя елемент към колекцията
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        /// EN: The element  
        /// BG: Елементът
        /// </param>
        void Add(Type element);

        /// <summary>
        /// 
        /// EN: Adds multiple elements to the collection  
        /// 
        /// BG: Добавя множество елементи към колекцията
        /// 
        /// </summary>
        /// 
        /// <param name="elements">
        /// EN: The sequence of elements  
        /// BG: Последователността от елементи
        /// </param>
        void AddMultipleElements(params Type?[] elements);

        /// <summary>
        /// 
        /// EN: Adds another collection to the current collection  
        /// 
        /// BG: Добавя друга колекция към текущата колекция
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The external collection  
        /// BG: Външната колекция
        /// </param>
        void AddCollection(IEnumerable<Type> collection);

        /// <summary>
        /// 
        /// EN: Removes an element from the collection and returns true if successful  
        /// 
        /// BG: Премахва елемент от колекцията и връща true при успех
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        /// EN: The element  
        /// BG: Елементът
        /// </param>
        /// 
        /// <returns>
        /// EN: True if successfully removed, otherwise false  
        /// BG: True при успешно премахване, иначе false
        /// </returns>
        bool RemoveElement(Type element);

        /// <summary>
        /// 
        /// EN: Removes an element from the collection by its index  
        /// 
        /// BG: Премахва елемент от колекцията по индекс
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the element  
        /// BG: Индексът на елемента
        /// </param>
        void RemoveElementAt(int index);

        /// <summary>
        /// 
        /// EN: Removes elements from the collection in a given range  
        /// 
        /// BG: Премахва елементи от колекцията в зададен диапазон
        /// 
        /// </summary>
        /// 
        /// <param name="startIndex">
        /// EN: Starting index  
        /// BG: Начален индекс
        /// </param>
        /// 
        /// <param name="endIndex">
        /// EN: Ending index  
        /// BG: Краен индекс
        /// </param>
        void RemoveInDiapason(int startIndex, int endIndex);

        /// <summary>
        /// 
        /// EN: Inserts an element at a given index  
        /// 
        /// BG: Вмъква елемент на зададен индекс
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index  
        /// BG: Индексът
        /// </param>
        /// 
        /// <param name="element">
        /// EN: The element to insert  
        /// BG: Елементът за вмъкване
        /// </param>
        void InsertElementAt(int index, Type element);

        /// <summary>
        /// 
        /// EN: Inserts multiple elements at a given index  
        /// 
        /// BG: Вмъква множество елементи на зададен индекс
        /// 
        /// </summary>
        /// <param name="index">
        /// EN: The index  
        /// BG: Индексът
        /// </param>
        /// 
        /// <param name="elements">
        /// EN: The elements  
        /// BG: Елементите
        /// </param>
        void InsertMultipleElementsAt(int index, params Type?[] elements);

        /// <summary>
        /// 
        /// EN: Inserts another collection at a given index  
        /// 
        /// BG: Вмъква друга колекция на зададен индекс
        /// 
        /// </summary>
        /// <param name="index">
        /// EN: The index  
        /// BG: Индексът
        /// </param>
        /// 
        /// <param name="collection">
        /// EN: The collection to insert  
        /// BG: Колекцията за вмъкване
        /// </param>
        void InsertCollectionAt(int index, IEnumerable<Type> collection);

        /// <summary>
        /// 
        /// EN: Checks if the collection contains the given element  
        /// 
        /// BG: Проверява дали колекцията съдържа дадения елемент
        /// 
        /// </summary>
        /// <param name="element">
        /// EN: The element  
        /// BG: Елементът
        /// </param>
        /// 
        /// <returns>
        /// EN: True if the element is found  
        /// BG: True ако елементът е намерен
        /// </returns>
        bool ContainsElement(Type element);

        /// <summary>
        /// 
        /// EN: Finds the index of the given element  
        /// 
        /// BG: Намира индекса на дадения елемент
        /// 
        /// </summary>
        /// <param name="element">
        /// EN: The element  
        /// BG: Елементът
        /// </param>
        /// 
        /// <returns>
        /// EN: The index of the element  
        /// BG: Индексът на елемента
        /// </returns>
        int FindIndexOf(Type element);

        /// <summary>
        /// 
        /// EN: Finds the last index of the given element  
        /// 
        /// BG: Намира последния индекс на дадения елемент
        /// 
        /// </summary>
        /// <param name="element">
        /// EN: The element  
        /// BG: Елементът
        /// </param>
        /// 
        /// <returns>
        /// EN: The last index of the element  
        /// BG: Последният индекс на елемента
        /// </returns>
        int FindLastIndexOf(Type element);

        /// <summary>
        /// 
        /// EN: Finds the first element that matches the condition  
        /// 
        /// BG: Намира първия елемент, който отговаря на условието
        /// 
        /// </summary>
        /// <param name="condition">
        /// EN: The predicate condition  
        /// BG: Условието като предикат
        /// </param>
        /// 
        /// <returns>
        /// EN: The matching element or default value  
        /// BG: Съвпадащият елемент или стойност по подразбиране
        /// </returns>
        Type? FindElementByCondition(Predicate<Type> condition);

        /// <summary>
        /// 
        /// EN: Finds all elements that match the condition  
        /// 
        /// BG: Намира всички елементи, които отговарят на условието
        /// 
        /// </summary>
        /// <param name="condition">
        /// EN: The predicate condition  
        /// BG: Условието като предикат
        /// </param>
        /// 
        /// <returns>
        /// EN: Array of matching elements or default values  
        /// BG: Масив от съвпадащи елементи или стойности по подразбиране
        /// </returns>
        Type?[]? FindElementsByCondition(Predicate<Type> condition);

        /// <summary>
        /// 
        /// EN: Sorts the collection in ascending order  
        /// 
        /// BG: Сортира колекцията във възходящ ред
        /// 
        /// </summary>
        void SortCollection();

        /// <summary>
        /// 
        /// EN: Reverses the collection  
        /// 
        /// BG: Обръща колекцията
        /// 
        /// </summary>
        void ReverseCollection();

        /// <summary>
        /// 
        /// EN: Checks if all elements match the condition  
        /// 
        /// BG: Проверява дали всички елементи отговарят на условието
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: True if all match, otherwise false  
        /// BG: True ако всички съвпадат, иначе false
        /// </returns>
        bool IsTrueForAll(Predicate<Type> condition);

        /// <summary>
        /// 
        /// EN: Executes an action on each element in the collection  
        /// 
        /// BG: Изпълнява действие върху всеки елемент в колекцията
        /// 
        /// </summary>
        /// 
        /// <param name="command">
        /// EN: The action to execute  
        /// BG: Действието за изпълнение
        /// </param>
        void ExecuteOnEach(Action<Type> command);

        /// <summary>
        /// 
        /// EN: Truncates the collection and resets capacity to default  
        /// 
        /// BG: Изчиства колекцията и връща капацитета към стойността по подразбиране
        /// 
        /// </summary>
        void TruncateCollection();
    }
}