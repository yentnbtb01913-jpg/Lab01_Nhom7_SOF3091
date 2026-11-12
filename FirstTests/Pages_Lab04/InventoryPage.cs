using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages_Lab04;

public class InventoryPage : BasePage
{
    private readonly By tieuDe = By.ClassName("title");
    private readonly By danhSach = By.ClassName("inventory_list");
    private readonly By khungSanPham = By.CssSelector("[data-test='inventory-item']");
    private readonly By tenSanPham = By.CssSelector("[data-test='inventory-item-name']");
    private readonly By giaSanPham = By.CssSelector("[data-test='inventory-item-price']");
    private readonly By boLocSapXep = By.ClassName("product_sort_container");

    public InventoryPage(IWebDriver driver) : base(driver) { }

    public bool DaHienThi()
    {
        return driver.Url.Contains("inventory.html") && Tim(tieuDe).Displayed;
    }

    public string LayDuongDan()
    {
        return driver.Url;
    }

    public InventoryPage ChoDuongDanKetThuc(string suffix)
    {
        wait.Until(d => d.Url.EndsWith(suffix, StringComparison.Ordinal));
        return this;
    }

    public InventoryPage ChoDanhSachSanPham()
    {
        Tim(danhSach);
        wait.Until(d => d.FindElements(khungSanPham).Count > 0);
        return this;
    }

    // Chaining: hanh dong tren cung trang tra ve this.
    public InventoryPage SapXep(string value)
    {
        SelectElement boLoc = new SelectElement(Tim(boLocSapXep));
        boLoc.SelectByValue(value);
        return this;
    }

    // Tat ca locator nam trong Page, file test chi nhan du lieu.
    public List<(string Ten, string Gia)> LayDanhSachSanPham()
    {
        ChoDanhSachSanPham();
        var ketQua = new List<(string Ten, string Gia)>();
        foreach (IWebElement item in driver.FindElements(khungSanPham))
        {
            string ten = item.FindElement(tenSanPham).Text.Trim();
            string gia = item.FindElement(giaSanPham).Text.Trim();
            ketQua.Add((ten, gia));
        }
        return ketQua;
    }

    public List<decimal> LayDanhSachGia()
    {
        return LayDanhSachSanPham()
            .Select(x => decimal.Parse(x.Gia.Replace("$", ""), CultureInfo.InvariantCulture))
            .ToList();
    }
}
