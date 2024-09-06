using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public sealed class AGCModeType: EnumLikeBase<AGCModeType, byte>
{
    private AGCModeType(string name, byte value) : base(name, value)
    {
    }

    public static readonly AGCModeType AGCOff = new AGCModeType("AGCOff", 0x20);
    public static readonly AGCModeType AGCFast = new AGCModeType("AGCFast", 0x20);
    public static readonly AGCModeType AGCSlow = new AGCModeType("AGCSlow", 0x20);
}