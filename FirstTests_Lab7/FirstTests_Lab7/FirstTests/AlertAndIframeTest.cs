
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using FirstTests.Page;

namespace FirstTests;

[TestFixture]
public class AlertAndIframeTests : BaseTest
{
    private AlertAndIframePages alertPage = null!;

    [SetUp]
    public void SetupPages()
    {
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
    [Explicit("Chỉ chạy riêng để minh chứng log ERROR")]
    [Category("LoggingDemo")]
    public void TestScreenshotOnFailure()
    {
        alertPage.MoTrangChu();

        Assert.Fail(
            "Test that bai de kiem tra chup anh"
        );
    }

}