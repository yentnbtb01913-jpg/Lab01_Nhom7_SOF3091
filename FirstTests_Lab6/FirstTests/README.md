# Y3 – Xử lý tình huống thực tế và chờ tường minh

## 1. Mục tiêu
- Sử dụng Explicit Wait thay cho Thread.Sleep.
- Xử lý tình huống phần tử tải chậm trên trang web.
- Đảm bảo Selenium chỉ thực hiện kiểm tra khi phần tử đã xuất hiện và hiển thị.
- Kiểm tra kết quả thực tế của phần tử trên trang web.

## 2. Nội dung thực hiện

Sử dụng file `DynamicLoadingTest.cs` để kiểm thử chức năng Dynamic Loading trên trang web.

Đường dẫn kiểm thử: `https://the-internet.herokuapp.com/dynamic_loading/1`

Các bước thực hiện:
1. Mở trang Dynamic Loading.
2. Nhấn nút Start.
3. Sử dụng WebDriverWait để chờ phần tử có ID `finish` xuất hiện.
4. Kiểm tra phần tử đã hiển thị.
5. Kiểm tra nội dung phần tử bằng `Hello World!`.
6. Thực hiện Assert để xác nhận kết quả kiểm thử.

## 3. Chờ tường minh (Explicit Wait)

Sử dụng đoạn code:

```csharp
IWebElement finish = wait.Until(d =>
{
    var element = d.FindElement(By.Id("finish"));

    return element.Displayed &&
           element.Text == "Hello World!"
        ? element
        : null;
});

Assert.That(finish.Text, Is.EqualTo("Hello World!"));
```

### Giải thích
- `WebDriverWait`: Chờ phần tử trong thời gian tối đa 10 giây.
- `wait.Until()`: Chờ điều kiện được đáp ứng.
- `element.Displayed`: Kiểm tra phần tử đã hiển thị.
- `Assert.That()`: Kiểm tra nội dung thực tế với nội dung mong đợi.

## 4. Kết quả kiểm thử

| Nội dung | Kết quả |
|---|---|
| Mở trang Dynamic Loading | Passed |
| Nhấn nút Start | Passed |
| Chờ phần tử xuất hiện | Passed |
| Kiểm tra nội dung Hello World! | Passed |
| Sử dụng Explicit Wait | Passed |
| Không sử dụng Thread.Sleep trong code C# | Passed |
| Chạy kiểm thử lặp lại 3 lần | Passed nếu cả 3 lần đều thành công |

## 5. Kết luận

Đã áp dụng Explicit Wait trong kiểm thử Selenium NUnit để xử lý tình huống phần tử tải chậm. Việc sử dụng WebDriverWait giúp chương trình chờ đúng điều kiện cần thiết, hạn chế thời gian chờ không cần thiết và nâng cao độ ổn định của kiểm thử tự động.

## 6. Minh chứng

Các ảnh minh chứng được lưu trong thư mục `screenshots`:
- Ảnh code sử dụng WebDriverWait.
- Ảnh kết quả chạy Test Explorer.
- Ảnh kết quả chạy test 3 lần liên tiếp.
- Ảnh tìm kiếm Thread.Sleep trên toàn Solution.

# LAB 6 – Test Runner, báo cáo, logging và chạy song song

## A. Category
- `LoginTest` và `DataDrivenLoginTests` thuộc nhóm `Login`.
- Các test được chọn chạy nhanh có `[Category("Smoke")]`.
- `DynamicLoadingTests` có nhóm `Slow`.
- NUnit adapter của project dùng thuộc tính lọc `TestCategory` khi chạy `dotnet test`.

Chạy Smoke:
```bash
dotnet test --filter "TestCategory=Smoke"
```
Chạy loại trừ Slow:
```bash
dotnet test --filter "TestCategory!=Slow"
```

