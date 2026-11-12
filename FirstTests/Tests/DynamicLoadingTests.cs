
using NUnit.Framework;
using FirstTests.Pages;

namespace FirstTests.Tests
{
    public class DynamicLoadingTests : BaseTest
    {
        // BƯỚC 1: Không chờ - cố ý thất bại
        [Test, Explicit("Cố ý thất bại để minh chứng NoSuchElementException")]
        public void A1_KhongWait_RotNoSuchElement()
        {
            DynamicLoadingPage page = new DynamicLoadingPage(driver);

            page.MoTrang()
                .BamStart();

            // Tìm ngay khi JavaScript chưa tạo phần tử
            string result = page.LayNoiDungNgayLapTuc();

            Assert.That(result, Is.EqualTo("Hello World!"));
        }

        // BƯỚC 2: Có Explicit Wait - thành công
        [Test]
        public void A2_CoExplicitWait_Pass()
        {
            DynamicLoadingPage page = new DynamicLoadingPage(driver);

            page.MoTrang()
                .BamStart();

            // Chờ phần tử xuất hiện và lấy nội dung
            string result = page.LayNoiDungSauKhiCho();

            Assert.That(
                result,
                Is.EqualTo("Hello World!"),
                "Nội dung sau khi tải không chính xác."
            );

            TestContext.Out.WriteLine("Nội dung: " + result);
            TestContext.Out.WriteLine(
                "Explicit Wait hoạt động thành công!"
            );
        }
    }
}
