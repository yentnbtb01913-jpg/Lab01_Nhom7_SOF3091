using OpenQA.Selenium;

namespace FirstTests.Pages;

// Chỉ dùng cho Phần D - chứng minh lỗi locator có kiểm soát.
// Không phải mã nghiệp vụ chính thức; để riêng khỏi CheckoutPage.
public sealed class DebugLocatorPage : BasePage
{
    // Cố ý sai để người học quan sát lỗi và ảnh Failed thật khi chọn D01.
    private readonly By locatorSai = By.Id("inventory_title_KHONG_TON_TAI");

    public DebugLocatorPage(IWebDriver driver) : base(driver) { }

    public string LayTieuDeBangLocatorSai() => Tim(locatorSai).Text;
}
