using System.Text;

namespace Interface;

public abstract class RigDriverBase: IRigDriver
{
    public RigDriverBase(string name)
    {
        Name = name;
    }
    
    public virtual string Name { get; }
    
    public virtual bool Initialize()
    {
        throw new NotImplementedException();
    }

    public virtual bool Connect(string connectionString)
    {
        throw new NotImplementedException();
    }

    public virtual bool Disconnect()
    {
        throw new NotImplementedException();
    }

    public virtual bool SetFrequency(double frequency)
    {
        throw new NotImplementedException();
    }

    public virtual double GetFrequency()
    {
        throw new NotImplementedException();
    }

    public virtual bool SetMode(string mode)
    {
        throw new NotImplementedException();
    }

    public virtual string GetMode()
    {
        throw new NotImplementedException();
    }

    public virtual bool SetPtt(bool enabled)
    {
        throw new NotImplementedException();
    }

    public virtual bool GetPtt()
    {
        throw new NotImplementedException();
    }

    public virtual double GetPower()
    {
        throw new NotImplementedException();
    }

    public virtual bool StartAudioCapture()
    {
        throw new NotImplementedException();
    }

    public virtual bool StopAudioCapture()
    {
        throw new NotImplementedException();
    }

    public virtual void ProcessAudioData(byte[] data)
    {
        throw new NotImplementedException();
    }

    public virtual event Action<byte[]>? OnAudioReceived;
    
    #region Protected Methods
    protected double ConvertBcdToFrequency(byte[] bcdFrequency)
    {
        // StringBuilder to accumulate the digits
        StringBuilder freqStringBuilder = new StringBuilder();

        // Iterate over each byte in the BCD representation
        foreach (byte bcdByte in bcdFrequency)
        {
            // Extract the high and low nibbles (each represents a digit)
            int highNibble = (bcdByte >> 4) & 0x0F; // Upper 4 bits
            int lowNibble = bcdByte & 0x0F;         // Lower 4 bits

            // Append the digits to the StringBuilder
            freqStringBuilder.Append(highNibble);
            freqStringBuilder.Append(lowNibble);
        }

        // Convert the accumulated string to a long (Hz)
        long frequencyInHz = long.Parse(freqStringBuilder.ToString());

        // Convert Hz to MHz by dividing by 1,000,000 and return as double
        return frequencyInHz / 1_000_000.0;
    }
    
    protected byte[] ConvertFrequencyToBcd(double frequency)
    {
        // Convert the frequency to Hz (integer representation)
        long frequencyInHz = (long)(frequency * 1_000_000);

        // Convert the frequency to a 10-character string, padded with zeros if necessary
        string freqString = frequencyInHz.ToString().PadLeft(8, '0');

        // Create a 4-byte array to hold the BCD representation
        byte[] bcd = new byte[4];

        // Convert the string representation to BCD
        for (int i = 0; i < 4; i++)
        {
            bcd[i] = (byte)((freqString[2 * i] - '0') << 4 | (freqString[2 * i + 1] - '0'));
        }

        return bcd;
    }
    #endregion
}