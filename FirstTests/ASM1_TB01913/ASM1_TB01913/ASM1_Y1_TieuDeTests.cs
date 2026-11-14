using NUnit.Framework;

namespace FirstTests.ASM1_TB01913;

[TestFixture]
public class ASM1_Y1_TieuDeTests : ASM1_TestBase
{
    [Test]
    public void Y1_OpenSauceDemo_CheckPageTitle()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        string title = driver.Title;
        TestContext.WriteLine("Page title: " + title);
        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}
