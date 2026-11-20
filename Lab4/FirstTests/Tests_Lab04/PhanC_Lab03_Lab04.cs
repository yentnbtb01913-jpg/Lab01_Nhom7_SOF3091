using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.Tests_Lab04;

[TestFixture]
public class PhanC_Lab03_Lab04 : TestBase
{
    [Test]
    public void C1_Alert_Accept()
    {
        JavaScriptAlertsPage page = new JavaScriptAlertsPage(driver).MoTrang();
        page.MoAlert();
        Assert.That(page.LayNoiDungThongBao(), Is.EqualTo("I am a JS Alert"));
        page.ChapNhan();
        Assert.That(page.LayKetQua(), Is.EqualTo("You successfully clicked an alert"));
    }

    [Test]
    public void C2_Confirm_Dismiss()
    {
        JavaScriptAlertsPage page = new JavaScriptAlertsPage(driver).MoTrang();
        page.MoConfirm();
        Assert.That(page.LayNoiDungThongBao(), Is.EqualTo("I am a JS Confirm"));
        page.Huy();
        Assert.That(page.LayKetQua(), Is.EqualTo("You clicked: Cancel"));
    }

    [Test]
    public void C3_Prompt_SendKeys_Accept()
    {
        string noiDung = "Yen test Selenium";
        JavaScriptAlertsPage page = new JavaScriptAlertsPage(driver).MoTrang();
        page.MoPrompt();
        Assert.That(page.LayNoiDungThongBao(), Is.EqualTo("I am a JS prompt"));
        page.NhapPrompt(noiDung);
        page.ChapNhan();
        Assert.That(page.LayKetQua(), Is.EqualTo("You entered: " + noiDung));
    }

    [Test]
    public void C4_Iframe_GoChu()
    {
        string noiDung = "Xin chao iframe tu Selenium";
        IframePage page = new IframePage(driver).MoTrang();
        try
        {
            page.VaoIframe();
            page.GhiNoiDung(noiDung);
            Assert.That(page.LayNoiDung(), Is.EqualTo(noiDung));
        }
        finally
        {
            page.ThoatIframe();
        }
        Assert.That(page.LayTieuDeNgoaiIframe(), Does.Contain("iFrame"));
    }
}
