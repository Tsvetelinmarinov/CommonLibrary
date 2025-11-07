// CommonLibrary - library for common usage.

namespace CommonLibrary.Collections
{
    using CommonLibrary.AbstractDataTypes;
    using System.Collections.Generic;
    using System.ComponentModel;
    using CommonLibrary.Enums;
    using System.Diagnostics;
    using System.Linq;
    using System;

    /// <summary>
    ///  Defines array of linked modules similar to the LinkedList.
    /// </summary>
    /// 
    /// <typeparam name="T">
    ///  The data type of the values of the modules.
    /// </typeparam>
    [Description("Array of doubly linked modules")]
    [DebuggerDisplay("{DebuggerDisplayProp, nq}")]
    public sealed class ModularArray<T>
        where T : notnull
    {
        // The count of the linked modules.
        private int _modulesCount;


        /// <summary>
        ///  Gets the head of the modular array.
        /// </summary>
        public Module<T>? Head
        {
            get;
            private set;
        }

        /// <summary>
        ///  Gets the tail of the modular array.
        /// </summary>
        public Module<T>? Tail
        {
            get;
            private set;
        }

        /// <summary>
        ///  Gets the count of the linked modules.
        /// </summary>
        public int Count
        {
            get => this._modulesCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The count of the modules cannot be negative.", nameof(value));
                }

                this._modulesCount = value;
            }
        }

        // This property is used only for the DebuggerDisplay attribute.
        private string DebuggerDisplayProp
            => $"Modular array with {this.Count} linked modules";


        /// <summary>
        ///  Creates new empty modular array.
        /// </summary>
        public ModularArray() { }


        /// <summary>
        ///  Adds a module with the specified value to the 
        ///  specified position in the modular array.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value for the new module.
        /// </param>
        /// 
        /// <param name="position">
        ///  The position in the modular array.
        /// </param>
        public void Add(T value, ModulePosition position)
            => this.AddModuleTo(value, position);

        /// <summary>
        ///  Adds a collection of modules with the specified values
        ///  at the specified position in the modular array.
        /// </summary>
        /// 
        /// <param name="collection">
        ///  The collection with the values of the modules.
        /// </param>
        /// 
        /// <param name="position">
        ///  The position in the modular array.
        /// </param>
        public void Add(IEnumerable<T> collection, ModulePosition position)
            => this.AddMultipleModulesTo(collection, position);

        /// <summary>
        ///  Removes the module at the specified position
        ///  in the modular array.
        /// </summary>
        /// 
        /// <param name="position">
        ///  The position in the modular array.
        /// </param>
        public T Remove(ModulePosition position)
            => this.DeleteModuleAt(position);

        /// <summary>
        ///  Checks the modules for the value.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value to be checked.
        /// </param>
        /// 
        /// <returns>
        ///  True if some of the modules holds the value, otherwise
        ///  False.
        /// </returns>
        public bool Contains(T value)
            => this.CheckFor(value);

        /// <summary>
        ///  Clears the modular array.
        /// </summary>
        public void Clear()
            => this.ClearArray();

        /// <summary>
        ///  Returns the values of the modular array in a
        ///  standard array.
        /// </summary>
        public T[] ToArray()
            => this.ConvertToArray();


        #region Core Functionality

        private void AddModuleTo(T value, ModulePosition pos)
        {
            Module<T> current = new(value);

            if (pos == ModulePosition.Head)
            {
                if (this.Head is null)
                {
                    this.Head = current;
                    this.Tail = current;
                    this.Count++;

                    return;
                }

                this.Head.Previous = current;
                current.Next = this.Head;
                this.Head = current;

                this.Count++;
            }
            else
            {
                if (this.Head is null)
                {
                    this.Head = current;
                    this.Tail = current;
                    this.Count++;

                    return;
                }

                this.Tail!.Next = current;
                current.Previous = this.Tail;
                this.Tail = current;

                this.Count++;
            }
        }
        private void AddMultipleModulesTo(IEnumerable<T> collection, ModulePosition pos)
        {
            ArgumentNullException.ThrowIfNull(collection);

            if (!collection.Any())
            {
                string err = "The collection should have at least one element.";
                throw new ArgumentException(err, nameof(collection));
            }
                     
            if (pos == ModulePosition.Head)
            {
                if (collection.Count() > 1)
                {
                    collection = collection.Reverse();
                }

                foreach (T value in collection)
                {
                    this.AddModuleTo(value, ModulePosition.Head);
                }
            }
            else
            {
                foreach (T value in collection)
                {
                    this.AddModuleTo(value, ModulePosition.Tail);
                }
            }
        }
        private T DeleteModuleAt(ModulePosition pos)
        {
            if (this.Count is 0)
            {
                throw new InvalidOperationException("The modular array is empty.");
            }

            if (pos is ModulePosition.Head)
            {
                T removedValue = this.Head!.Value;
                this.Head = this.Head!.Next;

                if (this.Head is null)
                {
                    this.Tail = null;
                    this.Count = 0;
                }
                else
                {
                    this.Head.Previous = null;
                    this.Count--;
                }

                return removedValue;
            }
            else
            {
                T removed = this.Tail!.Value;
                this.Tail = this.Tail.Previous;

                if (this.Tail is null)
                {
                    this.Head = null;
                    this.Count = 0;
                }
                else
                {
                    this.Tail.Next = null;
                    this.Count--;
                }

                return removed;
            }
        }  
        private bool CheckFor(T value)
        {
            if (this.Head is null)
            {
                return false;
            }

            Module<T>? current = this.Head!;

            while (current is not null)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }
        private void ClearArray()
        {
            this.Head = null;
            this.Tail = null;
            this.Count = 1;
        }
        private T[] ConvertToArray()
        {
            if (this.Head is null)
            {
                return Array.Empty<T>();
            }

            List<T> values = new();
            Module<T>? current = this.Head!;

            while (current is not null)
            {
                values.Add(current.Value);
                current = current.Next;
            }

            return [.. values];
        }

        #endregion
    }
}