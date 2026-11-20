using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

public sealed class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    private readonly By taiKhoan = By.CssSelector("[data-test='username']");
    private readonly By matKhau = By.CssSelector("[data-test='password']");
    private readonly By dangNhap = By.CssSelector("[data-test='login-button']");
    private readonly By thongBaoLoi = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver webDriver, int timeoutSeconds)
    {
        driver = webDriver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
    }

    // Dien thong tin, ke ca khi username hoac password rong.
    public InventoryPage DangNhap(string username, string password)
    {
        IWebElement oUser = wait.Until(d => d.FindElement(taiKhoan));
        IWebElement oPass = wait.Until(d => d.FindElement(matKhau));
        oUser.Clear();
        oPass.Clear();
        if (!string.IsNullOrEmpty(username)) oUser.SendKeys(username);
        if (!string.IsNullOrEmpty(password)) oPass.SendKeys(password);
        wait.Until(d => d.FindElement(dangNhap)).Click();
        return new InventoryPage(driver, wait.Timeout.TotalSeconds);
    }

    public bool CoThongBaoLoi()
    {
        return driver.FindElements(thongBaoLoi).Any(x => x.Displayed);
    }

    public string LayThongBaoLoi()
    {
        var items = driver.FindElements(thongBaoLoi);
        return items.Count == 0 ? string.Empty : items[0].Text.Trim();
    }
}
