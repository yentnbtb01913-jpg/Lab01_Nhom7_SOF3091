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

        // BƯỚC 2: Có Explicit Wait -> PASS
        [Test]
        public void A2_CoExplicitWait_Pass()
        {
            driver.Navigate().GoToUrl(Url);

            // Nhấn nút Start
            driver.FindElement(By.CssSelector("#start button")).Click();

            // Chờ phần tử xuất hiện, hiển thị và có nội dung chính xác
            IWebElement finish = wait.Until(d =>
            {
                var element = d.FindElement(By.Id("finish"));

                return element.Displayed &&
                       element.Text == "Hello World!"
                    ? element
                    : null;
            });

            // Kiểm tra nội dung
            Assert.That(finish.Text, Is.EqualTo("Hello World!"));

            TestContext.WriteLine("Nội dung: " + finish.Text);
            TestContext.WriteLine("Explicit Wait hoạt động thành công!");
        }
    }
}
