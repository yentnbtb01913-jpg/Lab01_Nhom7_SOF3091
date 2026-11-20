
using OpenQA.Selenium;

namespace FirstTests.Page
{
    public class DynamicLoadingPages : BasePage
    {
        private readonly By btnStart =
            By.CssSelector("#start button");

        private readonly By finish =
            By.Id("finish");

        public DynamicLoadingPages(IWebDriver driver) : base(driver)
        {
        }

        // Mở trang Dynamic Loading
        public void MoTrang()
        {
            driver.Navigate().GoToUrl(
                "https://the-internet.herokuapp.com/dynamic_loading/2"
            );
        }

        // Nhấn nút Start
        public void BamStart()
        {
            Tim(btnStart).Click();
        }

        // Tìm nội dung ngay, không sử dụng Explicit Wait
        public string LayNoiDungNgay()
        {
            return driver.FindElement(finish).Text;
        }

        // Chờ phần tử hiển thị và có nội dung chính xác
        public string ChoNoiDungHienThi()
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(finish);

                return element.Displayed &&
                       element.Text == "Hello World!"
                    ? element.Text
                    : null;
            });
        }
    }
}
