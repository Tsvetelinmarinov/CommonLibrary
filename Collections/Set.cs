// CommonLibrary - library for common usage

using System;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Exceptions;
using System.Collections.Generic;
using CommonLibrary.Base.Interfaces;

namespace CommonLibrary.Collections
{
    /// <summary>
    /// 
    ///  Describes a set of values. The set is data structure that 
    ///  keeps unique elements. The set is implemented by array of values. 
    ///  The capacity of the set is not dynamic, so should be manual changed
    ///  by the "SetCapacity" method. The set has maximum capacity of 1000 elements
    ///  so it's maded for small amount of data, but fast operations.
    ///  
    /// </summary>
    [Description("Generic set of values")]
    [Usage("Use set when you need collection with fast operations and unique values")]
    public class Set<Type> : IDataSet<Type>, IEnumerable<Type>, ICloneable
    {
        // Holds the elements of the set.
        private Type[] _elements;

        // The default capacity of the set.
        private const int DefCapacity = 0;

        // The maximum capacity of the set.
        private const int MaxCapacity = 1000;


        /// <summary>
        ///  Get the count of the elements in the set.
        /// </summary>
        public int Count
            => GetCount();

        /// <summary>
        ///  Gets or sets the capacity of the set.
        ///  That is the length of the inner array with values.
        /// </summary>
        public int Capacity
        {
            get => _elements.Length;
            set => SetCapacity(value);
        }


        /// <summary>
        ///  Creates an empty set with the default capacity.
        /// </summary>
        public Set()       
            => _elements = new Type[DefCapacity];

        /// <summary>
        ///  Creates an empty set with the specified capacity.
        /// </summary>
        public Set(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity);
            
            if (capacity < DefCapacity)
            {
                capacity = DefCapacity;
            }
            else if (capacity > MaxCapacity)
            {
                capacity = MaxCapacity;
            }

            _elements = new Type[capacity];
        }

        /// <summary>
        ///  Creates an empty set with the maxium capacity of 
        ///  1 bilion elements. Note that if the value of "maxCapacity"
        ///  is False, the set will be created with the default capacity.
        /// </summary>
        /// 
        /// <param name="maxCapacity">
        ///  Indicates when to use the maximum capacity.
        /// </param>
        public Set(bool maxCapacity) 
            : this()
        {
            if (maxCapacity)
            {
                SetCapacity(MaxCapacity);
            }
        }

        /// <summary>
        ///  Creates new set with the copied elements from the other 
        ///  collection, and capacity = the cout of the copied elements.
        /// </summary>
        /// 
        /// <param name="collection">
        ///  The extern collection, wich elements will be copied.
        /// </param>
        public Set(IEnumerable<Type> collection)
            => _elements = CopyFromExtern(collection);

        /// <summary>
        ///  Creates new set with the copied elements from the extern collection
        ///  and the specified capacity.
        /// </summary>
        /// 
        /// <param name="collection">
        ///  The extern collection.
        /// </param>
        /// 
        /// <param name="capacity">
        ///  The capacity of the set.
        /// </param>
        public Set(IEnumerable<Type> collection, int capacity)
            => _elements = CopyWithCapacity(collection, capacity);

        /// <summary>
        ///  Creates new set with the copied elements from the
        ///  extern collection and with maximum capacity. Note that
        ///  the maximum capacity will be used only if "maxCapacity" is True.
        /// </summary>
        /// 
        /// <param name="collection">
        ///  The extern collection.
        /// </param>
        /// 
        /// <param name="maxCapacity">
        ///  Indicates when to use maximum capacity.
        /// </param>
#pragma warning disable CS8618
        public Set(IEnumerable<Type> collection, bool maxCapacity)
        {
            if (maxCapacity)
            {
                _elements = CopyWithCapacity(collection, MaxCapacity);
            }
            else
            {
                _elements = CopyWithCapacity(collection, DefCapacity);
            }
        }
#pragma warning restore CS8618