## B. Logging
- `Utilities/Log.cs` có `Info()` và `Error()`.
- Mỗi dòng log có thời gian đến mili giây, mức độ INFO/ERROR và mã luồng.
- Ghi file được bảo vệ bằng `lock`, tránh các luồng ghi đè/chồng nội dung.
- `BaseTest` ghi mốc bắt đầu ở SetUp, kết quả ở TearDown và chụp ảnh khi test thất bại. `Core/BaseTests` cũng dùng cùng Log helper.
- Test cố ý thất bại được gắn `[Explicit]` và `LoggingDemo`: không chạy trong lượt test thường, chỉ chạy riêng khi cần minh chứng log ERROR.

Chạy minh chứng log lỗi:
```bash
dotnet test --filter "TestCategory=LoggingDemo" -l "console;verbosity=detailed"
```
File log: `bin/Debug/net10.0/Logs/test-log.txt`. Ảnh lỗi nằm trong `screenshots/` hoặc thư mục kết quả của test.

## C. Báo cáo HTML
Xuất TRX cho bộ test bình thường (loại trừ test cố ý rớt và nhóm Slow):
```bash
dotnet test --filter "TestCategory!=LoggingDemo&TestCategory!=Slow" --logger "trx;LogFileName=ketqua.trx" --results-directory TestResults
```
Cài công cụ và chuyển TRX thành HTML:
```bash
dotnet tool install --global trxlog2html --version 1.0.4
trxlog2html -i TestResults/ketqua.trx -o TestResults/ketqua.html
```

## D. Chạy song song và thread-safety
- `AssemblyInfo.cs` bật `[assembly: Parallelizable(ParallelScope.Fixtures)]` và `[assembly: LevelOfParallelism(4)]`.
- `Sequential.runsettings` đặt `NumberOfTestWorkers=0`; `Parallel.runsettings` đặt `NumberOfTestWorkers=4`.
- Mỗi test fixture có driver theo instance; không dùng `static IWebDriver`. Các fixture độc lập tạo và đóng driver riêng.
- `Log` dùng `lock` khi ghi file; tên ảnh có timestamp mili giây và thread ID để hạn chế trùng tên.

Chạy tuần tự (ghi lại thời gian từ terminal):
```bash
dotnet test --settings Sequential.runsettings --filter "TestCategory!=LoggingDemo&TestCategory!=Slow"
```
Chạy song song:
```bash
dotnet test --settings Parallel.runsettings --filter "TestCategory!=LoggingDemo&TestCategory!=Slow"
```
So sánh tổng thời gian và số Passed/Failed giữa hai lần. Chỉ kết luận thread-safe nếu số test và kết quả không đổi, không có lỗi phát sinh ngẫu nhiên.

**Giải thích thread-safety (2–3 câu):** `driver` là field instance của từng test fixture, không phải biến `static`, nên mỗi fixture/test lifecycle sở hữu một trình duyệt riêng. Các fixture chỉ chia sẻ helper `Log`, nhưng thao tác ghi file được bảo vệ bằng `lock`; vì vậy các luồng không ghi chồng dữ liệu log.

## E. AI sinh và thẩm định
Xem đầy đủ prompt, 10 bộ dữ liệu AI trả về và phân tích 2 ca không dùng được tại `AI_Review.md`. Tám ca hợp lệ đã được đưa vào `Data/users.json` đúng schema.

## Ảnh minh chứng cần chụp
Lưu ảnh thật vào `screenshots/`:
- `A_Smoke.png` – kết quả chạy lọc Smoke.
- `A_Exclude_Slow.png` – kết quả loại trừ Slow.
- `B_Logging.png` – terminal có INFO và ERROR sau khi chạy LoggingDemo.
- `C_HTML_Report.png` – trình duyệt mở báo cáo HTML, thấy tổng số Passed/Failed.
- `D_Sequential.png` – thời gian chạy tuần tự.
- `D_Parallel.png` – thời gian chạy song song.

**Lưu ý:** Cần chạy trên máy có .NET SDK và Chrome để tạo TRX, log, báo cáo và số đo thực tế. Không dùng số liệu hoặc ảnh minh chứng tạo sẵn.
