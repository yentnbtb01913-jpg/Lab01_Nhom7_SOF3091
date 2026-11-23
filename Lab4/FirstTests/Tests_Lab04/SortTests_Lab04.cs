using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

// Ban POM cua SortTests.cs; khong ke thua BaseTest cua Lab 3.
[TestFixture]
public class SortTests_Lab04 : TestBase
{
    [Test]
    public void B1_SapXepPriceLowToHigh_GiaTangDan()
    {
        LoginPage login = new LoginPage(driver).MoTrang();
        // Chaining bat buoc cua Lab 4.
        List<decimal> prices = login
            .DangNhap("standard_user", "secret_sauce")
            .SapXep("lohi")
            .LayDanhSachGia();
        Assert.That(prices.Count, Is.EqualTo(6));
        List<decimal> expected = new List<decimal>(prices);
        expected.Sort();
        Assert.That(prices, Is.EqualTo(expected),
            "Danh sách giá không tăng dần: " + string.Join(", ", prices));
        // The hien rang danh sach da sap xep tang dan, khong thay the assert goc.
        Assert.That(prices, Is.Ordered.Ascending);
    }
}
