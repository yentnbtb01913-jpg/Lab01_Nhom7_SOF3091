using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages
{
    public class InventoryPage : BasePage
    {
        // Thời gian chờ ngắn dùng cho các hàm trả lời CÓ/KHÔNG
        private const int GiayChoNgan = 3;

        // Locator của trang sản phẩm
        private readonly By tieuDe =
            By.ClassName("title");

        private readonly By danhSachSanPham =
            By.ClassName("inventory_item");

        private readonly By tenSanPham =
            By.CssSelector("[data-test='inventory-item-name']");

        private readonly By giaTrongSanPham =
            By.CssSelector("[data-test='inventory-item-price']");

        private readonly By oChonSapXep =
            By.ClassName("product_sort_container");

        private readonly By danhSachGia =
            By.ClassName("inventory_item_price");

        private readonly By nutThemBalo =
            By.Id("add-to-cart-sauce-labs-backpack");

        private readonly By huyHieuGioHang =
            By.CssSelector("[data-test='shopping-cart-badge']");

        private readonly By lienKetGioHang =
            By.CssSelector("[data-test='shopping-cart-link']");

        // Khởi tạo trang sản phẩm
        public InventoryPage(IWebDriver driver) : base(driver)
        {
        }

        // Hàm trả về bool thì PHẢI trả lời được cả "KHÔNG":
        // không vào được trang sản phẩm là một CÂU TRẢ LỜI (false), không phải sự cố.
        public bool DaHienThi()
        {
            // Timeout ngắn: ca nghịch nào cũng phải chờ hết khoảng này mới biết là "không"
            var choNgan = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(GiayChoNgan)
            );

            try
            {
                // FindElements (số nhiều) không ném lỗi khi không thấy phần tử
                return choNgan.Until(d =>
                    d.FindElements(tieuDe).Count > 0
                    && d.FindElement(tieuDe).Text == "Products"
                );
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
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

        // Lấy tên và giá của từng sản phẩm (chờ đủ số lượng mong đợi)
        public List<SanPham> LayDanhSachSanPham(int soLuongMongDoi)
        {
            var cacSanPham = wait.Until(d =>
            {
                var items = d.FindElements(danhSachSanPham);

                return items.Count == soLuongMongDoi ? items : null;
            });

            return cacSanPham
                .Select(item => new SanPham(
                    item.FindElement(tenSanPham).Text,
                    item.FindElement(giaTrongSanPham).Text))
                .ToList();
        }

        // Sắp xếp sản phẩm (az, za, lohi, hilo) và ở lại trang này
        public InventoryPage SapXep(string value)
        {
            var oChon = new SelectElement(Tim(oChonSapXep));
            oChon.SelectByValue(value);

            return this;
        }

        // Lấy danh sách giá theo thứ tự đang hiển thị
        public List<decimal> LayDanhSachGia()
        {
            return wait.Until(d =>
            {
                var cacGia = d.FindElements(danhSachGia);

                if (cacGia.Count == 0)
                    return null;

                return cacGia
                    .Select(e => decimal.Parse(
                        e.Text.Replace("$", ""),
                        CultureInfo.InvariantCulture))
                    .ToList();
            });
        }

        // Thêm Sauce Labs Backpack vào giỏ và ở lại trang này
        public InventoryPage ThemBaloVaoGio()
        {
            TimCoTheBam(nutThemBalo).Click();

            return this;
        }

        // Số lượng sản phẩm hiển thị trên biểu tượng giỏ hàng
        public string LaySoLuongTrongGio()
        {
            return wait.Until(d =>
            {
                var huyHieu = d.FindElements(huyHieuGioHang);

                return huyHieu.Count > 0 ? huyHieu[0].Text : null;
            });
        }

        // Mở giỏ hàng và chuyển sang trang giỏ hàng
        public CartPage MoGioHang()
        {
            Tim(lienKetGioHang).Click();

            return new CartPage(driver);
        }
    }
}
