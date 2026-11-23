using System;
using NUnit.Framework;

namespace FirstTests.Tests;

[TestFixture]
public class LAB05_PhanC : FirstTests.Core.BaseTest
{
    [Test]
    public void C01_BaseTest_DaMoDuongDanCauHinh()
    {
        Assert.That(driver.Url, Does.StartWith(config.BaseUrl.TrimEnd('/')));
        TestContext.WriteLine("BaseTest tu khoi tao, mo URL, va se tu dong don dep webdriver.");
    }

    // Chi chay rieng khi can chung minh screenshot. Test nay chu dong FAIL.
    [Test, Explicit("Chi chay rieng de chup anh Failed trong Phan C")]
    public void C02_CoYFail_DeChupAnh()
    {
        Assert.Fail("LAB05-PhanC: chu dong Failed de kiem tra ScreenshotHelper.");
    }
}
