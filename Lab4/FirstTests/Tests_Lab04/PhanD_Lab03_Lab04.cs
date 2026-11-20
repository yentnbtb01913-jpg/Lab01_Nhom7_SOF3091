using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

[TestFixture]
public class PhanD_Lab03_Lab04 : TestBase
{
    // Giu ca test co y rot de chup anh, khong chuyen thanh PASS.
    [Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
    public void D1_CoYRot()
    {
        HerokuHomePage page = new HerokuHomePage(driver).MoTrang();
        Assert.That(page.LayTieuDe(), Is.EqualTo("TIEU DE SAI CO Y"));
    }
}
