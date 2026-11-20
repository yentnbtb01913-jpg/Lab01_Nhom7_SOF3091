using System;
using NUnit.Framework;
using FirstTests.Config;
using FirstTests.Core;

namespace FirstTests.Tests;

[TestFixture]
public class LAB05_PhanB : FirstTests.Core.BaseTest
{
    [Test]
    public void B01_CungCode_ChayTheoBrowserTrongJson()
    {
        Assert.That(driver.Title, Does.Contain("Swag Labs"));
        string actual = driver.GetType().Name;
        TestContext.WriteLine($"Cung test: Browser={config.Browser}, Driver={actual}, Headless={config.Headless}");
        if (config.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            Assert.That(actual, Does.Contain("ChromeDriver"));
        else if (config.Browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            Assert.That(actual, Does.Contain("FirefoxDriver"));
        else Assert.Fail("Browser chua duoc ho tro: " + config.Browser);
    }

    [Test]
    public void B02_TenBrowserSai_ThongBaoRoRang()
    {
        var bad = new TestConfig
        {
            BaseUrl = config.BaseUrl, Browser = "Firefix",
            TimeoutSeconds = config.TimeoutSeconds,
            WindowWidth = config.WindowWidth, WindowHeight = config.WindowHeight
        };
        var ex = Assert.Throws<NotSupportedException>(() => DriverFactory.Create(bad));
        Assert.That(ex!.Message, Does.Contain("Firefix"));
    }
}
