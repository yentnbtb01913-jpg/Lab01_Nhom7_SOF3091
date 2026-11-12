
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages
{
    public class AlertIframePage : BasePage
    {
        private const string AlertUrl =
            "https://the-internet.herokuapp.com/javascript_alerts";

        private const string IframeUrl =
            "https://the-internet.herokuapp.com/iframe";

        // Locator các nút JavaScript Alert
        private readonly By nutJsAlert =
            By.CssSelector("button[onclick='jsAlert()']");

        private readonly By nutJsConfirm =
            By.CssSelector("button[onclick='jsConfirm()']");

        private readonly By nutJsPrompt =
            By.CssSelector("button[onclick='jsPrompt()']");

        // Locator kết quả và tiêu đề
        private readonly By ketQua =
            By.Id("result");

        private readonly By tieuDeIframe =
            By.TagName("h3");

        // Locator nội dung bên trong iframe
        private readonly By noiDungEditor =
            By.Id("tinymce");

        public AlertIframePage(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang JavaScript Alerts
        public AlertIframePage MoTrangAlert()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            return this;
        }

        // Mở trang iframe
        public AlertIframePage MoTrangIframe()
        {
            driver.Navigate().GoToUrl(IframeUrl);
            return this;
        }

        // Nhấn nút JavaScript Alert
        public AlertIframePage BamJsAlert()
        {
            Tim(nutJsAlert).Click();
            return this;
        }

        // Nhấn nút JavaScript Confirm
        public AlertIframePage BamJsConfirm()
        {
            Tim(nutJsConfirm).Click();
            return this;
        }

        // Nhấn nút JavaScript Prompt
        public AlertIframePage BamJsPrompt()
        {
            Tim(nutJsPrompt).Click();
            return this;
        }

        // Chờ Alert xuất hiện và lấy nội dung
        public string LayNoiDungAlert()
        {
            IAlert alert = wait.Until(d =>
            {
                try
                {
                    return d.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });

            return alert.Text;
        }

        // Chấp nhận Alert
        public AlertIframePage ChapNhanAlert()
        {
            wait.Until(d =>
            {
                try
                {
                    d.SwitchTo().Alert().Accept();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            });

            return this;
        }

        // Hủy Alert
        public AlertIframePage HuyAlert()
        {
            wait.Until(d =>
            {
                try
                {
                    d.SwitchTo().Alert().Dismiss();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            });

            return this;
        }

        // Nhập nội dung vào Prompt
        public AlertIframePage NhapNoiDungPrompt(string text)
        {
            IAlert alert = wait.Until(d =>
            {
                try
                {
                    return d.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });

            alert.SendKeys(text);
            return this;
        }

        // Lấy kết quả sau khi xử lý Alert
        public string LayKetQua()
        {
            return Tim(ketQua).Text;
        }

        // Chuyển vào iframe
        public AlertIframePage VaoIframe()
        {
            wait.Until(d =>
            {
                d.SwitchTo().Frame("mce_0_ifr");
                return true;
            });

            return this;
        }

        // Nhập văn bản vào trình soạn thảo
        public string NhapVanBanTrongIframe(string text)
        {
            IWebElement body = Tim(noiDungEditor);

            body.Click();
            body.SendKeys(Keys.Control + "a");
            body.SendKeys(Keys.Delete);
            body.SendKeys(text);

            return body.Text;
        }

        // Chuyển về trang chính
        public AlertIframePage RaKhoiIframe()
        {
            driver.SwitchTo().DefaultContent();
            return this;
        }

        // Lấy tiêu đề trang chính
        public string LayTieuDeTrangChinh()
        {
            return Tim(tieuDeIframe).Text;
        }
    }
}
