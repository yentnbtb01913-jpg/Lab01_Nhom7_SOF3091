
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

[TestFixture]
public class LoginTests
{
    private IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl(
            "https://www.saucedemo.com/"
        );
    }

    // Hàm đăng nhập dùng chung cho web
    private void Login(string username, string password)
    {
        driver.FindElement(
            By.CssSelector("[data-test='username']")
        ).SendKeys(username);

        driver.FindElement(
            By.CssSelector("[data-test='password']")
        ).SendKeys(password);

        driver.FindElement(
            By.CssSelector("[data-test='login-button']")
        ).Click();
    }

    // CA 1: Đăng nhập thành công
    [Test]
    public void LoginSuccess()
    {
        Login("standard_user", "secret_sauce");

        Assert.That(
            driver.Url,
            Does.EndWith("/inventory.html")
        );

        Assert.That(
            driver.FindElement(By.ClassName("title")).Text,
            Is.EqualTo("Products")
        );
    }

    // CA 2: Đăng nhập thất bại
    [Test]
    public void LoginFailed()
    {
        Login("locked_out_user", "secret_sauce");

        IWebElement error = driver.FindElement(
            By.CssSelector("[data-test='error']")
        );

        Assert.That(error.Displayed, Is.True);

        Assert.That(
            error.Text,
            Is.EqualTo(
                "Epic sadface: Sorry, this user has been locked out."
            )
        );

        Assert.That(
            driver.Url,
            Does.Not.EndWith("/inventory.html")
        );
    }

    [TearDown]
    public void TearDown()
    {
        if (driver != null)
        {
            try
            {
                driver.Quit();
            }
            finally
            {
                driver.Dispose();
            }
        }
    }
    // CA 3: Lấy danh sách và kiểm tra 6 sản phẩm
    [Test]
    public void Inventory_ShouldDisplaySixProducts()
    {
        // Đăng nhập thành công
        Login("standard_user", "secret_sauce");

        // Lấy danh sách 6 sản phẩm
        IReadOnlyCollection<IWebElement> products =
            driver.FindElements(By.CssSelector("[data-test='inventory-item']"));

        // Kiểm tra có đúng 6 sản phẩm
        Assert.That(products, Has.Count.EqualTo(6));

        var productNames = new List<string>();
        var productPrices = new List<string>();

        // Duyệt từng sản phẩm để lấy tên và giá
        foreach (IWebElement item in products)
        {
            string name = item.FindElement(
                By.CssSelector("[data-test='inventory-item-name']")).Text.Trim();

            string price = item.FindElement(
                By.CssSelector("[data-test='inventory-item-price']")).Text.Trim();

            productNames.Add(name);
            productPrices.Add(price);

            TestContext.WriteLine($"Tên sản phẩm: {name} | Giá: {price}");
        }

        // Kiểm tra tên sản phẩm không được để trống
        Assert.That(productNames, Has.All.Not.Empty);

        // Kiểm tra giá đều bắt đầu bằng ký hiệu $
        Assert.That(productPrices, Has.All.StartsWith("$"));

        // Kiểm tra danh sách có sản phẩm Sauce Labs Backpack
        Assert.That(productNames, Does.Contain("Sauce Labs Backpack"));
    }
}
