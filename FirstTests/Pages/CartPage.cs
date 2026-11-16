using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class CartPage : BasePage
    {
        private readonly By tenSanPham =
            By.CssSelector("[data-test='inventory-item-name']");

        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        // Chờ trang giỏ hàng tải xong rồi lấy tên sản phẩm đầu tiên
        public string LayTenSanPhamDauTien()
        {
            wait.Until(d => d.Url.Contains("cart.html"));

            return wait.Until(d =>
            {
                var cacTen = d.FindElements(tenSanPham);

                return cacTen.Count > 0 ? cacTen[0].Text : null;
            });
        }
    }
}
