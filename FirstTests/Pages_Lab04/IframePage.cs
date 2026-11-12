using OpenQA.Selenium;

namespace FirstTests.Pages_Lab04;

public class IframePage : BasePage
{
    private const string Url = "https://the-internet.herokuapp.com/iframe";
    private readonly By noiDung = By.Id("tinymce");
    private readonly By tieuDe = By.TagName("h3");

    public IframePage(IWebDriver driver) : base(driver) { }

    public IframePage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public void VaoIframe()
    {
        wait.Until(d => d.SwitchTo().Frame("mce_0_ifr"));
    }

    public void GhiNoiDung(string giaTri)
    {
        IWebElement body = Tim(noiDung);
        body.Click();
        body.SendKeys(Keys.Control + "a");
        body.SendKeys(Keys.Delete);
        body.SendKeys(giaTri);
    }

    public string LayNoiDung() => Tim(noiDung).Text.Trim();
    public void ThoatIframe() => driver.SwitchTo().DefaultContent();
    public string LayTieuDeNgoaiIframe() => Tim(tieuDe).Text;
}
