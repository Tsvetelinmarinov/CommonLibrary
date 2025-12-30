// CommonLibrary - library for common usage.

namespace CommonLibrary.Interfaces
{
    /// <summary>
    ///  Describes an array that can be indexed by index.
    /// </summary>
    public interface IArray<T>
    {
        /// <summary>
        ///  Indexer.
        /// </summary>
        /// 
        /// <param name="index">
        ///  The index of the element.
        /// </param>
        T? this[int index] { get; set; }
    }
}