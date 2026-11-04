using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests
{
    [TestFixture]
    [Category("Selenium")]
    public class FirstSeleniumTest
    {
        private const string BaseUrl = "https://example.com";
        private const string ExpectedTitle = "Example Domain";

        private IWebDriver? driver;

        // YÊU CẦU 1: Mở trình duyệt Chrome
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            // Bỏ comment dòng dưới nếu muốn chạy không hiển thị giao diện (CI/CD):
            // options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Thiết lập thời gian chờ để tránh test bị treo hoặc lỗi do tải trang chậm
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [Test]
        [Description("Mở example.com, in tiêu đề trang và kiểm tra tiêu đề đúng như mong đợi")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            Assert.That(driver, Is.Not.Null, "Trình duyệt chưa được khởi tạo.");

            // YÊU CẦU 2: Điều hướng đến trang web
            driver!.Navigate().GoToUrl(BaseUrl);

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
                    "URL hiện tại không phải là trang example.com.");
            });
        }

        // YÊU CẦU 4: Đóng trình duyệt sau khi kiểm thử
        [TearDown]
        public void TearDown()
        {
            if (driver == null) return;

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
                driver = null;
            }
        }
    }
}