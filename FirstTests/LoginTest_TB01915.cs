using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests;

[TestFixture]
public class LoginTest_TB01915

{
    private IWebDriver driver = null!;
    private WebDriverWait wait = null!;

    // Khoi tao trinh duyet truoc moi test
    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();

        wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10)
        );

        driver.Navigate().GoToUrl(
            "https://www.saucedemo.com/"
        );
    }

    // Ham dang nhap dung chung
    private void Login(string username, string password)
    {
        driver.FindElement(By.Id("user-name"))
            .SendKeys(username);

        driver.FindElement(By.Id("password"))
            .SendKeys(password);

        driver.FindElement(By.Id("login-button"))
            .Click();
    }

    // ==========================================
    // PHAN B - CA 1: DANG NHAP THANH CONG
    // ==========================================

    [Test]
    public void Login_Success()
    {
        Login("standard_user", "secret_sauce");

        wait.Until(d =>
            d.Url.EndsWith("/inventory.html")
        );

        Assert.That(
            driver.Url,
            Does.EndWith("/inventory.html"),
            "Dang nhap thanh cong phai chuyen den inventory.html"
        );

        TestContext.WriteLine(
            "Dang nhap thanh cong - inventory.html"
        );
    }

    // ==========================================
    // PHAN B - CA 2: TAI KHOAN BI KHOA
    // ==========================================

    [Test]
    public void Login_LockedOutUser()
    {
        Login("locked_out_user", "secret_sauce");

        wait.Until(d =>
            d.FindElements(
                By.CssSelector("[data-test='error']")
            ).Count > 0
        );

        IWebElement errorMessage = driver.FindElement(
            By.CssSelector("[data-test='error']")
        );

        string expectedMessage =
            "Epic sadface: Sorry, this user has been locked out.";

        Assert.Multiple(() =>
        {
            Assert.That(
                errorMessage.Displayed,
                Is.True,
                "Thong bao loi phai hien thi"
            );

            Assert.That(
                errorMessage.Text.Trim(),
                Is.EqualTo(expectedMessage),
                "Noi dung thong bao loi khong dung"
            );

            Assert.That(
                driver.Url,
                Does.Not.Contain("inventory.html"),
                "Tai khoan bi khoa khong duoc dang nhap"
            );
        });

        TestContext.WriteLine(
            "Dang nhap that bai dung nhu mong doi"
        );
    }

    // ==========================================
    // PHAN C - CA 3: THU THAP 6 SAN PHAM
    // ==========================================

    [Test]
    public void GetProductInformation()
    {
        Login("standard_user", "secret_sauce");

        wait.Until(d =>
            d.Url.EndsWith("/inventory.html")
        );

        // Cho danh sach san pham xuat hien
        wait.Until(d =>
            d.FindElements(
                By.CssSelector("[data-test='inventory-item']")
            ).Count > 0
        );

        // Lay danh sach san pham
        var items = driver.FindElements(
            By.CssSelector("[data-test='inventory-item']")
        );

        List<string> names = new List<string>();
        List<string> prices = new List<string>();

        TestContext.WriteLine(
            $"So san pham tim thay: {items.Count}"
        );

        int index = 1;

        foreach (IWebElement item in items)
        {
            // Lay ten ben trong tung san pham
            string name = item.FindElement(
                By.CssSelector(
                    "[data-test='inventory-item-name']"
                )
            ).Text.Trim();

            // Lay gia ben trong tung san pham
            string price = item.FindElement(
                By.CssSelector(
                    "[data-test='inventory-item-price']"
                )
            ).Text.Trim();

            names.Add(name);
            prices.Add(price);

            TestContext.WriteLine(
                $"{index}. {name} - {price}"
            );

            index++;
        }

        // Kiem tra du lieu thu thap
        Assert.Multiple(() =>
        {
            Assert.That(
                items.Count,
                Is.EqualTo(6),
                "Trang phai co dung 6 san pham"
            );

            Assert.That(
                names,
                Has.All.Not.Empty,
                "Ten san pham khong duoc rong"
            );

            Assert.That(
                prices,
                Has.All.StartsWith("$"),
                "Moi gia phai bat dau bang ky tu $"
            );

            Assert.That(
                names,
                Does.Contain("Sauce Labs Backpack"),
                "Khong tim thay Sauce Labs Backpack"
            );
        });

        TestContext.WriteLine(
            "Da hoan thanh kiem tra thong tin san pham"
        );
    }

    // ==========================================
    // DONG TRINH DUYET SAU MOI TEST
    // ==========================================

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
}