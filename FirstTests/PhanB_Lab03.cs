using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    public class PhanB_Lab03
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--incognito"); // tránh popup đổi mật khẩu của Chrome
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Chỉ chụp ảnh khi test Failed
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    string dir = Path.GetFullPath(Path.Combine(
                        TestContext.CurrentContext.TestDirectory, "..", "..", "..", "screenshots"));
                    Directory.CreateDirectory(dir);

                    string path = Path.Combine(dir, TestContext.CurrentContext.Test.Name + ".png");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path);
                }
            }
            finally
            {
                driver?.Quit();
                driver?.Dispose();
            }
        }

        [Test]
        public void B1_GiaTangDan()
        {
            // Đăng nhập bằng standard_user (KHÔNG dùng performance_glitch_user)
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            wait.Until(d => d.FindElement(By.ClassName("inventory_list")));

            // Chọn sắp xếp Price (low to high)
            var dropdown = new SelectElement(driver.FindElement(By.ClassName("product_sort_container")));
            dropdown.SelectByText("Price (low to high)");

            // Lấy 6 giá, bỏ ký tự $, chuyển sang số
            List<decimal> prices = driver.FindElements(By.ClassName("inventory_item_price"))
                .Select(e => decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
                .ToList();

            Assert.That(prices.Count, Is.EqualTo(6), "Phải có đủ 6 sản phẩm");

            // Assert giá THỰC SỰ tăng dần: so sánh danh sách với chính nó sau khi sắp xếp
            List<decimal> daSapXep = prices.OrderBy(p => p).ToList();
            Assert.That(prices, Is.EqualTo(daSapXep),
                "Giá không tăng dần. Thực tế: " + string.Join(", ", prices));
        }
    }
}