
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using NUnit.Framework.Interfaces;

namespace FirstTests;

[TestFixture]
public class AlertAndIframeTests
{
    private IWebDriver driver = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    // C1 - JavaScript Alert (Accept)
    [Test]
    public void TestJavaScriptAlert()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/javascript_alerts");

        driver.FindElement(By.XPath("//button[text()='Click for JS Alert']")).Click();

        IAlert alert = driver.SwitchTo().Alert();

        // Kiem tra noi dung Alert truoc khi Accept
        Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));

        alert.Accept();

        // Kiem tra ket qua hien thi
        string result = driver.FindElement(By.Id("result")).Text;

        Assert.That(result, Is.EqualTo("You successfully clicked an alert"));
    }

    // C1 - JavaScript Confirm (Dismiss)
    [Test]
    public void TestJavaScriptConfirm()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/javascript_alerts");

        driver.FindElement(By.XPath("//button[text()='Click for JS Confirm']")).Click();

        IAlert alert = driver.SwitchTo().Alert();

        Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"));

        // Chon Cancel
        alert.Dismiss();

        string result = driver.FindElement(By.Id("result")).Text;

        Assert.That(result, Is.EqualTo("You clicked: Cancel"));
    }

    // C1 - JavaScript Prompt (SendKeys + Accept)
    [Test]
    public void TestJavaScriptPrompt()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/javascript_alerts");

        driver.FindElement(By.XPath("//button[text()='Click for JS Prompt']")).Click();

        IAlert alert = driver.SwitchTo().Alert();

        Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"));

        // Nhap noi dung vao Prompt
        alert.SendKeys("Hello from Selenium");

        alert.Accept();

        string result = driver.FindElement(By.Id("result")).Text;

        Assert.That(result, Is.EqualTo("You entered: Hello from Selenium"));
    }

    // C2 - Xu ly iframe
    [Test]
    public void TestIframe()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/iframe");

        // Chuyen vao iframe
        driver.SwitchTo().Frame("mce_0_ifr");

        // Tim noi dung trong trinh soan thao
        IWebElement editor = driver.FindElement(By.Id("tinymce"));

        // Xoa noi dung cu va nhap noi dung moi
        editor.Clear();
        editor.SendKeys("Hello from Selenium iframe");

        // Chuyen tro lai trang chinh
        driver.SwitchTo().DefaultContent();

        Assert.That(driver.FindElement(By.Id("page-footer")).Displayed,
            Is.True);
    }
    [Test]
    public void TestScreenshotOnFailure()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/");

        Assert.Fail("Test that bai de kiem tra chup anh");
    }
    [TearDown]
    public void Cleanup()
    {
        // Chi chup anh khi test that bai
        if (TestContext.CurrentContext.Result.Outcome.Status
            == TestStatus.Failed)
        {
            if (driver != null)
            {
                try
                {
                    // Tao thu muc screenshots
                    string folder = Path.Combine(
                        TestContext.CurrentContext.WorkDirectory,
                        "screenshots");

                    Directory.CreateDirectory(folder);

                    // Dat ten anh theo ten ca test
                    string testName =
                        TestContext.CurrentContext.Test.Name;

                    string filePath = Path.Combine(
                        folder,
                        testName + ".png");

                    // Chup anh man hinh
                    Screenshot screenshot =
                        ((ITakesScreenshot)driver).GetScreenshot();

                    screenshot.SaveAsFile(filePath);

                    // Dinh kem anh vao ket qua test
                    TestContext.AddTestAttachment(filePath);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine(
                        "Khong the chup anh: " + ex.Message);
                }
            }
        }

        // Dong trinh duyet
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    } 
}
