// CommonLibrary - library for common usage.

using System;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Exceptions;
using System.Collections.Generic;
using CommonLibrary.Base.Abstract;
using CommonLibrary.AbstractDataTypes;

namespace CommonLibrary.Collections
{
    /// <summary>
    ///  Defines array of linked modules similar to the linked list. The array
    ///  contains modules and each module has a value and reference to the next module
    ///  and to the previous module. The first module is called by convention 'head' and
    ///  his previous module is equals to null. The last module by convention is called 
    ///  'tail' and his next module is equals to null.
    /// </summary>
    [Description("Array of linked modules")]
    public sealed class ModularArray<Type> : ModularArrayBase<Type>, ICloneable
    {
        // The head of the array - the fitst module.
        private Module<Type>? head;

        // The tails of the array = the last module.
        private Module<Type>? tail;

        // The count of the modules.
        private int count;


        /// <summary>
        ///  Gets the head module of the modular array.
        /// </summary>
        public Module<Type>? Head
        {
            get => this.head;
            private set => this.head = value;
        }

        /// <summary>
        ///  Gets the tail module of the modular array.
        /// </summary>
        public Module<Type>? Tail
        {
            get => this.tail;
            private set => this.tail = value;
        }

        /// <summary>
        ///  Gets the number of modules in the modular array.
        /// </summary>
        public int Count
        {
            get => this.count;
            private set
            {
                if (value < 0)
                {
                    throw new Error("The count of the modules can not be negative.");
                }

                this.count = value;
            }
        }

        /// <summary>
        ///  Gets all the modules linked together and returns them in an array.
        /// </summary>
        public Module<Type>[] Modules
            => CollectModules();

        /// <summary>
        ///  Extracts all the values from the modules in array.
        /// </summary>
        public Type?[] Values
            => CollectValues();


        /// <summary>
        ///  Creates new empty modular array.
        /// </summary>
        public ModularArray()
        {
            this.Head = default;
            this.Tail = default;
            this.Count = default;
        }

        /// <summary>
        ///  Creates new modular array by creating a modules with the values from the
        ///  extern array and link the modules together.
        /// </summary>
        /// 
        /// <param name="externArray">
        ///  The array with values.
        /// </param>
        public ModularArray(IEnumerable<Type> externArray)
            => CreateFromExtern(externArray);


        /// <summary>
        ///  Adds a new module to the modular array at the specified position.
        ///  The position for inserting should be specified with a flag form the 
        ///  "ModulePosition" enumeration.
        ///  Use flag "Head" to insert the element to the beginning.
        ///  Use flag "Tail" to insert the element to the end.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value to be added.
        /// </param>
        /// 
        /// <param name="position">
        ///  The position where the new module should be added.
        /// </param>
        public override void Add(Type value, ModulePosition position)
            => AddModuleAt(value, position);

        /// <summary>
        ///  Removes a module from the modular array at the specified position.
        /// </summary>
        /// 
        /// <param name="position">
        ///  The position where the module should be removed.
        /// </param>
        public override Type? Remove(ModulePosition position)
            => RemoveModuleAt(position);

        /// <summary>
        ///  Checks if some of the modules in the modular array contains the value.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value to be searched
        /// </param>
        /// 
        /// <returns>
        ///  True if some of the modules contains the value, otherwise false.
        /// </returns>
        public override bool ContainsValue(Type value)
            => CheckFor(value);

        /// <summary>
        ///  Clears all modules from the modular array.
        /// </summary>
        public override void Clear()
        {
            this.Head = this.Tail = default;
            this.Count = default;
        }

        /// <summary>
        ///  Executes a command on each module in the modular array.
        /// </summary>
        /// 
        /// <param name="command">
        ///   The command to execute on each module.
        /// </param>
        public override void ExecuteOnEach(Action<Type> command)
            => ExecuteOnEachValue(command);

        /// <summary>
        ///  Returns an array that contains all module values in the modular array.
        /// </summary>
        public override Type[] AsArray()
            => ExtractValues();

        /// <summary>
        ///  Returns the values of the array in a collection.
        /// </summary>
        public override Collection<Type> AsCollection()
            => [.. ExtractValues()];

