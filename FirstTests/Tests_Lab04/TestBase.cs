using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests.Tests_Lab04;

public abstract class TestBase
{
    protected IWebDriver driver = null!;

    // Cho phep tung bai Lab 4 tuy chinh Chrome, khong anh huong Lab 2-3.
    protected virtual ChromeOptions TaoChromeOptions()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver(TaoChromeOptions());
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
                    // Ghi rieng anh Lab 4, khong ghi de minh chung cua Lab 3.
                    string dir = Path.GetFullPath(Path.Combine(
                        TestContext.CurrentContext.TestDirectory,
                        "..", "..", "..", "screenshots", "Lab04"));
                    Directory.CreateDirectory(dir);
                    string fileName = TestContext.CurrentContext.Test.Name + "_Lab04.png";
                    string path = Path.Combine(dir, fileName);
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path, "Lab 4 - test that bai");
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
