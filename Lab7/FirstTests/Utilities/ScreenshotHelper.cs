using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using FirstTests.Reporting;

namespace FirstTests.Utilities;

public static class ScreenshotHelper
{
    private sealed class Rule
    {
        public string Match { get; set; } = "";
        public string FileName { get; set; } = "";
    }

    // Anh PNG chup trang THAT tu WebDriver, khong chup mo phong va khong ep Passed.
    public static string? Capture(IWebDriver driver)
    {
        if (driver is not ITakesScreenshot taker) return null;
        var context = TestContext.CurrentContext;
        string displayName = context.Test.Name;
        string fullName = context.Test.FullName;
        string prefix = "TC_" + Safe(displayName);
        string rulesPath = Path.Combine(AppContext.BaseDirectory, "EvidenceNames.json");
        if (File.Exists(rulesPath))
        {
            Rule[] rules = JsonSerializer.Deserialize<Rule[]>(File.ReadAllText(rulesPath))
                ?? Array.Empty<Rule>();
            Rule? rule = rules.FirstOrDefault(x =>
                fullName.Contains(x.Match, StringComparison.OrdinalIgnoreCase) ||
                displayName.Contains(x.Match, StringComparison.OrdinalIgnoreCase));
            if (rule != null)
                prefix = rule.FileName.Replace("<Browser>",
                    driver.GetType().Name.Replace("Driver", "", StringComparison.OrdinalIgnoreCase));
        }

        // DataDriven: moi bo du lieu phai co ten anh khac nhau.
        if (displayName.StartsWith("DangNhap_", StringComparison.OrdinalIgnoreCase))
            prefix += "_" + Safe(displayName);
        prefix += "_" + Safe(context.Test.ClassName ?? "Test");

        string dir = ProjectPaths.EnsureOutputDirectory("screenshots"); // KHONG tao folder PhanA, PhanB...
        string outcome = context.Result.Outcome.Status.ToString();
        string stem = Safe(prefix + "_" + outcome + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"));
        if (stem.Length > 145) stem = stem[..145];
        string file = Path.Combine(dir, stem + "_" + Guid.NewGuid().ToString("N")[..6] + ".png");
        taker.GetScreenshot().SaveAsFile(file);
        TestContext.AddTestAttachment(file, $"Anh trang web {outcome}: {displayName}");
        TestContext.WriteLine("[SCREENSHOT] " + file);
        return file;
    }

    private static string Safe(string input) =>
        Regex.Replace(input, @"[^a-zA-Z0-9_-]", "_").Trim('_');
}
