# LAB 01 – THIẾT LẬP MÔI TRƯỜNG VÀ SELENIUM ĐẦU TIÊN

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://example.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **1 ca Passed, 0 ca Failed** (lượt chạy lúc 19:27 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. THIẾT LẬP VÀ KIỂM TRA MÔI TRƯỜNG (30%)

**1. Môi trường sử dụng**

- .NET SDK, Visual Studio/VS Code, Chrome và các gói kiểm thử NUnit, Selenium WebDriver.
- Solution: `Lab1.slnx`; project: `FirstTests/FirstTests.csproj`.
- Tệp cấu hình: `FirstTests/Config/appsettings.json`, có `Browser`, `BaseUrl`, `TimeoutSeconds` và `Headless`.

**2. Kiểm tra phiên bản .NET**

```powershell
dotnet --version
dotnet --info
```

Hai lệnh lần lượt hiển thị phiên bản SDK mặc định và thông tin SDK/runtime, hệ điều hành, kiến trúc máy. Các màn hình này cần được chụp trực tiếp từ terminal khi trình bày phần cài đặt; ảnh trang web do WebDriver chụp không thay thế được ảnh terminal.

**3. Kiểm tra thư viện kiểm thử**

Mở `FirstTests/FirstTests.csproj` để đối chiếu các tham chiếu NUnit, NUnit3TestAdapter, Microsoft.NET.Test.Sdk và Selenium.WebDriver. Project sử dụng `Core/DriverFactory.cs` để tạo trình duyệt, `Core/BaseTest.cs` để quản lý vòng đời test.

## B. KỊCH BẢN SELENIUM ĐẦU TIÊN (40%)

**Ca kiểm thử:** `FirstSeleniumTest.OpenPage_ShouldHaveExpectedTitle` (`FirstTests/FirstSeleniumTest.cs`).

1. Mở Chrome thông qua lớp `DriverFactory` và `BaseTest`.
2. Truy cập `https://example.com/`.
3. Chờ trang tải xong, đọc `driver.Title` và in ra NUnit Test Output.
4. Kiểm tra tiêu đề bằng `Assert.That(title, Is.EqualTo("Example Domain"))`.
5. Kiểm tra URL bắt đầu bằng địa chỉ mong đợi.
6. Đóng trình duyệt tại `[TearDown]` qua `Quit()` và `Dispose()`.

**Kết quả mong đợi:** Tiêu đề là `Example Domain`, URL hợp lệ và ca kiểm thử Passed.  
**Kết quả từ báo cáo:** `OpenPage_ShouldHaveExpectedTitle` — **Passed** (6,598 giây).

**Ảnh minh chứng:** [B1 – Mở trang và kiểm tra tiêu đề](screenshots/B1_LAB1_MoTrangKiemTraTieuDe_FirstTests_FirstSeleniumTest_Passed_20261120_192707_414_992ec2.png).

![Màn hình trang kiểm thử](screenshots/B1_LAB1_MoTrangKiemTraTieuDe_FirstTests_FirstSeleniumTest_Passed_20261120_192707_414_992ec2.png)

## C. QUẢN LÝ MÃ NGUỒN VỚI GITHUB (20%)

**Cấu trúc thư mục chính:**

```text
Lab1/
├── DeBai.pdf
├── Lab1.slnx
├── README.md
├── FirstTests/
│   ├── FirstSeleniumTest.cs
│   ├── Core/
│   ├── Config/
│   ├── Utilities/
│   └── FirstTests.csproj
├── screenshots/
└── TestReport/
```

File `.gitignore` được đặt tại `FirstTests/.gitignore`. Các bước đưa mã nguồn lên GitHub:

```bash
git init
git add .
git commit -m "chore: khoi tao project selenium nunit"
git commit -m "test: kiem tra tieu de trang example"
git push -u origin main
```

Các thông điệp commit trên là định dạng phù hợp để sử dụng khi thực hiện; lịch sử commit và URL repository cần đối chiếu trực tiếp trên GitHub.

## D. AI INTEGRATION – GIẢI THÍCH LỖI (10%)

**1. Tình huống phân tích**

Khi tạo project NUnit và chạy bài Selenium đầu tiên, cần xử lý trường hợp Chrome không khởi động hoặc driver không tương thích với trình duyệt.

**2. Prompt sử dụng AI**

> Tôi sử dụng Selenium WebDriver với C# và NUnit trên .NET. Khi chạy test bằng Chrome, trình duyệt không khởi động hoặc xảy ra lỗi WebDriverException. Hãy giải thích các nguyên nhân có thể gặp, cách kiểm tra phiên bản Chrome, gói Selenium.WebDriver và cách đóng driver trong TearDown. Ưu tiên hướng khắc phục không làm thay đổi logic Assert.

**3. Nội dung AI đề xuất**

- Kiểm tra Chrome đã được cài đặt và có thể khởi chạy bình thường.
- Kiểm tra phiên bản Selenium.WebDriver và cơ chế quản lý ChromeDriver trong môi trường đang dùng.
- Tách việc tạo trình duyệt vào `DriverFactory`, trả về `IWebDriver` thay vì phụ thuộc trực tiếp một trình duyệt.
- Dùng `[SetUp]` để khởi tạo và `[TearDown]` kết hợp `Quit()` và `Dispose()` để tránh tiến trình Chrome tồn tại sau kiểm thử.
- Nếu lỗi xảy ra trong SetUp, vẫn cần dọn dẹp driver đã tạo một phần.

**4. Đối chiếu với mã nguồn**

Project hiện có `Core/DriverFactory.cs` và `Core/BaseTest.cs`. `BaseTest` đóng driver trong khối `finally`, vì vậy tránh bỏ sót thao tác dọn dẹp khi phần ghi nhận ảnh hoặc kết quả xảy ra ngoại lệ.

**5. Nhận xét**

AI giúp tôi xác định các nhóm nguyên nhân liên quan đến trình duyệt, driver và vòng đời NUnit. Các bước kiểm tra có thứ tự nên dễ tìm lỗi hơn so với việc sửa nhiều phần cùng lúc. Tuy nhiên, AI không thể kết luận phiên bản Chrome của máy bị sai nếu chưa có thông báo lỗi thực tế. Tôi sử dụng gợi ý về `IWebDriver`, `Quit()` và `Dispose()`, đồng thời dựa vào kết quả NUnit để kiểm tra lại.

## E. SCREENSHOTS VÀ TEST REPORT

- [Ảnh B1 – Passed](screenshots/B1_LAB1_MoTrangKiemTraTieuDe_FirstTests_FirstSeleniumTest_Passed_20261120_192707_414_992ec2.png).
- [Báo cáo PDF](TestReport/TestReport.pdf) · [Báo cáo Excel](TestReport/TestReport.xlsx).
- **Tổng kết lượt chạy:** 1 Passed / 0 Failed, lớp `FirstTests.FirstSeleniumTest`.
- Ảnh `dotnet --version`, `dotnet --info`, `.csproj` và lịch sử GitHub là bằng chứng thực hiện ngoài trình duyệt, cần có khi nộp những mục tương ứng.



