
using OpenQA.Selenium;
using System;
using System.IO;

namespace FirstTests.Utilities
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(
            IWebDriver driver,
            string testName)
        {
            try
            {
                // Tạo thư mục screenshots
                string folder = Path.Combine(
                    AppContext.BaseDirectory,
                    "screenshots"
                );

                Directory.CreateDirectory(folder);

                // Làm sạch tên file
                string safeName = string.Join(
                    "_",
                    testName.Split(Path.GetInvalidFileNameChars())
                );

                string fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss_fff}_{Environment.CurrentManagedThreadId}.png";

                string filePath = Path.Combine(folder, fileName);

                // Chụp ảnh
                ITakesScreenshot screenshotDriver =
                    (ITakesScreenshot)driver;

                Screenshot screenshot =
                    screenshotDriver.GetScreenshot();

                screenshot.SaveAsFile(filePath);

                TestContext.WriteLine(
                    $"Screenshot saved: {filePath}"
                );
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(
                    $"Không thể chụp ảnh: {ex.Message}"
                );
            }
        }
    }
}

