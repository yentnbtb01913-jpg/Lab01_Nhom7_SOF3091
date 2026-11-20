using OpenQA.Selenium;
namespace FirstTests.Pages
{
    public class LoginPage : BasePage
    {
        public const string Url = "https://www.saucedemo.com/";

        // Locator được đóng gói ở dạng private
        private readonly By oTaiKhoan = By.Id("user-name");
        private readonly By oMatKhau = By.Id("password");
        private readonly By nutLogin = By.Id("login-button");
        private readonly By thongBaoLoi =
            By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang đăng nhập
        public LoginPage MoTrang()
        {
            driver.Navigate().GoToUrl(Url);
            return this;
        }

        // Đăng nhập thành công và chuyển sang trang sản phẩm
        public InventoryPage DangNhap(string taiKhoan, string matKhau)
        {
            Tim(oTaiKhoan).SendKeys(taiKhoan);
            Tim(oMatKhau).SendKeys(matKhau);
            Tim(nutLogin).Click();

            return new InventoryPage(driver);
        }

        // Lấy thông báo khi đăng nhập thất bại
        public string LayThongBaoLoi()
        {
            return Tim(thongBaoLoi).Text;
        }
    }
}
