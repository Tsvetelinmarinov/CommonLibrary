// CommonLibrary - library for common usage.

namespace CommonLibrary.AbstractDataTypes
{
    using CommonLibrary.Exceptions;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Numerics;
    using System;

    /// <summary>
    ///  Defines a data type for numbers. The number is implemented with BigInteger so
    ///  can store very large values. All arithmetic operations are supported. The Number instance
    ///  wraps a BigInteger and make it reference type.
    /// </summary>
    [DebuggerDisplay("value: {Value}, hex: {Value.ToString(\"X\"), nq}, bin: {Value.ToString(\"B\"), nq}")]
    [Description("Data type for a number")]
    public sealed class Number : IDisposable, IComparable<Number>, IEquatable<Number>
    {
        // The number itself
        BigInteger value;


        // This property is used only for the DebuggerDisplay attribute.
        BigInteger Value
        {
            get => this.value;
            set => this.value = value;
        }
        

        /// <summary>
        ///  Creates a new instance of the Number class with specified value.
        ///  That constructor is private beacause the implicit conversion operators exists.
        ///  Only those operators should be used to create new instances
        ///  and only those operators uses that constructor.
        /// </summary>
        /// 
        /// <param name="value">
        ///  The value of the number.
        /// </param>
        Number(BigInteger value)
            => this.Value = value;

        /// <summary>
        ///  Releases the resources used by the Number instance.
        /// </summary>
        ~Number()
            => this.Dispose();


        /// <summary>
        ///  Sums another number to this number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to sum.
        /// </param>
        public void Sum(Number number)
            => this.SumCore(number.value);

        /// <summary>
        ///  Sums another number to this number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to sum.
        /// </param>
        public void Sum(int number)
            => this.SumCore((BigInteger)number);

        /// <summary>
        ///  Subtracts another number from this number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to subtract.
        /// </param>
        public void Subtract(Number number)
            => this.SubtractCore(number.value);

        /// <summary>
        ///  Subtracts another number from this number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to subtract.
        /// </param>
        public void Subtract(int number)
            => this.SubtractCore(number);

        /// <summary>
        ///  Multiplies this number by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to multiply by.
        /// </param>
        public void Multiply(Number number)
            => this.MultiplyCore(number.Value);

        /// <summary>
        ///  Multiplies this number by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to multiply by.
        /// </param>
        public void Multiply(int number)
            => this.MultiplyCore(number);

        /// <summary>
        ///  Divides this number by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///   The other number to divide by.
        /// </param>
        public void Divide(Number number)
            => this.DivideCore(number.Value);

        /// <summary>
        ///  Divides this number by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///   The other number to divide by.
        /// </param>
        public void Divide(int number)
            => this.DivideCore(number);

        /// <summary>
        ///  Gets the reminder of this number divided by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to get the reminder by.
        /// </param>
        public int Reminder(Number number)
            => (int)this.FindReminder(number.Value);

        /// <summary>
        ///  Gets the reminder of this number divided by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to get the reminder by.
        /// </param>
        public int Reminder(int number)
            => (int)this.FindReminder(number);

        /// <summary>
        ///  Gets the division and reminder of this number divided by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to divide by.
        /// </param>
        /// 
        /// <returns>
        ///  Tuple where the first item is the division result and the second item is the reminder.
        /// </returns>
        public (int, int) DivisionReminder(Number number)
            => ((int, int))this.GetDivisionAndReminder(number.Value);

        /// <summary>
        ///  Gets the division and reminder of this number divided by another number.
        /// </summary>
        /// 
        /// <param name="number">
        ///  The other number to divide by.
        /// </param>
        /// 
        /// <returns>
        ///  Tuple where the first item is the division result and the second item is the reminder.
        /// </returns>
        public (int, int) DivisionReminder(int number)
            => ((int, int))this.GetDivisionAndReminder(number);

        /// <summary>
        ///  Indicates to the Garbage Collector not to call the destructor(the finalizer).
        /// </summary>
        public void Dispose()
            => GC.SuppressFinalize(this);

        /// <summary>
        ///  Returns the string representation of the number.
        /// </summary>
        public override string ToString()
            => this.value.ToString();

        /// <summary>
        ///  Compares this number to another number.
        /// </summary>
        /// 
        /// <param name="other">
        ///  The other number to compare to.
        /// </param>
        /// 
        /// <returns>
        ///  1 if this number is greater than the other number,
        ///  0 if they are equal, and -1 if this number is less than the other number.
        /// </returns>
        public int CompareTo(Number? other)
        {
            if (other is null)
            {
                return 1;
            }

            if (this.value > other.value)
            {
                return 1;
            }
            else if (this.value < other.value)
            {
                return -1;
            }

            return 0; // in case of equality
        }