        /// <summary>
        ///  Clones the modular array.
        /// </summary>
        public object Clone()
            => this;


        #region Core Functionality

        // Creates and adds an module to the array.
        private void AddModuleAt(Type value, ModulePosition pos)
        {
            if (value == null)
            {
                throw new Error("Can not create module with null value.");
            }

            if (this.Count == 0)
            {
                Module<Type> firstModule = new(value);
                this.Head = firstModule;
                this.Tail = firstModule;
                this.Count++;
            }
            else
            {
                if (pos == ModulePosition.Head)
                {
                    Module<Type> newHead = new(value);
                    this.Head!.Previous = newHead;
                    newHead.Next = this.Head;
                    this.Head = newHead;
                }
                else if (pos == ModulePosition.Tail)
                {
                    Module<Type> newTail = new(value);
                    this.Tail!.Next = newTail;
                    newTail.Previous = this.Tail;
                    this.Tail = newTail;
                }

                this.Count++;
            }
        }

        // Removes module form the array.
        private Type? RemoveModuleAt(ModulePosition pos)
        {
            if (this.Count == 0)
            {
                throw new Error("The modular array is empty.");
            }

            if (pos == ModulePosition.Head)
            {
                Type? currentHead = this.Head!.Value;
                this.Head = this.Head.Next;

                if (this.Head != null)
                {
                    this.Head.Previous = null;
                }
                else
                {
                    this.Tail = null; // If the head is null the tail should be null to
                }

                --this.Count;
                return currentHead;
            }
            else if (pos == ModulePosition.Tail)
            {
                Type? currentTail = this.Tail!.Value;
                this.Tail = this.Tail.Previous;

                if (this.Tail != null)
                {
                    this.Tail.Next = null;
                }
                else
                {
                    this.Head = null; // If the tail is null the head should be null to
                }

                --this.Count;
                return currentTail;
            }

            return default; // That return statement is here because the "not every part of the code returns a value" error.
                           // The program will never get to here..
        }

        // Checks for the value.
        private bool CheckFor(Type value)
        {
            Module<Type>? current = this.Head;

            if (current == null)
            {
                return false; // Returns false when the array is empty.
            }

            while (current != null)
            {
                if (ReferenceEquals(current.Value, value))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        // Executes the command with each module value.
        private void ExecuteOnEachValue(Action<Type> cmd)
        {
            if (cmd == null)
            {
                throw new Error("The command gelegate should not be null.");
            }

            Module<Type>? current = this.Head ?? throw new Error("The modular array is empty.");
            
            while (current != null)
            {
                cmd(current.Value!);
                current = current.Next;
            }
        }

        // Extracts all the values from the modules in an array.
        private Type[] ExtractValues()
        {
            Type[] array = new Type[this.Count];
            Module<Type>? current = this.Head ?? throw new Error("The modular array is empty.");
            int counter = default;

            while (current != null)
            {
                array[counter++] = current.Value!;
                current = current.Next;
            }

            return array;
        }

        // Collects all the modules in an array.
        private Module<Type>[] CollectModules()
        {
            Module<Type>[] outputArray = new Module<Type>[this.Count];
            Module<Type>? module = this.Head;

            if (module == null)
            {
                return []; // an empty array if there are no modules.
            }

            int i = default;
            while (module != null)
            {
                outputArray[i++] = module;
                module = module.Next;
            }

            return outputArray;
        }

        // Collects all the values from the modules.
        private Type[] CollectValues()
        {
            Collection<Type> values = new(this.Count);
            Module<Type> current = this.Head!;

            if (current == null)
            {
                return [];
            }

            while (current != null)
            {
                values.Add(current.Value!);
                current = current.Next!;
            }

            return [.. values];
        }

        // Creates modules with the values from the extern array and link
        // the modules togetger.
        private void CreateFromExtern(IEnumerable<Type> externArr)
        {
            foreach (Type value in externArr)
            {
                this.AddModuleAt(value, ModulePosition.Tail);
            }
        }

        #endregion
    }
}