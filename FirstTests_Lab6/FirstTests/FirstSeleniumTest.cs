using NUnit.Framework;
using FirstTests.Page;

namespace FirstTests
{
    [TestFixture]
    public class FirstSeleniumTest : BaseTest
    {
        private ExamplePages examplePage = null!;

        [SetUp]
        public void SetupPages()
        {
            examplePage = new ExamplePages(driver);
        }

        [Test]
        [Category("Smoke")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            examplePage.MoTrangExample();

            string title = examplePage.LayTieuDeTrang();

            TestContext.Out.WriteLine("Tiêu đề trang: " + title);
            Assert.That(title, Is.EqualTo("Example Domain"));
        }
    }
}
