
using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class CartPage : BasePage
    {
        // Locator riêng của trang giỏ hàng
        private readonly By tieuDe =
            By.ClassName("title");

        private readonly By danhSachSanPham =
            By.ClassName("cart_item");

        private readonly By tenSanPham =
            By.ClassName("inventory_item_name");

        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        // Kiểm tra trang giỏ hàng đã hiển thị
        public bool DaHienThi()
        {
            return Tim(tieuDe).Text == "Your Cart";
        }

        // Lấy tên sản phẩm trong giỏ hàng
        public string LayTenSanPham()
        {
            return wait.Until(d =>
            {
                var items = d.FindElements(danhSachSanPham);

                return items.Count > 0
                    ? items[0].FindElement(tenSanPham).Text
                    : null;
            });
        }
    }
}
