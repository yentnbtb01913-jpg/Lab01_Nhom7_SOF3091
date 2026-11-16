using OpenQA.Selenium;

namespace FirstTests.Pages
{
    public class IframePage : BasePage
    {
        private const string TenFrame = "mce_0_ifr";

        private readonly By vungSoanThao = By.Id("tinymce");
        private readonly By tieuDeTrang = By.TagName("h3");

        public IframePage(IWebDriver driver) : base(driver)
        {
        }

        public IframePage MoTrang()
        {
            MoTrangTheInternet("iframe");
            return this;
        }

        // Vào frame, xóa chữ mặc định rồi gõ nội dung mới (ở lại trong frame)
        public IframePage GoVanBan(string noiDung)
        {
            wait.Until(d => d.SwitchTo().Frame(TenFrame));   // VÀO frame

            IWebElement vung = Tim(vungSoanThao);

            vung.Click();
            vung.SendKeys(Keys.Control + "a");   // chọn hết chữ mặc định
            vung.SendKeys(Keys.Delete);          // xóa
            vung.SendKeys(noiDung);

            return this;
        }

        // Chỉ dùng khi đang ở trong frame
        public string LayNoiDungTrinhSoanThao()
        {
            return Tim(vungSoanThao).Text;
        }

        // RA khỏi frame (như đóng ngoặc)
        public IframePage RaKhoiFrame()
        {
            driver.SwitchTo().DefaultContent();
            return this;
        }

        // Chỉ dùng khi đã ở ngoài frame
        public string LayTieuDeTrang()
        {
            return Tim(tieuDeTrang).Text;
        }
    }
}
