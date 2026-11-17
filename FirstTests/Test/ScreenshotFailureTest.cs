using NUnit.Framework;

namespace FirstTests.Test
{
    public class ScreenshotFailureTest : BaseTest
    {
        [Test]
        public void TestScreenshotWhenFailed()
        {
            driver.Navigate().GoToUrl(
                "https://www.saucedemo.com/"
            );

            Assert.Fail("Test that bai de kiem tra chup anh");
        }
    }
}