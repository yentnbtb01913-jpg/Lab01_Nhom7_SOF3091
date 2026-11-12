using OpenQA.Selenium;

namespace FirstTests.Pages_Lab04;

public class JavaScriptAlertsPage : BasePage
{
    private const string Url = "https://the-internet.herokuapp.com/javascript_alerts";
    private readonly By nutAlert = By.CssSelector("button[onclick='jsAlert()']");
    private readonly By nutConfirm = By.CssSelector("button[onclick='jsConfirm()']");
    private readonly By nutPrompt = By.CssSelector("button[onclick='jsPrompt()']");
    private readonly By ketQua = By.Id("result");

    public JavaScriptAlertsPage(IWebDriver driver) : base(driver) { }

    public JavaScriptAlertsPage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public void MoAlert() => Tim(nutAlert).Click();
    public void MoConfirm() => Tim(nutConfirm).Click();
    public void MoPrompt() => Tim(nutPrompt).Click();
    public string LayNoiDungThongBao() => driver.SwitchTo().Alert().Text;
    public void ChapNhan() => driver.SwitchTo().Alert().Accept();
    public void Huy() => driver.SwitchTo().Alert().Dismiss();
    public void NhapPrompt(string noiDung) => driver.SwitchTo().Alert().SendKeys(noiDung);
    public string LayKetQua() => Tim(ketQua).Text;
}
