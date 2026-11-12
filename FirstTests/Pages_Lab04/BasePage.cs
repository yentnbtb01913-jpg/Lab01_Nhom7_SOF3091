using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages_Lab04;

// Lop cha cua moi Page Object trong LAB 4.
public abstract class BasePage
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    protected BasePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    // Chi cac Page Object ke thua moi dung Tim().
    protected IWebElement Tim(By locator)
    {
        return wait.Until(d => d.FindElement(locator));
    }
}
