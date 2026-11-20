
using FirstTests.Page;
using FirstTests.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FirstTests
{
    [TestFixture]
    [Category("Login")]
    public class LoginTest : BaseTest
    {
        // CA 1: Đăng nhập thành công
        [Test]
        [Category("Smoke")]
        public void Login_Success()
        {
            LoginPages loginPage = new LoginPages(driver);

            InventoryPages inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            Assert.That(
                driver.Url,
                Does.Contain("inventory.html"),
                "Đăng nhập thành công phải chuyển đến trang inventory."
            );

            Assert.That(
                inventoryPage.DaHienThi(),
                Is.True,
                "Danh sách sản phẩm phải hiển thị."
            );
        }

        // CA 2: Đăng nhập thất bại
        [Test]
        public void Login_LockedOutUser()
        {
            LoginPages loginPage = new LoginPages(driver);

            loginPage.DangNhap("locked_out_user", "secret_sauce");

            string errorMessage = loginPage.LayThongBaoLoi();

            Assert.That(
                errorMessage,
                Is.EqualTo(
                    "Epic sadface: Sorry, this user has been locked out."
                ),
                "Thông báo lỗi không đúng."
            );

            Assert.That(
                driver.Url,
                Does.Not.Contain("inventory.html"),
                "Người dùng bị khóa không được vào trang inventory."
            );
        }

        // CA 3: Thu thập thông tin 6 sản phẩm
        [Test]
        public void GetProductInformation()
        {
            LoginPages loginPage = new LoginPages(driver);

            InventoryPages inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            List<string> productNames =
                inventoryPage.LayDanhSachTenSanPham();

            List<string> productPrices =
                inventoryPage.LayDanhSachGiaChuoi();

            Assert.That(productNames.Count, Is.EqualTo(6));
            Assert.That(productPrices.Count, Is.EqualTo(6));

            for (int i = 0; i < productNames.Count; i++)
            {
                string name = productNames[i];
                string price = productPrices[i];

                Assert.That(name, Is.Not.Empty);
                Assert.That(price, Does.StartWith("$"));
            }

            Assert.That(
                productNames,
                Does.Contain("Sauce Labs Backpack")
            );
        }

        // CA 4: Thêm sản phẩm vào giỏ hàng
        [Test]
        [Category("Smoke")]
        public void AddProductToCart()
        {
            LoginPages loginPage = new LoginPages(driver);

            InventoryPages inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            inventoryPage.ThemSanPhamVaoGio();

            string cartCount =
                inventoryPage.LaySoLuongTrongGio();

            Assert.That(
                cartCount,
                Is.EqualTo("1"),
                "Giỏ hàng phải có 1 sản phẩm."
            );

            CartPages cartPage = inventoryPage.MoGioHang();

            string productName =
                cartPage.LayTenSanPhamTrongGio();

            Assert.That(
                productName,
                Is.EqualTo("Sauce Labs Backpack"),
                "Sản phẩm trong giỏ hàng không đúng."
            );
        }
    }
}
