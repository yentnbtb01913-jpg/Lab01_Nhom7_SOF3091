using NUnit.Framework;
using OpenQA.Selenium;

namespace FirstTests;

// Bo sung duy nhat phan chua co kiem chung tu dong: Phan A - Locator.
// Selenium xac minh selector khop 1 phan tu; anh DevTools Ctrl+F van can chup rieng.
[TestFixture]
public class Lab2_LocatorTests : FirstTests.Core.BaseTest
{
    [Test] public void A01_LocatorUsername() => KiemTra(By.Id("user-name"));
    [Test] public void A02_LocatorPassword() => KiemTra(By.Id("password"));
    [Test] public void A03_LocatorLoginButton() => KiemTra(By.Id("login-button"));

    [Test] public void A04_LocatorProductName()
    {
        DangNhap();
        // Chi gioi han mot san pham cu the, khong dung locator tim nhieu san pham.
        KiemTra(By.CssSelector("#item_4_title_link [data-test='inventory-item-name']"));
    }

    [Test] public void A05_LocatorProductPrice()
    {
        DangNhap();
        // Cung khu vuc mot san pham cu the, giup khop duy nhat.
        KiemTra(By.CssSelector("div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']"));
    }

    private void DangNhap()
    {
        driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
        driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
        driver.FindElement(By.Id("login-button")).Click();
        wait.Until(d => d.FindElements(By.CssSelector("[data-test='inventory-item']")).Count == 6);
    }

    private void KiemTra(By locator)
    {
        // Cho DOM cap nhat neu phan tu xuat hien bat dong bo.
        wait.Until(d => d.FindElements(locator).Count > 0);
        var result = driver.FindElements(locator);
        TestContext.WriteLine($"Locator={locator}; Match={result.Count}");
        Assert.That(result.Count, Is.EqualTo(1), "Locator phai khop chinh xac 1 phan tu.");
        Assert.That(result[0].Displayed, Is.True);
    }
}
