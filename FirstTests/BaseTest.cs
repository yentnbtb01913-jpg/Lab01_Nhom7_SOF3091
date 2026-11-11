
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace FirstTests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver = null!;
        protected WebDriverWait wait = null!;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
