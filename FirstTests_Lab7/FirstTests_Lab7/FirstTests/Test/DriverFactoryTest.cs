using NUnit.Framework;
using OpenQA.Selenium;
using FirstTests.Config;
using FirstTests.Core;

namespace FirstTests.Test
{
    public class DriverFactoryTest
    {
        [Test]
        public void TestDriverFactory()
        {
            var config = ConfigReader.Load();

            IWebDriver driver = DriverFactory.Create(config);

            try
            {
                driver.Navigate().GoToUrl(config.BaseUrl);

                Assert.That(
                    driver.Url,
                    Does.Contain("saucedemo.com")
                );

                TestContext.Out.WriteLine(
                    "Browser: " + config.Browser
                );

                TestContext.Out.WriteLine(
                    "URL: " + driver.Url
                );
            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}