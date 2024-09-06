using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public partial class FT847Driver: IFrequencyModeControl
{
    public void SetMainVFO(IEnumLike<byte> vfo, int frequencyHz)
    {
        byte[] command = new byte[COMMAND_LENGTH];
        command[0] = (byte)(frequencyHz / 10000000 % 10);
        command[1] = (byte)(frequencyHz / 100000 % 100);
        command[2] = (byte)(frequencyHz / 1000 % 100);
        command[3] = (byte)(frequencyHz / 10 % 100);
        byte opCode = 0x01;
        switch (vfo.Name)
        {
            case "SatRX":
                opCode = 0x11;
                break;
            case "SatTX":
                opCode = 0x21;
                break;
        }

        command[4] = opCode;
        SendCommand(command);
    }

    public int GetMainVFO(IEnumLike<byte> vfo)
    {
        byte[] command = new byte[COMMAND_LENGTH];
        command[0] = 0;
        command[1] = 0;
        command[2] = 0;
        command[3] = 0;
        byte opCode = 0x03;
        command[4] = opCode;
        byte[] response = SendCommand(command, true);
        return 0;
    }

    public void SetMode(IEnumLike<byte> operatingMode)
    {
        throw new System.NotImplementedException();
    }

    public IEnumLike<byte> GetMode()
    {
        throw new System.NotImplementedException();
    }

    public void SetPTT(bool transmit)
    {
        throw new System.NotImplementedException();
    }

    public bool GetPTT()
    {
        throw new System.NotImplementedException();
    }
}