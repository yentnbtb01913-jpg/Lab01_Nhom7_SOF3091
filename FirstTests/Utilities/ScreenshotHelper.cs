using OpenQA.Selenium;

namespace FirstTests.Utilities;

public static class ScreenshotHelper
{
    public static void Chup(IWebDriver? driver)
    {
        if (driver is null) return;

        try
        {
            // bin/Debug/netX.0 -> lên 3 cấp là thư mục project, nơi có screenshots/
            string thuMuc = Path.GetFullPath(Path.Combine(
                TestContext.CurrentContext.TestDirectory, "..", "..", "..", "screenshots"));
            Directory.CreateDirectory(thuMuc);

            // Tên ca test có thể chứa ký tự cấm trong tên file (" < > ...) nên phải lọc
            string tenAnh = string.Join("_",
                TestContext.CurrentContext.Test.Name.Split(Path.GetInvalidFileNameChars()));
            string duongDan = Path.Combine(thuMuc, $"{tenAnh}_{DateTime.Now:HHmmss}.png");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(duongDan);
            TestContext.AddTestAttachment(duongDan, "Ảnh chụp khi test thất bại");
        }
        catch (Exception ex)
        {
            // Cơ chế hỗ trợ không được tự gây lỗi (ví dụ đang mở alert thì không chụp được)
            TestContext.Progress.WriteLine($"Không chụp được ảnh: {ex.Message}");
        }
    }
}
