// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Linq;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Interfaces;
using CommonLibrary.Exceptions;
using CommonLibrary.Collections;

namespace CommonLibrary.AbstractDataTypes
{
    /// <summary>
    /// 
    /// EN:
    ///    The stack is collection of data.
    ///    The first element that enters the stack, will come out last 
    ///    and the last element that enters the stack, will come out first.
    ///    That's mean the stack is LIFO (Last In - First Out) data structure.
    ///    That stack is strongly typed.
    ///    
    /// BG:
    ///   Стака е колекция от някакъв тип данни.
    ///   Първия елемент, който влезе в стака ще излезе от него последен.
    ///   Последния елемент, който взеле в стака, ще излезе пръв от него.
    ///   Това прави стака LIFO структора от данни, тоест Last In - First Out (Последен вътре - първи вън).
    ///   Стака е строго типизиран, което означава, че типът данни на елементите в него трябва
    ///   да се укаже изрично със създаването му.
    /// 
    /// </summary>
    /// 
    /// <typeparam name="DataType">
    ///  EN: The data type of the elements in the stack.
    ///  BG: Типът данни на елементите в стака.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Description("Stack of data")]
    [Usage("Use the DataStack when you need to collect some data in ordered and immutable way")]
    public class DataStack<DataType> : IDataStack<DataType>, ICloneable
    {
        //
        // Holds the elements of the stack.
        //
        // Съхранява елементите на стака.
        //
        private readonly DynamicArray<DataType?> _elements;

        //
        // The maximum capacity of the stack
        // of one bilion elements.
        //
        // Максимален апацитет на стака от
        // един милиард елемента.
        //
        private const int MaxCapacity = 1_000_000_000;

        //
        // The default capacity of the stack.
        //
        // Капацитета по подразбиране на стака.
        //
        private const int DefCapacity = 5;


        /// <summary>
        /// 
        /// EN:
        ///   View the first element in the stack.
        /// 
        /// BG:
        ///   Достъпва първия елемент в стака.
        /// 
        /// </summary>
        public DataType? FirstElement
        {
            get
            {
                if (_elements.Count > 0)
                {
                    return _elements[0];
                }

                return default!;
            }
        }

        /// <summary>
        /// 
        /// EN:
        /// 
        ///   View the last element in the stack.
        /// BG:
        ///   Достъпва последния елемент в стака.
        /// 
        /// </summary>
        public DataType? LastElement
        {
            get
            {
                if (_elements.Count > 0)
                {
                    return _elements[^1];
                }

                return default!;
            }
        }
        
        /// <summary>
        /// 
        /// EN:
        ///   Gets or sets the capacity of the stack.
        ///   
        /// BG:
        ///   Достъпва или променя капацитета на стака.
        /// 
        /// </summary>
        public int Capacity
        {
            get => _elements.Capacity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);

                if (value > MaxCapacity)
                {
                    throw new Error("The capacity can not be greater than the max capacity of one billion elements.");
                }

                _elements.Capacity = value;
            }
        }

        /// <summary>
        /// 
        /// EN:
        ///   Get the count of the elements in the stack.
        ///   
        /// BG:
        ///   Достъпва бройката на елементите в стака.
        /// 
        /// </summary>
        public int Count
        {
            get => _elements.Count;
        }


        /// <summary>
        /// 
        /// EN:
        ///   Creates new empty stack with the default capacity.
        ///   
        /// BG:
        ///   Създава нов празен стак с капацитет по подразбиране.
        /// 
        /// </summary>
        public DataStack()
        {
            _elements = new(DefCapacity);
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new empty stack with the specified capacity.
        ///   
        /// BG:
        ///   Създава нов празен стак с указания капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        ///  EN: The capacity.
        ///  BG: Капацитета.
        /// </param>
        public DataStack(int capacity)
            : this()
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new empty stack with the maximum capacity.
        ///   
        /// BG:
        ///   Създава нов празен стак с максимален капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="maxCapacity">
        ///  EN: Indicates when to use the maximum capacity.
        ///  BG: Индикира, кога да се използва максималния капацитет.
        /// </param>
        public DataStack(bool maxCapacity)
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
        ///   Adds an element to the end of the stack.
        ///   
        /// BG:
        ///   Добавя елемент на края на стака.
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        ///  EN: The element to be added.
        ///  BG: Елемента, който да бъде добавен.
        /// </param>
        public void Put(DataType? element)
            => _elements!.Add(element);

        /// <summary>
        /// 
        /// EN:
        ///   Adds array of elements to the end of the stack.
        ///   
        /// BG:
        ///   Добавя масив от елементи на края на стака.
        /// 
        /// </summary>
        /// 
        /// <param name="elements">
        ///  EN: The elements array to be added.
        ///  BG: Масива от елементи, който да бъде добавен.
        /// </param>
        public void Put(DataType?[] elements)
            => _elements!.AddMany(elements);

        /// <summary>
        /// 
        /// EN:
        ///   Removes the last element from the stack and returns it.
        ///   
        /// BG:
        ///   Премахва последния елемент от стака и го връща като стойност.
        /// 
        /// </summary>
        public DataType PullOut()
        {
            DataType last = LastElement!;
            _elements!.RemoveByValue(last);
            return last;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Remove the specified count of elements from the stack
        ///   and returns a collection with them.
        ///   
        /// BG:
        ///   Премахва желаното количество елементи от стака и ги 
        ///   връща в колекция.
        /// 
        /// </summary>
        /// 
        /// <param name="count">
        ///  EN: The number of the elements to be removed.
        ///  BG: Бройката на елементите, които да бъдат премахнати.
        /// </param>
        public DataType?[] PullOut(int count)
        {
            DataType[] pulledOut = new DataType[count];

            for (int i = count - 1; i >= 0; --i)
            {
                pulledOut[i] = PullOut();
            }

            return [.. pulledOut.Reverse()];
        }

        /// <summary>
        /// 
        /// EN:
        ///   Returns the element at that position in the stack
        ///   without removing it. The first element in the stack is 
        ///   at the first position and every next position is increased
        ///   by one.
        ///   
        /// BG:
        ///   Връща елемента на тази позиция в стака без да го премахва.
        ///   Първия елемент е на първа позиция и всяка следваща позиция
        ///   се инкрементира с едно;
        ///   
        /// 
        /// </summary>
        /// 
        /// <param name="position">
        ///  EN: The position of the element in the stack.
        ///  BG: Позицията на елемента в стака.
        /// </param>
        public DataType? ViewAt(int position)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(position);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(position, Capacity);

            return _elements![position - 1];
        }

        /// <summary>
        /// 
        /// EN:
        ///   Clears the stack. Removes everything.
        ///   
        /// BG:
        ///   Изчиства стака, премахва всичко от него.
        /// 
        /// </summary>
        public void Clear()
        {
            _elements!.RemoveAll();
            Capacity = DefCapacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Returns the stack as object.
        ///   
        /// BG:
        ///   Връща стака като обект.
        /// 
        /// </summary>
        public object Clone()
            => this;
    }
}