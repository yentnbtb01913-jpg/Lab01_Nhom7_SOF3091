# LAB 06 – TEST RUNNER, BÁO CÁO, LOGGING & CHẠY SONG SONG

**Môn học:** SOF3091  
**Dự án:** FirstTests – Selenium WebDriver & NUnit  
**Website kiểm thử:** https://www.saucedemo.com/

---

## 1. Giới thiệu

Lab 6 tiếp tục phát triển framework kiểm thử tự động Selenium WebDriver từ các bài Lab trước. Bài thực hành tập trung vào việc tổ chức test, logging, tạo báo cáo HTML, chạy kiểm thử song song và sử dụng AI để hỗ trợ xây dựng test case.

Các mục tiêu chính:

- Tổ chức test bằng NUnit `[Category]` và chạy chọn lọc.
- Tích hợp logging có thời gian và mức độ vào framework.
- Xuất kết quả kiểm thử thành file `.trx` và tạo báo cáo HTML.
- Thực hiện chạy song song và đánh giá tính Thread-safe.
- Sử dụng AI sinh test case, câu Assert NUnit và thẩm định kết quả.

## 2. Công nghệ sử dụng

| Công nghệ | Chức năng |
|---|---|
| C# / .NET 10 | Ngôn ngữ và nền tảng phát triển |
| Selenium WebDriver | Tự động hóa trình duyệt |
| NUnit | Framework kiểm thử |
| NUnit3TestAdapter | Kết nối NUnit với Test Explorer |
| ChromeDriver | Điều khiển trình duyệt Chrome |
| WebDriverWait | Chờ các phần tử xuất hiện |
| PowerShell | Hỗ trợ chạy lệnh và tạo báo cáo |
| TRX / HTML | Lưu trữ và hiển thị kết quả kiểm thử |

## 3. Cấu trúc project

```text
FirstTests/
│
├── Config/
│   ├── appsettings.json
│   ├── ConfigReader.cs
│   └── TestConfig.cs
│
├── Core/
│   ├── BaseTest.cs
│   └── DriverFactory.cs
│
├── Data/
│   └── users.json
│
├── Pages/
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   └── CartPage.cs
│
├── Tests/
│   ├── LoginTests.cs
│   ├── DataDrivenLoginTests.cs
│   ├── AlertIframeTests.cs
│   ├── WaitTests.cs
│   └── ...
│
├── Utilities/
│   ├── Log.cs
│   └── ScreenshotHelper.cs
│
├── Tools/
│   └── trx-to-html.ps1
│
├── TestResults/
│   └── ketqua.trx
│
├── AssemblyInfo.cs
├── FirstTests.csproj
└── README.md
```

## 4. Nội dung thực hiện Lab 6

### 4.1. Phần A – Tổ chức test với Category (15%)

Sử dụng thuộc tính `[Category]` của NUnit để phân nhóm các test case theo chức năng.

Các nhóm test được tổ chức gồm:

- Smoke
- Login
- DataDriven
- Inventory
- Wait
- AlertIframe
- Slow
- Demo

**Chạy nhóm Smoke:**

```powershell
dotnet test --filter "Category=Smoke"
```

**Chạy các test không thuộc nhóm Slow:**

```powershell
dotnet test --filter "Category!=Slow"
```

Mục đích là giúp lựa chọn và thực thi các nhóm kiểm thử cần thiết mà không phải chạy toàn bộ test suite.

### 4.2. Phần B – Logging vào BaseTest (25%)

Xây dựng lớp `Log` trong thư mục `Utilities` để ghi nhật ký trong quá trình kiểm thử.

Các phương thức chính:

- `Log.Info()`: Ghi thông tin.
- `Log.Warn()`: Ghi cảnh báo.
- `Log.Error()`: Ghi lỗi.

Thông tin được ghi gồm:

- Thời gian thực hiện.
- Mức độ nhật ký.
- Thread ID.
- Nội dung thông báo.

Ví dụ:

```text
[22:18:26.748] [INFO] [T15] BẮT ĐẦU: LoginTests
[22:18:27.050] [INFO] [T15] Mở Chrome
[22:18:28.139] [INFO] [T15] XONG: LoginTests
```

Logging được tích hợp vào `BaseTest`:

- `[SetUp]`: Ghi nhận thời điểm bắt đầu test.
- `[TearDown]`: Ghi nhận kết quả OK hoặc RỚT.

Các lớp test kế thừa `BaseTest` và không cần tự viết lệnh logging cho từng test.

### 4.3. Phần C – Sinh báo cáo HTML (15%)

Sử dụng kết quả kiểm thử NUnit để xuất báo cáo định dạng TRX.

**Lệnh chạy:**

```powershell
dotnet test --logger "trx;LogFileName=ketqua.trx"
```

File kết quả được tạo trong thư mục:

```text
TestResults/ketqua.trx
```

Sử dụng script PowerShell trong thư mục `Tools` để chuyển đổi file TRX thành báo cáo HTML.

Báo cáo HTML thể hiện:

- Tổng số test case.
- Số test Passed.
- Số test Failed.
- Tên class và tên test.
- Thời gian thực thi.
- Nội dung lỗi của các test thất bại.

### 4.4. Phần D – Chạy song song và Thread-safety (30%)

Sử dụng NUnit Parallel Execution để cho phép các class test chạy song song.

