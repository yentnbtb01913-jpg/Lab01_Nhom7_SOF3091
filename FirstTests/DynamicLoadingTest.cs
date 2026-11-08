
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests;

[TestFixture]
public class DynamicLoadingTests
{
    private IWebDriver driver = null!;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    // PHAN A - BUOC 1: TEST KHONG CO WAIT
    [Test]
    public void TestWithoutWait()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/dynamic_loading/2");

        driver.FindElement(By.TagName("button")).Click();

        // Tim phan tu ngay lap tuc
        IWebElement finish = driver.FindElement(By.Id("finish"));

        Assert.That(finish.Text, Is.EqualTo("Hello World!"));
    }

    // PHAN A - BUOC 2: TEST CO EXPLICIT WAIT
    [Test]
    public void TestWithExplicitWait()
    {
        driver.Navigate().GoToUrl(
            "https://the-internet.herokuapp.com/dynamic_loading/2");

        driver.FindElement(By.TagName("button")).Click();

        // Cho toi da 10 giay
        WebDriverWait wait = new WebDriverWait(
            driver, TimeSpan.FromSeconds(10));

        IWebElement finish = wait.Until(d =>
        {
            var elements = d.FindElements(By.Id("finish"));

            if (elements.Count > 0 && elements[0].Displayed)
            {
                return elements[0];
            }

            return null;
        })!;

        // Kiem tra noi dung
        Assert.That(finish.Text, Is.EqualTo("Hello World!"));
    }

    // DONG TRINH DUYET SAU MOI TEST
    [TearDown]
    public void Cleanup()
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
