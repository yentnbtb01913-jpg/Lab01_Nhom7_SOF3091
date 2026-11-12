
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FirstTests.Page;

namespace FirstTests
{
    [TestFixture]
    public class FirstSeleniumTest
    {
        private IWebDriver driver = null!;
        private ExamplePages examplePage = null!;

        // Mở trình duyệt Chrome
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            examplePage = new ExamplePages(driver);
        }

        [Test]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            // Điều hướng đến trang web
            examplePage.MoTrangExample();

            // Đọc tiêu đề trang
            string title = examplePage.LayTieuDeTrang();

            Console.WriteLine("Tiêu đề trang: " + title);
            TestContext.Out.WriteLine("Tiêu đề trang: " + title);

            // Kiểm tra kết quả mong đợi
            Assert.That(title, Is.EqualTo("Example Domain"));
        }

        // Đóng trình duyệt sau khi kiểm thử
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