-----------------------------------------------------------------------



# LAB 02 – SELENIUM WEBDRIVER CƠ BẢN

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://www.saucedemo.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **8 ca Passed, 0 ca Failed** (lượt chạy lúc 19:34 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. LOCATOR ANALYSIS (20%)

Chọn năm phần tử trên SauceDemo. Với tên và giá sản phẩm, locator Selenium được giới hạn vào riêng sản phẩm **Sauce Labs Backpack** để chỉ khớp một phần tử.

| STT | Phần tử | Locator 1 | Locator 2 | Locator được chọn | Lý do |
|---|---|---|---|---|---|
| 1 | Username | `By.Id("user-name")` | `By.Name("user-name")` | `By.Id("user-name")` | ID xác định trực tiếp ô tài khoản, dễ đọc. |
| 2 | Password | `By.Id("password")` | `By.Name("password")` | `By.Id("password")` | ID rõ ràng, ngắn và phù hợp dùng trong thao tác đăng nhập. |
| 3 | Login Button | `By.Id("login-button")` | `By.CssSelector("[data-test='login-button']")` | `By.Id("login-button")` | Chỉ rõ nút đăng nhập, tránh locator theo vị trí. |
| 4 | Product Name (Backpack) | `By.CssSelector("#item_4_title_link [data-test='inventory-item-name']")` | `By.CssSelector("#item_4_title_link .inventory_item_name")` | Locator 1 | Giới hạn vào một sản phẩm để không khớp cả sáu tên. |
| 5 | Product Price (Backpack) | `By.CssSelector("div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']")` | `By.CssSelector("div.inventory_item:has(#item_4_title_link) .inventory_item_price")` | Locator 1 | Chỉ lấy giá cùng thẻ sản phẩm Backpack. |

**Kiểm tra bằng Selenium:** `Lab2_LocatorTests.cs` gọi `FindElements(locator)` và Assert `Count == 1` cùng điều kiện `Displayed == true`; cả năm ca A01–A05 đều **Passed** trong báo cáo. **Kiểm tra bằng DevTools:** dán selector CSS (`#user-name`, `#password`, `#login-button`, `#item_4_title_link [data-test='inventory-item-name']`, `div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']`) vào ô tìm kiếm Elements bằng `Ctrl + F`; với selector đúng phạm vi, mục tiêu là `1 of 1`. Ảnh do Selenium tự lưu dưới đây minh chứng các ca test, không phải ảnh giao diện DevTools.

### SCREENSHOTS – LOCATOR VERIFICATION

- [A1 – Username](screenshots/A1_LAB2_LocatorUsername_FirstTests_Lab2_LocatorTests_Passed_20261120_193405_035_f87854.png)
- [A2 – Password](screenshots/A2_LAB2_LocatorPassword_FirstTests_Lab2_LocatorTests_Passed_20261120_193409_177_19f0d7.png)
- [A3 – Login Button](screenshots/A3_LAB2_LocatorNutLogin_FirstTests_Lab2_LocatorTests_Passed_20261120_193412_503_468da8.png)
- [A4 – Product Name](screenshots/A4_LAB2_LocatorTenSanPham_FirstTests_Lab2_LocatorTests_Passed_20261120_193416_170_7092e0.png)
- [A5 – Product Price](screenshots/A5_LAB2_LocatorGiaSanPham_FirstTests_Lab2_LocatorTests_Passed_20261120_193419_361_5f4455.png)

![Kiểm tra locator tên sản phẩm](screenshots/A4_LAB2_LocatorTenSanPham_FirstTests_Lab2_LocatorTests_Passed_20261120_193416_170_7092e0.png)

## B. LOGIN TESTS – LUỒNG THUẬN VÀ NGHỊCH (30%)

**Ca 1 – `Login_Success`**

- Dữ liệu: `standard_user` / `secret_sauce`.
- Các bước: nhập tài khoản, mật khẩu và nhấn Login.
- Mong đợi: URL chứa `inventory.html`.
- Kết quả báo cáo: **Passed**.
- Ảnh: [B1 – Đăng nhập thành công](screenshots/B1_LAB2_DangNhapThanhCong_FirstTests_LoginTests_Passed_20261120_193429_441_96287e.png).

**Ca 2 – `Login_LockedOutUser`**

- Dữ liệu: `locked_out_user` / `secret_sauce`.
- Mong đợi: ở lại màn hình đăng nhập, lỗi hiển thị đúng nội dung `Epic sadface: Sorry, this user has been locked out.`.
- Kiểm tra: `errorMessage.Displayed`, nội dung thông báo và URL không chứa `inventory.html`.
- Kết quả báo cáo: **Passed** vì ứng dụng từ chối tài khoản bị khóa đúng như kỳ vọng.
- Ảnh: [B2 – Tài khoản bị khóa](screenshots/B2_LAB2_DangNhapTaiKhoanBiKhoa_FirstTests_LoginTests_Passed_20261120_193426_279_c81787.png).

`LoginTests` kế thừa `Core/BaseTest`, nên NUnit thực hiện `[SetUp]`/`[TearDown]` ở lớp cha; mã nguồn có `driver.Quit()` và `driver.Dispose()` để đóng trình duyệt.

## C. PRODUCT DATA – THU THẬP SẢN PHẨM (25%)

**Ca kiểm thử:** `LoginTests.GetProductInformation`.

1. Đăng nhập tài khoản hợp lệ.
2. Dùng `driver.FindElements(By.CssSelector("[data-test='inventory-item']"))` lấy các thẻ sản phẩm.
3. Với mỗi `item`, dùng `item.FindElement(...)` để lấy tên và giá bên trong chính thẻ đó.
4. In danh sách sản phẩm theo dạng `STT. Tên sản phẩm - $Giá`.
5. Assert đủ **6 sản phẩm**, tên không rỗng, mọi giá bắt đầu bằng `$` và danh sách có **Sauce Labs Backpack**.

**Kết quả báo cáo:** `GetProductInformation` — **Passed**.  
**Ảnh:** [C1 – Thu thập sản phẩm](screenshots/C1_LAB2_DocSauSanPham_FirstTests_LoginTests_Passed_20261120_193422_458_1b136d.png).

## D. AI INTEGRATION – LOCATOR CHO NÚT ADD TO CART (10%)

**1. Element được chọn:** Nút `Add to cart` của `Sauce Labs Backpack`.

**2. Đoạn HTML dùng để phân tích**

```html
<button id="add-to-cart-sauce-labs-backpack"
        data-test="add-to-cart-sauce-labs-backpack"
        class="btn btn_primary btn_small btn_inventory">
  Add to cart
</button>
```

