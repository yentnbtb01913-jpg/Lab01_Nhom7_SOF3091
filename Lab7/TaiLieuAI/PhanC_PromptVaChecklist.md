# PHẦN C – CODE THEO POM

## Prompt AI (nguyên văn)

```text
VAI TRÒ: Senior Selenium C# Engineer.
NGỮ CẢNH: Framework FirstTests .NET 10, NUnit 4, BaseTest nắm WebDriver và SetUp/TearDown; BasePage nắm driver, WebDriverWait và Tim(By). LoginPage trả InventoryPage; CheckoutPage đang có BatDau/NhapThongTin/LayThongBaoLoi.
YÊU CẦU: Tạo hoặc chỉnh một Page Object cho luồng thêm Sauce Labs Backpack -> mở giỏ -> checkout. Mọi locator private readonly By; page kế thừa BasePage; mọi tương tác dùng Tim() có Explicit Wait; file test kế thừa BaseTest, không tự tạo WebDriver, không dùng Thread.Sleep; không có Assert trong Page; không trả IWebElement ra Test. Giữ nguyên tên hàm tiếng Việt của framework nếu đã có.
ĐỊNH DẠNG: Chỉ xuất thay đổi cần thiết theo từng tên file và một test NUnit có Assert chính xác thông báo Thank you for your order!, sau đó kèm checklist 6 điểm.
```

Khi gửi prompt, đính kèm nguyên văn các file mẫu: `BasePage.cs`, `LoginPage.cs`, `InventoryPage.cs`, `CheckoutPage.cs` để AI bắt chước phong cách dự án.
Ảnh hội thoại AI (prompt và mã AI trả về): `screenshots/minhchung/C09_*.png` (bổ sung khi chụp).

## Những file liên quan
- `FirstTests/Pages/InventoryPage.cs`: ThemBackpack(), MoGioHang().
- `FirstTests/Pages/CheckoutPage.cs`: BatDau(), NhapThongTin(), LayThongBaoLoi(), HoanTatVaLayThongBao().
- `FirstTests/Tests/DataDrivenCheckoutTests.cs`: TestCaseSource JSON, Assert bằng NUnit.

## Checklist 6 điểm
- [x] Locator nằm private readonly ở Page (CheckoutPage: nutCheckout, oTen, oHo, oMaBuuChinh, nutContinue, nutFinish, thongBaoHoanTat, loi); đã đối chiếu với DOM bằng DevTools (Minh chứng C07).
- [x] Assert chỉ nằm trong Test (`Assert.That` trong DataDrivenCheckoutTests), không có Assert trong Page.
- [x] Thao tác qua Tim(By) có Explicit Wait (`wait.Until`), không có Thread.Sleep trong các file .cs.
- [x] Page kế thừa BasePage, test kế thừa BaseTest.
- [x] Tái sử dụng LoginPage -> InventoryPage -> CheckoutPage, không tạo WebDriver trong test.
- [ ] Biên dịch và chạy test: NUnit ghi 19 Passed, 0 Failed ở lượt 18:33:29 ngày 23/11/2026 (`TestReport/TestReport.pdf`); ảnh terminal `dotnet build` và `dotnet test` lưu tại `screenshots/minhchung/C08_*.png` (bổ sung khi chụp).

## Điểm cần lưu ý khi thẩm định mã
- `Tim(By)` dùng `wait.Until(d => d.FindElement(by))` nên chỉ chờ phần tử tồn tại trong DOM, chưa chờ hiển thị hoặc bấm được.
- `InventoryPage.MoGioHang()` trả về `CheckoutPage`; chưa có Page Object riêng cho trang giỏ hàng (ảnh hưởng ca C02, C03, C17, C18 ở Phần B).
