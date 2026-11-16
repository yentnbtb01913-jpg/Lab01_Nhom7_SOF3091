using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FirstTests.Config;
using FirstTests.Utilities;

namespace FirstTests.Core;

// Lop quan ly vong doi chung cua moi lop test.
public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected WebDriverWait wait = null!;
    protected TestConfig config = null!;

    [SetUp]
    public void BaseSetUp()
    {
        config = ConfigReader.Load();
        driver = DriverFactory.Create(config);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(config.TimeoutSeconds));
        driver.Navigate().GoToUrl(config.BaseUrl);
        TestContext.WriteLine($"Browser={config.Browser}; Headless={config.Headless}; URL={config.BaseUrl}");
    }

    [TearDown]
    public void BaseTearDown()
    {
        if (driver is null) return;
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                try { ScreenshotHelper.Capture(driver); }
                catch (Exception ex) { TestContext.WriteLine("Khong chup duoc screenshot: " + ex.Message); }
            }
        }
        finally
        {
            try { driver.Quit(); }
            finally { driver.Dispose(); }
        }
    }
}