**3. Prompt sử dụng AI**

> Dựa trên outerHTML nút Add to cart của Sauce Labs Backpack ở SauceDemo, hãy đề xuất hai locator Selenium C#. Ưu tiên ID hoặc data-test; không sử dụng XPath theo vị trí hay chỉ dựa vào tên class. Chọn một locator ổn định hơn và giải thích lý do.

**4. Kết quả AI đề xuất**

- Locator 1: `By.Id("add-to-cart-sauce-labs-backpack")`.
- Locator 2: `By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']")`.
- **Locator được chọn:** `By.Id("add-to-cart-sauce-labs-backpack")` do ID định danh trực tiếp nút của sản phẩm; `data-test` là phương án thay thế phù hợp khi giao diện thay đổi CSS.

**5. Kiểm tra locator**

Trong DevTools → Elements → `Ctrl + F`, nhập `#add-to-cart-sauce-labs-backpack` hoặc `[data-test='add-to-cart-sauce-labs-backpack']`; một phần tử phù hợp sẽ hiển thị **1 of 1**. Đối chiếu tiếp bằng `driver.FindElements(...)` để xác minh số phần tử khớp.

**6. Nhận xét**

AI đề xuất hai locator dựa vào thuộc tính nhận diện thay vì thứ tự phần tử trên trang. ID ngắn gọn và thuận tiện cho Selenium, trong khi `data-test` cũng phù hợp cho kiểm thử tự động. Tôi không chọn locator theo class vì nhiều nút Add to cart có thể dùng chung class. Việc kiểm tra lại số phần tử khớp giúp tránh chọn nhầm nút của sản phẩm khác.

## E. QUẢN LÝ MÃ NGUỒN VÀ BÁO CÁO (15%)

**Ví dụ ba commit theo Conventional Commits:**

```text
test: them 5 ca kiem tra locator
test: bo sung dang nhap thuan va nghich
docs: cap nhat readme va minh chung lab 2
```

Tiếp tục dùng repository của Lab 1 khi push; kiểm tra số commit và lịch sử trên GitHub.  
**Báo cáo:** [TestReport.pdf](TestReport/TestReport.pdf) · [TestReport.xlsx](TestReport/TestReport.xlsx).  
**Tổng kết lượt chạy:** 8 Passed / 0 Failed, gồm 5 locator, 2 đăng nhập và 1 thu thập sản phẩm.



-----------------------------------------------------------------------



# LAB 03 – ĐỒNG BỘ, DROPDOWN, ALERT VÀ IFRAME

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://the-internet.herokuapp.com/ và https://www.saucedemo.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **7 ca Passed, 0 ca Failed** (lượt chạy lúc 19:35 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. EXPLICIT WAIT – VẤN ĐỀ ĐỒNG BỘ (25%)

**Website:** https://the-internet.herokuapp.com/dynamic_loading/2  
**Tệp mã nguồn:** `FirstTests/PhanA_Lab03.cs`.

**1. Ca không có Wait – `A1_RotKhongWait`**

- Nhấn Start rồi gọi ngay `driver.FindElement(By.Id("finish"))`.
- Nếu JavaScript chưa tạo `#finish`, Selenium ném `NoSuchElementException`.
- Ca này gắn `[Explicit]` nên phải chạy riêng để chụp minh chứng Failed; nó không nằm trong 7 ca Passed của báo cáo hiện tại.

**2. Ca có Wait – `A2_PassCoWait`**

```csharp
driver.FindElement(By.CssSelector("#start button")).Click();
IWebElement finish = wait.Until(d => d.FindElement(By.Id("finish")));
Assert.That(finish.Text, Is.EqualTo("Hello World!"));
```

- **Kết quả báo cáo:** `A2_PassCoWait` — **Passed**.
- **Ảnh:** [A2 – Có Explicit Wait](screenshots/A2_LAB3_CoExplicitWait_FirstTests_PhanA_Lab03_Passed_20261120_193535_434_a2311f.png).

**Giải thích:** Với ca không Wait, câu lệnh tìm phần tử có thể chạy khi nội dung vẫn chưa xuất hiện. `WebDriverWait` chờ đến lúc điều kiện tìm được `#finish` mới thực hiện Assert. Chờ theo điều kiện ổn định hơn việc đặt số giây cố định bằng `Thread.Sleep`.

## B. DROPDOWN VÀ ASSERT GIÁ TĂNG DẦN (25%)

**Website:** https://www.saucedemo.com/  
**Tệp:** `PhanB_Lab03.cs`, `SortTests.cs`.

1. Đăng nhập `standard_user` / `secret_sauce`.
2. Tìm dropdown `.product_sort_container` và dùng `SelectElement.SelectByText("Price (low to high)")`.
3. Dùng `FindElements` lấy sáu giá `.inventory_item_price`.
4. Bỏ ký tự `$`, phân tích chuỗi thành `decimal` theo `InvariantCulture`.
5. So sánh danh sách giá thực tế với bản đã `OrderBy(...)` hoặc `Sort()`.

```csharp
var expected = prices.OrderBy(p => p).ToList();
Assert.That(prices, Is.EqualTo(expected));
```

**Kết quả:** `B1_GiaTangDan` và `B1_SapXepPriceLowToHigh_GiaTangDan` — **Passed**.  
**Ảnh:** [B1 – Sắp xếp giá](screenshots/B1_LAB3_SapXepGia_FirstTests_PhanB_Lab03_Passed_20261120_193539_510_ff6f72.png) và [B1 – Kiểm tra giá tăng dần](screenshots/B1_LAB3_SapXepGiaTangDan_FirstTests_SortTests_Passed_20261120_193616_709_6e5502.png).

## C. JAVASCRIPT ALERT VÀ IFRAME (20%)

**Website:** https://the-internet.herokuapp.com/javascript_alerts và https://the-internet.herokuapp.com/iframe.

| Ca | Hành động Selenium | Kết quả cần Assert | Kết quả báo cáo |
|---|---|---|---|
| `C1_Alert_Accept` | `SwitchTo().Alert().Accept()` | `You successfully clicked an alert` | Passed |
| `C2_Confirm_Dismiss` | `SwitchTo().Alert().Dismiss()` | `You clicked: Cancel` | Passed |
| `C3_Prompt_SendKeys_Accept` | Nhập `Yen test Selenium`, sau đó Accept | `You entered: Yen test Selenium` | Passed |
| `C4_Iframe_GoChu` | `SwitchTo().Frame("mce_0_ifr")`, nhập văn bản và `DefaultContent()` | Nội dung là `Xin chao iframe tu Selenium`; tiêu đề ngoài frame có `iFrame` | Passed |

**Ảnh minh chứng:**