        /// <summary>
        ///  Adds the element to the set.
        ///  The order of the elements can not be guarantied.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        public void Add(Type element)
        {
            ArgumentNullException.ThrowIfNull(element);
           
            if (_elements.Contains(element))
            {
                throw new Error("The set can store only unique items.");
            }

            if (Count == Capacity)
            {
                SetCapacity(_elements.Length + 1);
            }

            _elements[Count] = element;
        }

        /// <summary>
        ///  Adds the collection to the set.
        /// </summary>
        /// 
        /// <param name="collection">
        ///  The collection wich elements will be added.
        /// </param>
        /// 
        /// <exception cref="Error">
        ///  The extern collection is empty.
        /// </exception>
        public void AddMany(IEnumerable<Type> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (Type element in collection)
            {
                if (!_elements.Contains(element))
                {
                    if (Count == Capacity)
                    {
                        SetCapacity(_elements.Length + 1);
                    }

                    _elements[Count] = element;
                }
            }
        }

        /// <summary>
        ///  Removes the element from the set, if the same exist.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The elemenet to be removed.
        /// </param>
        public void Remove(Type element)
        {
            ArgumentNullException.ThrowIfNull(element);

            if (_elements.Contains(element))
            {
                _elements = _elements
                    .Where(value => !Equals(value, element))
                    .ToArray();
            }
        }

        /// <summary>
        ///  Clears the set and resets the capacity to the
        ///  default capacity of 0 elements.
        /// </summary>
        public void Truncate()
            => EmptySet();

        /// <summary>
        ///  Check for the element in the set.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be searched.
        /// </param>
        /// 
        /// <returns>
        ///  True if the element exist in the set, otherwise - False.
        /// </returns>
        public bool ContainsElement(Type element)
            => _elements.Contains(element);

        /// <summary>
        ///  Returns the set as array.
        /// </summary>
        public Type[] ReturnAsArray()
            => [.. _elements];

        /// <summary>
        ///  Returns the set as collection.
        /// </summary>
        public Collection<Type> ReturnAsCollection()
            => [.. _elements];

        /// <summary>
        ///  Returns the set as object.
        /// </summary>
        public object Clone()
            => this;

        
        #region Private Members

        //  Copies the elements from the extern collection/array in a new array.
        private Type[] CopyFromExtern(IEnumerable<Type> ext)
        {
            Type[] buffer = new Type[ext.Count()];
            int index = default;

            foreach (Type element in ext)
            {
                buffer[index++] = element;
            }

            return buffer;
        }

        // Clears the set and resets the capacity.
        private void EmptySet()
        {
            Type[] newBuffer = new Type[DefCapacity];
            _elements = newBuffer;

            for (int i = 0; i < _elements.Length; ++i)
            {
                _elements[i] = default!;
            }
        }

        // Copy elements from extern collection with specified length - capacity.
        private Type[] CopyWithCapacity(IEnumerable<Type> ext, int capacity)
        {
            Type[] array = new Type[capacity];
            int index = default;

            foreach (Type element in ext)
            {
                array[index++] = element; 
            }

            return array;
        }

        // Changes the capacity of the set.
        private void SetCapacity(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity);

            if (capacity < DefCapacity)
            {
                capacity = DefCapacity;
            }
            else if (capacity > MaxCapacity)
            {
                capacity = MaxCapacity;
            }

            if (capacity < _elements.Length)
            {
                throw new Error("The capacity can not be less than the count of the elements.");
            }

            _elements = CopyWithCapacity(_elements, capacity);
        }

        //  Gets the count of the elements in the set.
        private int GetCount()
        {
            int counter = default;

            foreach (Type element in _elements)
            {
                if (!Equals(element, null) && !Equals(element, 0))
                {
                    counter++;
                }
            }

            return counter;
        }

        #endregion

        #region Enumerator

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}