using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

// Ban POM cua PhanA_Lab03.cs (Dynamic Loading).
[TestFixture]
public class PhanA_Lab03_Lab04 : TestBase
{
    [Test, Explicit("Chạy riêng để chụp ảnh test RỚT")]
    public void A1_RotKhongWait()
    {
        DynamicLoadingPage page = new DynamicLoadingPage(driver).MoTrang();
        page.BatDau();
        // Giu nguyen y nghia: tim NGAY khong Wait -> co the NoSuchElementException.
        string ketQua = page.LayKetQuaKhongWait();
        Assert.That(ketQua, Is.EqualTo("Hello World!"));
    }

    [Test]
    public void A2_PassCoWait()
    {
        DynamicLoadingPage page = new DynamicLoadingPage(driver).MoTrang();
        page.BatDau();
        string ketQua = page.LayKetQuaCoWait();
        Assert.That(ketQua, Is.EqualTo("Hello World!"));
    }
}
