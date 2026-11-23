# LAB 6 – SOF3091 – TEST RUNNER, LOGGING, BÁO CÁO, CHẠY SONG SONG

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07

## Cấu trúc và công nghệ
- `Lab6.slnx`: mở độc lập trong Visual Studio, source xuất phát từ Lab 5 của sinh viên, KHÔNG ghi đè bằng source mẫu giảng viên.
- `FirstTests/Tests/`: các test Lab5 giữ lại làm bộ kiểm thử nền, nhãn `Lab5KeThua` trong metadata báo cáo; thêm `LAB06_CategoryTests.cs` thực hành Lab6.
- `FirstTests/AssemblyInfo.cs`: `[assembly: Parallelizable(ParallelScope.Fixtures)]`, `[assembly: LevelOfParallelism(4)]`.
- `FirstTests/Utilities/Log.cs`: Info/Error; timestamp và file nhật ký riêng cho mỗi lượt Run.
- `FirstTests/Core/BaseTest.cs`: ghi log trong `[SetUp]`/`[TearDown]`, không đặt log tại file test.
- `screenshots/`: một thư mục phẳng, giữ tên ảnh hiện có và không ghi đè.
- `TestReport/`: mỗi lượt Run tự tạo PDF, Excel và bản xem HTML riêng, không tổng hợp nhầm giữa các lượt.
- `TieuChiTest/PhanA` đến `PhanE`: danh sách yêu cầu và minh chứng theo đúng đề.
- `DeBai.pdf` + `DeBai.md`: nội dung đề Lab6 từ người dùng.

## Chạy bằng Visual Studio
1. Mở `Lab6.slnx`, đợi NuGet Restore, mở Test Explorer.
2. Chọn `Run All Tests` hoặc các ca cần chạy. Không cần script hay file `.ps1`.
3. Khi NUnit kết thúc, mở `TestReport/TestReport.pdf`, `TestReport.xlsx`, `TestReport.html`. Chạy lượt sau tạo `TestReport1.pdf`, `TestReport1.xlsx`, `TestReport1.html`, v.v.
4. Các ca có `Explicit` KHÔNG chạy trong Run All. Chọn riêng `B01_CoYFail_DeChungMinhErrorLog` để xem bản ghi `[ERROR]` thật; không được coi ca này là lỗi hệ thống bất ngờ.
5. Số ca test trong báo cáo ứng với những test **thật sự đã được NUnit thực thi và ghi nhận**, không bao gồm các test không được chọn hoặc lỗi discovery không tới SetUp.
6. Nhật ký thật được tạo trong `TestReport/NhatKy_<thời_gian>.log`.

## Phần A – Category
- Các nhóm `Smoke`, `Login` hiện được gắn ở fixture `LAB06_Smoke`, `LAB06_Login` và hai fixture Lab5 kế thừa.
- Trên terminal đứng trong `Lab6/FirstTests`, chạy `dotnet test --filter "Category=Smoke"` và `dotnet test --filter "Category!=Slow"`.
- Trong Test Explorer có thể chọn Group By > Traits để lọc nhóm; cần chụp màn hình thực tế khi nộp bài.

## Phần B – Logging
- `Log.Info`/`Log.Error` dùng `TestContext.Progress.WriteLine` kèm thời gian và mức độ; `BaseTest` ghi mốc bắt đầu và kết quả từng ca.
- Muốn thấy log chi tiết: `dotnet test -l "console;verbosity=detailed"`.
- Để thấy `[ERROR]`, phải chạy riêng ca `B01_CoYFail_DeChungMinhErrorLog`; log không được bịa sẵn.