- [C1 – Alert](screenshots/C1_LAB3_C1_Alert_Accept_FirstTests_PhanC_Lab03_Passed_20261120_193546_541_a2a6c7.png)
- [C2 – Confirm](screenshots/C2_LAB3_C2_Confirm_Dismiss_FirstTests_PhanC_Lab03_Passed_20261120_193556_250_570c3b.png)
- [C3 – Prompt](screenshots/C3_LAB3_C3_Prompt_SendKeys_Accept_FirstTests_PhanC_Lab03_Passed_20261120_193602_844_b64759.png)
- [C4 – iFrame](screenshots/C4_LAB3_C4_Iframe_GoChu_FirstTests_PhanC_Lab03_Passed_20261120_193611_797_b4853b.png)

## D. SCREENSHOT ON FAILURE (15%)

- Ca `PhanD_Lab03.D1_CoYRot` chứa một Assert tiêu đề cố ý không đúng và được đánh dấu `[Explicit]`.
- Yêu cầu nghiệp vụ là chụp ảnh khi `Outcome.Status == TestStatus.Failed`, đặt tên theo tên ca test và thêm `TestContext.AddTestAttachment(path)`.
- Trong source được nộp, thao tác chụp ảnh và đính kèm được thực hiện tại `Core/BaseTest.cs` thông qua `ScreenshotHelper`; lớp này hiện chụp cho **mọi ca có driver**, không chỉ Failed. Vì vậy, để hoàn toàn đúng yêu cầu *chỉ chụp khi Failed* cần bổ sung điều kiện kiểm tra Outcome cho phần chụp ảnh.
- ZIP minh chứng cung cấp hiện tại có ảnh các ca Passed, chưa có ảnh chạy riêng `D1_CoYRot`.

## E. AI INTEGRATION – CHẨN ĐOÁN FLAKY TEST (15%)

**1. Tình huống:** `#finish` chỉ được JavaScript tạo ra sau khi nhấn Start nên việc tìm ngay lập tức có thể thất bại.

**2. Prompt sử dụng AI**

> Tôi kiểm thử trang https://the-internet.herokuapp.com/dynamic_loading/2 bằng C#, NUnit và Selenium. Sau khi click `#start button`, code gọi `driver.FindElement(By.Id("finish"))` ngay lập tức. Test có thể xuất hiện lỗi `OpenQA.Selenium.NoSuchElementException: no such element` vì nội dung tải động. Hãy phân tích nguyên nhân, đề xuất 3 cách xử lý và cho biết cách nào nên sử dụng. Không thay nghiệp vụ Assert `Hello World!`.

**3. AI đề xuất**

- **Gợi ý 1:** Dùng `WebDriverWait.Until(...)` để chờ phần tử `#finish` xuất hiện trước khi đọc text.
- **Gợi ý 2:** Dùng `Thread.Sleep(5000)` để cho trang thêm thời gian tải.
- **Gợi ý 3:** Chuyển sang tài khoản `performance_glitch_user` để tạo môi trường tải chậm.

**4. Đánh giá các gợi ý**

| Gợi ý | Đánh giá | Lý do |
|---|---|---|
| Explicit Wait | Phù hợp | Chờ đúng phần tử được tạo ra sau thao tác Start. |
| Thread.Sleep | Không lựa chọn | Chờ cố định gây chậm và có thể vẫn không đủ trên máy chậm. |
| performance_glitch_user | Không phù hợp | Đây là tài khoản SauceDemo, không liên quan trực tiếp tới JavaScript tạo `#finish` của trang đang kiểm thử. |

**5. Nhận xét**

AI xác định đúng vấn đề chính là thứ tự thực thi và việc phần tử được tạo bất đồng bộ. Phương án Explicit Wait phù hợp nhất vì có thể kiểm chứng bằng ca `A2_PassCoWait`. Gợi ý dùng Sleep đơn giản nhưng không giải quyết ổn định khi thời gian phản hồi thay đổi. Gợi ý thay tài khoản SauceDemo không liên quan đến trang Dynamic Loading nên tôi loại bỏ.

## F. TEST REPORT

**Tổng kết lượt chạy:** 7 Passed / 0 Failed; chưa tính các test `[Explicit]` chỉ chạy khi được chọn riêng.  
**Báo cáo:** [PDF](TestReport/TestReport.pdf) · [Excel](TestReport/TestReport.xlsx).  
**Bằng chứng thiếu để hoàn tất Phần A/D:** ảnh test không Wait rớt với `NoSuchElementException`; ảnh test cố ý Failed và kiểm tra cơ chế chụp chỉ khi Failed.



-----------------------------------------------------------------------



# LAB 04 – PAGE OBJECT MODEL VÀ REFACTOR TEST

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://www.saucedemo.com/ và https://the-internet.herokuapp.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **16 ca Passed, 0 ca Failed** (lượt chạy lúc 19:39 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. CẤU TRÚC PAGE OBJECT MODEL VÀ BASEPAGE (20%)

**Cấu trúc đang dùng:** `FirstTests/Pages_Lab04/` và `FirstTests/Tests_Lab04/` (có hậu tố `_Lab04` để tách biệt với các Lab khác).

```text
FirstTests/
├── Pages_Lab04/
│   ├── BasePage.cs
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── DynamicLoadingPage.cs
│   ├── JavaScriptAlertsPage.cs
│   ├── IframePage.cs
│   └── HerokuHomePage.cs
└── Tests_Lab04/
    ├── TestBase.cs
    ├── LoginTests_Lab04.cs
    ├── SortTests_Lab04.cs
    └── [các lớp kiểm thử được chuyển từ Lab 2–3]
```

- `BasePage` là `abstract`, giữ `protected readonly IWebDriver driver` và `protected readonly WebDriverWait wait`.
- Hàm `protected IWebElement Tim(By locator)` bao bọc việc chờ và tìm phần tử, dùng nội bộ cho lớp Page kế thừa.
- Page Object chứa locator; lớp test gọi hành vi công khai và thực hiện Assert.

## B. LOGINPAGE & INVENTORYPAGE (25%)

| Page | Hành vi công khai | Ý nghĩa |
|---|---|---|
| `LoginPage` | `MoTrang()`, `DangNhap(tk, mk)`, `LayThongBaoLoi()` | Truy cập trang đăng nhập, gửi thông tin và đọc lỗi. |
| `InventoryPage` | `DaHienThi()`, `SapXep(value)`, `LayDanhSachGia()` | Kiểm tra trang sản phẩm, sắp xếp và lấy giá. |
| `InventoryPage` | `ChoDanhSachSanPham()`, `LayDanhSachSanPham()` | Chờ và lấy danh sách sản phẩm. |
| `JavaScriptAlertsPage` | `MoAlert()`, `MoConfirm()`, `MoPrompt()`, `ChapNhan()`, `Huy()` | Đóng gói thao tác với JS Alert. |
| `IframePage` | `VaoIframe()`, `GhiNoiDung()`, `ThoatIframe()` | Đóng gói thao tác iframe. |

