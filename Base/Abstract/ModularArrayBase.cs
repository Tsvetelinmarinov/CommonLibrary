// CommonLibrary - library for common usage.

using System;
using CommonLibrary.Enums;
using System.ComponentModel;
using CommonLibrary.Collections;

namespace CommonLibrary.Base.Abstract
{
    /// <summary>
    ///  Defines a base class for a modular array. The modular array is similar to 
    ///  the LinkedList. It works with a modules linked together. Each module has a 
    ///  value and a references to the next and the previus module.
    /// </summary>
    [Description("Defines a base class for a modular array")]
    public abstract class ModularArrayBase<Type>
    {
        /// <summary>
        ///  Adds a new module to the modular array at the specified position.
        /// </summary>
        /// <param name="value">
        ///  The value to be added.
        /// </param>
        /// 
        /// <param name="position">
        ///  The position where the new module should be added.
        /// </param>
        public abstract void Add(Type value, ModulePosition position);

        /// <summary>
        ///  Removes a module from the modular array at the specified position.
        /// </summary>
        /// 
        /// <param name="position">
        ///  The position where the module should be removed.
        /// </param>
        public abstract Type? Remove(ModulePosition position);

        /// <summary>
        ///  Clears all modules from the modular array.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        ///  Executes a command on each module in the modular array.
        /// </summary>
        /// 
        /// <param name="command">
        ///   The command to execute on each module.
        /// </param>
        public abstract void ExecuteOnEach(Action<Type> command);

        /// <summary>
        ///  Returns an array that contains all module values in the modular array.
        /// </summary>
        public abstract Type[] AsArray();

        /// <summary>
        ///  Returns the values of the array in a collection.
        /// </summary>
        public abstract Collection<Type> AsCollection();

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
        public abstract bool ContainsValue(Type value);
    }
}