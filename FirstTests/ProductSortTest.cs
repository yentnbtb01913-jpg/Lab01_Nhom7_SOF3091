
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FirstTests.Page;

namespace FirstTests;

[TestFixture]
public class ProductSortTests
{
    private IWebDriver driver = null!;
    private InventoryPages inventoryPage = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        LoginPages loginPage = new LoginPages(driver);

        inventoryPage = loginPage.DangNhap(
            "standard_user",
            "secret_sauce"
        );

        inventoryPage.LayDanhSachGia();
    }

    [Test]
    public void SortPriceLowToHigh()
    {
        // Chaining: SapXep trả về this
        var prices = inventoryPage
            .SapXep("lohi")
            .LayDanhSachGia();

        // Kiểm tra đủ 6 sản phẩm
        Assert.That(prices.Count, Is.EqualTo(6));

        // Kiểm tra giá tăng dần
        Assert.That(
            prices,
            Is.Ordered,
            "Gia san pham khong duoc sap xep tang dan!"
        );
    }

    [TearDown]
    public void Cleanup()
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