`LoginPage` chứa các trường locator `private readonly By`. Các Page không đặt Assert bên trong và trả dữ liệu/hành vi cho lớp test thay vì công khai `IWebElement`.

## C. REFACTOR TEST LAB 2–3 SANG POM (30%)

**Nguyên tắc:** File test không tự tìm phần tử bằng `By`; locator được chuyển vào lớp Page. `Tests_Lab04` chứa các kịch bản đăng nhập thành công, tài khoản bị khóa, đọc sản phẩm, Dynamic Loading, Alert, iFrame và sắp xếp giá.

**Ví dụ luồng đăng nhập:**

```csharp
LoginPage login = new LoginPage(driver).MoTrang();
InventoryPage inventory = login.DangNhap("standard_user", "secret_sauce");
Assert.That(inventory.LayDuongDan(), Does.Contain("inventory.html"));
```

**Kết quả từ report:** 16 ca **Passed**, gồm các nhóm login, product, wait, alert/iframe và sort. Báo cáo hiện tại là lượt chạy sau POM; để so sánh *trước/sau refactor* theo đúng yêu cầu cần sử dụng kết quả Lab 2/3 làm mốc và đối chiếu các kịch bản tương ứng (không so sánh máy móc tổng số ca do phạm vi khác nhau).

**Ảnh minh chứng:**

- [C1 – Đăng nhập sau POM](screenshots/C1_LAB4_DangNhapSauPOM_FirstTests_Tests_Lab04_LoginTests_Lab04_Passed_20261120_194010_042_25c7c3.png)
- [C2 – Đăng nhập lỗi sau POM](screenshots/C2_LAB4_DangNhapLoiSauPOM_FirstTests_Tests_Lab04_LoginTests_Lab04_Passed_20261120_194006_807_e11fbf.png)
- [C3 – Đọc sản phẩm](screenshots/C3_LAB4_DocSanPhamSauPOM_FirstTests_Tests_Lab04_LoginTests_Lab04_Passed_20261120_194002_650_c817ff.png)
- [C5 – Explicit Wait qua Page](screenshots/C5_LAB4_TestDynamicPOM_FirstTests_Tests_Lab04_PhanA_Lab03_Lab04_Passed_20261120_194033_769_5479be.png)
- [C4 – iFrame qua Page](screenshots/C_LAB4_C4_Iframe_GoChu_FirstTests_Tests_Lab04_PhanC_Lab03_Lab04_Passed_20261120_194112_271_6f3547.png)

## D. CHAINING VÀ ASSERT SORT GIÁ TĂNG DẦN (15%)

File `Tests_Lab04/SortTests_Lab04.cs` gọi nối chuỗi:

```csharp
List<decimal> prices = login
    .DangNhap("standard_user", "secret_sauce")
    .SapXep("lohi")
    .LayDanhSachGia();
Assert.That(prices.Count, Is.EqualTo(6));
Assert.That(prices, Is.Ordered.Ascending);
```

`SapXep("lohi")` trả về `this` ở `InventoryPage`, cho phép nối tiếp với `LayDanhSachGia()`; điều kiện `Is.Ordered.Ascending` kiểm tra trực tiếp thứ tự giá.

**Ảnh:** [D1 – Sắp xếp giá qua POM](screenshots/D1_LAB4_SapXepGiaBangPOM_FirstTests_Tests_Lab04_PhanB_Lab03_Lab04_Passed_20261120_194038_196_d1a984.png) và [D2 – Chaining giá tăng dần](screenshots/D2_LAB4_ChainingGiaTangDan_FirstTests_Tests_Lab04_SortTests_Lab04_Passed_20261120_194117_369_11f648.png).

## E. AI INTEGRATION – THẨM ĐỊNH CODE POM (10%)

**1. File lựa chọn:** Chuyển thao tác Login từ Selenium trực tiếp sang `LoginPage`.

**2. Prompt sử dụng AI**

> Hãy refactor test đăng nhập SauceDemo bằng C# NUnit Selenium sang Page Object Model. Yêu cầu `BasePage` giữ IWebDriver và WebDriverWait; locator đặt `private readonly By`; Page chỉ cung cấp hành vi nghiệp vụ, không có Assert và không trả `IWebElement` ra lớp test. `LoginPage.DangNhap(tk, mk)` phải trả `InventoryPage`, còn Assert nằm trong test.

**3. Kết quả AI đề xuất (đoạn cần thẩm định)**

```csharp
public IWebElement GetLoginButton()
{
    return driver.FindElement(By.Id("login-button"));
}
```

Đồng thời AI gợi ý test gọi `GetLoginButton().Click()` và kiểm tra URL sau thao tác.

**4. Điểm chưa đúng:** `GetLoginButton()` trả `IWebElement` ra ngoài Page Object và làm file test phụ thuộc cấu trúc HTML; điều này trái quy ước POM của bài. Phương án phù hợp là giữ locator `private readonly By nutDangNhap` và cho `DangNhap(...)` tự thao tác click, sau đó trả `InventoryPage`.

**5. Cách thể hiện trong mã nguồn**

```csharp
private readonly By nutDangNhap = By.Id("login-button");
public InventoryPage DangNhap(string tk, string mk)
{
    Tim(oTaiKhoan).SendKeys(tk);
    Tim(oMatKhau).SendKeys(mk);
    Tim(nutDangNhap).Click();
    return new InventoryPage(driver);
}
```

**6. Nhận xét**

AI giúp xác định nên gom các thao tác giao diện vào lớp Page. Tuy nhiên, đoạn trả `IWebElement` khiến test vẫn phụ thuộc trực tiếp vào phần tử HTML. Tôi giữ locator dưới dạng `private readonly By` và chỉ công khai hành vi đăng nhập. Các Assert tiếp tục nằm ở lớp test để lớp Page có thể tái sử dụng.

## F. BÁO CÁO KẾT QUẢ

**TestReport:** 16 Passed / 0 Failed, 7 lớp kiểm thử trong lượt chạy.  
**Tệp:** [PDF](TestReport/TestReport.pdf) · [Excel](TestReport/TestReport.xlsx).  
**Lưu ý cấu trúc:** Thư mục thật của source là `Pages_Lab04` và `Tests_Lab04`, không phải tên ngắn `Pages`/`Tests` trong ví dụ đề.



-----------------------------------------------------------------------



# LAB 05 – TEST FRAMEWORK VÀ DATA-DRIVEN TESTING

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://www.saucedemo.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **13 ca Passed, 0 ca Failed** (lượt chạy lúc 19:42 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. CONFIGURATION LAYER (20%)

**Cấu trúc:** `FirstTests/Config/appsettings.json`, `Config/TestConfig.cs`, `Config/ConfigReader.cs`.

