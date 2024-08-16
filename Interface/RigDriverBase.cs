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
}