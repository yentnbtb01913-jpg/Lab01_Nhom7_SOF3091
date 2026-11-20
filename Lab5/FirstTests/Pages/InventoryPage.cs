using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages;

public sealed class InventoryPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    private readonly By tieuDe = By.ClassName("title");
    private readonly By danhSach = By.ClassName("inventory_list");

    public InventoryPage(IWebDriver webDriver, double timeoutSeconds)
    {
        driver = webDriver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
    }

    // Test nghich phai nhan false, khong phat sinh TimeoutException.
    public bool DaHienThi()
    {
        try
        {
            // Cho den khi xuat hien trang san pham hoac thong bao loi.
            // Nhu vay ca login thanh cong va that bai deu co ket qua on dinh.
            wait.Until(d =>
                d.Url.Contains("inventory.html", StringComparison.OrdinalIgnoreCase) ||
                d.FindElements(By.CssSelector("[data-test='error']")).Any(x => x.Displayed));
            if (!driver.Url.Contains("inventory.html", StringComparison.OrdinalIgnoreCase)) return false;
            return wait.Until(d =>
                d.FindElements(tieuDe).Any(x => x.Displayed) &&
                d.FindElements(danhSach).Any(x => x.Displayed));
        }
        catch (WebDriverTimeoutException) { return false; }
        catch (NoSuchElementException) { return false; }
    }
}
