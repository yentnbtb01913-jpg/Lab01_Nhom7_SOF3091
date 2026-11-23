
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
        [SetUp]
        public void MoTrangDangNhap()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

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

        // CART-07: Kiểm tra sản phẩm vẫn còn trong giỏ
        // sau khi quay lại trang Inventory
        [Test]
        [Category("Cart")]
        public void CartProduct_RemainsAfterReturningToInventory()
        {
            // Bước 1: Đăng nhập
            LoginPages loginPage = new LoginPages(driver);

            InventoryPages inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            // Bước 2: Thêm sản phẩm vào giỏ
            inventoryPage.ThemSanPhamVaoGio();

            Assert.That(
                inventoryPage.LaySoLuongTrongGio(),
                Is.EqualTo("1"),
                "Giỏ hàng phải có 1 sản phẩm sau khi thêm."
            );

            // Bước 3: Mở giỏ hàng và kiểm tra sản phẩm
            CartPages cartPage = inventoryPage.MoGioHang();

            Assert.That(
                cartPage.LayTenSanPhamTrongGio(),
                Is.EqualTo("Sauce Labs Backpack"),
                "Sản phẩm trong giỏ hàng không đúng."
            );

            // Bước 4: Quay lại trang Inventory
            driver.Navigate().Back();

            Assert.That(
                inventoryPage.DaHienThi(),
                Is.True,
                "Trang Inventory phải hiển thị khi quay lại."
            );

            // Bước 5: Kiểm tra số lượng sản phẩm trong giỏ
            Assert.That(
                inventoryPage.LaySoLuongTrongGio(),
                Is.EqualTo("1"),
                "Sản phẩm vẫn phải còn trong giỏ hàng."
            );

            // Bước 6: Mở lại giỏ hàng và xác nhận sản phẩm
            CartPages cartPageAgain = inventoryPage.MoGioHang();

            Assert.That(
                cartPageAgain.LayTenSanPhamTrongGio(),
                Is.EqualTo("Sauce Labs Backpack"),
                "Sản phẩm không còn trong giỏ sau khi quay lại."
            );
        }

        // D1: Test cố tình tạo lỗi để thực hành Debug với AI
        [Test]
        [Category("DebugAI")]
        public void Debug_Login_WrongExpectedUrl()
        {
            LoginPages loginPage = new LoginPages(driver);

            loginPage.DangNhap("standard_user", "secret_sauce");

            // CỐ TÌNH SAI:
            // Đăng nhập thành công phải vào inventory.html
            // nhưng ở đây lại kiểm tra cart.html
            Assert.That(
                driver.Url,
                Does.Contain("inventory.html"),
                "Đăng nhập thành công phải chuyển đến trang Inventory."
            );
        }

    }
}
