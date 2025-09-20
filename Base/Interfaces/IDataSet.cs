// CommonLibrary - library for common usage

using CommonLibrary.Exceptions;
using CommonLibrary.Collections;
using System.Collections.Generic;

namespace CommonLibrary.Base.Interfaces
{
    /// <summary>
    ///  Base interface for a set.
    ///  Describes a set of values. The set is data structure that 
    ///  keeps unique elements. The Hash Function generates unique value for every element.
    /// </summary>
    public interface IDataSet<Type>
    {
        /// <summary>
        ///  Get the length of the set. That is the
        ///  count of the elements in the set.
        /// </summary>
        public abstract int Count
        {
            get;
        }


        /// <summary>
        ///  Adds the element to the set.
        ///  The order of the elements can not be guarantied.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        public abstract void Add(Type element);

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
        public void AddMany(IEnumerable<Type> collection);

        /// <summary>
        ///  Removes the element from the set, if the same exist.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The elemenet to be removed.
        /// </param>
        public abstract void Remove(Type element);

        /// <summary>
        ///  Clears the set.
        /// </summary>
        public abstract void Truncate();

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
        public abstract bool ContainsElement(Type element);

        /// <summary>
        ///  Returns the current set as array.
        /// </summary>
        /// 
        /// <returns>
        ///  An array with the values copied from the set.
        /// </returns>
        public abstract Type[] ReturnAsArray();

        /// <summary>
        ///  Returns the current set as collection of 
        ///  data type CommonLibrary.Collections.Collection.
        /// </summary>
        /// 
        /// <returns>
        ///  Collection with the values copied from the set.
        /// </returns>
        public abstract Collection<Type> ReturnAsCollection();
    }
}