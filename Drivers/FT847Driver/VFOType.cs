using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public sealed class VFOType: EnumLikeBase<VFOType, byte>
{
    private VFOType(string name, byte value) : base(name, value)
    {
    }

    public static readonly VFOType Main = new VFOType("Main", 0x03);
    public static readonly VFOType SatRX = new VFOType("SatRX", 0x13);
    public static readonly VFOType SatTX = new VFOType("SatTX", 0x23);
}
