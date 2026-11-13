
using NUnit.Framework;
using OpenQA.Selenium;
using FirstTests.Pages;
using System.Collections.Generic;

namespace FirstTests.Tests
{
    public class LoginTests : BaseTest
    {
        // CA 1: Đăng nhập thành công
        [Test]
        public void Login_Success()
        {
            LoginPage loginPage = new LoginPage(driver);

            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            Assert.That(
                inventoryPage.DaHienThi(),
                Is.True,
                "Đăng nhập thành công phải chuyển đến trang sản phẩm."
            );

            TestContext.Out.WriteLine("Đăng nhập thành công!");
        }

        // CA 2: Đăng nhập thất bại
        [Test]
        public void Login_LockedOutUser()
        {
            LoginPage loginPage = new LoginPage(driver);

            loginPage.MoTrang()
                .DangNhap("locked_out_user", "secret_sauce");

            string errorMessage = loginPage.LayThongBaoLoi();

            Assert.That(
                errorMessage,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Thông báo lỗi không đúng."
            );

            Assert.That(
                driver.Url,
                Does.Not.Contain("inventory.html"),
                "Người dùng bị khóa không được vào trang sản phẩm."
            );

            TestContext.Out.WriteLine(
                "Đăng nhập thất bại đúng như mong đợi!"
            );
        }

        // CA 3: Thu thập thông tin 6 sản phẩm
        [Test]
        public void GetProductInformation()
        {
            LoginPage loginPage = new LoginPage(driver);

            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            // Lấy danh sách tên và giá sản phẩm thông qua Page Object
            List<string> productNames = inventoryPage.LayDanhSachTen();
            List<decimal> productPrices = inventoryPage.LayDanhSachGia();

            // Kiểm tra số lượng sản phẩm
            Assert.That(productNames.Count, Is.EqualTo(6));
            Assert.That(productPrices.Count, Is.EqualTo(6));

            // In thông tin sản phẩm
            for (int i = 0; i < productNames.Count; i++)
            {
                TestContext.Out.WriteLine(
                    "Tên: " + productNames[i]
                );

                TestContext.Out.WriteLine(
                    "Giá: $" + productPrices[i]
                );

                Assert.That(productNames[i], Is.Not.Empty);
                Assert.That(productPrices[i], Is.GreaterThan(0));
            }

            // Kiểm tra có sản phẩm Sauce Labs Backpack
            Assert.That(
                productNames,
                Does.Contain("Sauce Labs Backpack")
            );
        }


        // CA 4: Thêm sản phẩm vào giỏ hàng bằng POM
        [Test]
        public void AddProductToCart()
        {
            LoginPage loginPage = new LoginPage(driver);
                
            InventoryPage inventoryPage = loginPage
                .MoTrang()
                .DangNhap("standard_user", "secret_sauce");

            // Thêm sản phẩm vào giỏ hàng
            inventoryPage.ThemVaoGioHang(
                "add-to-cart-sauce-labs-backpack"
            );

            // Kiểm tra số lượng trong giỏ
            string cartCount = inventoryPage.LaySoLuongTrongGio();

            Assert.That(cartCount, Is.EqualTo("1"));

            // Mở giỏ hàng
            CartPage cartPage = inventoryPage.MoGioHang();

            // Kiểm tra trang giỏ hàng
            Assert.That(cartPage.DaHienThi(), Is.True);

            // Kiểm tra tên sản phẩm
            string productName = cartPage.LayTenSanPham();

            Assert.That(
                productName,
                Is.EqualTo("Sauce Labs Backpack")
            );

            TestContext.Out.WriteLine(
                "Sản phẩm: " + productName
            );

            TestContext.Out.WriteLine(
                "Số lượng trong giỏ: " + cartCount
            );
        }

    }
}
