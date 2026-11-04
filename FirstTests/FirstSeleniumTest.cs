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
        // ===== Cấu hình =====
        private const string BaseUrl = "https://example.com";
        private const string ExpectedTitle = "Example Domain";

        private static readonly TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan PollingInterval = TimeSpan.FromMilliseconds(250);

        private IWebDriver driver = null!;

        // ===== YÊU CẦU 1: Mở trình duyệt Chrome =====
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(BuildChromeOptions());
            ConfigureTimeouts(driver);
        }

        [Test]
        [Description("Mở example.com, in tiêu đề trang và kiểm tra tiêu đề đúng như mong đợi")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            Assert.That(driver, Is.Not.Null, "Trình duyệt chưa được khởi tạo.");

            // YÊU CẦU 2: Điều hướng đến trang web
            driver.Navigate().GoToUrl(BaseUrl);
            WaitUntilPageLoaded();

            // YÊU CẦU 3: Đọc và in tiêu đề trang
            string title = driver.Title;
            PrintLine("Tiêu đề trang: " + title);

            // YÊU CẦU 5: Kiểm tra kết quả mong đợi
            Assert.Multiple(() =>
            {
                Assert.That(title, Is.EqualTo(ExpectedTitle),
                    $"Tiêu đề trang không đúng. Mong đợi: '{ExpectedTitle}', thực tế: '{title}'.");

                Assert.That(driver.Url, Does.StartWith(BaseUrl),
                    $"URL hiện tại không phải là trang example.com. URL thực tế: '{driver.Url}'.");
            });
        }

        // ===== YÊU CẦU 4: Đóng trình duyệt sau khi kiểm thử =====
        [TearDown]
        public void TearDown()
        {
            if (driver == null) return;

            try
            {
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
                CloseBrowser();
            }
        }

        // ===== Các hàm hỗ trợ =====

        private static ChromeOptions BuildChromeOptions()
        {
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            options.AddArguments(
                "--disable-notifications",
                "--disable-infobars",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--remote-allow-origins=*");
            options.AddExcludedArgument("enable-logging");

            if (IsHeadlessMode())
            {
                // Headless không maximize được nên đặt kích thước cố định
                options.AddArguments("--headless=new", "--window-size=1920,1080");
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            return options;
        }

        private static bool IsHeadlessMode()
        {
            bool headlessFlag = string.Equals(
                Environment.GetEnvironmentVariable("HEADLESS"), "true",
                StringComparison.OrdinalIgnoreCase);

            bool isCi = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));

            return headlessFlag || isCi;
        }

        private static void ConfigureTimeouts(IWebDriver webDriver)
        {
            var timeouts = webDriver.Manage().Timeouts();
            timeouts.PageLoad = PageLoadTimeout;
            timeouts.ImplicitWait = TimeSpan.Zero; // chỉ dùng chờ tường minh
        }

        private void WaitUntilPageLoaded()
        {
            var wait = new WebDriverWait(driver, WaitTimeout)
            {
                PollingInterval = PollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                wait.Until(d => IsDocumentComplete(d) && !string.IsNullOrEmpty(d.Title));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Trang '{BaseUrl}' không tải xong sau {WaitTimeout.TotalSeconds} giây.");
            }
        }

        private static bool IsDocumentComplete(IWebDriver d)
        {
            var state = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState");
            return state?.ToString() == "complete";
        }

        private static void PrintLine(string message)
        {
            Console.WriteLine(message);
            TestContext.Out.WriteLine(message);
        }

        private void CloseBrowser()
        {
            try
            {
                driver.Quit();
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