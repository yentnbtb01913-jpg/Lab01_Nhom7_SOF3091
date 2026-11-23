# PHẦN D – DEBUG CÓ AI

## Lỗi có kiểm soát
- `D01_TruocSua_LocatorSai_CoYFailed`: test `[Explicit]`, chỉ chạy riêng; `DebugLocatorPage` dùng `By.Id("inventory_title_KHONG_TON_TAI")`.
- `D02_SauSua_LocatorDung_DaHienThi`: dùng `InventoryPage.DaHienThi()` với locator `By.ClassName("title")`.

## Kết quả các lượt chạy (từ báo cáo NUnit trong `TestReport/`)
| Lượt | Giờ bắt đầu | Ca | Kết quả | Thời gian |
|---|---|---|---|---|
| TestReport | 2026-11-23 18:33:29 | 19 ca đầy đủ (gồm D02) | 19 Passed, 0 Failed | – |
| TestReport1 | 18:34:53 | D01 | Failed | 14,565 giây |
| TestReport2 | 20:35:32 | D01 | Failed | 25,874 giây |
| TestReport3 | 20:36:56 | D02 | Passed | 5,064 giây |
| TestReport4 | 20:43:04 | D01 | Failed | 15,315 giây |

Thông báo lỗi D01 (cả ba lượt giống nhau): `WebDriverTimeoutException : Timed out after 10 seconds ----> NoSuchElementException : no such element: Unable to locate element: {"method":"css selector","selector":"#inventory_title_KHONG_TON_TAI"}`.

## Phán đoán trước khi hỏi AI
Phần tử tiêu đề Products hiển thị trên trang, nhưng locator `By.Id("inventory_title_KHONG_TON_TAI")` không tương ứng ID của phần tử nào trong DOM. `Tim(By)` gọi `WebDriverWait.Until()` nên chờ hết 10 giây rồi phát sinh `WebDriverTimeoutException`, bên trong có `NoSuchElementException`. Cần sửa locator tại Page Object; không dùng Thread.Sleep để che lỗi.

## Prompt AI (nguyên văn, đã dán stack trace thật)

```text
VAI TRÒ: QA Debugger Selenium C#/NUnit.

NGỮ CẢNH: Lab7, môi trường .NET 10 + NUnit 4 + Selenium WebDriver 4, Page Object Model. Website kiểm thử: https://www.saucedemo.com/. Test có hai phiên bản trước và sau khi sửa. Locator khai báo private readonly By; mọi tìm phần tử đi qua Tim(By), hàm này chờ tối đa TimeoutSeconds = 10 giây (đọc từ appsettings.json).

DỮ KIỆN:
1) Test chạy riêng D01 (Explicit) cho kết quả Failed sau 14.565 giây. Thông báo lỗi nguyên văn từ NUnit:

OpenQA.Selenium.WebDriverTimeoutException : Timed out after 10 seconds
  ----> OpenQA.Selenium.NoSuchElementException : no such element: Unable to locate element: {"method":"css selector","selector":"#inventory_title_KHONG_TON_TAI"}
  (Session info: chrome=157.0.8103.52); For documentation on this error, please visit: https://www.selenium.dev/documentation/webdriver/troubleshooting/errors#nosuchelementexception

2) Mã gây lỗi:

// Pages/BasePage.cs
protected IWebElement Tim(By by) => wait.Until(d => d.FindElement(by));

// Pages/DebugLocatorPage.cs
private readonly By locatorSai = By.Id("inventory_title_KHONG_TON_TAI");
public string LayTieuDeBangLocatorSai() => Tim(locatorSai).Text;

// Tests/LAB07_DebugTruocSuaTests.cs
[Test, Explicit("Chỉ chạy riêng khi cần minh chứng Debug trước khi sửa")]
public void D01_TruocSua_LocatorSai_CoYFailed()
{
    new LoginPage(driver).DangNhap("standard_user", "secret_sauce");
    var tieuDe = new DebugLocatorPage(driver).LayTieuDeBangLocatorSai();
    Assert.That(tieuDe, Is.EqualTo("Products"));
}

3) Quan sát: ảnh chụp lúc lỗi cho thấy đã đăng nhập thành công và trang Products hiển thị đầy đủ; tiêu đề "Products" của trang này nằm trong phần tử có class title, không có phần tử nào có id inventory_title_KHONG_TON_TAI.

4) Phán đoán của tôi trước khi hỏi: locator ID không tồn tại trong DOM nên Tim() chờ hết 10 giây rồi báo timeout.

YÊU CẦU:
- Phân loại lỗi này (lỗi trong code test, lỗi môi trường hay lỗi của ứng dụng).
- Đề xuất tối đa 3 nguyên nhân, xếp theo thứ tự khả năng.
- Giải thích vì sao locator sai lại làm WebDriverTimeoutException xuất hiện bên ngoài, còn NoSuchElementException nằm bên trong.
- Nhận xét phán đoán ở mục 4 của tôi là đúng hay thiếu gì.
- Đề xuất cách sửa tối thiểu, giữ nguyên cấu trúc Page Object và hàm Tim(); không dùng Thread.Sleep.
- Nêu cách kiểm chứng locator bằng DevTools (tab Elements, Ctrl+F).
- Nếu chưa chắc điều gì thì nói rõ, đừng đoán. KHÔNG khẳng định test sẽ Passed khi chưa chạy lại.
```

