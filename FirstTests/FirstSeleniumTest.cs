
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests
{
    [TestFixture]
    public class FirstSeleniumTest
    {
        private IWebDriver driver = null!;

        // YÊU CẦU 1: Mở trình duyệt Chrome
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            // YÊU CẦU 2: Điều hướng đến trang web
            driver.Navigate().GoToUrl("https://example.com");

            // YÊU CẦU 3: Đọc và in tiêu đề trang
            string title = driver.Title;

            Console.WriteLine("Tiêu đề trang: " + title);
            TestContext.Out.WriteLine("Tiêu đề trang: " + title);

            // YÊU CẦU 5: Kiểm tra kết quả mong đợi
            Assert.That(title, Is.EqualTo("Example Domain"));
        }

        // YÊU CẦU 4: Đóng trình duyệt sau khi kiểm thử
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
