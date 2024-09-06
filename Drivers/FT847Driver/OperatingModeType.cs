using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public sealed class OperatingModeType: EnumLikeBase<OperatingModeType, byte>
{
    private OperatingModeType(string name, byte value) : base(name, value)
    {
    }

    public static readonly OperatingModeType LSB = new OperatingModeType("LSB", 0x00);
    public static readonly OperatingModeType USB = new OperatingModeType("USB", 0x01);
    public static readonly OperatingModeType CW = new OperatingModeType("CW", 0x02);
    public static readonly OperatingModeType CWR = new OperatingModeType("CWR", 0x03);
    public static readonly OperatingModeType AM = new OperatingModeType("AM", 0x04);
    public static readonly OperatingModeType FM = new OperatingModeType("FM", 0x08);
    public static readonly OperatingModeType CWN = new OperatingModeType("CWN", 0x82);
    public static readonly OperatingModeType CWNR = new OperatingModeType("CWNR", 0x83);
    public static readonly OperatingModeType AMN = new OperatingModeType("AMN", 0x84);
    public static readonly OperatingModeType FMN = new OperatingModeType("FMN", 0x88);
}