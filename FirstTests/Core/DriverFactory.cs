using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using FirstTests.Config;

namespace FirstTests.Core
{
    public static class DriverFactory
    {
        public static IWebDriver Create(TestConfig cfg)
        {
            IWebDriver driver;

            string browser = cfg.Browser.Trim().ToLower();

            switch (browser)
            {
                case "chrome":

                    ChromeOptions chromeOptions = new ChromeOptions();

                    if (cfg.Headless)
                    {
                        chromeOptions.AddArgument("--headless=new");
                    }

                    chromeOptions.AddArgument(
                        $"--window-size={cfg.WindowWidth},{cfg.WindowHeight}"
                    );

                    driver = new ChromeDriver(chromeOptions);

                    break;

                case "firefox":

                    FirefoxOptions firefoxOptions = new FirefoxOptions();

                    if (cfg.Headless)
                    {
                        firefoxOptions.AddArgument("--headless");
                    }

                    firefoxOptions.AddArgument(
                        $"--width={cfg.WindowWidth}"
                    );

                    firefoxOptions.AddArgument(
                        $"--height={cfg.WindowHeight}"
                    );

                    driver = new FirefoxDriver(firefoxOptions);

                    break;

                default:

                    throw new ArgumentException(
                        $"Trình duyệt '{cfg.Browser}' không được hỗ trợ!"
                    );
            }

            driver.Manage().Window.Size = new System.Drawing.Size(
                cfg.WindowWidth,
                cfg.WindowHeight
            );

            return driver;
        }
    }
}