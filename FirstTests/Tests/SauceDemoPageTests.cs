using FirstTests.Core;
using FirstTests.Pages;
namespace FirstTests.Tests;

public class SauceDemoPageTests : BaseTest
{
    // BaseTest đã mở sẵn trang chủ (BaseUrl trong appsettings.json)
    [Test]
    public void OpenSauceDemo_CheckPageTitle()
    {
        string title = new LoginPage(driver).LayTieuDeTrinhDuyet();

        TestContext.WriteLine("Page title: " + title);
        Assert.That(title, Is.EqualTo("Swag Labs"));
    }
}
