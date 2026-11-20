using FirstTests.Core;
using FirstTests.Pages;
using NUnit.Framework;

namespace FirstTests.Tests
{
    [Category("AlertIframe")]
    public class AlertIframeTests : BaseTest
    {
        [Test]
        public void C1_JsAlert_Accept()
        {
            AlertPage trang = new AlertPage(driver).MoTrang().BamJsAlert();

            Assert.That(trang.LayNoiDungCanhBao(), Is.EqualTo("I am a JS Alert"));   // Assert TRƯỚC
            trang.ChapNhanCanhBao();

            Assert.That(trang.LayKetQua(), Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void C2_JsConfirm_Dismiss()
        {
            AlertPage trang = new AlertPage(driver).MoTrang().BamJsConfirm();

            Assert.That(trang.LayNoiDungCanhBao(), Is.EqualTo("I am a JS Confirm"));
            trang.HuyCanhBao();

            Assert.That(trang.LayKetQua(), Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void C3_JsPrompt_SendKeysAndAccept()
        {
            AlertPage trang = new AlertPage(driver).MoTrang().BamJsPrompt();

            Assert.That(trang.LayNoiDungCanhBao(), Is.EqualTo("I am a JS prompt"));
            trang.NhapVaChapNhan("Xin chao Selenium");

            Assert.That(trang.LayKetQua(), Is.EqualTo("You entered: Xin chao Selenium"));
        }

        [Test]
        public void C4_Iframe_GoChuVaoTrinhSoanThao()
        {
            string noiDung = "Xin chao iframe tu Selenium";

            IframePage trang = new IframePage(driver)
                .MoTrang()
                .GoVanBan(noiDung);

            Assert.That(trang.LayNoiDungTrinhSoanThao(), Is.EqualTo(noiDung));

            trang.RaKhoiFrame();   // RA khỏi frame (như đóng ngoặc)

            // chứng minh đã ra ngoài: tìm được tiêu đề của trang chính
            Assert.That(trang.LayTieuDeTrang(), Does.Contain("iFrame"));
        }
    }
}
