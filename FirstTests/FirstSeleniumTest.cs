using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    [Category("Selenium")]
    public class FirstSeleniumTest
    {
        private const string BaseUrl = "https://example.com";
        private const string ExpectedTitle = "Example Domain";

        private static readonly TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(10);

        // Dùng null! vì driver luôn được khởi tạo trong Setup trước khi test chạy
        private IWebDriver driver = null!;

        // YÊU CẦU 1: Mở trình duyệt Chrome
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--remote-allow-origins=*");
            options.AddExcludedArgument("enable-logging");
            options.PageLoadStrategy = PageLoadStrategy.Normal;

            // Tự động chạy headless khi ở môi trường CI (GitHub Actions, Azure DevOps, Jenkins...)
            // hoặc khi đặt biến môi trường HEADLESS=true
            bool headless =
                string.Equals(Environment.GetEnvironmentVariable("HEADLESS"), "true", StringComparison.OrdinalIgnoreCase) ||
                !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));

            if (headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080"); // headless không maximize được
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            // Selenium Manager (Selenium >= 4.6) tự tải ChromeDriver phù hợp với phiên bản Chrome
            driver = new ChromeDriver(options);

            // Chỉ dùng PageLoad timeout; KHÔNG trộn ImplicitWait với WebDriverWait
            // (trộn hai loại chờ có thể gây thời gian chờ khó đoán).
            driver.Manage().Timeouts().PageLoad = PageLoadTimeout;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        }

        [Test]
        [Description("Mở example.com, in tiêu đề trang và kiểm tra tiêu đề đúng như mong đợi")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            Assert.That(driver, Is.Not.Null, "Trình duyệt chưa được khởi tạo.");

            // YÊU CẦU 2: Điều hướng đến trang web
            driver.Navigate().GoToUrl(BaseUrl);

            // Chờ tường minh cho đến khi trang tải xong thay vì đọc Title ngay lập tức
            var wait = new WebDriverWait(driver, WaitTimeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                wait.Until(d =>
                    ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")?.ToString() == "complete"
                    && !string.IsNullOrEmpty(d.Title));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Trang '{BaseUrl}' không tải xong sau {WaitTimeout.TotalSeconds} giây.");
            }

            // YÊU CẦU 3: Đọc và in tiêu đề trang
            string title = driver.Title;
            Console.WriteLine("Tiêu đề trang: " + title);
            TestContext.Out.WriteLine("Tiêu đề trang: " + title);

            // YÊU CẦU 5: Kiểm tra kết quả mong đợi
            Assert.Multiple(() =>
            {
                Assert.That(title, Is.EqualTo(ExpectedTitle),
                    $"Tiêu đề trang không đúng. Mong đợi: '{ExpectedTitle}', thực tế: '{title}'.");

                // Kiểm tra bổ sung: đảm bảo đã vào đúng trang
                Assert.That(driver.Url, Does.StartWith(BaseUrl),
                    $"URL hiện tại không phải là trang example.com. URL thực tế: '{driver.Url}'.");
            });
        }

        // YÊU CẦU 4: Đóng trình duyệt sau khi kiểm thử
        [TearDown]
        public void TearDown()
        {
            if (driver == null) return;

            try
            {
                // Chụp ảnh màn hình khi test thất bại để dễ tìm nguyên nhân
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    TakeScreenshot();
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("Không thể chụp ảnh màn hình: " + ex.Message);
            }
            finally
            {
                try
                {
                    driver.Quit(); // Quit đã giải phóng cả driver, Dispose chỉ để chắc chắn
                }
                catch (Exception ex)
                {
                    TestContext.Out.WriteLine("Lỗi khi đóng trình duyệt: " + ex.Message);
                }
                finally
                {
                    driver.Dispose();
                    driver = null!;
                }
            }
        }

        private void TakeScreenshot()
        {
            if (driver is not ITakesScreenshot screenshotDriver) return;

            string dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
            Directory.CreateDirectory(dir);

            string fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string path = Path.Combine(dir, fileName);

            screenshotDriver.GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path, "Ảnh chụp khi test lỗi");
            TestContext.Out.WriteLine("Đã lưu ảnh chụp: " + path);
        }
    }
}