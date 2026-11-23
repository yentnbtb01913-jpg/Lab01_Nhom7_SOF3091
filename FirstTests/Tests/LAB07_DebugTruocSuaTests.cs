using FirstTests.Core;
using FirstTests.Pages;

namespace FirstTests.Tests;

[TestFixture]
[Category("Lab7_Debug")]
public sealed class LAB07_DebugTruocSuaTests : BaseTest
{
    // CA CỐ Ý FAIL: chỉ chạy riêng để lấy stack trace/screenshot phục vụ Phần D.
    [Test, Explicit("Chỉ chạy riêng khi cần minh chứng Debug trước khi sửa")]
    public void D01_TruocSua_LocatorSai_CoYFailed()
    {
        new LoginPage(driver).DangNhap("standard_user", "secret_sauce");
        var tieuDe = new DebugLocatorPage(driver).LayTieuDeBangLocatorSai();
        Assert.That(tieuDe, Is.EqualTo("Products"));
    }

    // CA ĐÃ SỬA: dùng Page Object thật, không gọi By trong test.
    [Test]
    public void D02_SauSua_LocatorDung_DaHienThi()
    {
        var sanPham = new LoginPage(driver).DangNhap("standard_user", "secret_sauce");
        Assert.That(sanPham.DaHienThi(), Is.True);
    }
}
