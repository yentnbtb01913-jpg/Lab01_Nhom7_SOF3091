using NUnit.Framework;
using OpenQA.Selenium;
namespace FirstTests
{
    public class AlertIframeTests : BaseTest
    {
        private const string AlertUrl = "https://the-internet.herokuapp.com/javascript_alerts";
        private const string IframeUrl = "https://the-internet.herokuapp.com/iframe";

        // chờ alert xuất hiện bằng Explicit Wait
        private IAlert WaitForAlert() =>
            wait.Until(d =>
            {
                try { return d.SwitchTo().Alert(); }
                catch (NoAlertPresentException) { return null; }
            });

        private string Result() => driver.FindElement(By.Id("result")).Text;

        [Test]
        public void C1_JsAlert_Accept()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsAlert()']")).Click();

            IAlert alert = WaitForAlert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));   // Assert TRƯỚC
            alert.Accept();

            Assert.That(Result(), Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void C2_JsConfirm_Dismiss()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsConfirm()']")).Click();

            IAlert alert = WaitForAlert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"));
            alert.Dismiss();

            Assert.That(Result(), Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void C3_JsPrompt_SendKeysAndAccept()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsPrompt()']")).Click();

            IAlert alert = WaitForAlert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"));
            alert.SendKeys("Xin chao Selenium");
            alert.Accept();

            Assert.That(Result(), Is.EqualTo("You entered: Xin chao Selenium"));
        }

        [Test]
        public void C4_Iframe_GoChuVaoTrinhSoanThao()
        {
            driver.Navigate().GoToUrl(IframeUrl);
            string text = "Xin chao iframe tu Selenium";

            // VÀO frame: chờ đến khi chuyển được thì thôi
            wait.Until(d =>
            {
                try { d.SwitchTo().Frame("mce_0_ifr"); return true; }
                catch (NoSuchFrameException) { return false; }
            });

            // chờ body của editor xuất hiện rồi mới thao tác
            IWebElement body = wait.Until(d => d.FindElement(By.Id("tinymce")));

            // click để editor nhận focus, rồi chọn tất cả + xóa (thay cho Clear())
            body.Click();
            body.SendKeys(Keys.Control + "a");
            body.SendKeys(Keys.Delete);
            body.SendKeys(text);

            // chờ chữ hiện ra rồi mới assert, tránh đọc quá sớm
            wait.Until(d => body.Text == text);
            Assert.That(body.Text, Is.EqualTo(text));

            driver.SwitchTo().DefaultContent();                 // RA khỏi frame

            // chứng minh đã ra ngoài: tìm được tiêu đề của trang chính
            Assert.That(driver.FindElement(By.TagName("h3")).Text, Does.Contain("iFrame"));
        }
    }
}
