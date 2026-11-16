using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FirstTests.Utilities;

public static class ScreenshotHelper
{
    // Luu anh khi Failed, tao ten rieng de khong ghi de anh cu.
    public static string? Capture(IWebDriver driver)
    {
        if (driver is not ITakesScreenshot screenshot) return null;
        string folder = Path.Combine(TestContext.CurrentContext.WorkDirectory, "screenshots", "Lab05");
        Directory.CreateDirectory(folder);
        string name = Regex.Replace(TestContext.CurrentContext.Test.Name, @"[^a-zA-Z0-9_-]", "_");
        string path = Path.Combine(folder, $"{name}_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png");
        screenshot.GetScreenshot().SaveAsFile(path);
        TestContext.AddTestAttachment(path, "Anh chup test Failed");
        TestContext.WriteLine("Da luu screenshot: " + path);
        return path;
    }
}
