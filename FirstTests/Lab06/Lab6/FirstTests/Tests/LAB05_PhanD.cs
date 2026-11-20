using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using FirstTests.Pages;

namespace FirstTests.Tests;

public sealed class DuLieuNguoiDung
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool ThanhCong { get; set; }
    public string GhiChu { get; set; } = string.Empty;
}

[TestFixture]
[Category("Login")]
public class LAB05_PhanD : FirstTests.Core.BaseTest
{
    // NUnit doc du lieu truoc khi tao class test, vi vay phai static.
    public static IEnumerable<TestCaseData> DuLieuDangNhap()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", "users.json");
        if (!File.Exists(path))
            throw new FileNotFoundException("Khong tim thay Data/users.json trong output.", path);
        List<DuLieuNguoiDung> data = JsonSerializer.Deserialize<List<DuLieuNguoiDung>>(
            File.ReadAllText(path)) ?? throw new InvalidDataException("users.json khong hop le.");
        if (data.Count < 5) throw new InvalidDataException("users.json phai co it nhat 5 bo du lieu.");
        for (int i = 0; i < data.Count; i++)
        {
            DuLieuNguoiDung user = data[i];
            string nhan = string.IsNullOrWhiteSpace(user.Username) ? "Trong" : "CoDuLieu";
            yield return new TestCaseData(user.Username, user.Password, user.ThanhCong, user.GhiChu)
                .SetName($"DangNhap_{i + 1:00}_{nhan}_{(user.ThanhCong ? "ThanhCong" : "ThatBai")}");
        }
    }

    [TestCaseSource(nameof(DuLieuDangNhap))]
    public void D01_DangNhapTheoDuLieuJson(string username, string password, bool thanhCong, string ghiChu)
    {
        var page = new LoginPage(driver, config.TimeoutSeconds);
        bool actual = page.DangNhap(username, password).DaHienThi();
        TestContext.WriteLine($"Username=[{username}]; Expected={thanhCong}; Actual={actual}; GhiChu={ghiChu}");
        Assert.That(actual, Is.EqualTo(thanhCong), "Kiem tra ket qua dang nhap: " + ghiChu);
        if (!thanhCong)
            Assert.That(page.CoThongBaoLoi(), Is.True, "Dang nhap that bai phai co thong bao loi.");
    }
}
