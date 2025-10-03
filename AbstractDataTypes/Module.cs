// CommonLibrary - library for common usage.

using System.ComponentModel;
using CommonLibrary.Attributes;
using CommonLibrary.Exceptions;

namespace CommonLibrary.AbstractDataTypes
{
    /// <summary>
    ///  Defines a module for a modular array.
    /// </summary>
    [Description("Defines a module for a modular array like the LinkedListNode class")]
    [Usage("Used by the ModularArray class as his module/modules")]
    public sealed class Module<Type>
    {
        // The value of the module.
        private Type? value;

        // The previous module is the modular array.
        private Module<Type>? previous;

        // The next module in the modular array.
        private Module<Type>? next;


        /// <summary>
        ///  Gets the value of the module.
        /// </summary>
        public Type? Value
        {
            get => this.value;
            private set
            {
                if (value == null)
                {
                    throw new Error("Can not create a module with null value.");
                }

                this.value = value;
            }
        }

        /// <summary>
        ///  Gets the previous module in the modular array.
        /// </summary>
        public Module<Type>? Previous
        {
            get => this.previous;
            set => this.previous = value;
        }

        /// <summary>
        ///  Gets the next module in the modular array.
        /// </summary>
        public Module<Type>? Next
        {
            get => this.next;
            set => this.next = value;
        }


#pragma warning disable IDE0290
        /// <summary>
        ///  Creates a new module with the specified value.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value of the module.
        /// </param>
        public Module(Type value)
        {
            this.Value = value;
            this.Previous = null;
            this.Next = null;
        }
#pragma warning restore IDE0290
    }
}