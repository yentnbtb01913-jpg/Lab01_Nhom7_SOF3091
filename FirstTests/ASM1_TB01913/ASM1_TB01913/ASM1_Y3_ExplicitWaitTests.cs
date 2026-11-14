using NUnit.Framework;
using FirstTests.Pages_Lab04;

namespace FirstTests.ASM1_TB01913;

[TestFixture]
public class ASM1_Y3_ExplicitWaitTests : ASM1_TestBase
{
    // Ba lan chay doc lap de chung minh Explicit Wait hoat dong on dinh.
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void Y3_DynamicLoading_CoWait_LapBaLan(int lan)
    {
        string actual = new DynamicLoadingPage(driver)
            .MoTrang()
            .BatDau()
            .LayKetQuaCoWait();

        Assert.That(actual, Is.EqualTo("Hello World!"));
        TestContext.WriteLine("Lan " + lan + ": " + actual);
    }
}
