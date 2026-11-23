using NUnit.Framework;
using FirstTests.Config;

namespace FirstTests.Test
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void TestReadConfig()
        {
            var config = ConfigReader.Load();

            Assert.That(config.BaseUrl, Is.Not.Empty);
            Assert.That(config.Browser, Is.EqualTo("Chrome"));

            TestContext.Out.WriteLine("URL: " + config.BaseUrl);
            TestContext.Out.WriteLine("Browser: " + config.Browser);
            TestContext.Out.WriteLine("Timeout: " + config.TimeoutSeconds);
        }
    }
}