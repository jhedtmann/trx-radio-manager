namespace TrxRadioManager.API.Interfaces;

public interface IEnumLike<TType> where TType: struct
{
    string Name { get; }
    TType Value { get; }
}