
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class ExamplePages : BasePage
    {
        public ExamplePages(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang Example
        public void MoTrangExample()
        {
            driver.Navigate().GoToUrl("https://example.com");
        }

        // Lấy tiêu đề trang
        public string LayTieuDeTrang()
        {
            return driver.Title;
        }
    }
}
