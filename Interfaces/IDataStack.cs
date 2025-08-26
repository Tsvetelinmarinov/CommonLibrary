// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using CommonLibrary.Attributes;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// 
    /// EN:
    ///    Base interface for a stack. The stack is collection of data.
    ///    The first element that enters the stack, will come out last 
    ///    and the last element that enters the stack, will come out first.
    ///    That's mean the stack is LIFO (Last In - First Out) data structure.
    ///    That stack is strongly typed.
    ///    
    /// BG:
    ///   Базов интерфейс за стак. Стака е колекция от някакъв тип данни.
    ///   Първия елемент, който влезе в стака ще излезе от него последен.
    ///   Последния елемент, който взеле в стака, ще излезе пръв от него.
    ///   Това прави стака LIFO структора от данни, тоест First In - Last Out (Първи вътре - последен вън).
    ///   Стака е строго типизиран, което означава, че типът данни на елементите в него трябва
    ///   да се укаже изрично със създаването му.
    /// 
    /// </summary>
    /// 
    /// <typeparam name="DataType">
    ///  EN: The data type of the elements in the stack.
    ///  BG: Типът данни на елементите в стака.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Usage("Use the DataStack when you need to collect some data in ordered and immutable way")]
    public interface IDataStack<DataType>
    {
        /// <summary>
        /// 
        /// EN:
        ///   View the first element in the stack.
        /// 
        /// BG:
        ///   Достъпва първия елемент в стака.
        /// 
        /// </summary>
        public abstract DataType? FirstElement
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        /// 
        ///   View the last element in the stack.
        /// BG:
        ///   Достъпва последния елемент в стака.
        /// 
        /// </summary>
        public abstract DataType? LastElement
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Get the count of the elements in the stack.
        ///   
        /// BG:
        ///   Достъпва бройката на елементите в стака.
        /// 
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// 
        /// EN:
        ///   Adds an element to the end of the stack.
        ///   
        /// BG:
        ///   Добавя елемент на края на стака.
        /// 
        /// </summary>
        /// 
        /// <param name="element">
        ///  EN: The element to be added.
        ///  BG: Елемента, който да бъде добавен.
        /// </param>
        public abstract void Put(DataType? element);

        /// <summary>
        /// 
        /// EN:
        ///   Adds array of elements to the end of the stack.
        ///   
        /// BG:
        ///   Добавя масив от елементи на края на стака.
        /// 
        /// </summary>
        /// 
        /// <param name="elements">
        ///  EN: The elements array to be added.
        ///  BG: Масива от елементи, който да бъде добавен.
        /// </param>
        public abstract void Put(DataType?[] elements);

        /// <summary>
        /// 
        /// EN:
        ///   Removes the last element from the stack and returns it.
        ///   
        /// BG:
        ///   Премахва последния елемент от стака и го връща като стойност.
        /// 
        /// </summary>
        public abstract DataType PullOut();

        /// <summary>
        /// 
        /// EN:
        ///   Remove the specified count of elements from the stack
        ///   and returns a collection with them.
        ///   
        /// BG:
        ///   Премахва желаното количество елементи от стака и ги 
        ///   връща в колекция.
        /// 
        /// </summary>
        /// 
        /// <param name="count">
        ///  EN: The number of the elements to be removed.
        ///  BG: Бройката на елементите, които да бъдат премахнати.
        /// </param>
        public abstract DataType?[] PullOut(int count);

        /// <summary>
        /// 
        /// EN:
        ///   Returns the element at that position in the stack
        ///   without removing it. The first element in the stack is 
        ///   at the first position and every next position is increased
        ///   by one.
        ///   
        /// BG:
        ///   Връща елемента на тази позиция в стака без да го премахва.
        ///   Първия елемент е на първа позиция и всяка следваща позиция
        ///   се инкрементира с едно;
        ///   
        /// 
        /// </summary>
        /// 
        /// <param name="position">
        ///  EN: The position of the element in the stack.
        ///  BG: Позицията на елемента в стака.
        /// </param>
        public abstract DataType? ViewAt(int position);

        /// <summary>
        /// 
        /// EN:
        ///   Clears the stack. Removes everything.
        ///   
        /// BG:
        ///   Изчиства стака, премахва всичко от него.
        /// 
        /// </summary>
        public abstract void Clear();
    }
}