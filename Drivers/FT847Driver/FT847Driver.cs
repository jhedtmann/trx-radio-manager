using System;
using System.IO.Ports;
using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.Drivers.FT847;

public partial class FT847Driver : RigDriverBase
{
    private const int COMMAND_LENGTH = 5;
    private SerialPort? _serialPort;

    public FT847Driver() : base("Yaesu FT-847")
    {
    }

    public override string Name => "Yaesu FT-847";

    public override bool Initialize()
    {
        return true;
    }

    public override bool Connect(string port, int baudRate = 9600)
    {
        try
        {
            _serialPort = new SerialPort(port, baudRate, Parity.None, 7, StopBits.Two)
            {
                Handshake = Handshake.None,
                DtrEnable = true,
                RtsEnable = true,
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            _serialPort.Open();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to Yaesu FT-847: {ex.Message}");
            return false;
        }
    }

    public override bool Disconnect()
    {
        try
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error disconnecting from Yaesu FT-847: {ex.Message}");
            return false;
        }
    }
    
    #region Private Methods

    private byte[] SendCommand(byte[] command, bool expectResponse = false)
    {
        if (command.Length != COMMAND_LENGTH)
            throw new ArgumentException("Command length must be exactly 5 bytes!", nameof(command));
        
        _serialPort.Write(command, 0, COMMAND_LENGTH);
        if (expectResponse == false)
            return [];
        
        byte[] response = new byte[COMMAND_LENGTH];
        _serialPort.Read(response, 0, COMMAND_LENGTH);
        return response;
    }
    #endregion
}