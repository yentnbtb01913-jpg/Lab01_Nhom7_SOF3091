
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
            // Ghi log bắt đầu kiểm thử
            Log.Info(
                $"[START] Test: {TestContext.CurrentContext.Test.Name}"
            );

            // Đọc cấu hình
            cfg = ConfigReader.Load();

            // Tạo trình duyệt từ DriverFactory
            driver = DriverFactory.Create(cfg);

            // Mở trang web
            driver.Navigate().GoToUrl(cfg.BaseUrl);
            TestContext.WriteLine(
    "Duong dan log: " + AppDomain.CurrentDomain.BaseDirectory
);
        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result;

            try
            {
                // Ghi log kết quả kiểm thử
                if (result.Outcome.Status == TestStatus.Passed)
                {
                    Log.Info(
                        $"[OK] Test: {TestContext.CurrentContext.Test.Name}"
                    );
                }
                else
                {
                    Log.Error(
                        $"[FAILED] Test: {TestContext.CurrentContext.Test.Name} - {result.Message}"
                    );
                }

                // Chụp ảnh khi test thất bại
                if (result.Outcome.Status == TestStatus.Failed
                    && driver != null)
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
                    driver = null!;
                }
            }
        }
    }
}
