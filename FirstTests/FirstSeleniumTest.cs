using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    public class FirstSeleniumTest
    {
        private const string BaseUrl = "https://example.com";
        private const string ExpectedTitle = "Example Domain";

        private IWebDriver driver = null!;

        // YÊU CẦU 1: Mở trình duyệt Chrome
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [Test]
        [Description("Mở example.com, đọc tiêu đề trang và kiểm tra tiêu đề đúng như mong đợi")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            // YÊU CẦU 2: Điều hướng đến trang web
            driver.Navigate().GoToUrl(BaseUrl);

            // Chờ tiêu đề trang được tải xong (tối đa 10 giây) thay vì đọc ngay lập tức
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !string.IsNullOrEmpty(d.Title));

            // YÊU CẦU 3: Đọc và in tiêu đề trang
            string title = driver.Title;

            Console.WriteLine("Tiêu đề trang: " + title);
            TestContext.Out.WriteLine("Tiêu đề trang: " + title);

            // YÊU CẦU 5: Kiểm tra kết quả mong đợi
            Assert.That(
                title,
                Is.EqualTo(ExpectedTitle),
                $"Tiêu đề trang không đúng. Mong đợi: '{ExpectedTitle}', thực tế: '{title}'");
        }

        // YÊU CẦU 4: Đóng trình duyệt sau khi kiểm thử
        [TearDown]
        public void TearDown()
        {
            if (driver == null)
            {
                return;
            }

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