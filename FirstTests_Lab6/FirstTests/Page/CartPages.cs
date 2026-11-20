
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class CartPages : BasePage
    {
        // Locator
        private readonly By productName =
            By.CssSelector("[data-test='inventory-item-name']");

        // Constructor
        public CartPages(IWebDriver driver) : base(driver)
        {
        }

        // Lấy tên sản phẩm trong giỏ hàng
        public string LayTenSanPhamTrongGio()
        {
            return Tim(productName).Text;
        }
    }
}
