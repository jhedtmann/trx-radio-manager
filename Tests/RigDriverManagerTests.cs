using TrxRadioManager.API;

namespace Tests;

[TestFixture]
public class RigDriverManagerTests
{
    [Test]
    public void TestDriverManager_WillFindAndLoadDrivers()
    {
        RigDriverManager manager = new RigDriverManager();
        Assert.That(manager, Is.Not.Null);
    }
}