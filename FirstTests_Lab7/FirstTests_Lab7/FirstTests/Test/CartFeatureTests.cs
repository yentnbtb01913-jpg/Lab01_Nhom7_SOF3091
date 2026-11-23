using FirstTests.Page;
using NUnit.Framework;

namespace FirstTests
{
    [TestFixture]
    [Category("Cart")]
    public class CartFeatureTests : BaseTest
    {
        private InventoryPages inventoryPage = null!;

        [SetUp]
        public void MoTrangVaDangNhap()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            LoginPages loginPage = new LoginPages(driver);

            inventoryPage = loginPage.DangNhap(
                "standard_user",
                "secret_sauce"
            );

            // Chờ danh sách sản phẩm tải xong trước khi kiểm thử.
            inventoryPage.LayDanhSachTenSanPham();
        }

        [Test]
        [Category("Smoke")]
        public void ThemBackpackVaoGio_VaKiemTraThongTin()
        {
            // Act: thêm Sauce Labs Backpack vào giỏ hàng.
            inventoryPage.ThemSanPhamVaoGio();

            // Assert 1: huy hiệu giỏ hàng phải hiển thị số lượng 1.
            Assert.That(
                inventoryPage.LaySoLuongTrongGio(),
                Is.EqualTo("1"),
                "Giỏ hàng phải hiển thị 1 sản phẩm sau khi thêm Backpack."
            );

            // Act: mở trang giỏ hàng.
            CartPages cartPage = inventoryPage.MoGioHang();

            // Assert 2: chuyển đúng trang Cart.
            Assert.That(
                driver.Url,
                Does.Contain("cart.html"),
                "Sau khi nhấn biểu tượng giỏ hàng phải mở trang Cart."
            );

            // Assert 3: đúng sản phẩm đã được thêm.
            Assert.That(
                cartPage.LayTenSanPhamTrongGio(),
                Is.EqualTo("Sauce Labs Backpack"),
                "Giỏ hàng phải chứa đúng Sauce Labs Backpack."
            );
        }
    }
}
