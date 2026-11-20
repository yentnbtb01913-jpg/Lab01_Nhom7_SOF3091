# LAB 4 – SOF3091 – KIỂM THỬ NÂNG CAO CÙNG AI (.NET)

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07

## Mục tiêu Lab 4

Xem đề chính thức trong `DeBai.pdf` và yêu cầu chi tiết theo phần trong `TieuChiTest/`.
Các test Selenium/NUnit kế thừa từ mã nguồn gốc; chỉ bổ sung cơ chế ghi nhận ảnh và báo cáo.

## Cấu trúc

- `Lab4.slnx`: mở trực tiếp bằng Visual Studio (một project NUnit độc lập).
- `FirstTests/`: source C#, `FirstTests.csproj`, lớp test và thư viện trợ giúp.
- `screenshots/`: **một thư mục phẳng**, lưu ảnh PNG thực tế sau từng test (nếu tạo được WebDriver).
- `TestReport/`: sau mỗi đợt Run Test, tạo một cặp báo cáo mới: `TestReport.pdf`/`.xlsx`, rồi `TestReport1.pdf`/`.xlsx`, `TestReport2.pdf`/`.xlsx`... (không ghi đè).
- `TieuChiTest/`: yêu cầu và tỷ trọng các phần đúng đề Lab 4.
- `DeBai.pdf`: PDF đề gốc.

## Xem đầy đủ tệp trong Visual Studio

- Mở `Lab4.slnx`. Trong **Solution Explorer (Solution)**, mục `TaiLieu` chứa `README.md` và `DeBai.pdf`; mục `TieuChiTest` chứa các file yêu cầu phần A–E (Lab 1 có A–D).
- Mở rộng project `FirstTests` để thấy các thư mục mã nguồn, **`screenshots`** và **`TestReport`**. Hai thư mục kết quả là các liên kết đến `Lab4/screenshots/` và `Lab4/TestReport/` thật trên ổ đĩa; **không có thư mục `PhanA`/`PhanB` bên trong screenshots**.
- Nhấn **Run** trong Test Explorer. Sau khi chạy, mở `screenshots` xem PNG, `TestReport` xem PDF/XLSX. Khi Visual Studio chưa tự nạp các file vừa sinh, nhấn **Refresh/Reload Project** hoặc chọn tab **File System** trong Solution Explorer (tab này liệt kê tức thời đúng cấu trúc thư mục trên ổ đĩa).
- `Lab4.slnx` chỉ hiển thị các file tài liệu và project được đăng ký; phần file sinh sau test được liên kết bằng wildcard trong `FirstTests.csproj`. Không tạo ảnh hoặc báo cáo giả trước khi chạy.

## Chạy bằng Visual Studio — KHÔNG cần script

1. Giải nén toàn bộ thư mục `Lab4`.
2. Cài **Visual Studio 2026 (18.0+)** để dùng .NET 10 và `.slnx`; cài Chrome hoặc Firefox theo cấu hình.
3. Mở `Lab4.slnx` (nằm ngay trong thư mục `Lab4`).
4. Đợi Visual Studio restore các gói NuGet (NUnit, Selenium, ClosedXML, QuestPDF...).
5. Mở **Test > Test Explorer**, chọn **Run All Tests** hoặc chọn từng test và bấm **Run**.
6. Sau khi NUnit hoàn thành lượt chạy, xem bộ báo cáo PDF/XLSX **mới nhất** trong `Lab4/TestReport/`, và ảnh trong `Lab4/screenshots/`.

> Không phải chạy `RunLab.ps1`, `dotnet run` hay công cụ xuất báo cáo riêng.  
> Báo cáo được tạo tự động bởi `[SetUpFixture]` / `[OneTimeTearDown]` trong mã NUnit, từ kết quả thật của test vừa chạy.  
> Chỉ thống kê test thật sự thực thi; test bị `[Explicit]` loại khỏi Run All sẽ không có kết quả cho tới khi bạn chạy riêng.

## Cách đọc kết quả

- **PDF:** Mỗi lần Run tạo tệp riêng theo thứ tự. Bố cục theo từng lớp test, thống kê Passed / Failed đúng lần đó; nêu rõ tên test và lỗi NUnit. Không gộp các lượt chạy trước.
- **Excel:** Mỗi lần Run tạo tệp cùng số thứ tự với PDF. Sheet `TestCases` chứa tên chính xác từng ca theo NUnit (thêm `NUnit FullName`), các bước/expected tham khảo từ `EvidenceNames.json`, trạng thái và thông báo lỗi **thực tế NUnit**; sheet `TongHop` thống kê đúng lượt đó.
- **PNG:** ảnh trang web được Selenium chụp **trước khi driver đóng**. Ảnh DevTools/Visual Studio/PowerShell/GitHub không thể chụp bằng API WebDriver; những bằng chứng này cần cung cấp riêng khi nộp đề.
- `FirstTests/EvidenceNames.json` chứa tên minh chứng và metadata của test; sửa file này nếu cần đổi tên báo cáo, không sửa logic kiểm thử.

## Điều cần chú ý

- Những test cố ý Fail để minh họa lỗi trong Lab 3/4/5 được đánh dấu `[Explicit]`, cần chủ động chọn test đó khi muốn chạy.
- Không có sẵn ảnh PASS hay báo cáo giả trong ZIP: chúng chỉ hình thành sau khi chạy thật.
- Nếu lỗi lúc `SetUp` trước khi WebDriver có hiệu lực, báo cáo ghi lỗi và có thể không có PNG.
- Chỉ mở **một Lab** tại một thời điểm để tránh việc các project tên `FirstTests` giống nhau bị chạy lẫn.
- Mã cần được build và chạy thực tế trên máy Windows/Visual Studio; môi trường đóng gói hiện tại không có .NET SDK để xác nhận các bài test đã Passed.

## Chạy nhiều lần và đối chiếu với Test Explorer

- Lần đầu Run → `TestReport.pdf` và `TestReport.xlsx`.
- Lần thứ hai → `TestReport1.pdf` và `TestReport1.xlsx`.
- Lần thứ ba → `TestReport2.pdf` và `TestReport2.xlsx`. Thứ tự tăng thêm nếu đã có file từ trước; không có ghi đè.
- Trường **Tên ca kiểm thử** và **NUnit FullName** trong Excel là tên do Test Explorer/NUnit thực thi; **Passed / Failed / khác** được đọc từ `TestContext.CurrentContext.Result`. Khi Failed, file ghi lại nguyên văn thông báo lỗi NUnit.
- Một lần **Run Selected** chỉ xuất các ca đã chạy được chọn. Ca `Explicit` hoặc ca không được chạy không bị giả lập thành Passed. Ca lỗi trong quá trình discovery có thể không đi đến `[SetUp]` và không có bản ghi tương ứng; cần xem Test Explorer/Output.
- Các file PNG trong `screenshots/` tiếp tục được đặt tên riêng từng ca và **không bị xóa hoặc ghi đè**.
- Nếu xuất báo cáo gặp lỗi (ví dụ tệp đang bị khóa), NUnit in lỗi rõ ràng trong Progress và ghi `TestReport/LoiXuatBaoCao.log` nếu có quyền ghi; lỗi báo cáo không được thay đổi kết quả test.
- Đóng PDF/Excel đang mở trước khi chạy lượt mới. Không cần chạy script ngoài, vẫn nhấn Run trong Visual Studio.
