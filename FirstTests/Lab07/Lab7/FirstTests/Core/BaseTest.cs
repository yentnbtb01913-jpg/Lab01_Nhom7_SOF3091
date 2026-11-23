using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FirstTests.Config;
using FirstTests.Utilities;
using FirstTests.Reporting;

namespace FirstTests.Core;

// Giu nguyen vai tro BaseTest: SetUp tao driver, TearDown chup anh + dong driver.
public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected WebDriverWait wait = null!;
    protected TestConfig config = null!;
    private Stopwatch watch = new();

    [SetUp]
    public void BaseSetUp()
    {
        watch = Stopwatch.StartNew();
        try
        {
            FirstTests.Utilities.Log.Info("BẮT ĐẦU: " + TestContext.CurrentContext.Test.Name);
            config = ConfigReader.Load();
            driver = DriverFactory.Create(config);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(config.TimeoutSeconds));
            driver.Navigate().GoToUrl(config.BaseUrl);
            TestContext.WriteLine($"Browser={config.Browser}; Headless={config.Headless}; URL={config.BaseUrl}");
        }
        catch (Exception ex)
        {
            watch.Stop();
            FirstTests.Utilities.Log.Error("SETUP FAILED: " + TestContext.CurrentContext.Test.Name + ": " + ex.Message);
            // Neu SetUp loi, NUnit co the khong goi TearDown: van luu ket qua that.
            ReportRun.Record(watch.Elapsed.TotalSeconds, null, "Failed", "SetUp: " + ex);
            if (driver != null)
            {
                try { driver.Quit(); } catch { }
                try { driver.Dispose(); } catch { }
            }
            throw;
        }
    }

    [TearDown]
    public void BaseTearDown()
    {
        string? screenshot = null;
        try
        {
            if (driver != null)
            {
                try { screenshot = ScreenshotHelper.Capture(driver); }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Khong chup duoc anh: " + ex.Message);
                }
            }
        }
        finally
        {
            try
            {
                if (driver != null)
                {
                    try { driver.Quit(); }
                    catch (Exception ex) { TestContext.WriteLine("Quit: " + ex.Message); }
                    try { driver.Dispose(); }
                    catch (Exception ex) { TestContext.WriteLine("Dispose: " + ex.Message); }
                }
            }
            finally
            {
                watch.Stop();
                var ketQua = TestContext.CurrentContext.Result.Outcome.Status.ToString();
                try
                {
                    if (ketQua.Equals("Failed", StringComparison.OrdinalIgnoreCase))
                        FirstTests.Utilities.Log.Error("RỚT: " + TestContext.CurrentContext.Test.Name + ": " + TestContext.CurrentContext.Result.Message);
                    else
                        FirstTests.Utilities.Log.Info("KẾT QUẢ " + ketQua.ToUpperInvariant() + ": " + TestContext.CurrentContext.Test.Name);
                }
                catch (Exception ex)
                {
                    TestContext.Progress.WriteLine("LOI GHI NHAT KY: " + ex.Message);
                }
                ReportRun.Record(watch.Elapsed.TotalSeconds, screenshot);
            }
        }
    }
}
