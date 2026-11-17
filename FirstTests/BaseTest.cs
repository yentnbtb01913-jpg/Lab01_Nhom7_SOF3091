
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace FirstTests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver = null!;
        protected WebDriverWait wait = null!;

        // Khoi tao trinh duyet truoc moi test
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );
        }

        // Chup anh neu test that bai va dong trinh duyet
        [TearDown]
        public void TearDown()
        {
            try
            {
                if (driver != null &&
                    TestContext.CurrentContext.Result.Outcome.Status
                    == TestStatus.Failed)
                {
                    // Chup anh man hinh
                    var screenshot =
                        ((ITakesScreenshot)driver).GetScreenshot();

                    // Tao thu muc screenshots
                    string folder = Path.GetFullPath(
                        Path.Combine(
                            AppContext.BaseDirectory,
                            "..", "..", "..", "screenshots"
                        )
                    );

                    Directory.CreateDirectory(folder);

                    // Dat ten anh theo ten test
                    string fileName =
                        TestContext.CurrentContext.Test.Name + ".png";

                    string path = Path.Combine(folder, fileName);

                    // Luu anh
                    screenshot.SaveAsFile(path);

                    // Dinh kem anh vao Test Explorer
                    TestContext.AddTestAttachment(path);

                    TestContext.Out.WriteLine(
                        "Da chup anh: " + path
                    );
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine(
                    "Loi chup anh: " + ex.Message
                );
            }
            finally
            {
                if (driver != null)
                {
                    try
                    {
                        driver.Quit();
                    }
                    finally
                    {
                        driver.Dispose();
                        driver = null!;
                    }
                }
            }
        }
    }
}
