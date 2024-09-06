namespace TrxRadioManager.API.Interfaces;

public interface IRigDriver
{
    string Name { get; }
    bool Initialize();
    bool Connect(string port, int baudRate = 9600);
    bool Disconnect();
}