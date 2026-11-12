using OpenQA.Selenium;

namespace FirstTests.Pages_Lab04;

public class LoginPage : BasePage
{
    private readonly By oTaiKhoan = By.Id("user-name");
    private readonly By oMatKhau = By.Id("password");
    private readonly By nutDangNhap = By.Id("login-button");
    private readonly By thongBaoLoi = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver) : base(driver) { }

    // Mo trang dang nhap SauceDemo.
    public LoginPage MoTrang()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        return this;
    }

    // Chuyen huong den trang san pham neu thong tin hop le.
    // Neu dang nhap that bai, test van co the doc loi tu LoginPage hien tai.
    public InventoryPage DangNhap(string tk, string mk)
    {
        Tim(oTaiKhoan).Clear();
        Tim(oTaiKhoan).SendKeys(tk);
        Tim(oMatKhau).Clear();
        Tim(oMatKhau).SendKeys(mk);
        Tim(nutDangNhap).Click();
        return new InventoryPage(driver);
    }

    public string LayThongBaoLoi()
    {
        return Tim(thongBaoLoi).Text.Trim();
    }

    public bool ThongBaoLoiDaHienThi()
    {
        return Tim(thongBaoLoi).Displayed;
    }

    public string LayDuongDan()
    {
        return driver.Url;
    }
}
