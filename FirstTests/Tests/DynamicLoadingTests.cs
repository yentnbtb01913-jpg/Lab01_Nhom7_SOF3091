using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    public class DynamicLoadingTests : BaseTest
    {
        private const string Url = "https://the-internet.herokuapp.com/dynamic_loading/2";
        private const string ExpectedText = "Hello World!";

        private static readonly By StartButton = By.CssSelector("#start button");
        private static readonly By FinishText = By.Id("finish");

        // BƯỚC 1: KHÔNG chờ -> cố ý RỚT với NoSuchElementException
        [Test, Explicit("Bước 1: cố ý rớt để chụp ảnh minh chứng Phần A")]
        public void A1_KhongWait_RotNoSuchElement()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(StartButton).Click();

            // Tìm NGAY sau khi bấm Start -> #finish chưa được JS tạo ra
            IWebElement finish = driver.FindElement(FinishText);
            Assert.That(finish.Text, Is.EqualTo(ExpectedText));
        }

        // BƯỚC 2: thêm Explicit Wait -> PASS
        [Test]
        public void A2_CoExplicitWait_Pass()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(StartButton).Click();

            // Chờ đến khi phần tử tồn tại VÀ hiển thị (WebDriverWait mặc định bỏ qua NoSuchElementException)
            IWebElement finish = wait.Until(d =>
            {
                var element = d.FindElement(FinishText);
                return element.Displayed ? element : null;
            });

            Assert.That(finish.Text, Is.EqualTo(ExpectedText));
        }
    }
}