using System.Linq;
using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.ASM1_TB01913;

[TestFixture]
public class ASM1_Y4_POMTests : ASM1_TestBase
{
    [Test]
    public void Y4_POM_Login_Success()
    {
        InventoryPage inventory = new LoginPage(driver)
            .MoTrang()
            .DangNhap("standard_user", "secret_sauce");

        Assert.That(inventory.DaHienThi(), Is.True);
    }

    [Test]
    public void Y4_POM_CheckSixProducts()
    {
        var inventory = new LoginPage(driver)
            .MoTrang()
            .DangNhap("standard_user", "secret_sauce");
        Assert.That(inventory.LayDanhSachSanPham().Count, Is.EqualTo(6));
    }

    [Test]
    public void Y4_POM_GetProductNames()
    {
        var inventory = new LoginPage(driver)
            .MoTrang()
            .DangNhap("standard_user", "secret_sauce");
        var names = inventory.LayDanhSachSanPham().Select(x => x.Ten).ToList();
        Assert.That(names, Has.Count.EqualTo(6));
        Assert.That(names, Does.Contain("Sauce Labs Backpack"));
        foreach (var name in names) TestContext.WriteLine("San pham: " + name);
    }

    [Test]
    public void Y4_POM_SapXepGiaTangDan()
    {
        var prices = new LoginPage(driver)
            .MoTrang()
            .DangNhap("standard_user", "secret_sauce")
            .SapXep("lohi")
            .LayDanhSachGia();
        Assert.That(prices, Has.Count.EqualTo(6));
        Assert.That(prices, Is.Ordered.Ascending);
    }
}
