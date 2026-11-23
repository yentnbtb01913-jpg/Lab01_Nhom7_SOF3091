using NUnit.Framework;
using FirstTests.Pages;

namespace FirstTests.Tests;

[TestFixture]
[Category("Smoke")]
public class LAB06_Smoke : FirstTests.Core.BaseTest
{
    [Test]
    public void A01_TrangDangNhapHienThi()
    {
        Assert.That(driver.Title, Does.Contain("Swag Labs"));
    }
}

[TestFixture]
[Category("Login")]
public class LAB06_Login : FirstTests.Core.BaseTest
{
    [Test]
    public void A02_DangNhapThanhCong()
    {
        bool visible = new LoginPage(driver, config.TimeoutSeconds)
            .DangNhap("standard_user", "secret_sauce").DaHienThi();
        Assert.That(visible, Is.True);
    }

    // Chạy RIÊNG khi cần minh chứng [ERROR], không phá Run All bình thường.
    [Test, Explicit("Chỉ chạy riêng để kiểm chứng dòng log ERROR và báo cáo Failed")]
    public void B01_CoYFail_DeChungMinhErrorLog()
    {
        Assert.Fail("LAB06: ca cố ý thất bại để kiểm tra Log.Error và ảnh PNG.");
    }
}
