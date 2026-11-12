
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        // Locator danh sách giá
        private readonly By danhSachGia =
            By.ClassName("inventory_item_price");

        // Locator hộp chọn sắp xếp
        private readonly By sapXepDropdown =
            By.ClassName("product_sort_container");

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
            wait.Until(d =>
                d.FindElements(danhSachSanPham).Count == 6
            );

            return driver.FindElements(danhSachSanPham).Count;
        }

        // Lấy danh sách tên sản phẩm
        public List<string> LayDanhSachTen()
        {
            wait.Until(d =>
                d.FindElements(danhSachSanPham).Count == 6
            );

            return driver.FindElements(danhSachSanPham)
                .Select(item => item.FindElement(tenSanPham).Text)
                .ToList();
        }

        // Sắp xếp sản phẩm theo giá hoặc tên
        public InventoryPage SapXep(string value)
        {
            SelectElement dropdown =
                new SelectElement(Tim(sapXepDropdown));

            dropdown.SelectByValue(value);

            return this;
        }

        // Lấy danh sách giá sản phẩm
        public List<decimal> LayDanhSachGia()
        {
            wait.Until(d =>
                d.FindElements(danhSachGia).Count == 6
            );

            return driver.FindElements(danhSachGia)
                .Select(item =>
                    decimal.Parse(
                        item.Text.Replace("$", ""),
                        CultureInfo.InvariantCulture
                    )
                )
                .ToList();
        }

        // Thêm sản phẩm vào giỏ hàng
        public InventoryPage ThemVaoGioHang(string productId)
        {
            By nutThem = By.Id(productId);
            Tim(nutThem).Click();

            return this;
        }

        // Lấy số lượng sản phẩm trong giỏ
        public string LaySoLuongTrongGio()
        {
            By badge = By.CssSelector(
                "[data-test='shopping-cart-badge']"
            );

            return Tim(badge).Text;
        }

        // Mở trang giỏ hàng
        public CartPage MoGioHang()
        {
            By nutGioHang = By.CssSelector(
                "[data-test='shopping-cart-link']"
            );

            Tim(nutGioHang).Click();

            return new CartPage(driver);
        }

    }

}
