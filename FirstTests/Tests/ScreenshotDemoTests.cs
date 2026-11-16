using FirstTests.Core;
using FirstTests.Pages;
using NUnit.Framework;

namespace FirstTests.Tests
{
    public class ScreenshotDemoTests : BaseTest
    {
        [Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
        public void D1_CoY_LamRot_DeChupAnh()
        {
            LoginPage loginPage = new LoginPage(driver);

            Assert.That(loginPage.LayTieuDeTrinhDuyet(), Is.EqualTo("Tieu de sai co y"));   // cố ý sai
        }
    }
}
