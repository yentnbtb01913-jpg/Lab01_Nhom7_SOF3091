using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FirstTests.Pages_Lab04;

namespace FirstTests.ASM1_TB01913;

[TestFixture]
public class ASM1_Y2_GioHangTests : ASM1_TestBase
{
    [Test]
    public void Y2_AddProductToCart_Backpack()
    {
        // Tai su dung thao tac dang nhap da duoc dong goi; lab cu giu nguyen.
        var inventory = new LoginPage(driver)
            .MoTrang()
            .DangNhap("standard_user", "secret_sauce");
        Assert.That(inventory.DaHienThi(), Is.True);

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var add = wait.Until(d => d.FindElement(By.Id("add-to-cart-sauce-labs-backpack")));
        add.Click();

        string badge = wait.Until(d =>
        {
            var list = d.FindElements(By.CssSelector("[data-test='shopping-cart-badge']"));
            return list.Count == 1 ? list[0].Text : null;
        })!;
        Assert.That(badge, Is.EqualTo("1"));

        driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']")).Click();
        wait.Until(d => d.Url.Contains("cart.html"));
        string productName = wait.Until(d =>
            d.FindElement(By.CssSelector("[data-test='inventory-item-name']")).Text);
        Assert.That(productName, Is.EqualTo("Sauce Labs Backpack"));
        TestContext.WriteLine("Gio hang: " + productName + " (so luong " + badge + ")");
    }
}
