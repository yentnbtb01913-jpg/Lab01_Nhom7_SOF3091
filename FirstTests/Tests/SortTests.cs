using FirstTests.Core;
using FirstTests.Pages;
using NUnit.Framework;

namespace FirstTests.Tests
{
    public class SortTests : BaseTest
    {
        [Test]
        public void B1_SapXepPriceLowToHigh_GiaTangDan()
        {
            // Nối chuỗi: đăng nhập -> sắp xếp giá thấp đến cao -> lấy danh sách giá
            List<decimal> gia = new LoginPage(driver)
                .DangNhap("standard_user", "secret_sauce")
                .SapXep("lohi")
                .LayDanhSachGia();

            Assert.That(gia.Count, Is.EqualTo(6));   // chỉ là điều kiện phụ

            // ASSERT CHÍNH: danh sách giá phải tăng dần
            Assert.That(gia, Is.Ordered.Ascending,
                "Danh sách giá không tăng dần: " + string.Join(", ", gia));
        }
    }
}
