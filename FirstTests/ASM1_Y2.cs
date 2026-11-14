using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using OpenQA.Selenium.Support.UI;
namespace FirstTests
{
    public class LoginTests
    {
        private IWebDriver driver;

        // Khởi tạo trình duyệt trước mỗi test
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        // Đóng trình duyệt sau mỗi test
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        // Hàm đăng nhập dùng chung
        private void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);

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

            // Kiểm tra thông báo lỗi hiện ra
            Assert.That(errorMessage.Displayed, Is.True,
                "Thông báo lỗi phải hiển thị.");

            // Kiểm tra nội dung thông báo lỗi
            Assert.That(
                errorMessage.Text,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Thông báo lỗi không đúng."
            );

            // Kiểm tra vẫn ở trang đăng nhập
            Assert.That(driver.Url, Does.Not.Contain("inventory.html"),
                "Người dùng bị khóa không được vào trang inventory.");

            TestContext.WriteLine("Đăng nhập thất bại đúng như mong đợi!");
        }

        // CA 3: Thu thập thông tin 6 sản phẩm
        [Test]
        public void GetProductInformation()
        {
            // Đăng nhập trước
            Login("standard_user", "secret_sauce");

            // Chờ danh sách sản phẩm tải xong
            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );

            var products = wait.Until(d =>
            {
                var items = d.FindElements(By.ClassName("inventory_item"));

                return items.Count == 6 ? items : null;
            });
            // Kiểm tra có đúng 6 sản phẩm
            Assert.That(products.Count, Is.EqualTo(6));

            var productNames = new System.Collections.Generic.List<string>();

            foreach (var item in products)
            {
                // Lấy tên sản phẩm bên trong từng item
                string name = item.FindElement(
                    By.CssSelector("[data-test='inventory-item-name']")
                ).Text;

                // Lấy giá sản phẩm bên trong từng item
                string price = item.FindElement(
                    By.CssSelector("[data-test='inventory-item-price']")
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

        // CA 4: Thêm sản phẩm vào giỏ hàng
        [Test]
        public void AddProductToCart()
        {
            // Bước 1: Đăng nhập
            Login("standard_user", "secret_sauce");

            // Bước 2: Khởi tạo Explicit Wait
            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );

            // Bước 3: Tìm và nhấn nút Add to cart
            IWebElement addButton = wait.Until(d =>
            {
                var button = d.FindElement(
                    By.Id("add-to-cart-sauce-labs-backpack")
                );

                return button.Displayed && button.Enabled
                    ? button
                    : null;
            });

            addButton.Click();

            // Bước 4: Kiểm tra số lượng sản phẩm trong giỏ
            string cartCount = wait.Until(d =>
            {
                var badge = d.FindElements(
                    By.CssSelector("[data-test='shopping-cart-badge']")
                );

                return badge.Count > 0 ? badge[0].Text : null;
            });

            Assert.That(cartCount, Is.EqualTo("1"),
                "Giỏ hàng phải có 1 sản phẩm.");

            // Bước 5: Mở giỏ hàng
            driver.FindElement(
                By.CssSelector("[data-test='shopping-cart-link']")
            ).Click();

            // Bước 6: Chờ trang giỏ hàng tải xong
            wait.Until(d => d.Url.Contains("cart.html"));

            // Bước 7: Kiểm tra tên sản phẩm trong giỏ
            string productName = wait.Until(d =>
            {
                var item = d.FindElements(
                    By.CssSelector("[data-test='inventory-item-name']")
                );

                return item.Count > 0 ? item[0].Text : null;
            });

            Assert.That(
                productName,
                Is.EqualTo("Sauce Labs Backpack"),
                "Sản phẩm trong giỏ hàng không đúng."
            );

            TestContext.WriteLine("Sản phẩm: " + productName);
            TestContext.WriteLine("Số lượng trong giỏ: " + cartCount);
            TestContext.WriteLine("Kiểm tra giỏ hàng thành công!");
        }

    }
}
