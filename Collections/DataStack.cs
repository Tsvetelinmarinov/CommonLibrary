// CommonLibrary - library for common usage.

using System;
using System.Linq;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using System.Collections.Generic;
using CommonLibrary.AbstractDataTypes;

namespace CommonLibrary.Collections
{
    /// <summary>
    ///  Defines stack of data. The stack is data structure of type LIFO (Last In - First Out) and
    ///  the first element that enters the stack will come out last, and the last element
    ///  that enters the stack will come out first. The maximum allowed capacity of the stack is
    ///  one billion elements and the default capacity is zero elements. The stack is implemented 
    ///  with a modular array which is similar to the linked list. That allows fast operation like adding
    ///  and removing without losing the idea of the stack.
    /// </summary>
    [Description("Stack of data with fast add and remove functions")]
    public class DataStack<Type>
    {
        // The modular array that keeps the linked modules that keeps the stack values.
        private readonly ModularArray<Type?> modules;

        // The count of the elements in the stack.
        private int count;


        /// <summary>
        ///  Gets the count of the elements in the stack.
        /// </summary>
        public int Count
        {
            get => this.count;
            private set
            {
                if (value < 0)
                {
                    throw new Error("The count of the elements in the stack can not be negative.");
                }

                this.count = value;
            }
        }

        /// <summary>
        ///  Gets all the values of the stack in an array.
        ///  The last value in the stack is the first value of
        ///  the returned array.
        /// </summary>
        public Type[] Values
            => GetValues();


        /// <summary>
        ///  Creates new empty stack.
        /// </summary>
        public DataStack()
        {
            this.modules = new();
            this.count = this.modules.Count;
        }

        /// <summary>
        ///  Creates new stack with copied elements from the extern array.
        /// </summary>
        /// 
        /// <param name="array">
        ///  The extern array which elements will be copied.
        /// </param>
        public DataStack(IEnumerable<Type> array)
        {
            this.modules = new(array);
            this.count = this.modules.Count;
        }


        /// <summary>
        ///  Adds an element to the top of the stack.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The element to be added.
        /// </param>
        public void Put(Type element)
            => IncreaseStack(element);

        /// <summary>
        ///  Adds an array of values to the top of the stack.
        /// </summary>
        /// 
        /// <param name="element">
        ///  The array of values to be added.
        /// </param>
        public void Put(Type[] element)
            => IncreaseStackMultiple(element);

        /// <summary>
        ///  Removes the element on the top of the stack.
        /// </summary>
        /// 
        /// <returns>
        ///  The element of the top.
        /// </returns>
        public Type PullOut()
            => RemoveTopElement();

        /// <summary>
        ///  Removes the specified amount of elements
        ///  from the stack.
        /// </summary>
        /// 
        /// <param name="count">
        ///  The count of the elements to be removed.
        /// </param>
        /// 
        /// <returns>
        ///  An array with the removed values.
        /// </returns>
        public Type[] PullOut(int count)
            => RemoveMultipleElements(count);

        /// <summary>
        ///  Peek at the top of the stack. This method does not remove the value.
        /// </summary>
        /// 
        /// <returns>
        ///  The top element of the stack.
        /// </returns>
        public Type ViewTop()
            => SeeTopElement();

        /// <summary>
        ///  Returns a collection with the stack values.
        /// </summary>
        public Collection<Type> AsCollection()
            => GetValuesCollection();

        /// <summary>
        ///  Returns a set with the stack values.
        /// </summary>
        public Set<Type> AsSet()
            => GetValuesSet();

        /// <summary>
        ///  Returns a modular array with the stack values.
        /// </summary>
        public ModularArray<Type> AsModularArray()
            => GetValuesModular();

        /// <summary>
        ///  Returns a list with the stack values.
        /// </summary>
        public List<Type> AsList()
            => GetValuesList();


        #region Stack Core Functionality

        // Returns an array with the stack values.
        private Type[] GetValues()
        {
            Type[] values = new Type[this.Count];
            Module<Type?>? module = modules.Head;
            int i = default;
            
            if (module == null)
            {
                return Array.Empty<Type>();
            }

            while (module != null)
            {
                values[i++] = module.Value!;
                module = module.Next;
            }

            return values
                .Reverse()
                .ToArray();
        }

        // Adds an element to the top of the stack.
        private void IncreaseStack(Type element)
        {
            if (element == null)
            {
                throw new Error("The element should not be null.");
            }

            this.modules.Add(element, ModulePosition.Tail);
            this.Count = this.modules.Count;
        }

        // Adds an array of elements to the top of the stack.
        private void IncreaseStackMultiple(Type[] elements)
        {
            foreach (Type element in elements)
            {
                if (element == null)
                {
                    throw new Error("The elements of the stack can not be null.");
                }

                this.modules.Add(element, ModulePosition.Tail);
            }

            this.Count = this.modules.Count;
        }

        // Removes the top element from the stack.
        private Type RemoveTopElement()
        { 
            Type element =  this.modules.Remove(ModulePosition.Tail) ?? 
                  throw new Error("The element is null.");

            this.Count = this.modules.Count; // Updating the count. The count all the times should
                                            // be updated manual.
            return element;
        }

        // Removes the specified amount of elements from the stack.
        // Starts with the top element and going backwards.
        // Tha current last element in the stack will be first
        // in the returned array.
        private Type[] RemoveMultipleElements(int elementsCount)
        {
            if (elementsCount <= 0)
            {
                throw new Error("The count of the elements to remove can not be zero or negative.");
            }

            if (elementsCount > this.Count)
            {
                string msg = 
                    "The count of the elements to remove can not be greater than the actual" +
                    " count of the elements in the stack.";

                throw new Error(msg);
            }

            Type[] data = new Type[elementsCount];

            for (int i = 0; i < elementsCount; i++)
            {
                data[i] = RemoveTopElement();
            }

            this.Count = this.modules.Count;

            return data;
        }

        // Returns the top element of the stack without removing it.
        private Type SeeTopElement()
            => this.modules!.Tail!.Value ?? 
                throw new Error("The element is null.");

        // Returns the stack values in an collection.
        private Collection<Type> GetValuesCollection()
            => [.. GetValues()];

        // Returns the stack values in a set.
        private Set<Type> GetValuesSet()
            => [.. GetValues()];

        // Returns the stack values in a modular array.
        private ModularArray<Type> GetValuesModular()
            => new(this.modules.Values!);

        // Returns the stack values in a list.
        private List<Type> GetValuesList()
            => [.. GetValues()];

        #endregion
    }
}