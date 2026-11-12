using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests.Tests;

public class SauceDemoPageTests
{
    [Test]
    public void OpenSauceDemo_CheckPageTitle()
    {
        using IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        string title = driver.Title;
        TestContext.WriteLine("Page title: " + title);
        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}