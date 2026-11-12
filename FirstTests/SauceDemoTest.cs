
using FirstTests.Page;
using FirstTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests
{
    public class SauceDemoPageTests
    {
        [Test]
        public void OpenSauceDemo_CheckPageTitle()
        {
            using IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(
                "https://www.saucedemo.com/"
            );

            SauceDemoPage page = new SauceDemoPage(driver);

            string title = page.LayTieuDeTrang();

            TestContext.WriteLine("Page title: " + title);

            Assert.That(title, Is.EqualTo("Swag Labs"));
        }
    }
}
