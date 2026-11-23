# Tiêu chí Phần C – Lab 5

**Tên:** BaseTest

**Tỷ trọng:** 20%

**Nguồn:** `Lab5/DeBai.pdf` — mục 2, Phần C.

## Nguyên văn tiêu chí theo đề

```text
Phần C — BaseTest (20%)

 • Viết Core/BaseTest.cs (abstract) gom [SetUp] (đọc config → tạo driver → mở URL)
   và [TearDown].
 • Tích hợp cơ chế chụp ảnh khi Failed của Bài 3 (tách ra
   Utilities/ScreenshotHelper.cs).
 • Bọc try/finally: phần chụp ảnh trong try, phần Quit()/Dispose() trong
   finally — chụp ảnh lỗi KHÔNG được ngăn việc đóng trình duyệt, nếu không sẽ để
   lại Chrome chạy ngầm.
 • Mọi file test phải kế thừa: public class LoginTests : BaseTest.
 • Tiêu chí bắt buộc: trong file test KHÔNG còn [SetUp], [TearDown], new
   ChromeDriver().
```

## Ghi nhận kết quả

- Mã kiểm thử tương ứng: [Điền theo Test Explorer, không tự nhận kết quả]
- Minh chứng ảnh: `Lab5/screenshots/PhanC/`
- Kết quả thực tế: [Sau khi chạy NUnit / kiểm chứng trực tiếp]
- Hạng mục yêu cầu thực hiện thủ công: [Theo yêu cầu trong đề nếu có]
