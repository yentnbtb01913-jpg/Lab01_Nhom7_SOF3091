
using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FirstTests.Page;

namespace FirstTests;

[TestFixture]
public class AlertAndIframeTests
{
    private IWebDriver driver = null!;
    private AlertAndIframePages alertPage = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        alertPage = new AlertAndIframePages(driver);
    }

    // C1 - JavaScript Alert (Accept)
    [Test]
    public void TestJavaScriptAlert()
    {
        alertPage.MoTrangJavaScriptAlerts();

        string alertText = alertPage.BamNutJSAlert();

        Assert.That(alertText, Is.EqualTo("I am a JS Alert"));

        alertPage.ChapNhanAlert();

        string result = alertPage.LayKetQua();

        Assert.That(
            result,
            Is.EqualTo("You successfully clicked an alert")
        );
    }

    // C1 - JavaScript Confirm (Dismiss)
    [Test]
    public void TestJavaScriptConfirm()
    {
        alertPage.MoTrangJavaScriptAlerts();

        string alertText = alertPage.BamNutJSConfirm();

        Assert.That(alertText, Is.EqualTo("I am a JS Confirm"));

        alertPage.HuyConfirm();

        string result = alertPage.LayKetQua();

        Assert.That(
            result,
            Is.EqualTo("You clicked: Cancel")
        );
    }

    // C1 - JavaScript Prompt (SendKeys + Accept)
    [Test]
    public void TestJavaScriptPrompt()
    {
        alertPage.MoTrangJavaScriptAlerts();

        string alertText = alertPage.BamNutJSPrompt();

        Assert.That(alertText, Is.EqualTo("I am a JS prompt"));

        alertPage.NhapPromptVaChapNhan("Hello from Selenium");

        string result = alertPage.LayKetQua();

        Assert.That(
            result,
            Is.EqualTo("You entered: Hello from Selenium")
        );
    }

    // C2 - Xử lý iframe
    [Test]
    public void TestIframe()
    {
        alertPage.MoTrangIframe();

        alertPage.NhapNoiDungIframe(
            "Hello from Selenium iframe"
        );

        Assert.That(
            alertPage.FooterDaHienThi(),
            Is.True
        );
    }

    // Kiểm tra chức năng chụp ảnh khi test thất bại
    [Test]
    public void TestScreenshotOnFailure()
    {
        alertPage.MoTrangChu();

        Assert.Fail(
            "Test that bai de kiem tra chup anh"
        );
    }

    [TearDown]
    public void Cleanup()
    {
        // Chỉ chụp ảnh khi test thất bại
        if (TestContext.CurrentContext.Result.Outcome.Status
            == TestStatus.Failed)
        {
            if (driver != null)
            {
                try
                {
                    string folder = Path.Combine(
                        TestContext.CurrentContext.WorkDirectory,
                        "screenshots"
                    );

                    Directory.CreateDirectory(folder);

                    string testName =
                        TestContext.CurrentContext.Test.Name;

                    string filePath = Path.Combine(
                        folder,
                        testName + ".png"
                    );

                    Screenshot screenshot =
                        ((ITakesScreenshot)driver).GetScreenshot();

                    screenshot.SaveAsFile(filePath);

                    TestContext.AddTestAttachment(filePath);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine(
                        "Khong the chup anh: " + ex.Message
                    );
                }
            }
        }

        // Đóng trình duyệt
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
