using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace FirstTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        // Khởi tạo WebDriver và Explicit Wait
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10)
            );
        }

        // Tìm phần tử và chờ phần tử xuất hiện
        protected IWebElement Tim(By by)
        {
            return wait.Until(d => d.FindElement(by));
        }
    }
}