        /// <summary>
        ///  Checks if this number is equal to another number.
        /// </summary>
        /// 
        /// <param name="other">
        ///  The other number to compare to.
        /// </param>
        /// 
        /// <returns>
        ///  True if the numbers are equal, false otherwise.
        /// </returns>
        public bool Equals(Number? other)
        {
            if (other is null)
            {
                return false;
            }

            if (this.value == other.value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Checks if this number is equal to another number.
        /// </summary>
        /// 
        /// <param name="obj">
        ///  The other number to compare to.
        /// </param>
        /// 
        /// <returns>
        ///  True if the numbers are equal, false otherwise.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is Number number)
            {
                return this.value == number.value;
            }
            else
            {
                throw new Error("Incorrect data type for comparsion. The data type should be BigInteger.");
            }
        }

        /// <summary>
        ///  Generates the hash code for the number.
        /// </summary>
        public override int GetHashCode()
            => this.Value.GetHashCode();


        void SumCore(BigInteger number)
            => this.Value += number;
        void SubtractCore(BigInteger number)
            => this.Value -= number;
        void MultiplyCore(BigInteger number)
            => this.Value *= number;
        void DivideCore(BigInteger number)
            => this.Value /= number;
        BigInteger FindReminder(BigInteger number)
            => this.Value % number;
        (BigInteger, BigInteger) GetDivisionAndReminder(BigInteger number)
            => (this.Value / number, this.Value % number);
        

        // Implicit conversion operators
        public static implicit operator Number (byte value)
            => new(value);
        public static implicit operator Number (sbyte value)
            => new(value);
        public static implicit operator Number (short value)
            => new(value);
        public static implicit operator Number (ushort value)
            => new(value);
        public static implicit operator Number (int value)
            => new(value);
        public static implicit operator Number (uint value)
            => new(value);
        public static implicit operator Number (long value)
            => new(value);
        public static implicit operator Number (ulong value)
            => new(value);
        public static implicit operator Number (float value)
            => new((BigInteger)value);
        public static implicit operator Number (double value)
            => new((BigInteger)value);
        public static implicit operator Number (decimal value)
            => new((BigInteger)value);
        public static implicit operator Number (BigInteger value)
            => new(value);

        public static implicit operator byte (Number value)
            => (byte)value.value;
        public static implicit operator sbyte (Number value)
            => (sbyte)value.value;
        public static implicit operator int (Number value)
            => (int)value.value;
        public static implicit operator uint (Number value)
            => (uint)value.value;
        public static implicit operator short (Number value)
            => (short)value.value;
        public static implicit operator ushort (Number value)
            => (ushort)value.value;
        public static implicit operator long (Number value)
            => (long)value.value;
        public static implicit operator ulong (Number value)
            => (ulong)value.value;
        public static implicit operator float (Number value)
            => (float)value.value;
        public static implicit operator double (Number value)
            => (double)value.value;
        public static implicit operator decimal (Number value)
            => (decimal)value.value;
        public static implicit operator BigInteger (Number value)
            => value.value;


        // Equality operators
        public static bool operator == (Number left, Number right)
        {
            if (left is null)
            {
                return false;
            }
            else if (right is null)
            {
                return false;
            }

            if (left is null && right is null)
            {
                return true;
            }

            return left!.value == right.value;
        }
        public static bool operator != (Number left, Number right)
        {
            if (left is null)
            {
                return true;
            }
            else if (right is null)
            {
                return true;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.value != right.value;
        }
        public static bool operator < (Number left, Number right)
        {
            if (left is null)
            {
                return true;
            }
            else if (right is null)
            {
                return false;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.value < right.value;
        }
        public static bool operator <= (Number left, Number right)
        {
            if (left is null)
            {
                return true;
            }
            else if (right is null)
            {
                return false;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.value <= right.value;
        }
        public static bool operator > (Number left, Number right)
        {
            if (left is null)
            {
                return false;
            }
            else if (right is null)
            {
                return true;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.value > right.value;
        }
        public static bool operator >= (Number left, Number right)
        {
            if (left is null)
            {
                return false;
            }
            else if (right is null)
            {
                return true;
            }

            if (left is null && right is null)
            {
                return false;
            }

            return left!.value >= right.value;
        }


        // Arithmetic operators
        public static Number operator + (Number left, Number right)
            => new(left.value + right.value);
        public static Number operator - (Number left, Number right)
            => new(left.value - right.value);
        public static Number operator * (Number left, Number right)
            => new(left.value * right.value);
        public static Number operator / (Number left, Number right)
            => new(left.value / right.value);
        public static Number operator % (Number left, Number right)
            => new(left.value % right.value);
        public static Number operator ++ (Number value)
            => new(value.value + BigInteger.One);
        public static Number operator -- (Number value)
            => new(value.value - BigInteger.One);


        //Bitwise operators
        public static Number operator & (Number left, Number right)
            => new(left.value & right.value);
        public static Number operator | (Number left, Number right)
            => new(left.value | right.value);
        public static Number operator ^ (Number left, Number right)
            => new(left.value ^ right.value);
        public static Number operator ~ (Number value)
            => new(~value.value);
        public static Number operator << (Number value, int shift)
            => new(value.value << shift);
        public static Number operator >> (Number value, int shift)
            => new(value.value >> shift);
    }
}