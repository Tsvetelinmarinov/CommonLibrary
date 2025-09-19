// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Base.Abstract
{
    /// <summary>
    /// 
    /// EN:
    ///   Base class for a queue. Provides funcionality for a queue.
    ///   The queue is a FIFO data structore: First In - First Out.
    ///   First element that will enter the queue will come out first.
    ///   
    /// BG:
    ///   Базов клас за опашка. Предоставя необходимите функций и методи,
    ///   необходими на опашка. Опашката е FIFO структора от данни: 
    ///   First In - First Out. Първия елемент влязъл в опашката ще излезе пръв от нея.
    ///   Последния ще излезе последен.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("Collection of data of type FIFO")]
    public abstract class DataQueueBase<DataType>
    {
        /// <summary>
        /// 
        /// EN:
        ///   Get the first element in the queue.
        ///   
        /// BG:
        ///   Достъпва първия елемент на опашката.
        /// 
        /// </summary>
        public abstract DataType? FirstElement
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Get the last element in the queue.
        ///   
        /// BG:
        ///   Достъпва последния елемент на опашката.
        /// 
        /// </summary>
        public abstract DataType? LastElement
        {
            get;
        }


        /// <summary>
        /// 
        /// EN:
        ///   Adds an element to the queue.
        ///   
        /// BG:
        ///   Добавя елемент на опашката.
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        ///  EN: The element.
        ///  BG:  Елемента.
        /// </param>
        public abstract void Increase(DataType element);

        /// <summary>
        /// 
        /// EN:
        ///   Removes the first element from the queue.
        ///   
        /// BG:
        ///   Премахва първият елемент от опашката.
        /// 
        /// </summary>
        public abstract void Decrease();

        /// <summary>
        /// 
        /// EN:
        ///   Get the element at that index in the queue.
        ///   
        /// BG:
        ///   Достъпва стойността на този индекс в опашката.
        ///   Опашката не подържа дирекно индексиране.
        /// 
        /// </summary>
        /// <param name="index"></param>
        public abstract DataType? ViewAt(int index);

        /// <summary>
        /// 
        /// EN:
        ///   Clears the queue.
        ///   
        /// BG:
        ///   Изчиства опашката.
        /// 
        /// </summary>
        public abstract void Clear();
    }
}