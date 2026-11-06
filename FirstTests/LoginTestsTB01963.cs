using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SauceDemoTests
{
    [TestFixture]
    public class SauceDemoTest
    {
        private IWebDriver driver;
        private const string BaseUrl = "https://www.saucedemo.com/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        // Hàm đăng nhập dùng chung cho các ca test
        private void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).Clear();
            driver.FindElement(By.Id("user-name")).SendKeys(username);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys(password);

            driver.FindElement(By.Id("login-button")).Click();
        }

        // CA 1: Đăng nhập thành công
        [Test]
        public void Login_Success()
        {
            Login("standard_user", "secret_sauce");

            // Kiểm tra URL sau khi đăng nhập
            Assert.That(
                driver.Url,
                Does.Contain("inventory.html"),
                "Đăng nhập thành công phải chuyển đến trang inventory."
            );

            TestContext.WriteLine("Đăng nhập thành công!");
        }

        // CA 2: Đăng nhập thất bại
        [Test]
        public void Login_LockedOutUser()
        {
            Login("locked_out_user", "secret_sauce");

            // Lấy thông báo lỗi
            IWebElement errorMessage = driver.FindElement(
                By.CssSelector("[data-test='error']")
            );

            string actualMessage = errorMessage.Text;

            // Kiểm tra nội dung thông báo lỗi
            Assert.That(
                actualMessage,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Thông báo lỗi không đúng."
            );

            TestContext.WriteLine("Đăng nhập thất bại đúng như mong đợi!");
        }

        // CA 3: Thu thập thông tin 6 sản phẩm
        [Test]
        public void GetProductInformation()
        {
            // Đăng nhập trước
            Login("standard_user", "secret_sauce");

            // Lấy danh sách sản phẩm
            var products = driver.FindElements(
                By.ClassName("inventory_item")
            );

            // Kiểm tra có đúng 6 sản phẩm
            Assert.That(products.Count, Is.EqualTo(6));

            var productNames = new System.Collections.Generic.List<string>();

            foreach (var item in products)
            {
                // Lấy tên sản phẩm bên trong từng item
                string name = item.FindElement(
                    By.ClassName("inventory_item_name")
                ).Text;

                // Lấy giá sản phẩm bên trong từng item
                string price = item.FindElement(
                    By.ClassName("inventory_item_price")
                ).Text;

                TestContext.WriteLine("Tên: " + name);
                TestContext.WriteLine("Giá: " + price);

                productNames.Add(name);

                // Kiểm tra tên không được rỗng
                Assert.That(name, Is.Not.Empty);

                // Kiểm tra giá phải bắt đầu bằng $
                Assert.That(price, Does.StartWith("$"));
            }

            // Kiểm tra danh sách có sản phẩm Sauce Labs Backpack
            Assert.That(
                productNames,
                Does.Contain("Sauce Labs Backpack")
            );
        }
    }
}