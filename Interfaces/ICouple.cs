// CommonLibrary - library for common usage.
// ReSharper disable All

namespace CommonLibrary.Interfaces
{
    /// <summary>
    ///  Describes a couple of elements.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The data type of the first member.
    /// </typeparam>
    /// 
    /// <typeparam name="U">
    ///  The data type of the second member.
    /// </typeparam>
    public interface ICouple<T, U>
    {
        /// <summary>
        ///  Gets the first member of the couple
        /// </summary>
        T? Member1 { get; }
        
        /// <summary>
        ///  Gets the second member of the couple
        /// </summary>
        U? Member2 { get; }
    }
}