using FirstTests.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

namespace FirstTests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver = null!;
        protected WebDriverWait wait = null!;

        // Đổi thành false nếu muốn xem trình duyệt khi chạy 1 test để debug
        private const bool Headless = true;

        [SetUp]
        public void Setup()
        {
            // Ghi log bắt đầu NGAY ĐẦU, trước khi mở Chrome
            Log.Info($"[START] Test: {TestContext.CurrentContext.Test.Name}");

            var options = new ChromeOptions();
            if (Headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }

            driver = new ChromeDriver(options);

            if (!Headless)
                driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result;
            string testName = TestContext.CurrentContext.Test.Name;

            try
            {
                // 1. Ghi log kết quả
                if (result.Outcome.Status == TestStatus.Passed)
                {
                    Log.Info($"[OK] Test: {testName}");
                }
                else if (result.Outcome.Status == TestStatus.Failed)
                {
                    Log.Error($"[RỚT] Test: {testName} - {result.Message}");
                }
                else
                {
                    Log.Info($"[{result.Outcome.Status}] Test: {testName}");
                }

                // 2. Chụp ảnh nếu rớt
                if (driver != null && result.Outcome.Status == TestStatus.Failed)
                {
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                    string folder = Path.GetFullPath(
                        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "screenshots")
                    );
                    Directory.CreateDirectory(folder);

                    // Bỏ ký tự không hợp lệ trong tên file + thêm giờ để tránh ghi đè khi chạy song song
                    string safeName = string.Concat(
                        testName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)
                    );
                    string fileName = $"{safeName}_{DateTime.Now:HHmmssfff}.png";
                    string path = Path.Combine(folder, fileName);

                    screenshot.SaveAsFile(path);
                    TestContext.AddTestAttachment(path);
                    Log.Info("Da chup anh: " + path);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Loi trong TearDown: " + ex.Message);
            }
            finally
            {
                // 3. Đóng trình duyệt
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