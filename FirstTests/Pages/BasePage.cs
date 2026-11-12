
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        // Nhận WebDriver từ BaseTest
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;

            // Explicit Wait tối đa 10 giây
            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );
        }

        // Tìm phần tử và chờ đến khi phần tử hiển thị
        protected IWebElement Tim(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);

                return element.Displayed
                    ? element
                    : null;
            });
        }
    }
}
