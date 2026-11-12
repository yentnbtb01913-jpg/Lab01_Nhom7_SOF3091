using NUnit.Framework;

namespace FirstTests.Tests
{
    public class ScreenshotDemoTests : BaseTest
    {
        [Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
        public void D1_CoY_LamRot_DeChupAnh()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            Assert.That(driver.Title, Is.EqualTo("Tieu de sai co y"));   // cố ý sai
        }
    }
}