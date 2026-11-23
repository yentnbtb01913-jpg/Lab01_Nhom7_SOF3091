using System;
using System.IO;
using NUnit.Framework;
using FirstTests.Config;

namespace FirstTests.Tests;

[TestFixture]
[Category("Smoke")]
public class LAB05_PhanA : FirstTests.Core.BaseTest
{
    [Test]
    public void A01_DocCauHinhTuJson()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
        Assert.That(File.Exists(path), Is.True, "JSON phai duoc copy vao output.");
        Assert.Multiple(() =>
        {
            Assert.That(config.BaseUrl, Is.Not.Empty);
            Assert.That(config.Browser, Is.Not.Empty);
            Assert.That(config.TimeoutSeconds, Is.GreaterThan(0));
            Assert.That(config.WindowWidth, Is.GreaterThanOrEqualTo(640));
        });
        TestContext.WriteLine($"BaseUrl={config.BaseUrl}, Browser={config.Browser}, Headless={config.Headless}, Timeout={config.TimeoutSeconds}");
    }

    [Test]
    public void A02_CauHinhKhongHopLeCoThongBao()
    {
        var invalid = new TestConfig
        {
            BaseUrl = config.BaseUrl, Browser = config.Browser,
            TimeoutSeconds = 0, WindowWidth = config.WindowWidth,
            WindowHeight = config.WindowHeight
        };
        var ex = Assert.Throws<InvalidDataException>(() => ConfigReader.Validate(invalid));
        Assert.That(ex!.Message, Does.Contain("TimeoutSeconds"));
    }
}
