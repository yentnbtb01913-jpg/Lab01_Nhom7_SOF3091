
using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests;

[TestFixture]
public class ProductSortTests
{
    private IWebDriver driver = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        // Mo trang SauceDemo
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        // Dang nhap
        driver.FindElement(By.Id("user-name"))
            .SendKeys("standard_user");

        driver.FindElement(By.Id("password"))
            .SendKeys("secret_sauce");

        driver.FindElement(By.Id("login-button")).Click();

        // Cho danh sach san pham hien ra
        WebDriverWait wait = new WebDriverWait(
            driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
            d.FindElements(By.ClassName("inventory_item")).Count == 6);
    }

    [Test]
    public void SortPriceLowToHigh()
    {
        // Chon Price (low to high)
        IWebElement sortDropdown =
            driver.FindElement(By.ClassName("product_sort_container"));

        SelectElement select = new SelectElement(sortDropdown);

        select.SelectByValue("lohi");

        // Lay danh sach gia san pham
        var priceElements = driver.FindElements(
            By.ClassName("inventory_item_price"));

        // Chuyen gia tu chuoi sang so
        var prices = priceElements.Select(element =>
            decimal.Parse(
                element.Text.Replace("$", ""),
                CultureInfo.InvariantCulture
            )).ToList();

        // Kiem tra co du 6 gia
        Assert.That(prices.Count, Is.EqualTo(6));

        // Kiem tra gia thuc su tang dan
        var sortedPrices = prices.OrderBy(price => price).ToList();

        Assert.That(prices, Is.EqualTo(sortedPrices),
            "Gia san pham khong duoc sap xep tang dan!");
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
