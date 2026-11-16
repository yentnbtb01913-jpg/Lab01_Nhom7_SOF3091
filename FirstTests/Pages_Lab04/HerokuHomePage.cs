using OpenQA.Selenium;

namespace FirstTests.Pages_Lab04;

public class HerokuHomePage : BasePage
{
    private static string Url => FirstTests.Config.ConfigReader.Load().Site("TheInternet");

    public HerokuHomePage(IWebDriver driver) : base(driver) { }

    public HerokuHomePage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public string LayTieuDe() => driver.Title;
}