```json
{
  "BaseUrl": "https://www.saucedemo.com/",
  "Browser": "Chrome",
  "TimeoutSeconds": 8,
  "Headless": false,
  "WindowWidth": 1366,
  "WindowHeight": 768
}
```

`ConfigReader` đọc dữ liệu; `TestConfig` chứa giá trị cấu hình; project copy JSON ra output khi chạy. Hai test `A01_DocCauHinhTuJson` và `A02_CauHinhKhongHopLeCoThongBao` đều **Passed**.

**Ảnh:** [A1 – Đọc JSON](screenshots/A1_LAB5_DocCauHinhJSON_FirstTests_Tests_LAB05_PhanA_Passed_20261120_194207_417_42f950.png) và [A2 – Cấu hình không hợp lệ](screenshots/A2_LAB5_KiemTraConfigKhongHopLe_FirstTests_Tests_LAB05_PhanA_Passed_20261120_194210_896_7f7fae.png).

## B. DRIVER FACTORY (20%)

`Core/DriverFactory.cs` có `Create(TestConfig cfg)` trả `IWebDriver`, tạo `ChromeDriver` hoặc `FirefoxDriver` theo `Browser`. Khi bật `Headless`, code thêm tùy chọn chạy ẩn cho cả hai trình duyệt; giá trị tên trình duyệt không hợp lệ sẽ sinh `NotSupportedException` chứa tên lỗi.

- `B01_CungCode_ChayTheoBrowserTrongJson`: kiểm tra loại driver theo cấu hình — **Passed** với cấu hình trong lượt chạy.
- `B02_TenBrowserSai_ThongBaoRoRang`: kiểm tra thông báo khi dùng `Firefix` — **Passed**.

**Ảnh:** [B1 – Driver Factory](screenshots/B1_LAB5_DriverFactory_Chrome_FirstTests_Tests_LAB05_PhanB_Passed_20261120_194213_848_367b93.png) và [B3 – Browser không hợp lệ](screenshots/B3_LAB5_XuLyBrowserKhongHopLe_FirstTests_Tests_LAB05_PhanB_Passed_20261120_194217_112_2a99a7.png).  
**Yêu cầu so sánh 2 trình duyệt:** mã nguồn hỗ trợ Chrome/Firefox; ZIP ảnh cung cấp chưa tách một cặp ảnh chứng minh riêng hai lần chạy đổi duy nhất `Browser` trong JSON.

## C. BASETEST & SCREENSHOTHELPER (20%)

`Core/BaseTest.cs` gom `[SetUp]` đọc config, tạo driver, vào BaseUrl; `[TearDown]` chụp ảnh, `Quit()` và `Dispose()` trong `finally`. File `Utilities/ScreenshotHelper.cs` chịu trách nhiệm lưu ảnh và gắn vào kết quả NUnit. Các test phần A–D kế thừa `BaseTest` nên tránh lặp lại thao tác khởi tạo trình duyệt.

- `C01_BaseTest_DaMoDuongDanCauHinh`: **Passed**.
- `C02_CoYFail_DeChupAnh`: được đánh dấu `[Explicit]`, không chạy trong 13 ca của báo cáo.
- Cơ chế chụp hiện tại trong `BaseTest` thực hiện với mọi test có driver, không chỉ test Failed; để đáp ứng đúng điều kiện *chỉ chụp khi Failed* cần xét `TestContext.CurrentContext.Result.Outcome.Status` trước khi gọi Capture.

**Ảnh:** [C1 – SetUp và TearDown](screenshots/C1_LAB5_BaseTestSetUpTearDown_FirstTests_Tests_LAB05_PhanC_Passed_20261120_194220_098_4f00ab.png).

## D. DATA-DRIVEN TESTING VỚI JSON (25%)

**Nguồn dữ liệu:** `FirstTests/Data/users.json` gồm **8 bộ dữ liệu**, mỗi bộ có bốn trường `Username`, `Password`, `ThanhCong`, `GhiChu`.

| Ca | Username | Password | ThanhCong | Loại kiểm thử |
|---|---|---|---|---|
| 01 | `standard_user` | `secret_sauce` | true | Luồng thuận |
| 02 | `locked_out_user` | `secret_sauce` | false | Tài khoản bị khóa |
| 03 | *(rỗng)* | `secret_sauce` | false | Giá trị biên: username rỗng |
| 04 | `standard_user` | *(rỗng)* | false | Giá trị biên: password rỗng |
| 05 | `sai_tai_khoan` | `sai_mat_khau` | false | Cả hai giá trị không đúng |
| 06 | `!@#$%^&*` | `secret_sauce` | false | Username có ký tự đặc biệt |
| 07 | `standard_user  ` | `secret_sauce` | false | Username có khoảng trắng cuối |
| 08 | `khong_ton_tai_08` | `secret_sauce` | false | Tài khoản không tồn tại |

**Cách chạy:** `LAB05_PhanD.DuLieuDangNhap()` là phương thức `static` trả `IEnumerable<TestCaseData>`; test dùng `[TestCaseSource(nameof(DuLieuDangNhap))]` và `.SetName(...)`. Mỗi mục JSON tương ứng một ca trong Test Explorer. `InventoryPage.DaHienThi()` trả `false` khi đăng nhập thất bại và không để Timeout làm gián đoạn Assert thông thường.

**Kết quả:** 8 ca Data-Driven đều **Passed** trong lượt báo cáo; ngoài ra 5 ca kiểm tra Config/Factory/BaseTest cũng Passed.  
**Ảnh:** [D1 – Đăng nhập hợp lệ](screenshots/D1_LAB5_DangNhapTheoJSON_DangNhap_01_CoDuLieu_ThanhCong_FirstTests_Tests_LAB05_PhanD_Passed_20261120_194223_117_636aea.png) và [D1 – Username rỗng](screenshots/D1_LAB5_DangNhapTheoJSON_DangNhap_03_Trong_ThatBai_FirstTests_Tests_LAB05_PhanD_Passed_20261120_194230_859_7b90fa.png).

**Minh chứng tính mở rộng:** để chứng minh JSON tăng thêm 1 bộ mà không sửa `.cs`, cần đối chiếu số test trước/sau khi thêm dữ liệu và chụp Test Explorer ở hai lần. Báo cáo đính kèm chứng nhận lượt đang có 8 bộ.

## E. AI INTEGRATION – SINH VÀ THẨM ĐỊNH DỮ LIỆU (15%)

**1. Prompt sử dụng AI**

> Hãy đề xuất 10 bộ dữ liệu kiểm thử form đăng nhập SauceDemo dùng cho NUnit, Selenium C#. Kết quả trả về đúng JSON array với bốn trường `Username`, `Password`, `ThanhCong`, `GhiChu`. Có luồng hợp lệ, tài khoản khóa, dữ liệu sai và các ca biên như rỗng, khoảng trắng, ký tự đặc biệt. Tôi sẽ đánh giá lại kỳ vọng và tính hợp lệ của từng bộ trước khi đưa vào `users.json`.

