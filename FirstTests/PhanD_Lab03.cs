using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces; // cho TestStatus
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests
{
    [TestFixture]
    public class PhanD_Lab03
    {
        private IWebDriver driver = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        // Chỉ chụp ảnh khi test Failed, tên ảnh = tên ca test, đính kèm bằng AddTestAttachment
        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    // Ảnh lưu ở <thư mục project>/screenshots
                    string dir = Path.GetFullPath(Path.Combine(
                        TestContext.CurrentContext.TestDirectory, "..", "..", "..", "screenshots"));
                    Directory.CreateDirectory(dir);

                    string path = Path.Combine(dir, TestContext.CurrentContext.Test.Name + ".png");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path);
                }
            }
            finally
            {
                driver?.Quit();
                driver?.Dispose();
            }
        }

        // Cố ý làm rớt để sinh ảnh minh chứng. Chuột phải test này -> Run.
        [Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
        public void D1_CoYRot()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");

            Assert.That(driver.Title, Is.EqualTo("TIEU DE SAI CO Y"));
        }
    }
}