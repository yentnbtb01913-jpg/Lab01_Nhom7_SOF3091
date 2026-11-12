using System;
using System.Collections.Generic;
using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

// Ban POM cua file goc FirstTests/LoginTest_TB01915.cs; file goc KHONG thay doi.
[TestFixture]
public class LoginTest_TB01915_Lab04 : TestBase
{
    [Test]
    public void Login_Success()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
        inventory.ChoDuongDanKetThuc("/inventory.html");
        Assert.That(inventory.LayDuongDan(), Does.EndWith("/inventory.html"),
            "Dang nhap thanh cong phai chuyen den inventory.html");
        TestContext.WriteLine("Dang nhap thanh cong - inventory.html");
    }

    [Test]
    public void Login_LockedOutUser()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        login.DangNhap("locked_out_user", "secret_sauce");
        bool errorDisplayed = login.ThongBaoLoiDaHienThi();
        string actualMessage = login.LayThongBaoLoi();
        string expectedMessage = "Epic sadface: Sorry, this user has been locked out.";
        Assert.Multiple(() =>
        {
            Assert.That(errorDisplayed, Is.True, "Thong bao loi phai hien thi");
            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                "Noi dung thong bao loi khong dung");
            Assert.That(login.LayDuongDan(), Does.Not.Contain("inventory.html"),
                "Tai khoan bi khoa khong duoc dang nhap");
        });
        TestContext.WriteLine("Dang nhap that bai dung nhu mong doi");
    }

    [Test]
    public void GetProductInformation()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
        inventory.ChoDuongDanKetThuc("/inventory.html").ChoDanhSachSanPham();
        var items = inventory.LayDanhSachSanPham();
        List<string> names = new List<string>();
        List<string> prices = new List<string>();
        TestContext.WriteLine($"So san pham tim thay: {items.Count}");
        int index = 1;
        foreach (var item in items)
        {
            names.Add(item.Ten);
            prices.Add(item.Gia);
            TestContext.WriteLine($"{index}. {item.Ten} - {item.Gia}");
            index++;
        }
        Assert.Multiple(() =>
        {
            Assert.That(items.Count, Is.EqualTo(6), "Trang phai co dung 6 san pham");
            Assert.That(names, Has.All.Not.Empty, "Ten san pham khong duoc rong");
            Assert.That(prices, Has.All.StartsWith("$"), "Moi gia phai bat dau bang ky tu $");
            Assert.That(names, Does.Contain("Sauce Labs Backpack"),
                "Khong tim thay Sauce Labs Backpack");
        });
        TestContext.WriteLine("Da hoan thanh kiem tra thong tin san pham");
    }
}
