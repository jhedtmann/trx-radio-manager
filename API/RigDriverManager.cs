using System.Reflection;
using Interface;

namespace API;

public class RigDriverManager
{
    private IRigDriver? _currentDriver;
    private readonly IDictionary<string, Type> _drivers;

    public RigDriverManager()
    {
        _drivers = new Dictionary<string, Type>();
        string path =  TryGetSolutionDirectoryInfo().ToString();
        string driverPath = Path.Combine(path, "API/build/Drivers");
        LoadDrivers(driverPath);
    }

    public void LoadDrivers(string path)
    {
        string[] driverFiles = Directory.GetFiles(path, "*.dll");

        foreach (string file in driverFiles)
        {
            Assembly assembly = Assembly.LoadFrom(file);
            IEnumerable<Type> driverTypes =
                assembly.GetTypes().Where(t => 
                    typeof(IRigDriver).IsAssignableFrom(t) && 
                    !t.IsInterface && 
                    t.BaseType != null &&
                    t.BaseType.IsAbstract);

            foreach (Type type in driverTypes)
            {
                try
                {
                    IRigDriver driverInstance = (IRigDriver)Activator.CreateInstance(type)!;
                    _drivers[driverInstance.Name] = type;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }

    public bool SelectDriver(string driverName)
    {
        if (!_drivers.TryGetValue(driverName, value: out Type? driver))
            return false;

        _currentDriver = (IRigDriver)Activator.CreateInstance(driver)!;
        return true;
    }

    public bool InitializeDriver() => _currentDriver?.Initialize() ?? false;

    public bool Connect(string connectionString) => _currentDriver?.Connect(connectionString) ?? false;

    public bool Disconnect() => _currentDriver?.Disconnect() ?? false;

    public bool SetFrequency(double frequency) => _currentDriver?.SetFrequency(frequency) ?? false;

    public double GetFrequency() => _currentDriver?.GetFrequency() ?? 0.0;

    public bool SetMode(string mode) => _currentDriver?.SetMode(mode) ?? false;

    public string GetMode() => _currentDriver?.GetMode() ?? "Unknown";

    public bool SetPtt(bool enabled) => _currentDriver?.SetPtt(enabled) ?? false;

    public bool GetPtt() => _currentDriver?.GetPtt() ?? false;

    public double GetPower() => _currentDriver?.GetPower() ?? 0.0;
    
    private static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
    {
        DirectoryInfo? directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory;
    }
}