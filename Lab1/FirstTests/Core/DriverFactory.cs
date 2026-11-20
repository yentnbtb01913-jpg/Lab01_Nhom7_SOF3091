using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using FirstTests.Config;

namespace FirstTests.Core;

public static class DriverFactory
{
    // Tra ve IWebDriver de test khong phu thuoc ChromeDriver hay FirefoxDriver.
    public static IWebDriver Create(TestConfig cfg)
    {
        if (cfg is null) throw new ArgumentNullException(nameof(cfg));
        ConfigReader.Validate(cfg);
        IWebDriver browser;
        switch (cfg.Browser.Trim().ToLowerInvariant())
        {
            case "chrome":
                var chrome = new ChromeOptions();
                chrome.AddArgument("--incognito");
                chrome.AddArgument("--disable-notifications");
                chrome.AddUserProfilePreference("credentials_enable_service", false);
                chrome.AddUserProfilePreference("profile.password_manager_enabled", false);
                if (cfg.Headless)
                {
                    chrome.AddArgument("--headless=new");
                    chrome.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
                }
                browser = new ChromeDriver(chrome);
                break;
            case "firefox":
                var firefox = new FirefoxOptions();
                if (cfg.Headless)
                {
                    firefox.AddArgument("--headless");
                    firefox.AddArgument($"--width={cfg.WindowWidth}");
                    firefox.AddArgument($"--height={cfg.WindowHeight}");
                }
                browser = new FirefoxDriver(firefox);
                break;
            default:
                throw new NotSupportedException(
                    $"Trinh duyet '{cfg.Browser}' khong duoc ho tro. Chi ho tro Chrome hoac Firefox.");
        }
        try
        {
            browser.Manage().Window.Size = new Size(cfg.WindowWidth, cfg.WindowHeight);
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            return browser;
        }
        catch
        {
            try { browser.Quit(); } finally { browser.Dispose(); }
            throw;
        }
    }
}
