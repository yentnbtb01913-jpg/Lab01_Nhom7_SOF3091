using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

[TestFixture]
public class FirstSeleniumTest
{
    private IWebDriver _driver = null!;

    [SetUp]
    public void Setup()
    {
        // YÊU CẦU 1: Mở trình duyệt Chrome
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void OpenPage_ShouldHaveExpectedTitle()
    {
        // YÊU CẦU 2: Điều hướng đến một trang web
        _driver.Navigate().GoToUrl("https://example.com");

        // YÊU CẦU 3: Đọc và in tiêu đề trang
        string title = _driver.Title;
        Console.WriteLine($"Tiêu đề trang: {title}");
        TestContext.Out.WriteLine($"Tiêu đề trang: {title}");

        // YÊU CẦU 5: Assert khẳng định kết quả mong đợi
        Assert.That(title, Is.EqualTo("Example Domain"));
    }

    [TearDown]
    public void TearDown()
    {
        // YÊU CẦU 4: Đóng trình duyệt bằng Quit()
        _driver.Quit();
        _driver.Dispose();
    }
}