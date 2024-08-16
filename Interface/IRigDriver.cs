namespace Interface;

public interface IRigDriver
{
    string Name { get; }
    bool Initialize();
    bool Connect(string connectionString);
    bool Disconnect();
    bool SetFrequency(double frequency);
    double GetFrequency();
    bool SetMode(string mode);
    string GetMode();
    bool SetPtt(bool enabled);
    bool GetPtt();
    double GetPower();

    // Audio streaming methods
    bool StartAudioCapture();
    bool StopAudioCapture();
    void ProcessAudioData(byte[] data);
    event Action<byte[]> OnAudioReceived;
}