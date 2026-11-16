using FirstTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
namespace FirstTests.Core;

public static class DriverFactory
{
    // Trả về IWebDriver (KHÔNG phải ChromeDriver): đây là chỗ tính đa hình phát huy tác dụng.
    public static IWebDriver Create(TestConfig cfg)
    {
        string ten = cfg.Browser.Trim().ToLowerInvariant();

        return ten switch
        {
            "chrome" => new ChromeDriver(TaoOptionsChrome(cfg)),
            "firefox" => new FirefoxDriver(TaoOptionsFirefox(cfg)),
            "edge" => new EdgeDriver(TaoOptionsEdge(cfg)),
            _ => throw new ArgumentException(
                     $"Trình duyệt không được hỗ trợ: '{cfg.Browser}'. Hợp lệ: chrome | firefox | edge.")
        };
    }

    private static ChromeOptions TaoOptionsChrome(TestConfig cfg)
    {
        var options = new ChromeOptions();
        if (cfg.Headless) options.AddArgument("--headless=new");   // Chrome: có "=new"
        options.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        options.AddArgument("--disable-notifications");
        return options;
    }

    private static FirefoxOptions TaoOptionsFirefox(TestConfig cfg)
    {
        var options = new FirefoxOptions();
        if (cfg.Headless) options.AddArgument("--headless");       // Firefox: KHÔNG có "=new"
        options.AddArgument($"--width={cfg.WindowWidth}");
        options.AddArgument($"--height={cfg.WindowHeight}");
        return options;
    }

    private static EdgeOptions TaoOptionsEdge(TestConfig cfg)
    {
        var options = new EdgeOptions();
        if (cfg.Headless) options.AddArgument("--headless=new");   // Edge giống Chrome
        options.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        return options;
    }
}
