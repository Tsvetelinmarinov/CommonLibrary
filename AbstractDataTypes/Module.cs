// CommonLibrary - library for common usage.

namespace CommonLibrary.AbstractDataTypes
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    ///  Defines a module for a modular array.
    /// </summary>
    /// 
    /// <typeparam name="T">
    ///  The data type of the module value.
    /// </typeparam>
    [Description("Module for a modular array")]
    [DebuggerDisplay("{Value}")]
    public sealed class Module<T>
        where T : notnull
    {
        /// <summary>
        ///  Gets the value of the module.
        /// </summary>
        public T Value
        {
            get;
            private init;
        }

        /// <summary>
        ///  Gets the previous module.
        /// </summary>
        public Module<T>? Previous 
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets the next module.
        /// </summary>
        public Module<T>? Next
        {
            get;
            set;
        }


        /// <summary>
        ///  Creates new module with the specified value.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value of the module.
        /// </param>
        public Module(T value)
        {
            this.Value = value;
            this.Previous = null;
            this.Next = null;
        }
    }
}