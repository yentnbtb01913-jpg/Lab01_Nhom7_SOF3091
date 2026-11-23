# LAB 7 – KHAI THÁC AI CHUYÊN SÂU TRONG QUY TRÌNH KIỂM THỬ

**Họ và tên:** Trần Nguyễn Bình Yên  
**MSSV:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI (.NET)

## Mục tiêu
Dùng AI như trợ lý sinh ca kiểm thử, code theo Page Object Model, phân tích log, refactor và đánh giá đề xuất. Người học phê duyệt đầu vào và kiểm tra mọi kết quả, không xem AI là bằng chứng đã kiểm thử.

## Mở dự án trong Visual Studio
- Giải nén **nguyên thư mục Lab7**, mở `Lab7.slnx` (Visual Studio 2026 và .NET 10 SDK).
- NuGet Restore; mở **Test > Test Explorer**, chọn Run All hoặc Run Selected.
- Hoặc chạy `dotnet build .\Lab7.slnx` và `dotnet test .\Lab7.slnx` ở thư mục Lab7.
- `Config/appsettings.json` quyết định Browser, BaseUrl, Headless, timeout. Mặc định Chrome headless; có thể chuyển `Headless` thành false để quan sát.

## Cấu trúc
- `FirstTests/`: C# source đã được chỉnh tối thiểu từ `Bai7_Source_FirstTests.zip`, gồm `Config`, `Core`, `Data`, `Pages`, `Tests`, `Utilities`, `Reporting`.
- `screenshots/`: **chỉ một thư mục phẳng**, ảnh chụp thật từ Selenium sau mỗi test có WebDriver, không ghi đè.
- `TestReport/`: sau mỗi lượt Run của NUnit, xuất `TestReport.pdf`, `TestReport.xlsx`, `TestReport.html`; lượt sau thêm số `1`, `2`... (không ghi đè lần cũ). Log `NhatKy_*.log` là log thực tế.
- `TieuChiTest/PhanA..PhanE`: yêu cầu và minh chứng theo đúng đề Lab 7.
- `TaiLieuAI/`: prompt, đề xuất test, phân loại GIỮ/BỎ/SỬA, checklist và ghi chép debug.
- `BaoCao_Lab7_TB01913.docx`: báo cáo Word hoàn chỉnh phần nội dung, có chỗ bổ sung bằng chứng chưa chạy.

## Tiêu chí đề chính thức
| Phần | Nội dung | Trọng số |
|---|---|---:|
| A | Prompt đủ bốn thành phần, AI đề xuất ca thường/biên/lỗi | 20% |
| B | Thẩm định GIỮ/BỎ/SỬA; giải thích loại ít nhất 3 ca | 15% |
| C | Sinh Page Object hoặc test mới, checklist sáu điểm, chạy build/test | 25% |
| D | Debug từ stack trace; tự đoán, hỏi AI, sửa, ảnh trước/sau | 25% |
| E | Chỉ ra ít nhất 1 lỗi AI hoặc chứng minh đã rà checklist | 15% |

## Các file C# trọng tâm
- `Tests/DataDrivenCheckoutTests.cs`: `TestCaseSource` lấy dữ liệu `Data/checkout_cases.json`, đi qua LoginPage -> InventoryPage -> CheckoutPage.
- `Pages/InventoryPage.cs`: thêm sản phẩm và mở giỏ theo POM (`ThemBackpack`, `MoGioHang`).
- `Pages/CheckoutPage.cs`: nhập First Name / Last Name / Postal Code, xác nhận `Thank you for your order!` sau Finish.
- `Tests/LAB07_DebugTruocSuaTests.cs`: D01 `[Explicit]` cố tình dùng locator sai; D02 dùng POM đúng. **D01 phải chạy riêng** để tạo lỗi phục vụ Phần D, không chạy trong Run All mặc định.
- `Core/BaseTest.cs`: Setup/TearDown, screenshot, logging; `Reporting/`: tạo PDF/Excel/HTML từ NUnit.

