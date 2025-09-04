// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using CommonLibrary.Attributes;
using CommonLibrary.Collections;
using System.Collections.Generic;
using CommonLibrary.Base.Abstract;

namespace CommonLibrary.AbstractDataTypes
{
    /// <summary>
    /// 
    /// EN:
    ///   Provides funcionality for a queue.
    ///   The queue is a FIFO data structore: First In - First Out.
    ///   First element that will enter the queue will come out first.
    ///   
    /// BG:
    ///   Предоставя необходимите функций и методи,
    ///   необходими на опашка. Опашката е FIFO структора от данни: 
    ///   First In - First Out. Първия елемент влязъл в опашката ще излезе пръв от нея.
    ///   Последния ще излезе последен.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Queue of data")]
    public class DataQueue<DataType> : DataQueueBase<DataType>, ICloneable
    {
        //
        // Holds the elements of the queue.
        //
        // Съдържа елементите на опашката.
        //
        private readonly Collection<DataType?> _data;

        //
        // Default capacity
        //
        // Капацитет по подразбиране.
        //
        private const int DefCapacity = 5;

        //
        // Maximum capacity
        //
        // Mаксимален капацитет.
        //
        private const int MaxCapacity = 1_000_000_000;


        /// <summary>
        /// 
        /// EN:
        ///   Get the first element in the queue.
        ///   
        /// BG:
        ///   Достъпва първия елемент на опашката.
        /// 
        /// </summary>
        public override DataType? FirstElement
            => _data != null ? _data[0] : default;

        /// <summary>
        /// 
        /// EN:
        ///   Get the last element in the queue.
        ///   
        /// BG:
        ///   Достъпва последния елемент на опашката.
        /// 
        /// </summary>
        public override DataType? LastElement
            => _data != null ? _data[^1] : default;

        /// <summary>
        /// 
        /// EN:
        ///   Gets or sets the capacity of the queue.
        ///   
        /// BG:
        ///   Достъпва капацитета на опашката.
        /// 
        /// </summary>
        public int Capacity
        {
            get => _data.Capacity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);

                if (value < DefCapacity)
                {
                    throw new Error("The capacity can not be less than the default capacity of 5.");
                }
                else if (value > MaxCapacity)
                {
                    throw new Error("The capacity can not be greater than maximum capacity of one bilion.");
                }

                _data.Capacity = value;
            }
        }

        /// <summary>
        /// 
        /// EN:
        ///   Gets the count of the elements in the queue.
        ///   
        /// BG:
        ///   Достъпва броя на елементите в опашката.
        /// 
        /// </summary>
        public int Count
            => _data != null ? _data.Count : 0;
        


        /// <summary>
        /// 
        /// EN:
        ///   Creates empty queue with the default capacity
        ///   
        /// BG:
        ///   Създава празна опашка със капацитет по подразбиране.
        /// 
        /// </summary>
        public DataQueue()
            => _data = new(DefCapacity);

        /// <summary>
        /// 
        /// EN:
        ///   Creates empty queue with the specified capacity
        ///   
        /// BG:
        ///   Създава празна опашка с указания капацитет.
        /// 
        /// </summary>
        public DataQueue(int capacity)
            : this()
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates empty queue with the maximum capacity
        ///   
        /// BG:
        ///   Създава празна опашка с максимален капацитет.
        /// 
        /// </summary
        public DataQueue(bool maxCapacity)
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
        ///   Creates new queue with the elements from the specified collection
        ///   and capacity = the count of the elements.
        ///   
        /// BG:
        ///   Създава опашка с копирани елементи от указаната колекция и с капацитет
        ///   равен на бройят им.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        ///  EN: The collection.
        ///  BG: Колекцията.
        /// </param>
        public DataQueue(IEnumerable<DataType> collection)
            => _data = [.. collection];


        /// <summary>
        /// 
        /// EN:
        ///   Creates new queue with the elements from the specified collection
        ///   and specified capacity.
        ///   
        /// BG:
        ///   Създава опашка с копирани елементи от указаната колекция и с 
        ///   указания капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        ///  EN: The collection.
        ///  BG: Колекцията.
        /// </param>
        /// 
        /// <param name="capacity">
        ///  EN: The capacity.
        ///  BG: Капацитета.
        /// </param>
        public DataQueue(IEnumerable<DataType> collection, int capacity)
            : this(collection)
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Creates new queue with the elements from the specified collection
        ///   and maximum capacity.
        ///   
        /// BG:
        ///   Създава опашка с копирани елементи от указаната колекция и с 
        ///   максимален капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        ///  EN: The collection.
        ///  BG: Колекцията.
        /// </param>
        /// 
        /// <param name="maxCapacity">
        ///  EN: Indicates when to use maximum capacity.
        ///  BG: Индикира, когато трябва да се използва максималния капацитет.
        /// </param>
        public DataQueue(IEnumerable<DataType> collection, bool maxCapacity)
            : this(collection)
        {
            if (maxCapacity)
            {
                Capacity = MaxCapacity;
            }
        }


        /// <summary>
        /// 
        /// EN:
        ///   Adds an element to the queue.
        ///   
        /// BG:
        ///   Добавя елемент на опашката.
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        ///  EN: The element.
        ///  BG:  Елемента.
        /// </param>
        public override void Increase(DataType? element)
            => _data.Add(element);

        /// <summary>
        /// 
        /// EN:
        ///   Removes the first element from the queue.
        ///   
        /// BG:
        ///   Премахва първият елемент от опашката.
        /// 
        /// </summary>
        public override void Decrease()
            => _data.RemoveElementAt(0);

        /// <summary>
        /// 
        /// EN:
        ///   Get the element at that index in the queue.
        ///   
        /// BG:
        ///   Достъпва стойността на този индекс в опашката.
        ///   Опашката не подържа дирекно индексиране.
        /// 
        /// </summary>
        /// <param name="index"></param>
        public override DataType? ViewAt(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index > _data.Capacity)
            {
                throw new Error("The index can not be greater than the actual capacity.");
            }

            return _data[index];
        }

        /// <summary>
        /// 
        /// EN:
        ///   Clears the queue.
        ///   
        /// BG:
        ///   Изчиства опашката.
        /// 
        /// </summary>
        public override void Clear()
            => _data.TruncateCollection();

        /// <summary>
        /// 
        /// EN: 
        ///   Returns the queue as object.
        ///   
        /// BG:
        ///   Връща опашката като обект  
        /// 
        /// </summary>
        public object Clone()
            => this;
    }
}