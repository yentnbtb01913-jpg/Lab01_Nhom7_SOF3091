using OpenQA.Selenium;

namespace FirstTests;

// Giu ten lop cu de SortTests khong phai doi namespace.
public abstract class BaseTest : FirstTests.Core.BaseTest
{
    // Ham ho tro duoc SortTests va Lab 3 tai su dung.
    protected void LoginSauceDemo(string user = "standard_user", string pass = "secret_sauce")
    {
        driver.Navigate().GoToUrl(config.Site("SauceDemo"));
        driver.FindElement(By.Id("user-name")).SendKeys(user);
        driver.FindElement(By.Id("password")).SendKeys(pass);
        driver.FindElement(By.Id("login-button")).Click();
        wait.Until(d => d.FindElements(By.ClassName("inventory_list")).Count > 0);
    }
}
