using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace FirstTests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;   // khai báo ở cấp class

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();            // KHÔNG đặt ImplicitWait
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Phần D: CHỈ chụp ảnh khi test Failed
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    string folder = Path.GetFullPath(Path.Combine(
                        TestContext.CurrentContext.TestDirectory, "..", "..", "..", "screenshots"));
                    Directory.CreateDirectory(folder);

                    // đặt tên file theo tên ca test
                    string path = Path.Combine(folder,
                        TestContext.CurrentContext.Test.Name + ".png");

                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path, "Ảnh chụp khi test thất bại");
                }
            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        // Đăng nhập saucedemo (dùng cho Phần B)
        protected void LoginSauceDemo(string user = "standard_user", string pass = "secret_sauce")
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.FindElement(By.Id("user-name")).SendKeys(user);
            driver.FindElement(By.Id("password")).SendKeys(pass);
            driver.FindElement(By.Id("login-button")).Click();
            wait.Until(d => d.FindElement(By.ClassName("inventory_list")));
        }
    }
}
