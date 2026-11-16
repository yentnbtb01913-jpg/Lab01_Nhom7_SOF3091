using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class AlertPage : BasePage
    {
        private readonly By nutJsAlert =
            By.CssSelector("button[onclick='jsAlert()']");

        private readonly By nutJsConfirm =
            By.CssSelector("button[onclick='jsConfirm()']");

        private readonly By nutJsPrompt =
            By.CssSelector("button[onclick='jsPrompt()']");

        private readonly By ketQua = By.Id("result");

        public AlertPage(IWebDriver driver) : base(driver)
        {
        }

        public AlertPage MoTrang()
        {
            MoTrangTheInternet("javascript_alerts");
            return this;
        }

        public AlertPage BamJsAlert()
        {
            Tim(nutJsAlert).Click();
            return this;
        }

        public AlertPage BamJsConfirm()
        {
            Tim(nutJsConfirm).Click();
            return this;
        }

        public AlertPage BamJsPrompt()
        {
            Tim(nutJsPrompt).Click();
            return this;
        }

        // Chờ alert xuất hiện bằng Explicit Wait
        private IAlert ChoCanhBao()
        {
            return wait.Until(d =>
            {
                try { return d.SwitchTo().Alert(); }
                catch (NoAlertPresentException) { return null; }
            });
        }

        public string LayNoiDungCanhBao()
        {
            return ChoCanhBao().Text;
        }

        public AlertPage ChapNhanCanhBao()
        {
            ChoCanhBao().Accept();
            return this;
        }

        public AlertPage HuyCanhBao()
        {
            ChoCanhBao().Dismiss();
            return this;
        }

        public AlertPage NhapVaChapNhan(string noiDung)
        {
            IAlert canhBao = ChoCanhBao();
            canhBao.SendKeys(noiDung);
            canhBao.Accept();
            return this;
        }

        public string LayKetQua()
        {
            return Tim(ketQua).Text;
        }
    }
}
