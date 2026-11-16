using FirstTests.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using FirstTests.Config;

namespace FirstTests.Core
{
    public abstract class BaseTests
    {
        protected IWebDriver driver = null!;
        protected TestConfig cfg = null!;

        [SetUp]
        public void Setup()
        {
            // Đọc cấu hình
            cfg = ConfigReader.Load();

            // Tạo trình duyệt từ DriverFactory
            driver = DriverFactory.Create(cfg);

            // Mở trang web
            driver.Navigate().GoToUrl(cfg.BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Chụp ảnh khi test thất bại
                if (TestContext.CurrentContext.Result.Outcome.Status
                    == TestStatus.Failed)
                {
                    ScreenshotHelper.TakeScreenshot(
                        driver,
                        TestContext.CurrentContext.Test.Name
                    );
                }
            }
            finally
            {
                // Luôn đóng trình duyệt
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }
            }
        }
    }
}
