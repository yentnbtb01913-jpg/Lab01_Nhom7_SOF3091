
using NUnit.Framework;
using FirstTests.Pages;

namespace FirstTests.Tests
{
    public class AlertIframeTests : BaseTest
    {
        // CA 1: JavaScript Alert - Accept
        [Test]
        public void C1_JsAlert_Accept()
        {
            AlertIframePage page = new AlertIframePage(driver);

            page.MoTrangAlert()
                .BamJsAlert();

            string alertText = page.LayNoiDungAlert();

            Assert.That(
                alertText,
                Is.EqualTo("I am a JS Alert")
            );

            page.ChapNhanAlert();

            Assert.That(
                page.LayKetQua(),
                Is.EqualTo("You successfully clicked an alert")
            );
        }

        // CA 2: JavaScript Confirm - Dismiss
        [Test]
        public void C2_JsConfirm_Dismiss()
        {
            AlertIframePage page = new AlertIframePage(driver);

            page.MoTrangAlert()
                .BamJsConfirm();

            string alertText = page.LayNoiDungAlert();

            Assert.That(
                alertText,
                Is.EqualTo("I am a JS Confirm")
            );

            page.HuyAlert();

            Assert.That(
                page.LayKetQua(),
                Is.EqualTo("You clicked: Cancel")
            );
        }

        // CA 3: JavaScript Prompt - SendKeys và Accept
        [Test]
        public void C3_JsPrompt_SendKeysAndAccept()
        {
            AlertIframePage page = new AlertIframePage(driver);

            page.MoTrangAlert()
                .BamJsPrompt();

            string alertText = page.LayNoiDungAlert();

            Assert.That(
                alertText,
                Is.EqualTo("I am a JS prompt")
            );

            page.NhapNoiDungPrompt("Xin chao Selenium")
                .ChapNhanAlert();

            Assert.That(
                page.LayKetQua(),
                Is.EqualTo("You entered: Xin chao Selenium")
            );
        }

        // CA 4: Nhập chữ vào trình soạn thảo iFrame
        [Test]
        public void C4_Iframe_GoChuVaoTrinhSoanThao()
        {
            AlertIframePage page = new AlertIframePage(driver);

            page.MoTrangIframe();

            string text = "Xin chao iframe tu Selenium";

            // Vào iframe và nhập nội dung
            page.VaoIframe();

            string actualText =
                page.NhapVanBanTrongIframe(text);

            // Kiểm tra nội dung bên trong iframe
            Assert.That(
                actualText,
                Is.EqualTo(text)
            );

            // Trở về trang chính
            page.RaKhoiIframe();

            // Kiểm tra tiêu đề trang chính
            Assert.That(
                page.LayTieuDeTrangChinh(),
                Does.Contain("iFrame")
            );
        }
    }
}
