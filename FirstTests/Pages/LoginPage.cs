using System.Linq;
using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class LoginPage : BasePage
    {
        // Locator được đóng gói ở dạng private
        private readonly By oTaiKhoan = By.Id("user-name");
        private readonly By oMatKhau = By.Id("password");
        private readonly By nutLogin = By.Id("login-button");
        private readonly By thongBaoLoi =
            By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        // Đăng nhập thành công và chuyển sang trang sản phẩm
        public InventoryPage DangNhap(string taiKhoan, string matKhau)
        {
            NhapThongTinVaBamDangNhap(taiKhoan, matKhau);

            return new InventoryPage(driver);
        }

        // Đăng nhập thất bại: vẫn ở lại trang đăng nhập
        public LoginPage DangNhapThatBai(string taiKhoan, string matKhau)
        {
            NhapThongTinVaBamDangNhap(taiKhoan, matKhau);

            return this;
        }

        // Thông báo lỗi có đang hiển thị hay không
        public bool ThongBaoLoiDangHienThi()
        {
            var phanTu = wait.Until(
                d => d.FindElements(thongBaoLoi).FirstOrDefault()
            );

            return phanTu.Displayed;
        }

        // Lấy thông báo khi đăng nhập thất bại
        public string LayThongBaoLoi()
        {
            return Tim(thongBaoLoi).Text;
        }

        private void NhapThongTinVaBamDangNhap(string taiKhoan, string matKhau)
        {
            Tim(oTaiKhoan).SendKeys(taiKhoan);
            Tim(oMatKhau).SendKeys(matKhau);
            TimCoTheBam(nutLogin).Click();
        }
    }
}
