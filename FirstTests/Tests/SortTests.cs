
using NUnit.Framework;
using FirstTests.Pages;
using System.Collections.Generic;

namespace FirstTests.Tests
{
    public class SortTests : BaseTest
    {
        // CA 1: Sắp xếp giá từ thấp đến cao
        [Test]
        public void B1_SapXepPriceLowToHigh_GiaTangDan()
        {
            LoginPage loginPage = new LoginPage(driver);

            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            // Method chaining: sắp xếp rồi lấy danh sách giá
            List<decimal> prices = inventoryPage
                .SapXep("lohi")
                .LayDanhSachGia();

            // Kiểm tra có đủ 6 sản phẩm
            Assert.That(prices.Count, Is.EqualTo(6));

            // Kiểm tra giá tăng dần
            Assert.That(
                prices,
                Is.Ordered.Ascending,
                "Danh sách giá không tăng dần."
            );

            TestContext.Out.WriteLine(
                "Giá tăng dần: " + string.Join(", ", prices)
            );
        }

        // CA 2: Sắp xếp giá từ cao xuống thấp
        [Test]
        public void B2_SapXepPriceHighToLow_GiaGiamDan()
        {
            LoginPage loginPage = new LoginPage(driver);

            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            // Method chaining: sắp xếp rồi lấy danh sách giá
            List<decimal> prices = inventoryPage
                .SapXep("hilo")
                .LayDanhSachGia();

            // Kiểm tra có đủ 6 sản phẩm
            Assert.That(prices.Count, Is.EqualTo(6));

            // Kiểm tra giá giảm dần
            Assert.That(
                prices,
                Is.Ordered.Descending,
                "Danh sách giá không giảm dần."
            );

            TestContext.Out.WriteLine(
                "Giá giảm dần: " + string.Join(", ", prices)
            );
        }
    }
}
