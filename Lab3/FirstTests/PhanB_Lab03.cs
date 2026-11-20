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
    public class PhanB_Lab03 : FirstTests.Core.BaseTest
    {

        [Test]
        public void B1_GiaTangDan()
        {
            // Đăng nhập bằng standard_user (KHÔNG dùng performance_glitch_user)
            driver.Navigate().GoToUrl(FirstTests.Config.ConfigReader.Load().Site("SauceDemo"));
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