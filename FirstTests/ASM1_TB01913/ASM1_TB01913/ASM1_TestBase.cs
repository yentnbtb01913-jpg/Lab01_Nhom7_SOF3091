using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests.ASM1_TB01913;

// Chi dung cho Assignment 1; khong thay doi BaseTest cua Lab 1-3.
public abstract class ASM1_TestBase
{
    protected IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        if (driver == null) return;
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                try
                {
                    string dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "ASM1_Screenshots");
                    Directory.CreateDirectory(dir);
                    string name = TestContext.CurrentContext.Test.Name + ".png";
                    string path = Path.Combine(dir, name);
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path);
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Khong chup duoc anh: " + ex.Message);
                }
            }
        }
        finally
        {
            try { driver.Quit(); }
            finally { driver.Dispose(); }
        }
    }
}
