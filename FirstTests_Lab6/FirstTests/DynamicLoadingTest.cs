
using NUnit.Framework;
using OpenQA.Selenium;
using FirstTests.Page;

namespace FirstTests
{
    public class DynamicLoadingTests : BaseTest
    {
        private DynamicLoadingPages dynamicPage = null!;

        [SetUp]
        public void SetupDynamicLoading()
        {
            dynamicPage = new DynamicLoadingPages(driver);
        }

        // BƯỚC 1: Không chờ -> cố ý rớt
        [Test]
        [Explicit("Bước 1: cố ý rớt để chụp ảnh minh chứng")]
        [Category("Slow")]
        public void A1_KhongWait_RotNoSuchElement()
        {
            dynamicPage.MoTrang();

            dynamicPage.BamStart();

            string content = dynamicPage.LayNoiDungNgay();

            Assert.That(
                content,
                Is.EqualTo("Hello World!")
            );
        }

        // BƯỚC 2: Có Explicit Wait -> PASS
        [Test]
        [Category("Slow")]
        public void A2_CoExplicitWait_Pass()
        {
            dynamicPage.MoTrang();

            dynamicPage.BamStart();

            string content =
                dynamicPage.ChoNoiDungHienThi();

            Assert.That(
                content,
                Is.EqualTo("Hello World!")
            );

            TestContext.WriteLine("Nội dung: " + content);

            TestContext.WriteLine(
                "Explicit Wait hoạt động thành công!"
            );
        }
    }
}
