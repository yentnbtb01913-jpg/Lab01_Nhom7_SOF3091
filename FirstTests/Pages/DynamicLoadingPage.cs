using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class DynamicLoadingPage : BasePage
    {
        private readonly By nutStart = By.CssSelector("#start button");
        private readonly By ketQua = By.Id("finish");

        public DynamicLoadingPage(IWebDriver driver) : base(driver)
        {
        }

        public DynamicLoadingPage MoTrang()
        {
            MoTrangTheInternet("dynamic_loading/2");
            return this;
        }

        public DynamicLoadingPage BamStart()
        {
            Tim(nutStart).Click();
            return this;
        }

        // CỐ Ý KHÔNG chờ: dùng cho ca A1 để chứng minh lỗi NoSuchElementException
        public string LayKetQuaKhongCho()
        {
            return driver.FindElement(ketQua).Text;
        }

        // Có Explicit Wait: chờ kết quả hiển thị và có đúng nội dung
        public string ChoVaLayKetQua()
        {
            IWebElement phanTu = wait.Until(d =>
            {
                var element = d.FindElement(ketQua);

                return element.Displayed &&
                       element.Text == "Hello World!"
                    ? element
                    : null;
            });

            return phanTu.Text;
        }
    }
}
