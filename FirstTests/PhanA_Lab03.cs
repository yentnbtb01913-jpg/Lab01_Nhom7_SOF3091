using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    public class PhanA_Lab03
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;
        private const string Url = "https://the-internet.herokuapp.com/dynamic_loading/2";

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);

            // Chỉ dùng Explicit Wait, KHÔNG dùng Implicit Wait
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        // Bước 1: KHÔNG wait -> test RỚT với NoSuchElementException
        // [Explicit] = phải chuột phải vào test này và chọn Run thì mới chạy
        [Test, Explicit("Chạy riêng để chụp ảnh test RỚT")]
        public void A1_RotKhongWait()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(By.CssSelector("#start button")).Click();

            // Tìm NGAY -> #finish chưa được JavaScript tạo ra -> NoSuchElementException
            IWebElement finish = driver.FindElement(By.Id("finish"));

            Assert.That(finish.Text, Is.EqualTo("Hello World!"));
        }

        // Bước 2: có Explicit Wait -> test PASS
        [Test]
        public void A2_PassCoWait()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(By.CssSelector("#start button")).Click();

            // Chờ tối đa 10 giây tới khi #finish xuất hiện
            IWebElement finish = wait.Until(d => d.FindElement(By.Id("finish")));

            Assert.That(finish.Text, Is.EqualTo("Hello World!"));
        }
    }
}