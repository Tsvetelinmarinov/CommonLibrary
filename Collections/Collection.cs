// Collection of elements

namespace CommonLibrary.Collections
{
    using System.Collections.Generic;
    using Interfaces;
    using System.Collections;
    using System.Linq;
    using System;

    /// <summary>
    ///  Strongly-typed collection of elements.
    /// </summary>
    /// 
    /// <typeparam name="T">
    ///  The data type of the elements in the collection.
    /// </typeparam>
    public sealed class Collection<T> : IGenericCollection<T>
    {
        private T?[]? _data;
        private int _count;
        private const int DefaultCapacity = 6;


        /// <summary>
        ///  Indexer.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index in the collection.
        /// </param>
        /// 
        /// <returns>
        ///  The element at that index in the collection.
        /// </returns>
        public T this[int index] 
        { 
            get
            {
                if (index >= 0 && index < this._data!.Length)
                {
                    return this._data![index]!;
                }
                
                const string message = "The index cannot be negative and the index cannot be" +
                                       "greater than the actual count of the elements.";

                throw new ArgumentOutOfRangeException(nameof(index), message);

            }
            set
            {
                if (index < 0 || index >= this._data!.Length)
                {
                    string message = "The index cannot be negative and the index cannot be" +
                        "greater than the actual count of the elements.";

                    throw new ArgumentOutOfRangeException(nameof(index), message);
                }

                this._data[index] = value;
            }
        }

        /// <summary>
        ///  Gets the count of the elements
        ///  in the collection.
        /// </summary>
        public int Count
        {
            get => this._count;
            private set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                this._count = value;
            }
        }

        /// <summary>
        ///  Gets the capacity of the collection.
        ///  The collection resizes when the capacity is reached.
        /// </summary>
        public int Capacity => this._data!.Length;


        /// <summary>
        ///  Creates new empty collection with the
        ///  default capacity of six elements.
        /// </summary>
        public Collection()
        {
            this._data = new T[DefaultCapacity];
        }

        /// <summary>
        ///  Creates new empty collection with the specified capacity.
        /// </summary>
        /// 
        /// <param name="capacity">
        ///  The capacity of the collection.
        /// </param>
        public Collection(int capacity)
        {
            this._data = new T[capacity];
        }

        /// <summary>
        ///  Creates new collection with the elements from the specified IEnumerable
        ///  and capacity = the count of the elements.
        /// </summary>
        /// 
        /// <param name="array">
        ///  The extern enumerable with the values.
        /// </param>
        public Collection(IEnumerable<T> array)
        {
            var enumerable = array as T[] ?? array.ToArray();
            this._data = [.. enumerable];
            this.Count = enumerable!.Length;
        }

        /// <summary>
        ///  Creates new collection with the elements from the specified array and 
        ///  with specified capacity.
        /// </summary>
        /// 
        /// <param name="array">
        ///  The array with values.
        /// </param>
        /// 
        /// <param name="capacity">
        ///  The capacity of the collection.
        /// </param>
        public Collection(IEnumerable<T> array, int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

            if (capacity < DefaultCapacity)
            {
                capacity = DefaultCapacity;
            }

            this._data = new T[capacity];
            this.Copy([.. array ?? []], this._data!); // If the IEnumerable is
                                                                      // null the internal array
                                                                     // left empty.

            this.Count = array!.Count();
        }


        /// <summary>
        ///  Adds an element to the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        public void Add(T element)
        {
            this.AddElement(element);
        }

        /// <summary>
        ///  Adds the elements to the collection.
        /// </summary>
        /// 
        /// <param name="elements">
        ///  The elements to be added.
        /// </param>
        public void Add(params T[] elements)
        {
            this.AddManyElements(elements);
        }

        /// <summary>
        ///  Removes an element from the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be removed.
        /// </param>
        /// 
        /// <returns>
        ///  The element that is removed, or the default value of the
        ///  data type if there are no matches.
        /// </returns>
        public T? Remove(T element)
        {
            return this.RemoveElement(element);
        }

        /// <summary>
        ///  Checks if the specified element exists in
        ///  the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element that will be checked.
        /// </param>
        public bool Contains(T element)
        {
            return this.CheckForElement(element);
        }

        /// <summary>
        ///  Clears the collection.
        /// </summary>
        public void Clear()
        {
            this.Truncate();
        }