**2. Kết quả AI trả về (10 bộ dữ liệu)**

```json
[
  {"Username":"standard_user","Password":"secret_sauce","ThanhCong":true,"GhiChu":"Tai khoan hop le"},
  {"Username":"locked_out_user","Password":"secret_sauce","ThanhCong":false,"GhiChu":"Tai khoan bi khoa"},
  {"Username":"","Password":"secret_sauce","ThanhCong":false,"GhiChu":"Username rong"},
  {"Username":"standard_user","Password":"","ThanhCong":false,"GhiChu":"Password rong"},
  {"Username":"sai_tai_khoan","Password":"sai_mat_khau","ThanhCong":false,"GhiChu":"Thong tin dang nhap sai"},
  {"Username":"!@#$%^&*","Password":"secret_sauce","ThanhCong":false,"GhiChu":"Username co ky tu dac biet"},
  {"Username":"standard_user  ","Password":"secret_sauce","ThanhCong":true,"GhiChu":"Cho phep username co khoang trang cuoi"},
  {"Username":"khong_ton_tai_08","Password":"secret_sauce","ThanhCong":false,"GhiChu":"Tai khoan khong ton tai"},
  {"Username":"locked_out_user","Password":"secret_sauce","ThanhCong":true,"GhiChu":"Tai khoan khoa van dang nhap duoc"},
  {"Username":"standard_user","Password":"sai_mat_khau","ThanhCong":false,"GhiChu":"Dung username, sai password"}
]
```

**3. Thẩm định hai bộ không phù hợp**

- **Bộ 07** đặt `ThanhCong: true` khi Username là `standard_user  ` (có hai khoảng trắng cuối). SauceDemo không mặc nhiên bỏ khoảng trắng cuối; chính file `users.json` hiện tại đã đặt kỳ vọng `false`. Cần sửa `ThanhCong` thành `false` trước khi sử dụng.
- **Bộ 09** đặt `ThanhCong: true` cho `locked_out_user`. Tài khoản này bị khóa và phải báo lỗi; kỳ vọng `true` không phù hợp. Cần sửa thành `false` hoặc loại bộ dữ liệu trùng với bộ 02.

**4. Kết luận**

AI hỗ trợ tạo nhanh dữ liệu đa dạng và đúng tên trường JSON. Tuy nhiên, kỳ vọng `ThanhCong` không thể chấp nhận chỉ vì định dạng hợp lệ. Hai bộ 07 và 09 có dự đoán sai hành vi đăng nhập nên tôi không dùng nguyên trạng. Bộ dữ liệu triển khai thực tế trong `Data/users.json` gồm tám ca với giá trị mong đợi đã được lựa chọn riêng.

## F. TEST REPORT

- **Tổng số:** 13 Passed / 0 Failed; 8 ca từ JSON và 5 ca cấu hình/factory/base test.
- **Báo cáo:** [PDF](TestReport/TestReport.pdf) · [Excel](TestReport/TestReport.xlsx).



-----------------------------------------------------------------------



# LAB 06 – TEST RUNNER, LOGGING, HTML REPORT VÀ PARALLEL TEST

**Họ và tên:** Trần Nguyễn Bình Yên  
**Mã sinh viên:** TB01913  
**Nhóm:** 07  
**Môn học:** SOF3091 – Kiểm thử nâng cao cùng AI  
**Ngày báo cáo:** 20/11/2026

**Website kiểm thử:** https://www.saucedemo.com/  
**Công nghệ:** C#, .NET, NUnit, Selenium WebDriver.  
**Kết quả NUnit trong TestReport được cung cấp:** **15 ca Passed, 0 ca Failed** (lượt chạy lúc 19:16 ngày 20/11/2026). Đây là số ca *được thực thi trong lượt báo cáo*, không phải xác nhận tự động hoàn thành mọi yêu cầu nộp minh chứng của đề.

---

## A. TỔ CHỨC CA KIỂM THỬ BẰNG CATEGORY (15%)

`FirstTests/Tests/LAB06_CategoryTests.cs` gồm:

- `LAB06_Smoke.A01_TrangDangNhapHienThi`: gắn `[Category("Smoke")]`, kiểm tra tiêu đề `Swag Labs`.
- `LAB06_Login.A02_DangNhapThanhCong`: gắn `[Category("Login")]`, kiểm tra đăng nhập hợp lệ.
- Các fixture Lab 5 kế thừa tiếp tục tham gia chạy theo nhóm; `LAB05_PhanA` cũng có `Smoke`, `LAB05_PhanD` cũng có `Login`.

**Lệnh chạy lọc:**

```bash
dotnet test --filter "Category=Smoke"
dotnet test --filter "Category!=Slow"
```

**Kết quả lượt Run All:** Smoke và Login đều có ca Passed trong báo cáo. Lệnh lọc `Category=Smoke` và `Category!=Slow` cần chạy riêng nếu muốn chứng minh số ca được chọn lọc; tổng report Run All không thay thế ảnh hai lệnh lọc.

**Ảnh:** [A1 – Smoke](screenshots/A1_LAB6_Smoke_TrangDangNhap_FirstTests_Tests_LAB06_Smoke_Passed_20261120_191649_941_d97db3.png) và [A2 – Login](screenshots/A2_LAB6_Login_DangNhapThanhCong_FirstTests_Tests_LAB06_Login_Passed_20261120_191646_386_59fb0e.png).

## B. LOGGING TRONG BASETEST (25%)

**Tệp:** `FirstTests/Utilities/Log.cs`, `FirstTests/Core/BaseTest.cs`.

- `Log.Info(message)` và `Log.Error(message)` ghi thời gian, mức độ, nội dung vào `TestContext.Progress` và tệp `.log`.
- `[SetUp]` ghi `BẮT ĐẦU: <tên test>`; `[TearDown]` ghi `KẾT QUẢ PASSED...` hoặc `RỚT...` nếu Failed.
- Dùng `lock` khi ghi file để tránh các luồng song song ghi chồng lên nhau.

**Đoạn nhật ký có trong tệp cung cấp:**

```text
[2026-11-20 19:16:29.957 +07:00] [INFO] BẮT ĐẦU: A01_DocCauHinhTuJson
[2026-11-20 19:16:50.033 +07:00] [INFO] KẾT QUẢ PASSED: A01_DocCauHinhTuJson
[2026-11-20 19:16:46.960 +07:00] [INFO] KẾT QUẢ PASSED: A02_DangNhapThanhCong
```

