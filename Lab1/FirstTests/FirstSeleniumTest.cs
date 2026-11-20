using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    [TestFixture]
    [Category("Selenium")]
    public class FirstSeleniumTest : FirstTests.Core.BaseTest
    {
        // Cau hinh cua Lab 1 duoc lay tu JSON.
        private static string BaseUrl => FirstTests.Config.ConfigReader.Load().Site("Example");
        private const string ExpectedTitle = "Example Domain";

        private static TimeSpan WaitTimeout => TimeSpan.FromSeconds(FirstTests.Config.ConfigReader.Load().TimeoutSeconds);
        private static readonly TimeSpan PollingInterval = TimeSpan.FromMilliseconds(250);

        // Driver duoc tao tai Core.BaseTest, khong phai trong lop test.

        [Test]
        [Description("Mở example.com, in tiêu đề trang và kiểm tra tiêu đề đúng như mong đợi")]
        public void OpenPage_ShouldHaveExpectedTitle()
        {
            Assert.That(driver, Is.Not.Null, "Trình duyệt chưa được khởi tạo.");

            // YÊU CẦU 2: Điều hướng đến trang web
            driver.Navigate().GoToUrl(BaseUrl);
            WaitUntilPageLoaded();

            // YÊU CẦU 3: Đọc và in tiêu đề trang
            string title = driver.Title;
            PrintLine("Tiêu đề trang: " + title);

            // YÊU CẦU 5: Kiểm tra kết quả mong đợi
            Assert.Multiple(() =>
            {
                Assert.That(title, Is.EqualTo(ExpectedTitle),
                    $"Tiêu đề trang không đúng. Mong đợi: '{ExpectedTitle}', thực tế: '{title}'.");

                Assert.That(driver.Url, Does.StartWith(BaseUrl),
                    $"URL hiện tại không phải là trang example.com. URL thực tế: '{driver.Url}'.");
            });
        }

        // Cac ham ho tro cu van duoc giu cho Lab 1.

        private void WaitUntilPageLoaded()
        {
            var wait = new WebDriverWait(driver, WaitTimeout)
            {
                PollingInterval = PollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                wait.Until(d => IsDocumentComplete(d) && !string.IsNullOrEmpty(d.Title));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Trang '{BaseUrl}' không tải xong sau {WaitTimeout.TotalSeconds} giây.");
            }
        }

        private static bool IsDocumentComplete(IWebDriver d)
        {
            var state = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState");
            return state?.ToString() == "complete";
        }

        private static void PrintLine(string message)
        {
            Console.WriteLine(message);
            TestContext.Out.WriteLine(message);
        }
    }
}