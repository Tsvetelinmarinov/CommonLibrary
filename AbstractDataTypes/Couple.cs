// CommonLibrary - library for common usage.
//ReSharper disable All

namespace CommonLibrary.AbstractDataTypes;

using System.Runtime.CompilerServices;
using System.Collections.Generic;
using CommonLibrary.Interfaces;
using System.ComponentModel;
using CommonLibrary.Enums;
using System.Diagnostics;
    
/// <summary>
///  Couple of elements.
/// </summary>
/// 
/// <typeparam name="T">
///  The data type of the first member.
/// </typeparam>
/// 
/// <typeparam name="U">
///  The data type of the second member.
/// </typeparam>
[DebuggerDisplay("{Member1} - {Member2}")]
[Description("Couple of elements")]
public sealed class Couple<T, U> : ICouple<T, U>
{
    private T? _member1 = default;
    private U? _member2 = default;


    /// <inheritdoc />
    public T? Member1
    {
        get => this._member1;
        private set => this._member1 = value;
    }

    /// <inheritdoc/>
    public U? Member2
    {
        get => this._member2;
        private set => this._member2 = value;
    }


    /// <summary>
    ///  Creates empty couple.
    /// </summary>
    public Couple()
    {
        this.Member1 = default;
        this.Member2 = default;
    }

    /// <summary>
    ///  Creates a couple with the specified elements.
    /// </summary>
    /// 
    /// <param name="firstMember">
    ///  First member.
    /// </param>
    /// 
    /// <param name="secondMember">
    ///  Second member.
    /// </param>
    public Couple(T? firstMember, U? secondMember)
    {
        this.Member1 = firstMember;
        this.Member2 = secondMember;
    }
    

    /// <summary>
    ///  Replace the first member.
    /// </summary>
    /// 
    /// <param name="value">
    ///  The new value.
    /// </param>
    public void ReplaceFirst(T? value)
    {
        this.Member1 = value;
    }

    /// <summary>
    ///  Replace the second member.
    /// </summary>
    /// 
    /// <param name="value">
    ///  The new value.
    /// </param>
    public void ReplaceSecond(U? value)
    {
        this.Member2 = value;
    }

    /// <summary>
    ///  Gets the members in a object array.
    /// </summary>
    public object?[] GetMembers()
    {
        return [this.Member1, this.Member2];
    }
    
    /// <summary>
    ///  Gets the members as tuple.
    /// </summary>
    /// 
    /// <returns>
    /// Tuple with the members.
    /// </returns>
    public (T?, U?) GetMembersAsTuple()
    {
        return (this.Member1, this.Member2);
    }

    /// <summary>
    ///  Gets the members as key-value pair.
    /// </summary>
    /// 
    /// <returns>
    /// Pair with the members.
    /// </returns>
    public KeyValuePair<T?, U?> GetMembersAsPair()
    {
        return new(this.Member1, this.Member2);
    }
}