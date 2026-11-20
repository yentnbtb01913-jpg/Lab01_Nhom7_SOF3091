using NUnit.Framework;

namespace FirstTests.Test
{
    public class ScreenshotFailureTest : BaseTest
    {
        [Test]
        [Explicit("Chỉ chạy riêng để minh chứng log ERROR")]
        [Category("LoggingDemo")]
        public void TestScreenshotWhenFailed()
        {
            driver.Navigate().GoToUrl(
                "https://www.saucedemo.com/"
            );

            Assert.Fail("Test that bai de kiem tra chup anh");
        }
    }
}