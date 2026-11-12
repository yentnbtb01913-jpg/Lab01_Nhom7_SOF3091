using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

[TestFixture]
public class PhanB_Lab03_Lab04 : TestBase
{
    // Giu cau hinh incognito cua PhanB_Lab03.cs.
    protected override ChromeOptions TaoChromeOptions()
    {
        ChromeOptions options = base.TaoChromeOptions();
        options.AddArgument("--incognito");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        return options;
    }

    [Test]
    public void B1_GiaTangDan()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
        inventory.ChoDanhSachSanPham();
        List<decimal> prices = inventory.SapXep("lohi").LayDanhSachGia();
        Assert.That(prices.Count, Is.EqualTo(6), "Phải có đủ 6 sản phẩm");
        List<decimal> daSapXep = new List<decimal>(prices);
        daSapXep.Sort();
        Assert.That(prices, Is.EqualTo(daSapXep),
            "Giá không tăng dần. Thực tế: " + string.Join(", ", prices));
    }
}
