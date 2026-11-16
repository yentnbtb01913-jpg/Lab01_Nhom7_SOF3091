using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    public class PhanC_Lab03 : FirstTests.Core.BaseTest
    {

        private static string AlertUrl => FirstTests.Config.ConfigReader.Load().Site("JavaScriptAlerts");
        private static string IframeUrl => FirstTests.Config.ConfigReader.Load().Site("Iframe");



        [Test]
        public void C1_Alert_Accept()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsAlert()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert")); // Assert TRƯỚC khi Accept
            alert.Accept();

            Assert.That(driver.FindElement(By.Id("result")).Text,
                Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void C2_Confirm_Dismiss()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsConfirm()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"));
            alert.Dismiss();

            Assert.That(driver.FindElement(By.Id("result")).Text,
                Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void C3_Prompt_SendKeys_Accept()
        {
            string noiDung = "Yen test Selenium";

            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.CssSelector("button[onclick='jsPrompt()']")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"));
            alert.SendKeys(noiDung);
            alert.Accept();

            Assert.That(driver.FindElement(By.Id("result")).Text,
                Is.EqualTo("You entered: " + noiDung));
        }

        [Test]
        public void C4_Iframe_GoChu()
        {
            string noiDung = "Xin chao iframe tu Selenium";

            driver.Navigate().GoToUrl(IframeUrl);

            try
            {
                // Vào iframe (Until chờ tới khi frame sẵn sàng)
                wait.Until(d => d.SwitchTo().Frame("mce_0_ifr"));

                IWebElement body = driver.FindElement(By.Id("tinymce"));

                // Clear() không xóa sạch nội dung mặc định của TinyMCE -> chọn hết rồi xóa
                body.Click();
                body.SendKeys(Keys.Control + "a");
                body.SendKeys(Keys.Delete);
                body.SendKeys(noiDung);

                Assert.That(body.Text.Trim(), Is.EqualTo(noiDung));
            }
            finally
            {
                // Vào thì phải ra — như đóng ngoặc
                driver.SwitchTo().DefaultContent();
            }

            // Đã ra khỏi iframe: tìm lại được tiêu đề ngoài trang
            Assert.That(driver.FindElement(By.TagName("h3")).Text, Does.Contain("iFrame"));
        }
    }
}