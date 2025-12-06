// ICollection interface

namespace CommonLibrary.Interfaces
{
    using System.Collections.Generic;
    
    /// <summary>
    ///  Defines properties and methods to describe a collection 
    ///  of elements.
    /// </summary>
    /// 
    /// <typeparam name="T">
    ///  The data type of the elements of the collection.
    /// </typeparam>
    public interface IGenericCollection<T> : IReadOnlyCollection<T>
    {
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
        T this[int index] { get; set; }

        /// <summary>
        ///  Gets the capacity of the collection.
        /// </summary>
        int Capacity { get; }


        /// <summary>
        ///  Adds an element to the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        void Add(T element);

        /// <summary>
        ///  Removes an element from the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be removed.
        /// </param>
        T? Remove(T element);

        /// <summary>
        ///  Checks if the specified element exists in
        ///  the collection.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element that will be checked.
        /// </param>
        bool Contains(T element);

        /// <summary>
        ///  Clears the collection.
        /// </summary>
        void Clear();
    }
}