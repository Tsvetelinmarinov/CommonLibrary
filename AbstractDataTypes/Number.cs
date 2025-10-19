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
    [DebuggerDisplay("{Value}")]
    [DebuggerTypeProxy(typeof(int))]
    [Description("Data type for a number")]
    public sealed class Number : IDisposable, IComparable<Number>, IEquatable<Number>
    {
        // The number itself
        BigInteger value;


        /// <summary>
        ///  Gets or sets the value of the number.
        ///  That property is private because is used only to specify 
        ///  what to be shown in the the debugger display.
        /// </summary>
        private BigInteger Value
        {
            get;
            set;
        }


        /// <summary>
        ///  Creates a new instance of the Number class with specified value.
        ///  That constructor is private beacause the implicit conversion operators exists.
        ///  Only those operators or the default constructor should be used to create new instances
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
        ///  Indicates to the Garbage Collector not to call the destructor(the finalizer).
        /// </summary>
        public void Dispose()
            => GC.SuppressFinalize(this);

        /// <summary>
        ///  Returns the string representation of the number.
        /// </summary>
        public override string ToString()
            => this.Value.ToString();

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

            if (this.Value > other.Value)
            {
                return 1;
            }
            else if (this.Value < other.Value)
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

            if (this.Value == other.Value)
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
                return this.Value == number.Value;
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


        // Implicit conversion operators
        public static implicit operator Number(byte value)
            => new(value);
        public static implicit operator Number(sbyte value)
            => new(value);
        public static implicit operator Number(short value)
            => new(value);
        public static implicit operator Number(ushort value)
            => new(value);
        public static implicit operator Number(int value)
            => new(value);
        public static implicit operator Number(uint value)
            => new(value);
        public static implicit operator Number(long value)
            => new(value);
        public static implicit operator Number(ulong value)
            => new(value);
        public static implicit operator Number(BigInteger value)
            => new(value);
        public static implicit operator Number(float value)
            => new((BigInteger)value);
        public static implicit operator Number(double value)
            => new((BigInteger)value);
        public static implicit operator Number(decimal value)
            => new((BigInteger)value);
        public static implicit operator BigInteger(Number value)
            => value.Value;
        public static implicit operator int(Number value)
            => (int)value.Value;


        // Equality operators
        public static bool operator ==(Number left, Number right)
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

            return left!.Value == right.Value;
        }
        public static bool operator !=(Number left, Number right)
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

            return left!.Value != right.Value;
        }
        public static bool operator <(Number left, Number right)
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

            return left!.Value < right.Value;
        }
        public static bool operator <=(Number left, Number right)
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

            return left!.Value <= right.Value;
        }
        public static bool operator >(Number left, Number right)
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

            return left!.Value > right.Value;
        }
        public static bool operator >=(Number left, Number right)
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

            return left!.Value >= right.Value;
        }


        // Arithmetic operators
        public static Number operator +(Number left, Number right)
            => new(left.Value + right.Value);
        public static Number operator -(Number left, Number right)
            => new(left.Value - right.Value);
        public static Number operator *(Number left, Number right)
            => new(left.Value * right.Value);
        public static Number operator /(Number left, Number right)
            => new(left.Value / right.Value);
        public static Number operator %(Number left, Number right)
            => new(left.Value % right.Value);
        public static Number operator ++(Number value)
            => new(value.Value + BigInteger.One);
        public static Number operator --(Number value)
            => new(value.Value - BigInteger.One);        
    }
}