Ảnh hội thoại: `screenshots/minhchung/D04a_*.png` (prompt) và `D04b_*.png` (câu trả lời).

## Câu trả lời của AI (tóm tắt đúng nội dung ảnh D04b)
- **Phân loại lỗi:** lỗi trong code test (locator sai); không phải lỗi môi trường, không phải lỗi của ứng dụng.
- **Nguyên nhân theo khả năng:**
  1. Locator `By.Id("inventory_title_KHONG_TON_TAI")` không tồn tại trong DOM (rất cao). Kiểm chứng: DevTools, tab Elements, Ctrl+F `#inventory_title_KHONG_TON_TAI` cho 0 of 0; tìm `.title` có 1 kết quả "Products". Sửa tối thiểu: `private readonly By tieuDe = By.ClassName("title");` hoặc `By.CssSelector("[data-test='title']")` nếu có thuộc tính data-test (cần xem DevTools).
  2. Trang chưa tải xong khi tìm phần tử (thấp): `Tim()` đã chờ 10 giây, ảnh lúc lỗi cho thấy trang Products hiển thị đầy đủ; tăng timeout hay thêm Thread.Sleep đều không cứu được.
  3. Đăng nhập chưa thành công hoặc đang ở sai trang (rất thấp): ảnh lúc lỗi đang ở trang Products.
- **Kết luận của AI:** `Tim()` gọi `wait.Until(d => d.FindElement(by))`; `WebDriverWait` mặc định bỏ qua `NoSuchElementException` và thử lại mỗi 0,5 giây; hết 10 giây phần tử vẫn không xuất hiện thì ném `WebDriverTimeoutException`, bọc `NoSuchElementException` cuối cùng nên NUnit in dạng `----> NoSuchElementException`. Phán đoán của người dùng đúng về nguyên nhân; còn thiếu hai ý: lỗi gốc là `NoSuchElementException` (timeout chỉ là lớp bọc) và cần nêu bằng chứng loại trừ hai nguyên nhân còn lại. AI chưa thấy DOM thật nên chưa chắc thuộc tính `data-test`, và không khẳng định kết quả Passed khi chưa chạy lại.

## Đối chiếu và sửa
- Phán đoán trùng chẩn đoán ở nguyên nhân chính; phần thiếu là lớp ngoại lệ và việc loại trừ hai nguyên nhân còn lại.
- Cách sửa áp dụng: dùng `By.ClassName("title")` như `InventoryPage` (ca D02). Chạy lại: D02 Passed (5,064 giây, lượt 20:36:56) và Passed trong lượt 19 ca.
- Ảnh trước và sau: D01 Failed (`screenshots/D1_LAB7_TruocSua_*_Failed_*.png`), D02 Passed (`screenshots/D2_LAB7_SauSua_*_Passed_*.png`); DevTools: `screenshots/minhchung/D03a_*.png`, `D03b_*.png`.
