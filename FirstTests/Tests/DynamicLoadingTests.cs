using FirstTests.Core;
using FirstTests.Pages;
using NUnit.Framework;

namespace FirstTests.Tests
{
    [Category("Wait")]
    [Category("Slow")]
    public class DynamicLoadingTests : BaseTest
    {
        // BƯỚC 1: KHÔNG chờ -> cố ý RỚT với NoSuchElementException
        [Test, Explicit("Bước 1: cố ý rớt để chụp ảnh minh chứng Phần A")]
        public void A1_KhongWait_RotNoSuchElement()
        {
            DynamicLoadingPage trang = new DynamicLoadingPage(driver)
                .MoTrang()
                .BamStart();

            // Lấy kết quả NGAY sau khi bấm Start -> #finish chưa được JS tạo ra
            string noiDung = trang.LayKetQuaKhongCho();

            Assert.That(noiDung, Is.EqualTo("Hello World!"));
        }

        // BƯỚC 2: Có Explicit Wait -> PASS
        [Test]
        public void A2_CoExplicitWait_Pass()
        {
            DynamicLoadingPage trang = new DynamicLoadingPage(driver)
                .MoTrang()
                .BamStart();

            // Chờ kết quả xuất hiện, hiển thị và có nội dung chính xác
            string noiDung = trang.ChoVaLayKetQua();

            // Kiểm tra nội dung
            Assert.That(noiDung, Is.EqualTo("Hello World!"));

            TestContext.WriteLine("Nội dung: " + noiDung);
            TestContext.WriteLine("Explicit Wait hoạt động thành công!");
        }
    }
}
