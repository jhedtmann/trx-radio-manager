using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public sealed class AntennaPortType: EnumLikeBase<AntennaPortType, byte>
{
    private AntennaPortType(string name, byte value) : base(name, value)
    {
    }

    public static readonly AntennaPortType AntHF = new AntennaPortType("AntHF", 0x30);
    public static readonly AntennaPortType AntVHF = new AntennaPortType("AntVHF", 0x31);
    public static readonly AntennaPortType AntUHF = new AntennaPortType("AntUHF", 0x32);
}