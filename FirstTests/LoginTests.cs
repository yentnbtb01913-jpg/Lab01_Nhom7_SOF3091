using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

[TestFixture]
public class LoginTests
{
    private const string BaseUrl = "https://www.saucedemo.com";

    private IWebDriver driver = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(BaseUrl);
        Console.WriteLine($"[SetUp] Đã mở Chrome và truy cập {BaseUrl}");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
        Console.WriteLine("[TearDown] Đã đóng trình duyệt (Quit + Dispose)");
    }

    // Hàm dùng chung: các bước đăng nhập (Phần C sẽ tái sử dụng)
    private void Login(string username, string password)
    {
        Console.WriteLine($"Đăng nhập với: {username} / {password}");
        driver.FindElement(By.Id("user-name")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("login-button")).Click();
    }

    // ===== CA 1 — Luồng thuận: đăng nhập thành công =====
    [Test]
    public void Login_Success()
    {
        Login("standard_user", "secret_sauce");

        string currentUrl = driver.Url;
        Console.WriteLine($"URL sau khi đăng nhập: {currentUrl}");

        Assert.That(currentUrl, Does.Contain("inventory.html"),
            "Đăng nhập thành công phải chuyển đến trang inventory.html");

        Console.WriteLine("Đăng nhập THÀNH CÔNG (đúng như kịch bản thuận)");
        Console.WriteLine("KẾT QUẢ: Đã chuyển sang trang inventory.html => Test PASSED");
    }

    // ===== CA 2 — Luồng nghịch: đăng nhập thất bại (test PASSED khi hệ thống báo lỗi đúng) =====
    [Test]
    public void Login_LockedOutUser()
    {
        Login("locked_out_user", "secret_sauce");

        IWebElement errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));
        string actualMessage = errorMessage.Text;

        Console.WriteLine("Đăng nhập THẤT BẠI (đúng như kịch bản nghịch)");
        Console.WriteLine($"Thông báo lỗi hiển thị: {actualMessage}");
        Console.WriteLine($"URL hiện tại: {driver.Url}");

        Assert.Multiple(() =>
        {
            Assert.That(errorMessage.Displayed, Is.True,
                "Thông báo lỗi phải hiện ra");
            Assert.That(actualMessage,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Nội dung thông báo lỗi không đúng");
            Assert.That(driver.Url, Does.Not.Contain("inventory.html"),
                "Không được chuyển sang trang inventory");
        });

        Console.WriteLine("KẾT QUẢ: Hệ thống từ chối đăng nhập và báo lỗi đúng => Test PASSED");
    }

    // ===== PHẦN C — Thu thập dữ liệu sản phẩm =====
    [Test]
    public void GetProductInformation()
    {
        // Tái sử dụng hàm đăng nhập dùng chung
        Login("standard_user", "secret_sauce");

        // FindElements: lấy danh sách các sản phẩm
        IReadOnlyCollection<IWebElement> items =
            driver.FindElements(By.CssSelector("[data-test='inventory-item']"));
        Console.WriteLine($"Số sản phẩm tìm thấy: {items.Count}");

        var names = new List<string>();
        var prices = new List<string>();
        int index = 1;

        foreach (IWebElement item in items)
        {
            // item.FindElement: lấy thông tin BÊN TRONG từng sản phẩm
            string name = item.FindElement(By.CssSelector("[data-test='inventory-item-name']")).Text;
            string price = item.FindElement(By.CssSelector("[data-test='inventory-item-price']")).Text;

            names.Add(name);
            prices.Add(price);
            Console.WriteLine($"{index}. {name} - {price}");
            index++;
        }

        // Assert chặt hơn việc chỉ đếm số lượng
        Assert.Multiple(() =>
        {
            Assert.That(items.Count, Is.EqualTo(6),
                "Trang phải có đúng 6 sản phẩm");
            Assert.That(names, Has.All.Not.Empty,
                "Không được có tên sản phẩm rỗng");
            Assert.That(prices, Has.All.StartsWith("$"),
                "Mọi giá phải bắt đầu bằng ký tự $");
            Assert.That(names, Does.Contain("Sauce Labs Backpack"),
                "Danh sách phải chứa sản phẩm Sauce Labs Backpack");
        });

        Console.WriteLine("KẾT QUẢ: Thu thập đủ 6 sản phẩm, dữ liệu tên và giá hợp lệ");
    }
}