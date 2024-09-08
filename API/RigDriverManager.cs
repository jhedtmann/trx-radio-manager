using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TrxRadioManager.API.Interfaces;

namespace TrxRadioManager.API;

public class RigDriverManager
{
    private IRigDriver? _currentDriver;
    private readonly IDictionary<string, Type> _drivers;

    public RigDriverManager()
    {
        _drivers = new Dictionary<string, Type>();
        string path = TryGetSolutionDirectoryInfo().ToString();
        string driverPath = Path.Combine(path, "API/build/Drivers");
        LoadDrivers(driverPath);
    }

    public IEnumerable<string> DriverNames
    {
        get { return _drivers.Keys; }
    }

    public bool SelectDriver(string driverName)
    {
        if (!_drivers.TryGetValue(driverName, value: out Type? driver))
            return false;

        _currentDriver = (IRigDriver)Activator.CreateInstance(driver)!;
        return true;
    }

    public bool InitializeDriver() => _currentDriver?.Initialize() ?? false;

    public bool Connect(string port, int baudRate = 9600) => _currentDriver?.Connect(port, baudRate) ?? false;

    public bool Disconnect() => _currentDriver?.Disconnect() ?? false;

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

    private void LoadDrivers(string path)
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
}