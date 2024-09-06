namespace TrxRadioManager.API.Interfaces;

public interface IFrequencyModeControl
{
    void SetMainVFO(IEnumLike<byte> vfo, int frequencyHz);
    int GetMainVFO(IEnumLike<byte> vfo);
    void SetMode(IEnumLike<byte> operatingMode);
    IEnumLike<byte> GetMode();
    void SetPTT(bool transmit);
    bool GetPTT();
}