
using FirstTests.Page;
using FirstTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FirstTests
{
    public class SauceDemoPageTests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        public void OpenSauceDemo_CheckPageTitle()
        {
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
