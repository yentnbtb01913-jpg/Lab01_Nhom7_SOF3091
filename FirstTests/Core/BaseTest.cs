using FirstTests.Config;
using FirstTests.Utilities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace FirstTests.Core;

public abstract class BaseTest
{
    // Field thường (KHÔNG static): mỗi class test có đối tượng riêng nên có driver riêng
    protected IWebDriver driver = null!;
    protected TestConfig cfg    = null!;

    // Ghi mốc bắt đầu -> đọc config -> tạo driver -> mở URL
    [SetUp]
    public void BaseSetup()
    {
        Log.Info($"===== BẮT ĐẦU: {TestContext.CurrentContext.Test.Name} =====");

        cfg    = ConfigReader.Load();
        driver = DriverFactory.Create(cfg);
        Log.Info($"Mở {cfg.Browser} (headless={cfg.Headless}), tới {cfg.BaseUrl}");

        // KHÔNG gọi Maximize(): ở chế độ headless nó vô nghĩa.
        // Kích thước đã đặt trong DriverFactory nên headed và headless giống nhau.
        driver.Navigate().GoToUrl(cfg.BaseUrl);
    }

    // Ghi kết quả (OK/RỚT) -> chụp ảnh khi rớt -> luôn đóng trình duyệt
    [TearDown]
    public void BaseTearDown()
    {
        string ten = TestContext.CurrentContext.Test.Name;

        try
        {
            var ketQua = TestContext.CurrentContext.Result;

            if (ketQua.Outcome.Status == TestStatus.Failed)
            {
                Log.Error($"RỚT: {ten} | {DongDau(ketQua.Message)}");
                ScreenshotHelper.Chup(driver);
            }
            else if (ketQua.Outcome.Status == TestStatus.Passed)
            {
                Log.Info($"XONG: OK - {ten}");
            }
            else
            {
                Log.Warn($"XONG: {ketQua.Outcome.Status} - {ten}");
            }
        }
        finally
        {
            // finally: ghi log hay chụp ảnh lỗi KHÔNG được ngăn việc đóng trình duyệt
            driver?.Quit();
            driver?.Dispose();
        }
    }

    // Chỉ lấy dòng đầu của thông báo lỗi cho nhật ký gọn
    private static string DongDau(string? thongBao)
    {
        return (thongBao ?? "").Split('\n')[0].Trim();
    }
}
