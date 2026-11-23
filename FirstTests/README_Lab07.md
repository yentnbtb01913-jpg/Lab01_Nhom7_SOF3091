# LAB 07 – KHAI THÁC AI CHUYÊN SÂU TRONG QUY TRÌNH KIỂM THỬ

**Môn học:** SOF3091 · **Dự án:** FirstTests (Selenium WebDriver + NUnit, C#/.NET 10)
**Website kiểm thử:** https://www.saucedemo.com/ · **Tính năng chọn:** Thêm sản phẩm vào giỏ và thanh toán (checkout)
**AI đã dùng:** `[ghi tên AI + phiên bản, vd: Claude ...]`

> Quy ước trong file này: ☑ = đã kiểm xong · ☐ = CHƯA kiểm, phải tự làm/tự chụp trước khi nộp.

---

## Phần A – Prompt & bộ test case (20%)

### A.1. Prompt nguyên văn (đủ 4 thành phần)

```text
[1. VAI TRÒ]
Bạn là kỹ sư kiểm thử tự động (QA automation) có kinh nghiệm với Selenium WebDriver và NUnit bằng C#.

[2. NGỮ CẢNH FRAMEWORK]
Tôi đang kiểm thử website https://www.saucedemo.com/ bằng dự án FirstTests (.NET 10, Selenium 4, NUnit 4).
Kiến trúc: Page Object Model. Test kế thừa BaseTest (BaseTest tự tạo/đóng driver, mở BaseUrl).
Page Object kế thừa BasePage, có sẵn Tim(By) và TimCoTheBam(By) đã có chờ; locator khai báo private.
Đã có: LoginPage.DangNhap(), InventoryPage.ThemBaloVaoGio()/MoGioHang()/LaySoLuongTrongGio(), CartPage.
Tài khoản: standard_user / secret_sauce.

[3. YÊU CẦU RÕ]
Hãy liệt kê bộ test case cho tính năng "thêm sản phẩm vào giỏ và thanh toán (checkout)".
Phải có đủ 3 nhóm: ca thường (luồng đúng), ca biên (giá trị giới hạn, ký tự đặc biệt, độ dài),
ca lỗi (thiếu dữ liệu, dữ liệu sai). Mỗi ca có kết quả mong đợi CỤ THỂ (nêu nguyên văn thông báo/tiêu đề nếu có).
Không bịa chức năng mà trang không có; nếu không chắc hành vi thật của trang thì ghi "cần kiểm tra".

[4. ĐỊNH DẠNG ĐẦU RA]
Trình bày dạng bảng Markdown với các cột: ID | Nhóm (thường/biên/lỗi) | Các bước | Dữ liệu | Kết quả mong đợi.
Không viết code ở bước này.
```

### A.2. Danh sách AI trả về (nguyên văn, chưa chỉnh sửa)

| ID | Nhóm | Các bước | Dữ liệu | Kết quả mong đợi |
|---|---|---|---|---|
| TC01 | Thường | Đăng nhập → thêm 1 sản phẩm → Checkout → nhập thông tin → Continue → Finish | John / Doe / 10000 | Trang "Checkout: Complete!" và lời "Thank you for your order!" |
| TC02 | Thường | Thêm Sauce Labs Backpack vào giỏ | – | Biểu tượng giỏ hiện số 1 |
| TC03 | Thường | Thêm 2 sản phẩm khác nhau | – | Biểu tượng giỏ hiện số 2; cả 2 hiện trong giỏ |
| TC04 | Thường | Thêm 1 sản phẩm rồi bấm Remove trong giỏ | – | Giỏ trống, biểu tượng số giỏ biến mất |
| TC05 | Thường | Ở bước nhập thông tin bấm Cancel | – | Quay về trang giỏ hàng, sản phẩm vẫn còn |
| TC06 | Thường | Xem trang Overview sau Continue | Backpack $29.99 | Item total $29.99, Tax $2.40, Total $32.39 |
| TC07 | Thường | Hoàn tất đơn hàng | – | Giỏ hàng trống sau khi đặt xong |
| TC08 | Biên | Nhập mỗi ô đúng 1 ký tự | A / B / 1 | Sang được bước xác nhận |
| TC09 | Biên | Nhập tên 256 ký tự | 256 × "a" | Báo lỗi "tên quá dài" |
| TC10 | Biên | Nhập ký tự đặc biệt + tiếng Việt | Nguyễn @#$ / Văn A %^&  / 70000 | Chấp nhận, sang bước xác nhận |
| TC11 | Biên | Mã bưu chính chứa chữ | ABC12 | Báo lỗi sai định dạng mã bưu chính |
| TC12 | Biên | Tên chỉ gồm khoảng trắng | "   " | Báo lỗi "First Name is required" |
| TC13 | Biên | Bấm Checkout khi giỏ hàng rỗng | – | Bị chặn / có thông báo giỏ hàng trống |
| TC14 | Lỗi | Để trống Tên | "" / Doe / 10000 | "Error: First Name is required" |
| TC15 | Lỗi | Để trống Họ | John / "" / 10000 | "Error: Last Name is required" |
| TC16 | Lỗi | Để trống Mã bưu chính | John / Doe / "" | "Error: Postal Code is required" |
| TC17 | Lỗi | Để trống cả 3 ô | "" / "" / "" | "Error: First Name is required" |
| TC18 | Lỗi | Thanh toán bằng thẻ hết hạn / sai số thẻ | – | Báo lỗi thẻ không hợp lệ |
| TC19 | Lỗi | Nhập mã giảm giá sai | – | Báo "mã giảm giá không hợp lệ" |

☐ Ảnh chụp hội thoại với AI (prompt + bảng trả về) → `screenshots/A_prompt_va_bang_test_case.png`

---

## Phần B – Chọn lọc & thẩm định test case (15%)

Cách làm: mở saucedemo, thao tác tay từng ca, đối chiếu với kết quả mong đợi của AI.
Cột **Đã kiểm tay** phải tự đánh dấu sau khi thử thật – những dòng ghi "cần kiểm" là chỗ mình CHƯA chắc hành vi thật của trang.

| ID | Quyết định | Lý do / nội dung sửa | Đã kiểm tay |
|---|---|---|---|
| TC01 | GIỮ | Luồng chính; đã cài thành `Checkout_ThongTinHopLe_DatHangThanhCong` | ☐ |
| TC02 | GIỮ | Đã có `LaySoLuongTrongGio()` ở InventoryPage | ☐ |
| TC03 | GIỮ | Chưa cài (ngoài phạm vi Page Object mới) | ☐ |
| TC04 | GIỮ | Chưa cài | ☐ |
| TC05 | GIỮ | Chưa cài; cần thêm `BamHuy()` vào CheckoutPage nếu muốn cài | ☐ |
| TC06 | GIỮ | Tax 8% → $2.40 và Total $32.39 với Backpack; chưa cài | ☐ |
| TC07 | GIỮ | Chưa cài | ☐ |
| TC08 | GIỮ | Đã cài: `Checkout_Bien_MotKyTu` | ☐ |
| TC09 | **BỎ** | Saucedemo KHÔNG giới hạn độ dài tên, không có thông báo "quá dài" → ca mong đợi sai với trang này (cần kiểm tay để chắc chắn) | ☐ |
| TC10 | GIỮ | Đã cài: `Checkout_Bien_KyTuDacBietTiengViet` | ☐ |
| TC11 | **BỎ** | Saucedemo chỉ kiểm tra "có nhập hay chưa" cho Postal Code, không kiểm tra định dạng → nhập chữ vẫn qua (cần kiểm tay) | ☐ |
| TC12 | SỬA | Chưa chắc trang coi chuỗi toàn khoảng trắng là "đã nhập" hay "trống" → CẦN KIỂM TAY rồi mới viết kết quả mong đợi theo hành vi thật | ☐ |
| TC13 | SỬA | Theo hiểu biết của mình, saucedemo vẫn cho bấm Checkout khi giỏ rỗng (là lỗi của trang demo, không phải tính năng chặn) → CẦN KIỂM TAY; nếu đúng thì sửa kỳ vọng thành "vẫn sang trang nhập thông tin" và ghi chú là hành vi hiện tại | ☐ |
| TC14 | GIỮ | Đã cài: `Checkout_Loi_ThieuTen` | ☐ |
| TC15 | GIỮ | Đã cài: `Checkout_Loi_ThieuHo` | ☐ |
| TC16 | GIỮ | Đã cài: `Checkout_Loi_ThieuMaBuuChinh` | ☐ |
| TC17 | GIỮ | Đã cài: `Checkout_Loi_ThieuTatCa` (trang chỉ báo lỗi đầu tiên: First Name) | ☐ |
| TC18 | **BỎ** | Saucedemo KHÔNG có form nhập thẻ; trang Overview chỉ hiện "SauceCard #31337" cố định → không có chức năng để kiểm thử | ☐ |
| TC19 | **BỎ** | Saucedemo KHÔNG có ô nhập mã giảm giá | ☐ |

**Lý do BỎ (≥ 3 ca):** TC09 (không có giới hạn độ dài), TC11 (không kiểm định dạng mã bưu chính), TC18 (không có thanh toán thẻ), TC19 (không có mã giảm giá) – AI áp quy tắc của một shop thật vào một trang demo.

☐ Ảnh chụp thao tác thật trên trang cho ít nhất 3 ca BỎ/SỬA → `screenshots/B_*.png`

---

## Phần C – Sinh code đúng chuẩn framework (25%)

### C.1. Prompt nguyên văn

```text
[VAI TRÒ] Bạn là kỹ sư QA automation C# (Selenium 4 + NUnit 4).

[NGỮ CẢNH] Dự án FirstTests theo POM. Dưới đây là 1 file mẫu (LoginPage.cs) và BasePage.cs để bạn bắt chước phong cách:
<<< dán LoginPage.cs >>>
<<< dán BasePage.cs >>>

[YÊU CẦU] Sinh MỘT Page Object mới tên CheckoutPage cho trang thanh toán saucedemo
(checkout-step-one → checkout-step-two → checkout-complete). Bắt buộc:
- kế thừa BasePage; KHÔNG tạo driver, KHÔNG dùng Thread.Sleep;
- mọi tìm phần tử đi qua Tim()/TimCoTheBam() (có chờ);
- locator khai báo private readonly By; tên biến/hàm bằng tiếng Việt không dấu như file mẫu;
- hàm trả về bool phải trả lời được cả "KHÔNG" (không ném lỗi khi timeout);
- hàm chuyển trang trả về Page Object của trang kế tiếp hoặc this;
- chỉ dùng locator có thật trên trang; nếu không chắc thì nói rõ "cần kiểm tra".
Chức năng: nhập Tên/Họ/Mã bưu chính (ô nào rỗng thì bỏ trống), bấm Continue, đọc thông báo lỗi,
kiểm tra đã sang bước xác nhận, bấm Finish, đọc tiêu đề và lời cảm ơn ở trang hoàn tất.

[ĐỊNH DẠNG] Chỉ trả về 1 file CheckoutPage.cs, có comment tiếng Việt ngắn cho mỗi hàm.
```

### C.2. Code đã giữ lại sau thẩm định

Các file đã thêm/sửa trong repo:

| File | Thay đổi |
|---|---|
| `Pages/CheckoutPage.cs` | MỚI – Page Object trang thanh toán |
| `Pages/CartPage.cs` | Thêm `BamThanhToan()` trả về `CheckoutPage` (tái dùng CartPage, không tạo Page Object trùng) |
| `Tests/CheckoutTests.cs` | MỚI – 7 ca (1 thường, 2 biên, 4 lỗi; dùng chung 1 hàm chuẩn bị); chỉ dùng để chạy thử CheckoutPage |

### C.3. Checklist thẩm định 6 điểm

| # | Điểm kiểm | Trạng thái | Cách kiểm |
|---|---|---|---|
| 1 | Locator có thật | ☐ | Mở DevTools, Ctrl+F: `first-name`, `last-name`, `postal-code`, `continue`, `finish`, `[data-test='error']`, `[data-test='complete-header']`, `class="title"` – tự đánh dấu sau khi kiểm |
| 2 | Assert đúng nghiệp vụ | ☐ | So từng chuỗi mong đợi (3 thông báo lỗi, "Checkout: Complete!", "Thank you for your order!") với trang thật |
| 3 | Dùng wait, không Thread.Sleep | ☑ | `grep -rn "Thread.Sleep" Pages Tests` → không có kết quả; mọi truy cập phần tử qua `Tim`/`TimCoTheBam`/`wait.Until` |
| 4 | Đúng chuẩn khung | ☑ | Kế thừa BasePage/BaseTest; locator private; không `new ChromeDriver`; tái dùng LoginPage/InventoryPage/CartPage |
| 5 | Không thừa | ☑ | Không có hàm/using không dùng trong CheckoutPage |
| 6 | Hiểu từng dòng | ☐ | Tự giải thích được: vì sao `DaSangBuocXacNhan()` bắt `WebDriverTimeoutException`; vì sao `HoanTat()` chờ URL `checkout-complete`; vì sao `Dien()` bỏ qua ô rỗng |

### C.4. Chạy kiểm tra

```powershell
dotnet build
dotnet test --filter "Category=Checkout"
```

☐ Dán kết quả `dotnet build` (0 lỗi) và `dotnet test` (số ca Passed) + ảnh → `screenshots/C_build_test.png`

---

## Phần D – Debug có AI (25%)

Lỗi dùng để debug: `[ghi rõ: giảng viên phát / tự tạo – locator sai / thiếu chờ / Assert nhầm]`

1. **Chạy code lỗi, chụp ảnh khi đỏ** → `screenshots/D_truoc_khi_sua.png`
2. **Stack trace (dán nguyên văn, che dữ liệu nhạy cảm):**
   ```text
   <<< dán stack trace thật >>>
   ```
3. **TỰ phán đoán nguyên nhân (viết TRƯỚC khi hỏi AI):**
   `<<< viết bằng lời của bạn >>>`
4. **Prompt gửi AI (stack trace + code):**
   ```text
   <<< dán prompt >>>
   ```
5. **Chẩn đoán của AI:** `<<< dán >>>`
6. **Đối chiếu:** phán đoán của mình đúng/sai ở đâu so với AI: `<<< viết >>>`
7. **Cách sửa đã áp dụng + chạy lại tới khi xanh** → `screenshots/D_sau_khi_sua.png`

---

## Phần E – Thẩm định & bắt AI trả lời sai (15%)

**Chỗ AI làm sai (ít nhất 1):** `<<< điền sau khi kiểm tay >>>`

Ứng viên có sẵn từ Phần A/B (chỉ dùng nếu bạn đã tự kiểm tay trên trang thật và xác nhận đúng):
- AI đề xuất TC09, TC11, TC18, TC19 với kết quả mong đợi mà saucedemo không có (nghiệp vụ sai).
- AI đề xuất TC13 (giỏ rỗng bị chặn) – cần kiểm trang thật xem có đúng không.

Mẫu giải thích:
- **Prompt:** … · **AI trả lời sai:** … (ảnh `screenshots/E_ai_sai.png`)
- **Vì sao sai:** …
- **Phát hiện bằng cách nào:** thao tác tay trên trang / DevTools / chạy test thấy đỏ …

---

## Commit gợi ý (Conventional Commits)

```text
feat: them CheckoutPage va CartPage.BamThanhToan (AI de xuat, da tham dinh)
test: them ca kiem thu thanh toan (AI de xuat, da tham dinh)
docs: them README_Lab07 (prompt, bang giu/bo/sua, checklist)
```
