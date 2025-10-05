// CommonLibrary - library for common usage.

using System;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary.Collections
{
    /// <summary>
    ///  Defines stringly-typed collection of data with FIFO structore(First In - First Out).
    ///  So the first element that enters the queue will come out first and the last element
    ///  that enters the queue will come out last.
    /// </summary>
    /// 
    /// <typeparam name="Type">
    ///  The data type of the elements in the queue.
    /// </typeparam>
    [Description("Queue of data which is FIFO data structore")]
    public class DataQueue<Type>
    {
        // The modular array with the values of the queue.
        private readonly ModularArray<Type> buffer;

        // The count of the elements in the queue.
        private int count;


        /// <summary>
        ///  Gets the number of the elements in the queue.
        /// </summary>
        public int Count
        {
            get => this.count;
            private set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                this.count = value;
            }
        }

        /// <summary>
        ///  Gets the values of the queue in an array.
        /// </summary>
        public Type?[] Values
            => this.buffer.Values;


        /// <summary>
        ///  Creates new empty queue.
        /// </summary>
        public DataQueue()
        {
            this.buffer = new();
            this.count = 0;
        }

        /// <summary>
        ///  Creates new queue with copied elements from the 
        ///  specified array.
        /// </summary>
        /// 
        /// <param name="array">
        ///  The array which elements will be copied.
        /// </param>
        public DataQueue(IEnumerable<Type> array)
        {
            this.buffer = new(array);
            this.count = this.buffer.Count;
        }


        /// <summary>
        ///  Adds an element to the end of the queue.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        public void Enter(Type element)
            => IncreaseQueue(element);

        /// <summary>
        ///  Adds the specified elements from the array to the queue.
        /// </summary>
        /// 
        /// <param name="elements">
        ///  The array with the elements.
        /// </param>
        public void Enter(Type[] elements)
            => IncreateMultiple(elements);

        /// <summary>
        ///  Removes the element in the beginning of the queue
        ///  and returns it.
        /// </summary>
        public Type PullOut()
            => DecreaseQueue();

        /// <summary>
        ///  Removes the specified amount of elements from the
        ///  queue starting from the beginning.
        /// </summary>
        /// 
        /// <param name="count">
        ///  The count of the elements to remove.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the removed values.
        /// </returns>
        public Type[] PullOut(int count)
            => DecreaseMultiple(count);

        /// <summary>
        ///  Returns the first element in the queue without removing it.
        /// </summary>
        public Type ViewFirst()
            => PeekQueueHead();

        /// <summary>
        ///  Clears the queue.
        /// </summary>
        public void Clear()
            => Truncate();

        /// <summary>
        ///  Converts the queue to a list.
        /// </summary>
        public List<Type> AsList()
            => [.. this.buffer.Values];

        /// <summary>
        ///  Converts the queue to a collection.
        /// </summary>
        public Collection<Type> AsCollection()
            => [.. this.buffer.Values];

        /// <summary>
        ///  Converts the queue to a set.
        /// </summary>
        public HashSet<Type> AsSet()
            => [.. this.buffer.Values];

        /// <summary>
        ///  Converts the queue to a linked list.
        /// </summary>
        public LinkedList<Type> AsLinkedList()
            => new(this.buffer.Values!);


        #region Queue Core

        // Adds an element to the queue.
        private void IncreaseQueue(Type element)
        {
            ArgumentNullException.ThrowIfNull(element);

            this.buffer.Add(element, ModulePosition.Tail);
            this.Count++;
        }

        // Adds an array with elements to the queue.
        private void IncreateMultiple(Type[] array)
        {
            ArgumentNullException.ThrowIfNull(array);

            for (int i = 0; i < array.Length; i++)
            {
                ArgumentNullException.ThrowIfNull(array[i]);

                this.buffer.Add(array[i], ModulePosition.Tail);
                this.Count++;
            }
        }

        // Removes the first element in the queue.
        private Type DecreaseQueue()
        {
            Type? element = this.buffer.Remove(ModulePosition.Head);

            if (element == null)
            {
                throw new Error("The head value of the queue is null.");
            }

            this.Count--;
            return element;
        }

        // Removes multiple elements form the queue, starting from the beginning.
        private Type[] DecreaseMultiple(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            if (count > this.Count)
            {
                throw new Error("The count of the element to remove can not be greater that the actual count of the elements.");
            }

            Type[] array = new Type[count];

            for (int i = 0; i < count; i++)
            {
                array[i] = DecreaseQueue();
            }

            return array;
        }

        // Returns the first element in the queue without removing it.
        private Type PeekQueueHead()
            => this.buffer.Head!.Value ??
                 throw new Error("The element is null.");

        // Clears the queue.
        private void Truncate()
        {
            this.buffer.Clear();
            this.Count = default;
        }

        #endregion
    }
}
