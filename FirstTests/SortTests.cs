using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace FirstTests
{
    public class SortTests : BaseTest
    {
        [Test]
        public void B1_SapXepPriceLowToHigh_GiaTangDan()
        {
            LoginSauceDemo();

            var dropdown = new SelectElement(
                driver.FindElement(By.ClassName("product_sort_container")));
            dropdown.SelectByText("Price (low to high)");

            var priceElements = driver.FindElements(By.ClassName("inventory_item_price"));
            Assert.That(priceElements.Count, Is.EqualTo(6));   // chỉ là điều kiện phụ

            List<decimal> prices = priceElements
                .Select(e => decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
                .ToList();

            // ASSERT CHÍNH: so sánh danh sách vừa lấy với chính nó sau khi đã sắp xếp
            List<decimal> expected = prices.OrderBy(p => p).ToList();
            Assert.That(prices, Is.EqualTo(expected),
                "Danh sách giá không tăng dần: " + string.Join(", ", prices));

            // Có thể thêm: Assert.That(prices, Is.Ordered.Ascending);
        }
    }
}