Đường dẫn: [NhatKy_20261120_191629_932.log](TestReport/NhatKy_20261120_191629_932.log). Trong lượt chạy được nộp không có ca Failed nên log này chưa chứa dòng `[ERROR]` của một test Failed. Mã nguồn có ca `[Explicit]` `B01_CoYFail_DeChungMinhErrorLog` để chạy riêng nếu cần chứng minh nhánh ERROR.

## C. TRX VÀ BÁO CÁO HTML (15%)

**Báo cáo hiện có:** `TestReport/TestReport.html` ghi **15 Test Cases, 15 Passed, 0 Failed**; các tệp PDF và Excel thống kê cùng lượt Run.

**Xuất TRX khi chạy NUnit:**

```bash
dotnet test --logger "trx;LogFileName=ketqua.trx"
```

TRX là tệp kết quả theo định dạng VSTest, có thể dùng công cụ tương thích để chuyển thành HTML. **Lưu ý:** file HTML hiện tại trong minh chứng được tạo trực tiếp từ kết quả NUnit bởi cơ chế report của project; trong ZIP chưa có `ketqua.trx` hoặc ảnh chứng minh thao tác chuyển đổi từ TRX sang HTML. Vì vậy, cần đối chiếu bước chuyển đổi riêng nếu tiêu chí chấm yêu cầu đúng đường đi TRX → HTML.

**Liên kết báo cáo:** [HTML](TestReport/TestReport.html) · [PDF](TestReport/TestReport.pdf) · [Excel](TestReport/TestReport.xlsx).

## D. PARALLEL TESTING VÀ THREAD-SAFETY (30%)

`FirstTests/AssemblyInfo.cs` đã cấu hình:

```csharp
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
```

NUnit có thể chạy tối đa bốn fixture đồng thời; các test bên trong cùng fixture được xử lý theo cấu hình fixture. `BaseTest` dùng `driver` thuộc instance test, không dùng một `static IWebDriver` chung. Khi nhiều fixture chạy song song, driver cần được khởi tạo và dọn dẹp tách biệt, tránh một ca đóng trình duyệt mà ca khác đang sử dụng.

**Đối chiếu:** log có nhiều dòng `BẮT ĐẦU` tại cùng thời điểm `19:16:29.957`, phù hợp với thực thi đồng thời. Báo cáo cung cấp một lượt chạy song song với 15 Passed; chưa có cặp số đo tuần tự/song song trên cùng bộ ca để tính tốc độ tăng và xác nhận kết quả hai chế độ tương đương. Có thể bật `Headless` trong `Config/appsettings.json` khi chạy nhiều browser để giảm tải giao diện.

## E. AI INTEGRATION – TEST CASE VÀ ASSERT CHO LOGIN (15%)

**1. Phần tử lựa chọn:** ô Username của SauceDemo, `By.CssSelector("[data-test='username']")`.

**2. Prompt sử dụng AI**

> Tôi kiểm thử trang đăng nhập SauceDemo bằng NUnit và Selenium C#. Hãy đề xuất ít nhất 5 ca kiểm thử cho trường Username, bao gồm username hợp lệ, rỗng, có khoảng trắng cuối, ký tự đặc biệt và tài khoản bị khóa. Kèm dữ liệu đầu vào, kết quả mong đợi và ví dụ NUnit Assert. Không thay đổi mã nguồn của ứng dụng. Tôi sẽ kiểm tra lại các kỳ vọng theo hành vi SauceDemo.

**3. Kết quả AI đề xuất**

| Ca | Username | Password | Kỳ vọng AI | Ví dụ Assert |
|---|---|---|---|---|
| AI-01 | `standard_user` | `secret_sauce` | Đăng nhập thành công | `Assert.That(inventory.DaHienThi(), Is.True);` |
| AI-02 | *(rỗng)* | `secret_sauce` | Không đăng nhập; báo lỗi | `Assert.That(login.CoThongBaoLoi(), Is.True);` |
| AI-03 | `standard_user  ` | `secret_sauce` | Tự xóa khoảng trắng, đăng nhập thành công | `Assert.That(inventory.DaHienThi(), Is.True);` |
| AI-04 | `!@#$%^&*` | `secret_sauce` | Không đăng nhập; báo lỗi | `Assert.That(inventory.DaHienThi(), Is.False);` |
| AI-05 | `locked_out_user` | `secret_sauce` | Không đăng nhập; thông báo bị khóa | `Assert.That(login.LayThongBaoLoi(), Does.Contain("locked out"));` |
| AI-06 | `standard_user` | *(rỗng)* | Trình duyệt chặn submit trước khi gửi biểu mẫu | `Assert.That(login.CoThongBaoLoi(), Is.False);` |

**4. Hai trường hợp AI đề xuất chưa phù hợp**

- **AI-03:** Dự đoán hệ thống tự loại bỏ hai khoảng trắng cuối là không phù hợp với kỳ vọng thực tế đã dùng trong `Data/users.json` của Lab 5–6. Giá trị phù hợp là `ThanhCong = false`; kiểm tra thông báo lỗi khi đăng nhập.
- **AI-06:** AI giả định trường Password rỗng bị HTML5 chặn trước khi submit. Test data có sẵn tại `users.json` được thiết kế để bấm Login và kiểm tra **thông báo lỗi của SauceDemo**, vì vậy Assert `CoThongBaoLoi() == false` là không phù hợp.

**5. Điều chỉnh Assert**

```csharp
bool dangNhapThanhCong = new LoginPage(driver, config.TimeoutSeconds)
    .DangNhap(username, password)
    .DaHienThi();
Assert.That(dangNhapThanhCong, Is.EqualTo(thanhCong));
```

**6. Nhận xét**

AI hỗ trợ liệt kê các ca biên ngoài luồng đăng nhập hợp lệ. Tuy nhiên, các dự đoán về tự loại bỏ khoảng trắng và HTML5 validation không phản ánh đúng kỳ vọng kiểm thử của SauceDemo. Tôi đối chiếu lại với dữ liệu `users.json` và logic `LoginPage` trước khi chọn Assert. Cách kiểm tra dựa trên trang sản phẩm có hiển thị và thông báo lỗi rõ ràng hơn việc chỉ dựa vào giá trị nhập.

## F. QUẢN LÝ MÃ NGUỒN VÀ KẾT QUẢ

**Ví dụ commit theo Conventional Commits:**

```text
test: them category Smoke va Login
feat: bo sung logging trong BaseTest
test: cau hinh chay song song cac fixture
docs: bo sung bao cao html va phan tham dinh ai
```

**Tổng kết:** 15 Passed / 0 Failed; 6 test classes trong report, bao gồm các test Lab 5 kế thừa. Bằng chứng đang có là ảnh, file PDF/XLSX/HTML và nhật ký, còn minh chứng lọc test, TRX → HTML, log ERROR, so sánh thời gian tuần tự/song song cần chạy ở các chế độ tương ứng.
