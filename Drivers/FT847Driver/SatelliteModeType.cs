using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public sealed class SatelliteModeType: EnumLikeBase<SatelliteModeType, byte>
{
    private SatelliteModeType(string name, byte value) : base(name, value)
    {
    }

    public static readonly SatelliteModeType SatModeOff = new SatelliteModeType("SatModeOff", 0x00);
    public static readonly SatelliteModeType SatModeOn = new SatelliteModeType("SatModeOn", 0x01);
    public static readonly SatelliteModeType SatModeReverseOn = new SatelliteModeType("SatModeReverseOn", 0x02);
}