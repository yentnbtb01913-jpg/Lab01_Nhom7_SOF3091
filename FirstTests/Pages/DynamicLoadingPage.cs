
using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class DynamicLoadingPage : BasePage
    {
        private const string Url =
            "https://the-internet.herokuapp.com/dynamic_loading/2";

        // Locator của trang Dynamic Loading
        private readonly By nutStart =
            By.CssSelector("#start button");

        private readonly By ketQua =
            By.Id("finish");

        public DynamicLoadingPage(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang Dynamic Loading
        public DynamicLoadingPage MoTrang()
        {
            driver.Navigate().GoToUrl(Url);
            return this;
        }

        // Nhấn nút Start
        public DynamicLoadingPage BamStart()
        {
            Tim(nutStart).Click();
            return this;
        }

        // Cố ý tìm phần tử ngay, không sử dụng Explicit Wait
        // Dùng để minh chứng lỗi NoSuchElementException
        public string LayNoiDungNgayLapTuc()
        {
            return driver.FindElement(ketQua).Text;
        }

        // Chờ phần tử xuất hiện và hiển thị rồi lấy nội dung
        public string LayNoiDungSauKhiCho()
        {
            IWebElement element = wait.Until(d =>
            {
                var result = d.FindElement(ketQua);

                return result.Displayed &&
                       result.Text == "Hello World!"
                    ? result
                    : null;
            });

            return element.Text;
        }
    }
}
