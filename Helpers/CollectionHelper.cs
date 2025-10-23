// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Linq;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace CommonLibrary.Helpers
{
    /// <summary>
    /// 
    /// EN: 
    ///    Provides static methods for searching and editing a collection.  
    ///  
    /// BG: 
    ///    Предоставя набор от статични методи за манипулиране на колекция или масив.
    /// 
    /// </summary>
    [Description("Provides static methods for manipulating collection")]
    public static class CollectionHelper
    {
        /// <summary>
        /// 
        /// EN: Checks every element in the collection and extracts the one that matches the Predicate.  
        ///  
        /// BG: Проверява всеки елемент в колекцията и връща първия, който отговаря на условието.  
        ///     Ако няма съвпадения, връща стойност по подразбиране.
        /// 
        /// </summary>
        /// 
        /// <param name="condition">
        /// EN: The Predicate with the condition.  
        /// BG: Предикатът с условието.
        /// </param>
        /// 
        /// <param name="collection">
        /// EN: The collection whose elements will be iterated.  
        /// BG: Колекцията, чиито елементи ще бъдат итерирани.
        /// </param>
        /// 
        /// <returns>
        /// EN: The first matching element or default value if there is no match.  
        /// BG: Първият съвпадащ елемент или стойност по подразбиране, ако няма съвпадения.
        /// </returns>
        public static Type? FindElementThat<Type>(Predicate<Type> condition, IEnumerable<Type> collection)
        {
            ArgumentNullException.ThrowIfNull(condition);
            ArgumentNullException.ThrowIfNull(collection);

            foreach (Type element in collection)
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
        /// EN: Checks every element in the collection and extracts all that match the Predicate.  
        ///  
        /// BG: Проверява всеки елемент и връща всички, които отговарят на условието.  
        ///     Ако няма съвпадения, връща масив със стойности по подразбиране.
        /// 
        /// </summary>
        /// 
        /// <param name="condition">
        /// EN: The Predicate with the condition.  
        /// BG: Предикатът с условието.
        /// </param>
        /// 
        /// <param name="collection">
        /// EN: The collection whose elements will be checked.  
        /// BG: Колекцията, чиито елементи ще бъдат проверени.
        /// </param>
        /// 
        /// <returns>
        /// EN: Array of matching elements or array of default values if no matches found.  
        /// BG: Масив от съвпадащи елементи или стойности по подразбиране, ако няма съвпадения.
        /// </returns>
        public static Type?[] FindElementsThat<Type>(Predicate<Type> condition, IEnumerable<Type> collection)
        {
            ArgumentNullException.ThrowIfNull(condition);
            ArgumentNullException.ThrowIfNull(collection);

            Collection<Type?> elements = new(collection.Count());

            foreach (Type element in collection)
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
        /// EN: Iterates over the collection and executes the method on each element.  
        ///  
        /// BG: Обхожда колекцията и изпълнява метода върху всеки елемент.
        /// 
        /// </summary>
        /// 
        /// <typeparam name="Type">
        /// EN: The data type of the method parameter and the collection elements.  
        /// BG: Типът на параметъра и елементите в колекцията.
        /// </typeparam>
        /// 
        /// <param name="command">
        /// EN: The method to execute.  
        /// BG: Методът, който ще се изпълни.
        /// </param>
        /// 
        /// <param name="collection">
        /// EN: The collection to iterate.  
        /// BG: Колекцията, която ще се обхожда.
        /// </param>
        public static void ExecuteOnEachElement<Type>(Action<Type> command, IEnumerable<Type> collection)
        {
            ArgumentNullException.ThrowIfNull(command);
            ArgumentNullException.ThrowIfNull(collection);

            foreach (Type element in collection)
            {
                command(element);
            }
        }

        /// <summary>
        /// 
        /// EN: Iterates over the object array and executes the method on each element.  
        ///  
        /// BG: Обхожда масив от обекти и изпълнява метода върху всеки от тях.
        /// 
        /// </summary>
        /// 
        /// <param name="command">
        /// EN: The method to execute.  
        /// BG: Методът, който ще се изпълни.
        /// </param>
        /// 
        /// <param name="objects">
        /// EN: The array of objects to iterate.  
        /// BG: Масивът от обекти, който ще се обхожда.
        /// </param>
        public static void ExecuteOnEachElement(Action<object> command, object?[]? objects)
        {
            ArgumentNullException.ThrowIfNull(command);

            foreach (object? value in objects!)
            {
                command(value!);
            }
        }

        /// <summary>
        /// 
        /// EN: Converts a collection to an array of objects.  
        ///  
        /// BG: Преобразува колекцията в масив от обекти.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The collection to convert.  
        /// BG: Колекцията, която ще се преобразува.
        /// </param>
        /// 
        /// <returns>
        /// EN: Array of objects.  
        /// BG: Масив от обекти.
        /// </returns>
        public static object?[] ConvertToObjectArray<DataType>(IEnumerable<DataType> collection)
        {
            object[] objects = new object[collection.Count()];
            int index = default;

            foreach (DataType element in collection)
            {
                objects[index++] = element!;
            }

            return objects;
        }

        /// <summary>
        /// 
        /// EN: Converts an external collection to a CommonLibrary Collection.  
        ///  
        /// BG: Преобразува външна колекция в колекция от тип данни CommonLibrary.Collections.Generic.Collection.
        /// 
        /// </summary>
        /// 
        /// <typeparam name="Type">
        /// EN: The data type of the elements.  
        /// BG: Типът на елементите.
        /// </typeparam>
        /// 
        /// <param name="externCollection">
        /// EN: The external collection to convert.  
        /// BG: Външната колекция, която ще се преобразува.
        /// </param>
        /// 
        /// <returns>
        /// EN: A new Collection with copied elements.  
        /// BG: Нова колекция с копирани елементи.
        /// </returns>
#pragma warning disable IDE0306 , IDE0028
        public static Collection<Type> ReturnAsCollection<Type>(IEnumerable<Type> externCollection)
            => new(externCollection);
#pragma warning restore IDE0306, IDE0028

        /// <summary>
        /// 
        /// EN: Counts the elements that match a condition in the collection.  
        ///  
        /// BG: Брои елементите, които отговарят на условието в колекцията.
        /// 
        /// </summary>
        /// 
        /// <typeparam name="Type">
        /// EN: The data type of the elements.  
        /// BG: Типът на елементите.
        /// </typeparam>
        /// 
        /// <param name="collection">
        /// EN: The collection to check.  
        /// BG: Колекцията, която ще се проверява.
        /// </param>
        /// 
        /// <param name="condition">
        /// EN: The condition to match.  
        /// BG: Условието, което трябва да се изпълни.
        /// </param>
        /// 
        /// <returns>
        /// EN: Number of matching elements.  
        /// BG: Броя на съвпадащите елементи.
        /// </returns>
        public static int CountOfMatches<Type>(IEnumerable<Type> collection, Predicate<Type> condition)
        {
            int counter = default;

            foreach (Type element in collection)
            {
                if (condition(element))
                {
                    counter++;
                }
            }

            return counter;
        }

        /// <summary>
        /// 
        /// EN:
        ///    Creates empty array with
        ///    the specified capacity.
        /// 
        /// BG:
        ///    Създава нов празен масив с указания капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        ///  EN: The capacity of the array.
        ///  BG: Капацитета на масива.
        /// </param>
        ///
        /// <returns>
        ///  EN: Empty array.
        ///  BG: Празен масив.
        /// </returns>
        public static IEnumerable<Type> CreateEmptyCollection<Type>(int capacity)
            => new Collection<Type>(capacity);

        /// <summary>
        /// 
        /// EN:
        ///    Creates an array with the copied elements from another array and with
        ///    capacity equals the count of the elements.
        /// 
        /// BG:
        ///    Създава нов масив с копирани елементи от външния масив и капацитет 
        ///    равен на бройката на елементите.
        /// 
        /// </summary>
        /// 
        /// <param name="externCollection">
        ///  EN: The extern collection witch elements will be copied.
        ///  BG: Външната колекция, чийто елементи да бъдат копирани.
        /// </param>
        ///
        /// <returns>
        ///  EN: Array with the copied elements as IEnumerable.
        ///  BG: Масив с копираните елементи като IEnumerable.
        /// </returns>
        public static IEnumerable<Type> CreateFromAnother<Type>(IEnumerable<Type> externCollection)
            => [.. externCollection];

        /// <summary>
        /// 
        /// EN:
        ///    Creates an array with the copied elements from another array and with
        ///    the specified capacity.
        /// 
        /// BG:
        ///    Създава нов масив с копирани елементи от външния масив и капацитет 
        ///    равен на указания.
        /// 
        /// </summary>
        /// 
        /// <param name="externCollection">
        ///  EN: The extern collection witch elements will be copied.
        ///  BG: Външната колекция, чийто елементи да бъдат копирани.
        /// </param>
        /// 
        /// /// <param name="capacity">
        ///  EN: The capacity of the array.
        ///  BG: Капацитета на масива.
        /// </param>
        ///
        /// <returns>
        ///  EN: Array with the copied elements as IEnumerable.
        ///  BG: Масив с копираните елементи като IEnumerable.
        /// </returns>
        public static IEnumerable<Type> CreateFromAnother<Type>(IEnumerable<Type> externCollection, int capacity)
            => new Collection<Type>(externCollection, capacity); 

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
        /// <typeparam name="Type">
        ///  EN: The data type of the elements in the collection.
        ///  BG: Типът данни на елементите в колекцията.
        /// 
        /// </typeparam>
        /// 
        /// <param name="collection">
        ///  EN: The collection to be sorted.
        ///  BG: Колекцията, която да бъде сортирана.
        /// 
        /// </param>
        /// 
        /// <param name="options">
        ///  EN: The sorting options.
        ///  BG: Опцийте за сортиране.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The same collection, but sorted.
        ///  BG: Връща колекцията, но сортирана по желания начин.
        /// </returns>
        public static IEnumerable<Type> SortArrayBy<Type>(IEnumerable<Type> collection, SortingOptions options) 
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(collection);

            if (options == SortingOptions.Ascending)
            {
                return collection.Order();
            }
 
            return collection.OrderDescending();                            
        }

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
        /// <typeparam name="Type">
        ///  EN: The data type of the elements in the collection.
        ///  BG: Типът данни на елементите в колекцията.
        /// 
        /// </typeparam>
        /// 
        /// <param name="collection">
        ///  EN: The collection to be sorted.
        ///  BG: Колекцията, която да бъде сортирана.
        /// 
        /// </param>
        /// 
        /// <param name="options">
        ///  EN: The sorting options.
        ///  BG: Опцийте за сортиране.
        /// </param>
        /// 
        /// <returns>
        ///  EN: The same collection, but sorted.
        ///  BG: Връща колекцията, но сортирана по желания начин.
        /// </returns>
        public static Collection<Type> SortCollectionBy<Type>(IEnumerable<Type> collection, SortingOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(collection);

            if (options == SortingOptions.Ascending)
            {
                return [.. collection.Order()];
            }

            return  [.. collection.OrderDescending()];
        }

        /// <summary>
        ///  Clears the collection.
        /// </summary>
        /// 
        /// <typeparam name="Type">
        ///  The data type of the elements in the collection.
        /// </typeparam>
        /// 
        /// <param name="collection">
        ///  The collection to be cleared.
        /// </param>
        public static void Truncate<Type>(ref Type?[] collection)
            => collection = [];

        /// <summary>
        ///  Searches for a value in a numeric array and returns its index.
        /// </summary>
        /// 
        /// <typeparam name="Type">
        ///  The numeric data type of the elements in the array.
        /// </typeparam>
        /// 
        /// <param name="array">
        ///  The array to be searched.
        /// </param>
        /// 
        /// <param name="value">
        ///  The value to be searched.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the found value or -1 if not found.
        /// </returns>
        public static int Search<Type>(Type[] array, Type value)
            where Type : INumber<Type>
        {
            for (int i = default; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return i; 
                }
            }

            return -1;
        }

        /// <summary>
        ///  Searches for a value in a numeric array and returns its index.
        /// </summary>
        /// 
        /// <typeparam name="Type">
        ///  The numeric data type of the elements in the array.
        /// </typeparam>
        /// 
        /// <param name="array">
        ///  The array to be searched.
        /// </param>
        /// 
        /// <param name="value">
        ///  The value to be searched.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the found value or -1 if not found.
        /// </returns>
        public static int BinarySearch<Type>(Type[] array, Type value)
            where Type : INumber<Type>
        {
            int start = default;
            int end = array.Length - 1;

            while (end >= start)
            {
                int midIndex = (start + end) / 2;
              
                if (array[midIndex] == value) 
                {
                    return midIndex; // Ω (log2(value)) - one operation.
                }
                else if (array[midIndex] < value)
                {
                    start = midIndex + 1;
                }
                else if (array[midIndex] > value)
                {
                    end = midIndex - 1;
                }
            }

            return -1;
        }
    }
}