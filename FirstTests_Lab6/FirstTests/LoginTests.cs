
using NUnit.Framework;
using FirstTests.Core;
using FirstTests.Pages;

namespace FirstTests
{
    [TestFixture]
    [Category("Login")]
    public class LoginTests : BaseTests
    {
        private LoginPage loginPage = null!;

        [SetUp]
        public void InitPage()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        [Category("Smoke")]
        [Category("Login")]
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