        /// <summary>
        ///  Changes the capacity of the collection.
        /// </summary>
        /// 
        /// <param name="capacity">
        ///  The new capacity
        /// </param>
        public void ChangeCapacity(int capacity)
        {
            if (capacity < this._data!.Length)
            {
                string message = "The capacity cannot be less that the actual " +
                    "count of the elements.";

                throw new ArgumentOutOfRangeException(nameof(capacity), message);
            }

            /*
             * The _data.Length will be at least DefaultCapacity or above,
             * and there is no other way to be different,
             * but i need to be sure..
             */
            if (capacity < DefaultCapacity)
            {
                capacity = DefaultCapacity;
            }

            T[] newArray = new T[capacity];

            this.Copy(this._data!, newArray);
            this._data = newArray;
        }

        /// <summary>
        ///  Executes the method with each element in the collection.
        /// </summary>
        /// 
        /// <param name="action">
        /// The delegate to be executed.
        /// </param>
        public void ForEach(Action<T> action)
        {
            this.ExecuteOnEachElement(action);
        }

        /// <summary>
        ///  Returns the element that match the condition, defined
        ///  with the predicate or default value if there are no matches.
        /// </summary>
        /// 
        /// <param name="condition">
        ///  The condition.
        /// </param>
        /// 
        /// <returns>
        ///  True if the element matches the condition, otherwise False.
        /// </returns>
        public T? FindElementThat(Predicate<T> condition)
        {
            return this.GetElementByPredicate(condition);
        }

        /// <summary>
        ///  Extracts all elements that match the condition i a list.
        /// </summary>
        /// 
        /// <param name="condition">
        ///  The condition defined by a Predicate.
        /// </param>
        /// 
        /// <returns>
        ///  List with the values that match the condition or
        ///  empty list if there are no matches.
        /// </returns>
        public List<T> FindElementsThat(Predicate<T> condition)
        {
            return this.GetElementsByPredicate(condition);
        }

        /// <summary>
        ///  Find the index of the specified element.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element.
        /// </param>
        /// 
        /// <returns>
        ///  The index of the element if the same exists or -1 if there
        ///  are no matches.
        /// </returns>
        public int IndexOf(T element)
        {
            return this.FindIndex(element);
        }

        #region Core

        private void Copy(T[] original, T[] destination)
        {
            int counter = default;
            foreach (T element in original)
            {

                destination[counter++] = element;
            }
        }
        private void AddElement(T element)
        {
            if (this.Count == this.Capacity)
            {
                T[] newArray = new T[this.Capacity * 2];
                this.Copy(this._data!, newArray);
                this._data = newArray;
            }

            this._data![this.Count] = element;
            this.Count++;
        }
        private T? RemoveElement(T element)
        {
            int index = this._data.IndexOf(element);

            if (index is not -1)
            {
                T? value = this._data![index];
                this._data![index] = default;
                this.Count--;

                return value;
            }

            return default;
        }
        private bool CheckForElement(T element)
        {
            HashSet<T> set = this._data!.ToHashSet<T>();
            return set.Contains(element);
        }
        private void Truncate()
        {
            this._data = new T[DefaultCapacity];
            this.Count = 0;
        }
        private void ExecuteOnEachElement(Action<T> command)
        {
            ArgumentNullException.ThrowIfNull(command);

            foreach (var element in this._data!)
            {
                command(element!);
            }
        }
        private void AddManyElements(params T[] elements)
        {
            foreach (var element in elements)
            {
                this.AddElement(element);
            }
        }
        private T? GetElementByPredicate(Predicate<T> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);

            for (var i = 0; i < this._data!.Length; i++)
            {
                if (condition(this._data[i]!))
                {
                    return this._data[i];
                }
            }

            return default;
        }
        private List<T> GetElementsByPredicate(Predicate<T> condition)
        {
            ArgumentNullException.ThrowIfNull(condition);
            List<T> elements = [];

            for (var i = 0; i < this._data!.Length; ++i)
            {
                if (condition(this._data[i]!))
                {
                    elements.Add(this._data[i]!);
                }
            }

            return elements;
        }
        private int FindIndex(T element)
        {
            ArgumentNullException.ThrowIfNull(element);

            for (var i = 0; i < this._data!.Length; ++i)
            {
                if (Equals(this._data[i], element))
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region Enumerator

        /// <summary>
        ///  The enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._data!.Select(element => element!).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion
    }
}