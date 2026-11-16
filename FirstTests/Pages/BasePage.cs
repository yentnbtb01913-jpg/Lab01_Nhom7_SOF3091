using System;
using FirstTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        // Khởi tạo WebDriver và Explicit Wait dùng chung cho mọi Page
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;

            // Timeout lấy từ appsettings.json, không viết cứng số 10 như Bài 4
            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(ConfigReader.Load().TimeoutSeconds)
            );
        }

        // Tìm phần tử KÈM chờ: chỉ trả về khi phần tử đã hiển thị
        protected IWebElement Tim(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);

                return element.Displayed ? element : null;
            });
        }

        // Tìm phần tử KÈM chờ: phải hiển thị và bấm được (Enabled)
        protected IWebElement TimCoTheBam(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            });
        }

        // Mở một trang của site the-internet; địa chỉ gốc lấy từ appsettings.json
        protected void MoTrangTheInternet(string duongDan)
        {
            string diaChiGoc = ConfigReader.Load().TheInternetUrl.TrimEnd('/');

            driver.Navigate().GoToUrl(diaChiGoc + "/" + duongDan);
        }

        // Hành vi dùng chung: test không cần chạm vào driver
        public string LayUrlHienTai()
        {
            return driver.Url;
        }

        public string LayTieuDeTrinhDuyet()
        {
            return driver.Title;
        }
    }
}
