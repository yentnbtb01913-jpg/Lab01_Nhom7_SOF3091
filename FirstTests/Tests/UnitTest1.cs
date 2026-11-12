using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests.Tests;

public class Tests
{
    [Test]
    public void TestGooglePageTitle()
    {
        // Khởi tạo trình duyệt Chrome
        IWebDriver driver = new ChromeDriver();

        try
        {
            // Mở trang Google
            driver.Navigate().GoToUrl("https://www.google.com");

            // Lấy tiêu đề trang
            string title = driver.Title;

            // In tiêu đề ra màn hình Test Output
            TestContext.WriteLine("Tiêu đề trang: " + title);

            // Kiểm tra tiêu đề có chứa Google
            Assert.That(title, Does.Contain("Google"));
        }
        finally
        {
            // Đóng trình duyệt sau khi kiểm thử
            driver.Quit();
        }
    }
}
