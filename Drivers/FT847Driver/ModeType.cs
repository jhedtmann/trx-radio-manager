namespace FT847Driver;

public enum ModeType: byte
{   
    LSB = 0x00,
    USB = 0x01,
    CW = 0x02,
    CWR = 0x03,
    AM = 0x04,
    FM = 0x08,
    CWN = 0x82,
    CWNR = 0x83,
    AMN = 0x84,
    FMN = 0x88
}