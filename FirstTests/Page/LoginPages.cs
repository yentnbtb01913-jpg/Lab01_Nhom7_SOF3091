
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class LoginPages : BasePage
    {
        // Locator
        private readonly By txtUsername =
            By.Id("user-name");

        private readonly By txtPassword =
            By.Id("password");

        private readonly By btnLogin =
            By.Id("login-button");

        private readonly By errorMessage =
            By.CssSelector("[data-test='error']");

        // Constructor
        public LoginPages(IWebDriver driver) : base(driver)
        {
        }

        // Đăng nhập vào hệ thống
        public InventoryPages DangNhap(
            string taiKhoan,
            string matKhau)
        {
            Tim(txtUsername).SendKeys(taiKhoan);
            Tim(txtPassword).SendKeys(matKhau);
            Tim(btnLogin).Click();

            return new InventoryPages(driver);
        }

        // Lấy thông báo lỗi đăng nhập
        public string LayThongBaoLoi()
        {
            return Tim(errorMessage).Text;
        }
    }
}