File cấu hình `AssemblyInfo.cs`:

```csharp
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
```

Cấu hình cho phép các fixture chạy song song với tối đa 4 luồng.

Trong `appsettings.json`, bật chế độ Headless:

```json
"Headless": true
```

**Kết quả thực nghiệm:**

| Chỉ tiêu | Chạy tuần tự | Song song lần 1 | Song song lần 2 |
|---|---:|---:|---:|
| Tổng số test | 27 | 27 | 27 |
| Passed | 24 | 23 | 24 |
| Failed | 1 | 2 | 1 |
| Thời gian | 137.4s | 134.1s | 136.1s |

Nhận xét:

- Lần chạy song song đầu tiên xuất hiện thêm lỗi `AddProductToCart` do `WebDriverTimeoutException`.
- Khi chạy lại, lỗi `AddProductToCart` không còn xuất hiện.
- Lỗi `DangNhap_01_standard_user` vẫn xuất hiện giống lần chạy tuần tự.
- Thời gian chạy song song lần hai giảm khoảng 1.3 giây so với chạy tuần tự.

**Giải thích Thread-safety:**

Framework sử dụng biến `driver` là field thông thường trong `BaseTest`, không khai báo `static`. Mỗi fixture có đối tượng test và WebDriver riêng, được khởi tạo trong `[SetUp]` và đóng trong `[TearDown]`. Các lớp `DriverFactory` và `Log` không lưu trạng thái WebDriver dùng chung có thể bị thay đổi giữa các luồng. Cấu hình được nạp qua `Lazy<TestConfig>` và được sử dụng để đọc dữ liệu cấu hình.

Việc sử dụng `ParallelScope.Fixtures` giúp giới hạn phạm vi chạy song song ở cấp fixture, tránh các test trong cùng một fixture cùng thao tác trên một field driver.

**Đánh giá:** Kết quả lần chạy song song thứ hai có số test Failed bằng lần chạy tuần tự. Tuy nhiên, cần tiếp tục theo dõi lỗi phát sinh ngẫu nhiên để đánh giá đầy đủ tính ổn định của framework.

### 4.5. Phần E – AI sinh và thẩm định test case (15%)

Chọn ô nhập Zip/Postal Code trong trang Checkout: Your Information của SauceDemo.

**Prompt sử dụng:**

```text
Hãy liệt kê các ca kiểm thử cho ô nhập Zip/Postal Code
trong form "Checkout: Your Information" của trang
https://www.saucedemo.com/.

Với mỗi ca, hãy cho: tên ca, dữ liệu nhập,
kết quả mong đợi và câu Assert dạng NUnit (C#).

Gồm ca thường, ca biên (rỗng, khoảng trắng,
rất dài, ký tự đặc biệt, chữ thay cho số) và ca lỗi.
```

AI đề xuất 7 nhóm kiểm thử:

1. Nhập mã bưu điện hợp lệ.
2. Để trống dữ liệu.
3. Chỉ nhập khoảng trắng.
4. Nhập chuỗi rất dài.
5. Nhập ký tự đặc biệt.
6. Nhập chữ thay cho số.
7. Kiểm thử dữ liệu biên.

**Thẩm định kết quả AI:**

- TC04 – Nhập chuỗi rất dài: Câu Assert chỉ kiểm tra độ dài dữ liệu đầu vào và tìm chuỗi Application error, chưa xác minh chính xác hành vi xử lý của website.
- TC07 – Kiểm thử dữ liệu biên: AI đề xuất hai giá trị `1` và `00000` nhưng câu Assert chỉ kiểm tra giá trị `1`, thiếu kiểm tra giá trị còn lại.

Hai trường hợp trên chưa đủ điều kiện sử dụng nguyên trạng và cần chỉnh sửa trước khi đưa vào bộ kiểm thử chính thức.

## 5. Cách chạy project

**Bước 1:** Mở Terminal tại thư mục chứa `FirstTests.csproj`.

**Bước 2:** Khôi phục các package:

```powershell
dotnet restore
```

**Bước 3:** Chạy toàn bộ test:

```powershell
dotnet test
```

**Bước 4:** Chạy test theo Category:

```powershell
dotnet test --filter "Category=Smoke"
```

**Bước 5:** Xuất kết quả TRX:

```powershell
dotnet test --logger "trx;LogFileName=ketqua.trx"
```

## 6. Kết luận

Sau khi hoàn thành Lab 6, framework FirstTests được bổ sung khả năng phân nhóm và chạy chọn lọc test bằng NUnit Category, ghi nhật ký tự động thông qua BaseTest, tạo báo cáo HTML từ kết quả TRX và thực hiện kiểm thử song song.

Ngoài ra, việc sử dụng AI giúp đề xuất các test case và câu Assert NUnit. Tuy nhiên, kết quả AI cần được kiểm tra và thẩm định trước khi áp dụng vào framework thực tế.

Qua bài thực hành, tôi hiểu rõ hơn về cách tổ chức một framework kiểm thử tự động, theo dõi lỗi, đánh giá hiệu suất và đảm bảo tính ổn định khi chạy nhiều test đồng thời.

---

**Sinh viên:** ........................................  
**Mã sinh viên:** TB01945  
**Môn học:** SOF3091  
**Bài thực hành:** LAB 06
