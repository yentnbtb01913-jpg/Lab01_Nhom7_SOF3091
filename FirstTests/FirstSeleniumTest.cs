using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

[TestFixture]
public class SeleniumBasicTests
{
    private const string TargetUrl = "https://example.com";
    private const string ExpectedTitle = "Example Domain";

    private IWebDriver? _browser;

    // Lấy driver ra dùng, báo lỗi rõ ràng nếu chưa được khởi tạo
    private IWebDriver Browser =>
        _browser ?? throw new InvalidOperationException("Trình duyệt chưa được khởi tạo.");

    [SetUp]
    public void OpenBrowser()
    {
        // YÊU CẦU 1: Mở trình duyệt Chrome (phóng to toàn màn hình)
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        _browser = new ChromeDriver(options);
    }

    [Test]
    public void ExamplePage_TitleIsCorrect()
    {
        // YÊU CẦU 2: Điều hướng đến một trang web
        Browser.Navigate().GoToUrl(TargetUrl);

        // YÊU CẦU 3: Đọc và in tiêu đề trang
        string actualTitle = Browser.Title;
        Console.WriteLine($"Tiêu đề trang: {actualTitle}");
        TestContext.Out.WriteLine($"Tiêu đề trang: {actualTitle}");

        // YÊU CẦU 5: Assert khẳng định kết quả mong đợi
        Assert.That(actualTitle, Is.EqualTo(ExpectedTitle),
            $"Tiêu đề trang phải là '{ExpectedTitle}'.");
    }

    [TearDown]
    public void CloseBrowser()
    {
        // YÊU CẦU 4: Đóng trình duyệt bằng Quit()
        _browser?.Quit();
        _browser?.Dispose();
        _browser = null;
    }
}