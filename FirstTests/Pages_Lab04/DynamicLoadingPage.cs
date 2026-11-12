using OpenQA.Selenium;

namespace FirstTests.Pages_Lab04;

public class DynamicLoadingPage : BasePage
{
    private const string Url = "https://the-internet.herokuapp.com/dynamic_loading/2";
    private readonly By nutStart = By.CssSelector("#start button");
    private readonly By ketQua = By.Id("finish");

    public DynamicLoadingPage(IWebDriver driver) : base(driver) { }

    public DynamicLoadingPage MoTrang()
    {
        driver.Navigate().GoToUrl(Url);
        return this;
    }

    public DynamicLoadingPage BatDau()
    {
        Tim(nutStart).Click();
        return this;
    }

    // DUNG cho ca test Lab 3 A1: chu dong KHONG su dung Wait.
    public string LayKetQuaKhongWait()
    {
        return driver.FindElement(ketQua).Text;
    }

    // DUNG cho ca test Lab 3 A2: tim phan tu voi Explicit Wait.
    public string LayKetQuaCoWait()
    {
        return Tim(ketQua).Text;
    }
}
