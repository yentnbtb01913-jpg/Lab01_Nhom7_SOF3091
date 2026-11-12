
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class AlertAndIframePages : BasePage
    {
        // Locator
        private readonly By btnJSAlert =
            By.XPath("//button[text()='Click for JS Alert']");

        private readonly By btnJSConfirm =
            By.XPath("//button[text()='Click for JS Confirm']");

        private readonly By btnJSPrompt =
            By.XPath("//button[text()='Click for JS Prompt']");

        private readonly By result =
            By.Id("result");

        private readonly By editor =
            By.Id("tinymce");

        private readonly By pageFooter =
            By.Id("page-footer");

        // Constructor
        public AlertAndIframePages(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang JavaScript Alerts
        public void MoTrangJavaScriptAlerts()
        {
            driver.Navigate().GoToUrl(
                "https://the-internet.herokuapp.com/javascript_alerts");
        }

        // JavaScript Alert
        public string BamNutJSAlert()
        {
            Tim(btnJSAlert).Click();
            return driver.SwitchTo().Alert().Text;
        }

        public void ChapNhanAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }

        // JavaScript Confirm
        public string BamNutJSConfirm()
        {
            Tim(btnJSConfirm).Click();
            return driver.SwitchTo().Alert().Text;
        }

        public void HuyConfirm()
        {
            driver.SwitchTo().Alert().Dismiss();
        }

        // JavaScript Prompt
        public string BamNutJSPrompt()
        {
            Tim(btnJSPrompt).Click();
            return driver.SwitchTo().Alert().Text;
        }

        public void NhapPromptVaChapNhan(string noiDung)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(noiDung);
            alert.Accept();
        }

        // Lấy kết quả sau khi xử lý Alert
        public string LayKetQua()
        {
            return Tim(result).Text;
        }

        // Mở trang iframe
        public void MoTrangIframe()
        {
            driver.Navigate().GoToUrl(
                "https://the-internet.herokuapp.com/iframe");
        }

        // Nhập nội dung vào iframe
        public void NhapNoiDungIframe(string noiDung)
        {
            driver.SwitchTo().Frame("mce_0_ifr");

            IWebElement textEditor = Tim(editor);
            textEditor.Clear();
            textEditor.SendKeys(noiDung);

            driver.SwitchTo().DefaultContent();
        }

        // Kiểm tra footer hiển thị
        public bool FooterDaHienThi()
        {
            return Tim(pageFooter).Displayed;
        }

        // Mở trang chủ để kiểm tra chụp ảnh
        public void MoTrangChu()
        {
            driver.Navigate().GoToUrl(
                "https://the-internet.herokuapp.com/");
        }
    }
}
