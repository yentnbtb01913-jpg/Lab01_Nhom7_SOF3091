using FirstTests.Config;
using FirstTests.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
namespace FirstTests.Core;

public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected TestConfig cfg = null!;

    // Đọc config -> tạo driver -> mở URL
    [SetUp]
    public void BaseSetup()
    {
        cfg = ConfigReader.Load();
        driver = DriverFactory.Create(cfg);
        // KHÔNG gọi Maximize(): ở chế độ headless nó vô nghĩa.
        // Kích thước đã đặt trong DriverFactory nên headed và headless giống nhau.
        driver.Navigate().GoToUrl(cfg.BaseUrl);
    }

    [TearDown]
    public void BaseTearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                ScreenshotHelper.Chup(driver);
        }
        finally
        {
            // finally: chụp ảnh lỗi KHÔNG được ngăn việc đóng trình duyệt
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
