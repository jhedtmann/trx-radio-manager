using System;
using System.IO.Ports;
using Interface;

namespace FT847Driver;

public class FT847Driver : RigDriverBase
{
    private SerialPort? _serialPort;

    public FT847Driver() : base("Yaesu FT-847")
    {
    }

    public override string Name => "Yaesu FT-847";

    public override bool Initialize()
    {
        // Any initialization logic specific to Yaesu FT-847
        return true;
    }

    public override bool Connect(string connectionString)
    {
        try
        {
            _serialPort = new SerialPort(connectionString, 9600, Parity.None, 8, StopBits.One)
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

    public override bool SetFrequency(double frequency)
    {
        try
        {
            byte[] commandBlock = BuildFrequencyCommand(frequency);
            SendCommand(commandBlock);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting frequency on Yaesu FT-847: {ex.Message}");
            return false;
        }
    }

    public override bool SetMode(string mode)
    {
        try
        {
            string command = mode switch
            {
                "LSB" => "MD1;", // MD command sets the mode
                "USB" => "MD2;",
                "CW" => "MD3;",
                "FM" => "MD4;",
                "AM" => "MD5;",
                "RTTY" => "MD6;",
                "CW-R" => "MD7;",
                "RTTY-R" => "MD9;",
                _ => throw new ArgumentException("Invalid mode")
            };

            _serialPort?.WriteLine(command);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting mode on Yaesu FT-847: {ex.Message}");
            return false;
        }
    }

    public override string GetMode()
    {
        try
        {
            _serialPort?.WriteLine("MD;"); // MD command queries the mode
            string response = _serialPort?.ReadLine();
            return response switch
            {
                "MD1;" => "LSB",
                "MD2;" => "USB",
                "MD3;" => "CW",
                "MD4;" => "FM",
                "MD5;" => "AM",
                "MD6;" => "RTTY",
                "MD7;" => "CW-R",
                "MD9;" => "RTTY-R",
                _ => "Unknown"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting mode from Yaesu FT-847: {ex.Message}");
            return "Unknown";
        }
    }

    public override bool SetPtt(bool enabled)
    {
        try
        {
            if (_serialPort == null)
                throw new InvalidOperationException("Device not initialized.");

            byte command = enabled ? (byte)0x08 : (byte)0x88; // 0x08 to enable PTT, 0x88 to disable
            byte[] commandBlock = new byte[] { 0, 0, 0, 0, command };
            SendCommand(commandBlock);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting PTT on Yaesu FT-847: {ex.Message}");
            return false;
        }
    }

    public override bool GetPtt()
    {
        try
        {
            _serialPort?.WriteLine("TX;"); // TX command queries the PTT state
            string response = _serialPort?.ReadLine();
            return response == "TX1;";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting PTT state from Yaesu FT-847: {ex.Message}");
            return false;
        }
    }

    public override double GetPower()
    {
        try
        {
            _serialPort?.WriteLine("PC;"); // PC command queries power output
            string response = _serialPort?.ReadLine();
            if (response.StartsWith("PC") && double.TryParse(response.Substring(2), out double power))
            {
                return power;
            }

            return 0.0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting power from Yaesu FT-847: {ex.Message}");
            return 0.0;
        }
    }

    private byte[] BuildFrequencyCommand(double frequency)
    {
        // Create a 5-byte command block
        byte[] commandBlock = new byte[5];
        commandBlock = ConvertFrequencyToBcd(frequency);

        // Set the opcode for frequency setting (example opcode, replace with actual value)
        commandBlock[4] = 0x01; // Example opcode for setting frequency

        return commandBlock;
    }

    private bool SendCommand(byte[] commandBlock)
    {
        try
        {
            if (commandBlock.Length != 5)
            {
                throw new ArgumentException("Invalid command block. Must be exactly five bytes!", nameof(commandBlock));
            }

            _serialPort?.Write(commandBlock, 0, 5);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    
    private double GetFrequency(VFOType vfoType = VFOType.Main)
    {
        try
        {
            byte[] commandBlock = new byte[] { 0, 0, 0, 0, (byte)vfoType };
            if (SendCommand(commandBlock) == false)
                return -1.0;
            
            string response = _serialPort?.ReadLine();
            if (response.StartsWith("FA") && double.TryParse(response.Substring(2), out double frequency))
            {
                return frequency;
            }

            return 0.0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting frequency from Yaesu FT-847: {ex.Message}");
            return 0.0;
        }
    }
}