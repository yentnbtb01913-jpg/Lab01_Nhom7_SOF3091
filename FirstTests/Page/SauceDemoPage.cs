
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class SauceDemoPage : BasePage
    {
        public SauceDemoPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string LayTieuDeTrang()
        {
            return driver.Title;
        }
    }
}
