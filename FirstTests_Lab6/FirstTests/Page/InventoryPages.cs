
using FirstTests.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FirstTests.Page
{
    public class InventoryPages : BasePage
    {
        // Locator
        private readonly By inventoryContainer =
            By.ClassName("inventory_list");

        private readonly By sortDropdown =
            By.ClassName("product_sort_container");

        private readonly By productPrices =
            By.ClassName("inventory_item_price");

        private readonly By productNames =
            By.CssSelector("[data-test='inventory-item-name']");

        private readonly By addBackpackButton =
            By.Id("add-to-cart-sauce-labs-backpack");

        private readonly By shoppingCartBadge =
            By.CssSelector("[data-test='shopping-cart-badge']");

        private readonly By shoppingCartLink =
            By.CssSelector("[data-test='shopping-cart-link']");

        // Constructor
        public InventoryPages(IWebDriver driver) : base(driver)
        {
        }

        // Kiểm tra danh sách sản phẩm đã hiển thị
        public bool DaHienThi()
        {
            return Tim(inventoryContainer).Displayed;
        }

        // Sắp xếp sản phẩm
        public InventoryPages SapXep(string value)
        {
            SelectElement select =
                new SelectElement(Tim(sortDropdown));

            select.SelectByValue(value);

            return this;
        }

        // Lấy danh sách giá sản phẩm dạng số
        public List<decimal> LayDanhSachGia()
        {
            wait.Until(d =>
                d.FindElements(productPrices).Count > 0);

            return driver.FindElements(productPrices)
                .Select(element =>
                    decimal.Parse(
                        element.Text.Replace("$", ""),
                        CultureInfo.InvariantCulture))
                .ToList();
        }

        // Lấy danh sách tên sản phẩm
        public List<string> LayDanhSachTenSanPham()
        {
            wait.Until(d =>
                d.FindElements(productNames).Count == 6);

            return driver.FindElements(productNames)
                .Select(element => element.Text)
                .ToList();
        }

        // Lấy danh sách giá sản phẩm dạng chuỗi
        public List<string> LayDanhSachGiaChuoi()
        {
            wait.Until(d =>
                d.FindElements(productPrices).Count == 6);

            return driver.FindElements(productPrices)
                .Select(element => element.Text)
                .ToList();
        }

        // Thêm sản phẩm Backpack vào giỏ hàng
        public void ThemSanPhamVaoGio()
        {
            Tim(addBackpackButton).Click();
        }

        // Lấy số lượng sản phẩm trong giỏ
        public string LaySoLuongTrongGio()
        {
            return wait.Until(d =>
            {
                var badges = d.FindElements(shoppingCartBadge);

                return badges.Count > 0
                    ? badges[0].Text
                    : null;
            });
        }

        // Mở giỏ hàng
        public CartPages MoGioHang()
        {
            Tim(shoppingCartLink).Click();

            wait.Until(d => d.Url.Contains("cart.html"));

            return new CartPages(driver);
        }
    }
}
