using System.Text.Json;
using FirstTests.Core;
using FirstTests.Data;
using FirstTests.Pages;

namespace FirstTests.Tests;

public class DataDrivenCheckoutTests : BaseTest
{
    private static IEnumerable<TestCaseData> TuJson()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "checkout_cases.json");
        var ds   = JsonSerializer.Deserialize<List<CheckoutCase>>(File.ReadAllText(path))
                   ?? throw new InvalidOperationException("checkout_cases.json rỗng hoặc sai định dạng");

        var i = 0;
        foreach (var c in ds)
        {
            i++;
            yield return new TestCaseData(c)
                .SetName($"ThanhToan_{i:00}_{c.TenCa}");
        }
    }

    [TestCaseSource(nameof(TuJson))]
    public void ThanhToan_TuFileJson(CheckoutCase caTest)
    {
        // LoginPage -> InventoryPage -> CheckoutPage: toàn bộ thao tác qua POM.
        var checkoutPage = new LoginPage(driver)
            .DangNhap("standard_user", "secret_sauce")
            .ThemBackpack()
            .MoGioHang();

        // Bắt đầu checkout và nhập thông tin từ file JSON.
        checkoutPage.BatDau()
                    .NhapThongTin(caTest.Ten, caTest.Ho, caTest.MaBuuChinh);

        // 5. Kiểm tra kết quả
        if (caTest.MongDoiThanhCong)
        {
            var thongBao = checkoutPage.HoanTatVaLayThongBao();
            // Chỉ xác nhận đúng thông báo HOÀN TẤT, không OR các text của hai trang khác nhau.
            Assert.That(thongBao, Is.EqualTo("Thank you for your order!"));
        }
        else
        {
            Assert.That(checkoutPage.LayThongBaoLoi(), Is.EqualTo(caTest.ThongBaoLoi));
        }
    }
}