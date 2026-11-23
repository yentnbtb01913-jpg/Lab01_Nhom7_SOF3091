# PHẦN D – DEBUG CÓ AI

## Lỗi có kiểm soát
- `D01_TruocSua_LocatorSai_CoYFailed`: test `[Explicit]`, chỉ chạy riêng; gọi `By.Id("inventory_title_KHONG_TON_TAI")` trong `DebugLocatorPage` -> dự kiến timeout.
- `D02_SauSua_LocatorDung_DaHienThi`: test sử dụng `InventoryPage.DaHienThi()` với locator theo Page Object.

## Phán đoán trước AI
Locator ID không đúng thuộc tính trong DOM, do đó Tim(By) không tìm được phần tử trong thời gian chờ; lỗi có thể là WebDriverTimeoutException (cần lấy lỗi thật sau khi chạy).

## Prompt AI
VAI TRÒ: QA Debugger Selenium C#/NUnit.
NGỮ CẢNH: Lab7, môi trường .NET 10 + NUnit 4, POM; test có hai phiên bản trước và sau. Locator sử dụng private readonly By và Tim(By) chờ đến timeout.
DỮ KIỆN: Mã trong DebugLocatorPage có By.Id("inventory_title_KHONG_TON_TAI") nhưng trang Products sử dụng class title. [DÁN STACK TRACE THẬT TỪ VISUAL STUDIO SAU KHI CHẠY D01 VÀO ĐÂY].
YÊU CẦU: Phân loại lỗi, đề xuất tối đa 3 nguyên nhân theo thứ tự khả năng, giải thích vì sao locator sai gây timeout, đề xuất sửa tối thiểu và cách kiểm chứng bằng DevTools. KHÔNG đoán kết quả Passed.
ĐỊNH DẠNG: Bảng Nguyên nhân | Bằng chứng | Cách kiểm chứng | Thay đổi tối thiểu.

## Dữ liệu cần bổ sung sau khi chạy
- [ ] Stack trace nguyên văn của D01; ảnh PNG Failed trong screenshots/.
- [ ] Kết quả D02 và ảnh PNG tương ứng; ảnh Test Explorer.
- [ ] Ảnh DevTools Ctrl+F kiểm tra locator thật.
- [ ] Kết luận so sánh nguyên nhân tự đoán với phản hồi AI sau khi gửi log thật.
