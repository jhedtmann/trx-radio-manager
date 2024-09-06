using System.Collections.Generic;
using System.Linq;

namespace TrxRadioManager.API.Interfaces;

public abstract class EnumLikeBase<T, TType> : IEnumLike<TType> where TType : struct where T: EnumLikeBase<T, TType>
{
    protected EnumLikeBase(string name, TType value)
    {
        Name = name;
        Value = value;
    }
    
    public string Name { get; }

    public TType Value { get; }

    public static IEnumerable<T> GetValues()
    {
        return typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => (T)f.GetValue(null));
    }
}