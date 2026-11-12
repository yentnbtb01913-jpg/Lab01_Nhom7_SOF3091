using System;
using System.Collections.Generic;
using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

// Ban POM cua file goc FirstTests/LoginTests.cs; KHONG sua file goc.
[TestFixture]
public class LoginTests_Lab04 : TestBase
{
    [Test]
    public void Login_Success()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
        string currentUrl = inventory.LayDuongDan();
        Console.WriteLine($"URL sau khi dang nhap: {currentUrl}");
        Assert.That(currentUrl, Does.Contain("inventory.html"),
            "Đăng nhập thành công phải chuyển đến trang inventory.html");
    }

    [Test]
    public void Login_LockedOutUser()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        login.DangNhap("locked_out_user", "secret_sauce");
        bool hienThi = login.ThongBaoLoiDaHienThi();
        string actualMessage = login.LayThongBaoLoi();
        Console.WriteLine($"Thong bao loi: {actualMessage}");
        Console.WriteLine($"URL hien tai: {login.LayDuongDan()}");
        Assert.Multiple(() =>
        {
            Assert.That(hienThi, Is.True, "Thông báo lỗi phải hiện ra");
            Assert.That(actualMessage,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Nội dung thông báo lỗi không đúng");
            Assert.That(login.LayDuongDan(), Does.Not.Contain("inventory.html"),
                "Không được chuyển sang trang inventory");
        });
    }

    [Test]
    public void GetProductInformation()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
        var items = inventory.LayDanhSachSanPham();
        var names = new List<string>();
        var prices = new List<string>();
        int index = 1;
        foreach (var item in items)
        {
            names.Add(item.Ten);
            prices.Add(item.Gia);
            Console.WriteLine($"{index}. {item.Ten} - {item.Gia}");
            index++;
        }
        Assert.Multiple(() =>
        {
            Assert.That(items.Count, Is.EqualTo(6), "Trang phải có đúng 6 sản phẩm");
            Assert.That(names, Has.All.Not.Empty, "Không được có tên sản phẩm rỗng");
            Assert.That(prices, Has.All.StartsWith("$"), "Mọi giá phải bắt đầu bằng ký tự $");
            Assert.That(names, Does.Contain("Sauce Labs Backpack"),
                "Danh sách phải chứa sản phẩm Sauce Labs Backpack");
        });
    }
}
