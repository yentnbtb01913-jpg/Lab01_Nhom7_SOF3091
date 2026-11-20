
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Page
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement Tim(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);

                if (element.Displayed && element.Enabled)
                {
                    return element;
                }

                return null;
            });
        }
    }
}
