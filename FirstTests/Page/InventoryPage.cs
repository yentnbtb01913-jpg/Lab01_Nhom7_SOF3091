using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
namespace FirstTests.Pages
{
    public class InventoryPage : BasePage
    {
        // Locator của trang sản phẩm
        private readonly By tieuDe =
            By.ClassName("title");

        private readonly By danhSachSanPham =
            By.ClassName("inventory_item");

        private readonly By tenSanPham =
            By.CssSelector("[data-test='inventory-item-name']");

        // Khởi tạo trang sản phẩm
        public InventoryPage(IWebDriver driver) : base(driver)
        {
        }

        // Kiểm tra trang sản phẩm đã hiển thị
        public bool DaHienThi()
        {
            return Tim(tieuDe).Text == "Products";
        }

        // Đếm số lượng sản phẩm
        public int SoSanPham()
        {
            // Chờ đến khi có đủ 6 sản phẩm
            wait.Until(d =>
                d.FindElements(danhSachSanPham).Count == 6
            );

            // Sau khi đủ 6 sản phẩm thì trả về số lượng
            return driver.FindElements(danhSachSanPham).Count;
        }

        // Lấy danh sách tên sản phẩm
        public List<string> LayDanhSachTen()
        {
            // Chờ danh sách sản phẩm hiển thị đầy đủ
            wait.Until(d =>
                d.FindElements(danhSachSanPham).Count == 6
            );

            return driver.FindElements(danhSachSanPham)
                .Select(item => item.FindElement(tenSanPham).Text)
                .ToList();
        }
    }
}
