
using NUnit.Framework;
using FirstTests.Core;
using FirstTests.Pages;

namespace FirstTests
{
    [TestFixture]
    public class LoginTests : BaseTests
    {
        private LoginPage loginPage = null!;

        [SetUp]
        public void InitPage()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void LoginThanhCong()
        {
            loginPage.DangNhap(
                "standard_user",
                "secret_sauce"
            );

            Assert.That(
                driver.Url,
                Does.Contain("inventory")
            );
        }
    }
}
