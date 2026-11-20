using FirstTests.Core;
using FirstTests.Pages;
using NUnit.Framework;

namespace FirstTests.Tests
{
    [Category("Login")]
    public class LoginTests : BaseTest
    {
        // BaseTest đã mở sẵn trang đăng nhập (BaseUrl trong appsettings.json)
        private LoginPage loginPage => new LoginPage(driver);

        // CA 1: Đăng nhập thành công
        [Test]
        [Category("Smoke")]
        public void Login_Success()
        {
            InventoryPage inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            // Kiểm tra URL sau khi đăng nhập
            Assert.That(
                inventoryPage.LayUrlHienTai(),
                Does.Contain("inventory.html"),
                "Đăng nhập thành công phải chuyển đến trang inventory."
            );

            TestContext.WriteLine("Đăng nhập thành công!");
        }

        // CA 2: Đăng nhập thất bại
        [Test]
        public void Login_LockedOutUser()
        {
            loginPage.DangNhapThatBai("locked_out_user", "secret_sauce");

            // Kiểm tra thông báo lỗi hiện ra
            Assert.That(loginPage.ThongBaoLoiDangHienThi(), Is.True,
                "Thông báo lỗi phải hiển thị.");

            // Kiểm tra nội dung thông báo lỗi
            Assert.That(
                loginPage.LayThongBaoLoi(),
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Thông báo lỗi không đúng."
            );

            // Kiểm tra vẫn ở trang đăng nhập
            Assert.That(loginPage.LayUrlHienTai(), Does.Not.Contain("inventory.html"),
                "Người dùng bị khóa không được vào trang inventory.");

            TestContext.WriteLine("Đăng nhập thất bại đúng như mong đợi!");
        }

        // CA 3: Thu thập thông tin 6 sản phẩm
        [Test]
        public void GetProductInformation()
        {
            InventoryPage inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            // Chờ danh sách sản phẩm tải xong rồi lấy tên và giá
            List<SanPham> sanPhams = inventoryPage.LayDanhSachSanPham(6);

            // Kiểm tra có đúng 6 sản phẩm
            Assert.That(sanPhams.Count, Is.EqualTo(6));

            foreach (SanPham sanPham in sanPhams)
            {
                TestContext.WriteLine("Tên: " + sanPham.Ten);
                TestContext.WriteLine("Giá: " + sanPham.Gia);

                // Kiểm tra tên không được rỗng
                Assert.That(sanPham.Ten, Is.Not.Empty);

                // Kiểm tra giá phải bắt đầu bằng $
                Assert.That(sanPham.Gia, Does.StartWith("$"));
            }

            // Kiểm tra danh sách có sản phẩm Sauce Labs Backpack
            Assert.That(
                sanPhams.Select(sanPham => sanPham.Ten),
                Does.Contain("Sauce Labs Backpack")
            );
        }

        // CA 4: Thêm sản phẩm vào giỏ hàng
        [Test]
        public void AddProductToCart()
        {
            // Bước 1: Đăng nhập
            InventoryPage inventoryPage =
                loginPage.DangNhap("standard_user", "secret_sauce");

            // Bước 2: Thêm Sauce Labs Backpack vào giỏ
            inventoryPage.ThemBaloVaoGio();

            // Bước 3: Kiểm tra số lượng sản phẩm trong giỏ
            string soLuong = inventoryPage.LaySoLuongTrongGio();

            Assert.That(soLuong, Is.EqualTo("1"),
                "Giỏ hàng phải có 1 sản phẩm.");

            // Bước 4: Mở giỏ hàng và kiểm tra tên sản phẩm trong giỏ
            CartPage cartPage = inventoryPage.MoGioHang();
            string tenSanPham = cartPage.LayTenSanPhamDauTien();

            Assert.That(
                tenSanPham,
                Is.EqualTo("Sauce Labs Backpack"),
                "Sản phẩm trong giỏ hàng không đúng."
            );

            TestContext.WriteLine("Sản phẩm: " + tenSanPham);
            TestContext.WriteLine("Số lượng trong giỏ: " + soLuong);
            TestContext.WriteLine("Kiểm tra giỏ hàng thành công!");
        }
    }
}