## Phần C – TRX và HTML
- Bấm Run trong Visual Studio: tự sinh HTML từ **cùng kết quả NUnit với PDF/XLSX** để xem ngay, không tuyên bố đây là HTML chuyển đổi TRX.
- Để đáp ứng phần **TRX -> HTML** của đề: mở terminal tại `Lab6/FirstTests`, chạy `dotnet test --logger "trx;LogFileName=ketqua.trx"`. TRX sẽ được tạo trong thư mục `TestResults/` (theo cấu hình runner).
- Dùng công cụ chuyển TRX sang HTML như `trxlog2html` (nếu đã cài công cụ này), hoặc công cụ tương đương. Nộp ảnh HTML thực sự xuất từ TRX, có số Passed/Failed. Không dùng bản HTML trực tiếp từ NUnit để thay thế minh chứng chuyển TRX nếu giảng viên yêu cầu thao tác chuyển đổi.

## Phần D – Song song và thread-safety
- Lưu số liệu lượt chạy tuần tự trước khi bật song song (tạm thời vô hiệu `Parallelizable` tại `AssemblyInfo.cs` hoặc chỉ định chế độ tuần tự trong cấu hình runner).
- Sau đó bật lại `[assembly: Parallelizable(ParallelScope.Fixtures)]` và `[assembly: LevelOfParallelism(4)]`; chạy cùng tập test, cùng browser, cùng data để so thời gian và số Passed/Failed.
- **Không thể tự kết luận thread-safe chỉ từ việc đã khai báo attribute.** Trong framework này, `driver` là field instance trong `BaseTest` (không phải static), các fixture giữ driver riêng; bộ ghi báo cáo/ảnh dùng đồng bộ hóa và tên file riêng. Tuy nhiên vẫn phải chạy thực tế để kiểm chứng không có ca Failed mới.
- `FirstTests/Config/appsettings.json` mặc định đặt `Headless=true` riêng cho Lab6 để hạn chế mở nhiều cửa sổ khi chạy song song. Có thể đổi `false` nếu cần quan sát trực tiếp.

## Phần E – AI và thẩm định
- Chọn input thật của SauceDemo. Lưu nguyên văn prompt và 10 hoặc nhiều ca/assert do AI trả về, có boundary cases.
- Đánh giá ít nhất 2 ca AI đề xuất không dùng được, nêu bằng chứng nghiệp vụ thực tế. File `TieuChiTest/PhanE/` là checklist, **chưa có kết quả AI cá nhân**, không bịa.

## Đọc báo cáo và ảnh
- `TestReport.pdf`, `TestReport1.pdf`...: từng lượt chạy riêng, tổng theo lớp và đúng tên/status/lỗi NUnit.
- `TestReport.xlsx`, `TestReport1.xlsx`...: sheet TestCases (gồm cả `NUnit FullName`) và TongHop cho đúng lượt đó.
- `TestReport.html`, `TestReport1.html`...: báo cáo HTML xem nhanh sinh trực tiếp từ NUnit.
- `screenshots/`: ảnh PNG thật của trình duyệt sau mỗi ca khi còn WebDriver; những ảnh DevTools, terminal, trạng thái workflow cần chụp bổ sung bằng công cụ màn hình hệ điều hành.
- Khi mở PDF/XLSX, nên đóng trước khi nhấn Run lại. Nếu file báo cáo bị khóa, lỗi được ghi vào Test Explorer Output và `LoiXuatBaoCao.log` (nếu có thể).
- Môi trường đóng gói không có .NET SDK, vì vậy **chưa xác minh chạy Passed** trên Visual Studio của bạn.

### Tùy chọn TRX trong Visual Studio
Mở menu **Test > Configure Run Settings > Select Solution Wide runsettings File**, chọn `Lab6.runsettings`. File này dùng chuẩn VSTest để yêu cầu ghi TRX khi chạy; tùy phiên bản runner, TRX sẽ nằm trong `TestResults/`. Báo cáo PDF/XLSX/HTML tự động tạo bởi NUnit không phụ thuộc lựa chọn này. Khi nộp minh chứng Phần C, vẫn phải chụp ảnh báo cáo HTML **được chuyển từ TRX**.
