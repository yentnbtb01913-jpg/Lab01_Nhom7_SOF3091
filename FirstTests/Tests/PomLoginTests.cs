
using NUnit.Framework;
using FirstTests.Pages;
using System.Collections.Generic;

namespace FirstTests.Tests
{
    public class PomLoginTests : BaseTest
    {
        private LoginPage loginPage;

        // Khởi tạo Page Object sau khi BaseTest tạo driver
        [SetUp]
        public void KhoiTaoPage()
        {
            loginPage = new LoginPage(driver);
        }

        // CA 1: Đăng nhập thành công bằng POM
        [Test]
        public void POM_Login_Success()
        {
            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            Assert.That(
                inventoryPage.DaHienThi(),
                Is.True,
                "Đăng nhập thành công phải hiển thị trang Products."
            );

            TestContext.WriteLine("Đăng nhập bằng POM thành công!");
        }

        // CA 2: Kiểm tra số lượng sản phẩm bằng POM
        [Test]
        public void POM_CheckProductCount()
        {
            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            int soLuong = inventoryPage.SoSanPham();

            Assert.That(
                soLuong,
                Is.EqualTo(6),
                "Trang sản phẩm phải có đúng 6 sản phẩm."
            );

            TestContext.WriteLine("Số lượng sản phẩm: " + soLuong);
        }

        // CA 3: Lấy danh sách tên sản phẩm bằng POM
        [Test]
        public void POM_GetProductNames()
        {
            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            List<string> danhSachTen =
                inventoryPage.LayDanhSachTen();

            Assert.That(danhSachTen, Has.Count.EqualTo(6));

            Assert.That(
                danhSachTen,
                Does.Contain("Sauce Labs Backpack")
            );

            foreach (string ten in danhSachTen)
            {
                TestContext.WriteLine("Sản phẩm: " + ten);
            }
        }
    }
}
