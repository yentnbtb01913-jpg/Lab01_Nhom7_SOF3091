# PHẦN C – CODE THEO POM

## Prompt AI

VAI TRÒ: Senior Selenium C# Engineer.
NGỮ CẢNH: Framework FirstTests .NET 10, NUnit 4, BaseTest nắm WebDriver và SetUp/TearDown; BasePage nắm driver, WebDriverWait và Tim(By). LoginPage trả InventoryPage; CheckoutPage đang có BatDau/NhapThongTin/LayThongBaoLoi.
YÊU CẦU: Tạo hoặc chỉnh một Page Object cho luồng thêm Sauce Labs Backpack -> mở giỏ -> checkout. Mọi locator private readonly By; page kế thừa BasePage; mọi tương tác dùng Tim() có Explicit Wait; file test kế thừa BaseTest, không tự tạo WebDriver, không dùng Thread.Sleep; không có Assert trong Page; không trả IWebElement ra Test. Giữ nguyên tên hàm tiếng Việt của framework nếu đã có.
ĐỊNH DẠNG: Chỉ xuất thay đổi cần thiết theo từng tên file và một test NUnit có Assert chính xác thông báo Thank you for your order!, sau đó kèm checklist 6 điểm.

## Những file liên quan
- FirstTests/Pages/InventoryPage.cs: ThemBackpack(), MoGioHang().
- FirstTests/Pages/CheckoutPage.cs: BatDau(), NhapThongTin(), HoanTatVaLayThongBao().
- FirstTests/Tests/DataDrivenCheckoutTests.cs: TestCaseSource JSON, Assert bằng NUnit.

## Checklist 6 điểm
- [x] Locator nằm private readonly ở Page; cần xác minh trong DevTools khi chạy.
- [x] Assert chỉ nằm trong Test, không có Assert trong Page.
- [x] Thao tác qua Tim(By) có Explicit Wait, không Thread.Sleep.
- [x] Page kế thừa BasePage, test kế thừa BaseTest.
- [x] Tái sử dụng LoginPage, InventoryPage, CheckoutPage, không tạo WebDriver trong test.
- [ ] Chạy dotnet build, dotnet test trên máy Windows và lưu ảnh. **Chưa xác minh trong môi trường tạo file.**
