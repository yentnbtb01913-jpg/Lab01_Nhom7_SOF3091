using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace FirstTests
{
    public class DynamicLoadingTests : BaseTest
    {
        private const string Url = "https://the-internet.herokuapp.com/dynamic_loading/2";

        // BƯỚC 1: KHÔNG chờ -> cố ý RỚT với NoSuchElementException
        [Test, Explicit("Bước 1: cố ý rớt để chụp ảnh minh chứng Phần A")]
        public void A1_KhongWait_RotNoSuchElement()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(By.CssSelector("#start button")).Click();

            // Tìm NGAY sau khi bấm Start -> #finish chưa được JS tạo ra
            IWebElement finish = driver.FindElement(By.Id("finish"));
            Assert.That(finish.Text, Is.EqualTo("Hello World!"));
        }

        // BƯỚC 2: thêm Explicit Wait -> PASS
        [Test]
        public void A2_CoExplicitWait_Pass()
        {
            driver.Navigate().GoToUrl(Url);
            driver.FindElement(By.CssSelector("#start button")).Click();

            IWebElement finish = wait.Until(d => d.FindElement(By.Id("finish")));
            Assert.That(finish.Text, Is.EqualTo("Hello World!"));
        }
    }
}
