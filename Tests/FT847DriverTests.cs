using TrxRadioManager.API.Interfaces;
using TrxRadioManager.Drivers.FT847;

namespace Tests;

[TestFixture]
public class FT847DriverTests
{
    [Test]
    public void TestSetVFO_SetsTheFrequency()
    {
        IRigDriver driver = new FT847Driver();
        IFrequencyModeControl fmc = driver as IFrequencyModeControl;
        Assert.That(driver.Connect("/dev/ttyS0"), Is.True);
        fmc.SetMainVFO(VFOType.Main, 14200000);
        int status = fmc.GetMainVFO(VFOType.Main);
    }

    [Test]
    public void TestEnumLike_OperatingModes()
    {
        OperatingModeType mode = OperatingModeType.CWR;
        Assert.That(mode.Name, Is.EqualTo("CWR"));
        Assert.That(mode, Is.EqualTo(OperatingModeType.CWR));
    }
}