## Cơ chế báo cáo và ảnh
- Báo cáo được tạo từ `TestContext.CurrentContext.Result` của từng ca thực sự chạy: tên test, Outcome, lỗi và thời gian. Không điền PASS/FAIL dự kiến vào cột Actual Result.
- Nếu bạn chạy một ca riêng, báo cáo chỉ có ca đó; nếu chạy toàn bộ, có tất cả ca thực thi. Test `[Explicit]` không tự chạy khi chọn Run All.
- Ảnh được chụp từ WebDriver trước khi đóng trình duyệt. Ảnh hội thoại AI, DevTools, GitHub và ảnh màn hình ngoài trình duyệt cần bổ sung bằng cách chụp thật.
- Trong Visual Studio, nếu file mới chưa hiện trong Solution Explorer, hãy thử **Show All Files** hoặc tab **File System**. `*.pdf/*.xlsx` được khai báo wildcard trong `.csproj` để hiển thị khi project được làm mới.
- Nếu xuất báo cáo bị lỗi (ví dụ file đang mở khóa), xem Test Explorer Output hoặc `TestReport/LoiXuatBaoCao.log`; không kết luận đã xuất thành công khi chưa thấy file.

## Phần A và B – Prompt và đánh giá
Đọc `TaiLieuAI/PhanA_PromptVaDanhSachAI.md` và `PhanB_BangGiuBoSua.md`. AI đề xuất 12 trường hợp; **TC09 coupon, TC10 thanh toán Visa, TC11 OTP** bị loại vì không tương ứng luồng checkout có sẵn. Các ca TC05/06/07/08/12 phải kiểm chứng thêm, không tự gán PASS.

## Phần C – Quy tắc framework
Kế thừa BasePage/BaseTest, locator private, Tim(By) có Explicit Wait, không `Thread.Sleep`, không Assert trong Page, không tạo driver mới ở file Test, tái sử dụng Page Objects. File checklist tại `TaiLieuAI/PhanC_PromptVaChecklist.md`.

## Phần D – Debug
- Chọn riêng test `D01_TruocSua_LocatorSai_CoYFailed` -> dự kiến thất bại vì ID sai. **Chỉ lấy kết quả đúng của lần chạy thực tế.**
- Ghi nguyên văn stack trace và ảnh Failed vào báo cáo, tự phân tích trước khi hỏi AI.
- Chạy `D02_SauSua_LocatorDung_DaHienThi` để kiểm tra bản đúng, lưu ảnh Test Explorer và screenshot thật.
- Đọc `TaiLieuAI/PhanD_DebugTruocSau.md` để lấy mẫu prompt có chỗ dán stack trace thực tế.

## Phần E – Thẩm định AI
Tại `TaiLieuAI/PhanE_ThamDinhAI.md` ghi rõ các lỗi thực sự nhìn thấy trong *source mẫu* (kỳ vọng tài khoản không tồn tại, thao tác Selenium trực tiếp trong Test, Assert OR quá rộng). Không quy những lỗi này cho AI khi thiếu bản ghi AI. Lưu prompt và phản hồi AI thực tế khi thuyết trình/nộp bài.

## Điểm chỉnh so với ZIP mẫu giảng viên
- Sửa test `khong_ton_tai/sai_mat_khau` từ kỳ vọng thành công sai sang thất bại.
- Chuyển thao tác thêm sản phẩm/giỏ trong Test sang POM.
- Assert đúng thông báo `h2.complete-header`, không chấp nhận tùy tiện hai chuỗi khác nhau.
- Bổ sung D01/D02 minh họa debugging và cơ chế báo cáo từng lượt giống Lab trước.

## Phạm vi xác minh
Mã nguồn đã được rà soát tĩnh, **chưa chạy `dotnet build`, `dotnet test` ở môi trường tạo tệp vì không có .NET SDK**. Chưa có ảnh Passed/Failed, thời gian thực thi hoặc lịch sử GitHub giả. Cần chạy và hoàn tất bằng chứng trên máy học viên trước khi nộp.
