# SOF3091 – Kiểm thử nâng cao cùng AI (.NET) | Lab 1–4

**Sinh viên:** Trần Nguyễn Bình Yên — **MSSV:** TB01913 — **Nhóm:** 07  
**Project:** `FirstTests` — C#, NUnit, Selenium WebDriver  
**Repository nhóm:** [Lab01_Nhom7_SOF3091](https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091)

README này được đối chiếu với cấu trúc mã nguồn và tài liệu có trong repository. Các Lab sử dụng chung một project; **Lab 4 được tách riêng bằng hai thư mục `Pages_Lab04` và `Tests_Lab04`**, không di chuyển hoặc ghi đè test gốc của Lab 1–3.

## 1. Môi trường và chạy kiểm thử

Theo [`FirstTests.csproj`](FirstTests/FirstTests.csproj), project nhắm tới **.NET 10.0** và dùng các package: NUnit **4.3.2**, NUnit3TestAdapter **5.0.0**, Microsoft.NET.Test.Sdk **17.14.0**, Selenium.WebDriver và Selenium.Support **4.52.0**, cùng coverlet.collector **6.0.4**. Cần .NET SDK tương thích, Visual Studio có hỗ trợ .NET và Google Chrome.

1. Mở solution **[`FirstTests/FirstTests.slnx`](FirstTests/FirstTests.slnx)** trong Visual Studio (đây là `.slnx`, không phải `.sln`).
2. Vào **Build → Build Solution**; sau đó mở **Test → Test Explorer**.
3. Chọn **đúng lớp/nhóm test muốn chạy** rồi nhấn **Run**. Không dùng nút chạy project ▶ trên thanh công cụ như cách khởi động ứng dụng thông thường.
4. Có thể chạy lệnh tại thư mục `FirstTests/`:

   ```powershell
   dotnet restore
   dotnet build
   dotnet test
   ```

**Chú ý khi đối chiếu Lab 4:** Project có test cũ và test `_Lab04` cùng tồn tại. Nếu nhấn **Run All**, kết quả sẽ trộn cả hai bộ. Hai ca `A1_RotKhongWait` và `D1_CoYRot` được gắn `[Explicit]` để chạy riêng khi cần minh chứng test thất bại. Không tính chúng như lỗi phát sinh ngoài ý muốn.

## 2. Vị trí tài liệu và ảnh minh chứng

Các tệp Word minh chứng được lưu theo **phần A–D**, không phân chia thành thư mục Lab 1, Lab 2... Mỗi thư mục phần có đủ bốn file Word tương ứng Lab 1–4. **Phần E** của từng Lab (nếu có) được ghi trong báo cáo Lab hoặc README; không có thư mục `Phần E`.

### Báo cáo Lab chính

| Bài   | File Word                                                                  |
| ----- | -------------------------------------------------------------------------- |
| Lab 1 | [SOF3091_TB01913_LAB01.docx](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB01.docx) |
| Lab 2 | [SOF3091_TB01913_LAB02.docx](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB02.docx) |
| Lab 3 | [SOF3091_TB01913_LAB03.docx](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB03.docx) |
| Lab 4 | [SOF3091_TB01913_LAB04.docx](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB04.docx) |

### Word ảnh minh chứng theo thư mục thực tế

| Thư mục | Lab 1                                                                 | Lab 2                                                                 | Lab 3                                                                 | Lab 4                                                                 |
| ------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- |
| Phần A  | [Word Lab 1](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab1.docx) | [Word Lab 2](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab2.docx) | [Word Lab 3](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab3.docx) | [Word Lab 4](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab4.docx) |
| Phần B  | [Word Lab 1](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab1.docx) | [Word Lab 2](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab2.docx) | [Word Lab 3](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab3.docx) | [Word Lab 4](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab4.docx) |
| Phần C  | [Word Lab 1](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab1.docx) | [Word Lab 2](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab2.docx) | [Word Lab 3](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab3.docx) | [Word Lab 4](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab4.docx) |
| Phần D  | [Word Lab 1](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab1.docx) | [Word Lab 2](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab2.docx) | [Word Lab 3](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab3.docx) | [Word Lab 4](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab4.docx) |

Ví dụ: ảnh **Phần C của Lab 4** được cất trong [AnhPhanC_SOF3091_Lab4.docx](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab4.docx), **không phải** `screenshots/Lab04/C01.png`. Khi xem mã nguồn hay kiểm tra ảnh, mở đúng file Word nêu trong bảng.

Ngoài các Word trên, project có hai ảnh PNG **được tạo riêng cho test Lab 4 cố ý thất bại**: [A1_RotKhongWait_Lab04.png](FirstTests/screenshots/Lab04/A1_RotKhongWait_Lab04.png) và [D1_CoYRot_Lab04.png](FirstTests/screenshots/Lab04/D1_CoYRot_Lab04.png). **Hai ảnh này không thay thế ảnh so sánh trước/sau refactor của Phần C.**

> **Tình trạng minh chứng:** ZIP có đầy đủ các file Word nêu trên và ảnh đã nhúng trong các tài liệu. Tuy nhiên, một số báo cáo còn nhãn “VỊ TRÍ ẢNH”; cần kiểm tra trực tiếp nội dung Word trước khi nộp. README không tự xác nhận mọi ảnh hoặc mọi ca test đã Passed.

---

## 3. Lab 1 — Thiết lập môi trường và test Selenium đầu tiên

**Code:** [FirstSeleniumTest.cs](FirstTests/FirstSeleniumTest.cs).  
**Báo cáo:** [Word Lab 1](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB01.docx).

| Phần    | Nội dung thực hiện                                                                                             | Minh chứng                                                         |
| ------- | -------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------ |
| A (30%) | Cài đặt/kiểm tra .NET SDK, Visual Studio và môi trường.                                                        | [Word A1](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab1.docx) |
| B (40%) | Dùng Selenium mở `https://example.com`, lấy tiêu đề, Assert `Example Domain`, đóng Chrome trong `TearDown`.    | [Word B1](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab1.docx) |
| C (20%) | Quản lý mã nguồn bằng Git và GitHub.                                                                           | [Word C1](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab1.docx) |
| D (10%) | Dùng AI giải thích `[SetUp]`, `[TearDown]`, ChromeDriver, `Quit()` và `Dispose()`; có nhận xét về câu trả lời. | [Word D1](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab1.docx) |

**Test chính:** `FirstSeleniumTest.OpenPage_ShouldHaveExpectedTitle`. Trạng thái Passed/Failed cần đối chiếu Test Explorer hoặc báo cáo Word tại thời điểm chạy, không suy ra chỉ từ mã nguồn.

---

## 4. Lab 2 — Tương tác với trình duyệt và phần tử web

**Website:** [SauceDemo](https://www.saucedemo.com/).  
**Code:** [LoginTests.cs](FirstTests/LoginTests.cs); còn có [LoginTests2.cs](FirstTests/LoginTests2.cs) và [LoginTest_TB01915.cs](FirstTests/LoginTest_TB01915.cs).  
**Báo cáo:** [Word Lab 2](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB02.docx).

### Phần A — Bảng locator (20%)

Mỗi phần tử có ít nhất hai locator được đề xuất. Các locator dưới đây xuất phát từ bài Lab 2; kết quả `1 of 1` phải đối chiếu bằng DevTools và Word minh chứng, không mặc định tất cả selector luôn khớp duy nhất.

| Phần tử                 | Locator 1                                                                                         | Locator 2                                                                                                                                       | Lựa chọn / lý do                           |
| ----------------------- | ------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------ |
| Username                | `By.Id("user-name")`                                                                              | `By.Name("user-name")`                                                                                                                          | ID ngắn gọn, xác định rõ trường đăng nhập. |
| Password                | `By.Id("password")`                                                                               | `By.Name("password")`                                                                                                                           | ID chỉ rõ ô mật khẩu.                      |
| Login                   | `By.Id("login-button")`                                                                           | `By.Name("login-button")`                                                                                                                       | ID dễ đọc, không phụ thuộc thứ tự phần tử. |
| Tên Sauce Labs Backpack | `By.CssSelector("#item_4_title_link .inventory_item_name")`                                       | `By.XPath("//a[@id='item_4_title_link']//div[@data-test='inventory-item-name']")`                                                               | Giới hạn tìm đúng sản phẩm.                |
| Giá Sauce Labs Backpack | `By.CssSelector("div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']")` | `By.XPath("//a[@id='item_4_title_link']/ancestor::div[contains(@class,'inventory_item_description')]//div[@data-test='inventory-item-price']")` | Tìm giá trong cùng vùng sản phẩm.          |

Minh chứng DevTools: [Word A2](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab2.docx).

### Phần B — Đăng nhập thuận và nghịch (30%)

- `Login_Success`: dùng `standard_user` / `secret_sauce`, kiểm tra URL chứa `inventory.html`.
- `Login_LockedOutUser`: dùng `locked_out_user` / `secret_sauce`, kiểm tra thông báo bị khóa và không chuyển sang trang sản phẩm.

Minh chứng test: [Word B2](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab2.docx).

### Phần C — Thu thập sản phẩm (25%)

`GetProductInformation`: tìm danh sách **6 sản phẩm**, lấy tên và giá trong từng item, kiểm tra tên không rỗng, giá bắt đầu bằng `$` và danh sách có `Sauce Labs Backpack`. Minh chứng: [Word C2](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab2.docx).

### Phần D — AI hỗ trợ lựa chọn locator (10%)

Dùng `outerHTML` của ô Username để AI đề xuất và giải thích locator; người thực hiện kiểm tra lại bằng DevTools. Prompt, phản hồi và nhận xét: [Word D2](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab2.docx).

### Phần E — GitHub (15%)

Lưu các thay đổi trên **repository nhóm hiện tại**, quản lý commit theo Conventional Commits. Lịch sử và minh chứng xem trong [báo cáo Lab 2](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB02.docx) và repository; không tự xác nhận số commit nếu chưa đối chiếu Git.

---

## 5. Lab 3 — Đồng bộ, sắp xếp, Alert/iFrame và ảnh khi test thất bại

**Mã nguồn gốc:** [PhanA_Lab03.cs](FirstTests/PhanA_Lab03.cs), [PhanB_Lab03.cs](FirstTests/PhanB_Lab03.cs), [PhanC_Lab03.cs](FirstTests/PhanC_Lab03.cs), [PhanD_Lab03.cs](FirstTests/PhanD_Lab03.cs), [SortTests.cs](FirstTests/SortTests.cs) và lớp hỗ trợ [BaseTest.cs](FirstTests/BaseTest.cs).  
**Báo cáo:** [Word Lab 3](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB03.docx).

| Phần | Nội dung đã triển khai                                                                                    | Minh chứng                                                           |
| ---- | --------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------- |
| A    | `A1_RotKhongWait` tìm phần tử động ngay lập tức để minh chứng lỗi; `A2_PassCoWait` sử dụng Explicit Wait. | [Word A3](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab3.docx)   |
| B    | Dropdown `Price (low to high)`, lấy giá và Assert danh sách tăng dần.                                     | [Word B3](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab3.docx)   |
| C    | Alert, Confirm, Prompt và iFrame; kiểm tra nội dung hộp thoại, kết quả và thao tác thoát iFrame.          | [Word C3](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab3.docx)   |
| D    | Trong `TearDown`, chụp màn hình khi test Failed; `D1_CoYRot` cố ý thất bại để minh chứng.                 | [Word D3](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab3.docx)   |
| E    | Dùng AI chẩn đoán `NoSuchElementException` ở test không Wait và đánh giá cách sửa.                        | [Phần E trong báo cáo](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB03.docx) |

**Lưu ý:** Hai test `A1_RotKhongWait` và `D1_CoYRot` cố ý Failed, có `[Explicit]`; chỉ chọn chạy riêng khi cần minh chứng, không sửa chúng thành PASS.

---

## 6. Lab 4 — OOP và Page Object Model (POM)

**Mục tiêu:** Refactor test Lab 2–3 sang POM mà không thay đổi hành vi; toàn bộ mã nguồn mới nằm riêng trong `FirstTests/Pages_Lab04/` và `FirstTests/Tests_Lab04/`.  
**Báo cáo:** [Word Lab 4](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB04.docx).

### Cấu trúc Lab 4

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
├── Tests_Lab04/
│   ├── TestBase.cs
│   ├── LoginTests_Lab04.cs
│   ├── LoginTests2_Lab04.cs
│   ├── LoginTest_TB01915_Lab04.cs
│   ├── PhanA_Lab03_Lab04.cs
│   ├── PhanB_Lab03_Lab04.cs
│   ├── PhanC_Lab03_Lab04.cs
│   ├── PhanD_Lab03_Lab04.cs
│   └── SortTests_Lab04.cs
└── Các file Lab 1–3 gốc vẫn nằm ngoài hai thư mục trên
```

### Phần A — Cấu trúc và BasePage (20%)

[BasePage.cs](FirstTests/Pages_Lab04/BasePage.cs) là lớp `abstract`, quản lý `IWebDriver`, `WebDriverWait` và `Tim(By)` dùng chung. Các Page kế thừa BasePage, không khai báo lại `driver`/`wait`. Minh chứng: [Word A4](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab4.docx).

### Phần B — LoginPage và InventoryPage (25%)

- [LoginPage.cs](FirstTests/Pages_Lab04/LoginPage.cs): locator `private readonly By`, các hành vi đăng nhập và lấy lỗi.
- [InventoryPage.cs](FirstTests/Pages_Lab04/InventoryPage.cs): kiểm tra trang, sắp xếp sản phẩm và lấy danh sách giá.

Minh chứng: [Word B4](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab4.docx).

### Phần C — Refactor test Lab 2–3 (30%)

- Tám lớp test gốc Lab 2–3 có tổng **18 ca**. Bản POM cũng có **18 ca**, nằm riêng trong `Tests_Lab04`; không xóa hoặc thay đổi bản gốc.
- Đã đối chiếu mã nguồn: các file test `_Lab04.cs` không chứa `By`/`FindElement` trực tiếp; không có `Assert` trong Page Object; phương thức nghiệp vụ public không trả về `IWebElement`.
- Các test `[Explicit]` giữ nguyên mục đích. **Kết quả chạy trước/sau cần đối chiếu bằng Test Explorer**, không suy ra việc PASS từ việc code đúng cấu trúc.

**Minh chứng Phần C:** [Word C4](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab4.docx). Khi đánh giá refactor, so sánh cùng 18 ca ở hai nhóm, loại riêng test Lab 1 `FirstSeleniumTest` khỏi bộ đối chiếu.

### Phần D — Sắp xếp giá bằng method chaining (15%)

Trong [SortTests_Lab04.cs](FirstTests/Tests_Lab04/SortTests_Lab04.cs), test sử dụng chuỗi:

```csharp
login.DangNhap("standard_user", "secret_sauce")
     .SapXep("lohi")
     .LayDanhSachGia();
```

Sau đó đối chiếu danh sách giá và sử dụng `Is.Ordered.Ascending`. Minh chứng code và kết quả: [Word D4](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab4.docx).

### Phần E — Thẩm định AI (10%)

Gửi mã kiểm thử để AI hỗ trợ refactor, rồi tự kiểm tra các quy ước: kế thừa `BasePage`, locator `private readonly By`, `Assert` chỉ trong test, không lộ `IWebElement` từ phương thức nghiệp vụ, giữ nguyên hành vi và tách riêng `_Lab04`. Phần ghi nhận prompt, phản hồi và nhận xét nằm trong [báo cáo Lab 4](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB04.docx) (không có thư mục ảnh Phần E riêng).

**Điểm cần thẩm định:** Nếu cung cấp cho AI file `LoginTests_Lab04.cs` đã theo POM thì câu trả lời chỉ là rà soát/viết lại, **không phải** bằng chứng AI chuyển `LoginTests.cs` gốc từ script-based sang POM. Đồng thời cần giữ chức năng chụp ảnh test Failed trong `TestBase` nếu đã có từ bản hiện tại.

---

**Repository nộp:** https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091

---

# ASM1 — Kiểm thử nâng cao cùng AI (.NET) | Y1–Y4

**Sinh viên:** Trần Nguyễn Bình Yên · **MSSV:** TB01913 · **Nhóm:** 07  
**Môn:** SOF3091 · **Project:** `FirstTests` · **Ngôn ngữ:** C#  
**Repository nhóm:** [Lab01_Nhom7_SOF3091](https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091)  
**Website kiểm thử:** [SauceDemo](https://www.saucedemo.com/) và [The Internet](https://the-internet.herokuapp.com/)

README này mô tả **riêng Y1–Y4 của ASM1**, dựa trên `ASM1.pdf` và mã nguồn trong `FirstTests/ASM1_TB01913/`. Các mã nguồn Lab 1–3 và thư mục `Pages_Lab04`, `Tests_Lab04` được giữ riêng; phần ASM không thay thế các bài Lab gốc.

## 1. Công nghệ và cách chạy

| Thành phần | Công nghệ / phiên bản từ project |
|---|---|
| Nền tảng | .NET 10 (`net10.0`), C# |
| Kiểm thử | NUnit 4.3.2, NUnit3TestAdapter 5.0.0, Microsoft.NET.Test.Sdk 17.14.0 |
| Tự động hóa | Selenium.WebDriver 4.52.0, Selenium.Support 4.52.0 |
| Trình duyệt | Google Chrome; ChromeDriver do Selenium Manager quản lý |
| Thiết kế | `WebDriverWait`, Page Object Model (POM), Test Explorer |

Mở [`FirstTests/FirstTests.slnx`](FirstTests/FirstTests.slnx) bằng Visual Studio, chọn **Build → Build Solution**, sau đó mở **Test → Test Explorer** để chạy đúng nhóm ASM. Có thể chạy bằng Terminal tại **thư mục gốc repository**:

```powershell
dotnet restore FirstTests/FirstTests.csproj
dotnet build FirstTests/FirstTests.csproj
dotnet test FirstTests/FirstTests.csproj --filter "FullyQualifiedName~FirstTests.ASM1_TB01913" --logger "console;verbosity=normal"
```

> **Lưu ý:** Đây là lệnh chạy các test thuộc namespace ASM; kết quả thực tế chỉ xác định sau khi chạy trên máy. Các test Lab 3 được đánh dấu `[Explicit]` để minh chứng thất bại có chủ đích, không nên chạy toàn bộ Solution rồi lấy kết quả đó thay cho kết quả ASM.

## 2. Cấu trúc mã nguồn liên quan đến Y1–Y4

```text
FirstTests/
├── FirstTests.slnx
├── FirstTests.csproj
├── ASM1_TB01913/
│   ├── ASM1_TestBase.cs
│   ├── ASM1_Y1_TieuDeTests.cs
│   ├── ASM1_Y2_GioHangTests.cs
│   ├── ASM1_Y3_ExplicitWaitTests.cs
│   ├── ASM1_Y4_POMTests.cs
│   └── Pages/
│       ├── ASM1_InventoryCartPage.cs
│       └── ASM1_CartPage.cs
├── Pages_Lab04/
│   ├── BasePage.cs
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   └── DynamicLoadingPage.cs
├── Tests_Lab04/              # Giữ riêng test đã refactor của Lab 4
└── *.cs                      # Giữ nguyên các bài Lab 1–3
```

Các test ASM dùng lớp `ASM1_TestBase` để khởi tạo và đóng Chrome. Đối với Y4, test gọi phương thức từ Page Object, không đặt locator trực tiếp trong file test.

## 3. Y1 — Thiết lập môi trường & test đầu tiên

**Yêu cầu:** Có project NUnit/Selenium build được; mở SauceDemo, kiểm tra tiêu đề trang và có ít nhất một test Passed; sử dụng Git với Conventional Commits.

**File:** [`ASM1_Y1_TieuDeTests.cs`](FirstTests/ASM1_TB01913/ASM1_Y1_TieuDeTests.cs)

```csharp
[Test]
public void Y1_OpenSauceDemo_CheckPageTitle()
{
    string title = driver.Title;
    TestContext.WriteLine("Page title: " + title);
    Assert.That(title, Is.EqualTo("Swag Labs"));
}
```

`ASM1_TestBase` mở SauceDemo trong `[SetUp]`, vì vậy test đọc `driver.Title` và xác nhận bằng NUnit. **Đây là test ASM riêng**; bài Lab 1 gốc kiểm tra tiêu đề `example.com` và không được dùng thay ảnh SauceDemo.

**Minh chứng cần có:** ảnh `Build succeeded`, test `Y1_OpenSauceDemo_CheckPageTitle` Passed, Output có tiêu đề trang; ảnh commit Git theo Conventional Commits. **Chưa xác nhận kết quả chạy/commit từ mã nguồn ZIP.**

**Tài liệu liên quan:** [Lab 1 — Phần A](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab1.docx), [Phần B](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab1.docx), [Phần C](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab1.docx).

## 4. Y2 — Tự động hóa luồng với Selenium

**Yêu cầu:** Đăng nhập bằng Username/Password, xác nhận trang sản phẩm, dùng locator ổn định; bổ sung luồng thêm sản phẩm vào giỏ và mở giỏ hàng.

**File:** [`ASM1_Y2_GioHangTests.cs`](FirstTests/ASM1_TB01913/ASM1_Y2_GioHangTests.cs)  
**Page Object:** [`LoginPage.cs`](FirstTests/Pages_Lab04/LoginPage.cs), [`ASM1_InventoryCartPage.cs`](FirstTests/ASM1_TB01913/Pages/ASM1_InventoryCartPage.cs), [`ASM1_CartPage.cs`](FirstTests/ASM1_TB01913/Pages/ASM1_CartPage.cs).

**Luồng chính:**

1. Đăng nhập với tài khoản demo `standard_user` / `secret_sauce`.
2. Xác nhận trang danh sách sản phẩm hiển thị.
3. Thêm **Sauce Labs Backpack** vào giỏ.
4. Kiểm tra badge giỏ hàng bằng `"1"`; mở giỏ và kiểm tra đúng tên, số lượng sản phẩm.

```csharp
InventoryPage inventory = new LoginPage(driver)
    .DangNhap("standard_user", "secret_sauce");
Assert.That(inventory.DaHienThi(), Is.True);

ASM1_InventoryCartPage page = new ASM1_InventoryCartPage(driver)
    .ThemBaloVaoGio();
Assert.That(page.LaySoLuongGio(), Is.EqualTo("1"));

ASM1_CartPage cart = page.MoGioHang();
Assert.That(cart.LayTenSanPham(), Is.EqualTo("Sauce Labs Backpack"));
```

Các locator được đóng gói trong Page Object. Test Y2 không gọi trực tiếp `FindElement(By...)`.

**Minh chứng cần có:** test `Y2_AddProductToCart_Backpack` Passed, màn hình giỏ hàng có sản phẩm và Test Explorer. **Kết quả chạy chưa được xác nhận.**

**Tài liệu liên quan:** [Lab 2 — Phần A (locator)](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab2.docx), [Phần B (đăng nhập)](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab2.docx), [Phần C (sản phẩm)](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab2.docx).

## 5. Y3 — Tình huống thực tế & Explicit Wait

**Yêu cầu:** Không có `Thread.Sleep` trong mã nguồn; xử lý phần tử tải chậm bằng `WebDriverWait`; chứng minh **3 lần kiểm thử liên tiếp đều Passed**.

**File:** [`ASM1_Y3_ExplicitWaitTests.cs`](FirstTests/ASM1_TB01913/ASM1_Y3_ExplicitWaitTests.cs)  
**Page Object:** [`DynamicLoadingPage.cs`](FirstTests/Pages_Lab04/DynamicLoadingPage.cs).

```csharp
[TestCase(1)]
[TestCase(2)]
[TestCase(3)]
public void Y3_DynamicLoading_CoWait_LapBaLan(int lan)
{
    string actual = new DynamicLoadingPage(driver)
        .MoTrang().BatDau().LayKetQuaCoWait();
    Assert.That(actual, Is.EqualTo("Hello World!"));
}
```

Trang Dynamic Loading tạo kết quả sau khi nhấn **Start**. `LayKetQuaCoWait()` chờ phần tử xuất hiện, thay cho việc tìm ngay như test `A1_RotKhongWait` của Lab 3. Ba `[TestCase]` tạo ba lần thực thi; **phải chạy thực tế và chụp kết quả cả ba đều xanh**, không suy ra PASS chỉ từ khai báo.

**Minh chứng cần có:** ảnh Test Explorer với ba ca `Y3_DynamicLoading_CoWait_LapBaLan` Passed; ảnh hoặc kết quả tìm kiếm `Thread.Sleep` trong mã C# không có kết quả. Có thể tham khảo [Lab 3 — Phần A](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab3.docx) để minh họa tình huống không Wait/có Wait.

## 6. Y4 — OOP & Page Object Model (POM)

**Yêu cầu:** Dùng `BasePage` chung với hàm tìm phần tử kết hợp chờ; tách Login/Inventory/Cart thành Page Object; locator nằm trong `private readonly By`, test không thao tác trực tiếp locator; chứng minh thay locator tại **một nơi** không cần sửa test.

**File test:** [`ASM1_Y4_POMTests.cs`](FirstTests/ASM1_TB01913/ASM1_Y4_POMTests.cs)  
**Lớp Page:** [`BasePage.cs`](FirstTests/Pages_Lab04/BasePage.cs), [`LoginPage.cs`](FirstTests/Pages_Lab04/LoginPage.cs), [`InventoryPage.cs`](FirstTests/Pages_Lab04/InventoryPage.cs), [`ASM1_CartPage.cs`](FirstTests/ASM1_TB01913/Pages/ASM1_CartPage.cs).

Các test Y4 kiểm tra đăng nhập, danh sách 6 sản phẩm, tên sản phẩm, sắp xếp giá tăng dần và giỏ hàng. Đoạn **method chaining** trong bài sắp xếp giá:

```csharp
var prices = new LoginPage(driver)
    .DangNhap("standard_user", "secret_sauce")
    .SapXep("lohi")
    .LayDanhSachGia();
Assert.That(prices, Is.Ordered.Ascending);
```

### Minh chứng ASM-Y4-DOI-LOCATOR

Chỉ sửa locator Username trong **`Pages_Lab04/LoginPage.cs`**:

**Trước khi thay:**

```csharp
private readonly By oTaiKhoan = By.Id("user-name");
```

**Sau khi thay:**

```csharp
private readonly By oTaiKhoan = By.CssSelector("[data-test='username']");
```

Sau khi lưu, Build và chạy lại nhóm `ASM1_Y4_POMTests`. **Không sửa file test.** Chụp đoạn locator trước/sau và hai lượt Test Explorer trước/sau. Chỉ kết luận lợi ích POM được chứng minh khi test vẫn Passed thực tế.

**Minh chứng cần có:** ảnh cấu trúc Page Object, test Y4 Passed, kiểm tra test không chứa `By`, và ảnh thay locator. Xem [Lab 4 — Phần A](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab4.docx), [Phần B](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab4.docx), [Phần C](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab4.docx), [Phần D](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab4.docx).

## 7. Nơi lưu báo cáo và ảnh minh chứng

**Báo cáo ASM:** [ASM1_TB01913_NHOM7.docx](ASM1_TB01913_NHOM7.docx).  
**Báo cáo Lab:** [Lab 1](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB01.docx) · [Lab 2](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB02.docx) · [Lab 3](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB03.docx) · [Lab 4](LAB_TB01913_NHOM7/SOF3091_TB01913_LAB04.docx).

**Quan trọng:** Ảnh minh chứng Lab 1–4 được lưu **bên trong file Word** thuộc bốn thư mục `screenshots/Phần A`, `Phần B`, `Phần C`, `Phần D`. Không mặc định có ảnh PNG rời hoặc thư mục `screenshots/Lab04` cho tất cả minh chứng.

| Thư mục | Lab 1 | Lab 2 | Lab 3 | Lab 4 |
|---|---|---|---|---|
| Phần A | [Word A1](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab1.docx) | [Word A2](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab2.docx) | [Word A3](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab3.docx) | [Word A4](screenshots/Ph%E1%BA%A7n%20A/AnhPhanA_SOF3091_Lab4.docx) |
| Phần B | [Word B1](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab1.docx) | [Word B2](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab2.docx) | [Word B3](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab3.docx) | [Word B4](screenshots/Ph%E1%BA%A7n%20B/AnhPhanB_SOF3091_Lab4.docx) |
| Phần C | [Word C1](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab1.docx) | [Word C2](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab2.docx) | [Word C3](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab3.docx) | [Word C4](screenshots/Ph%E1%BA%A7n%20C/AnhPhanC_SOF3091_Lab4.docx) |
| Phần D | [Word D1](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab1.docx) | [Word D2](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab2.docx) | [Word D3](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab3.docx) | [Word D4](screenshots/Ph%E1%BA%A7n%20D/AnhPhanD_SOF3091_Lab4.docx) |

Những kết quả **riêng ASM1** như tiêu đề SauceDemo, giỏ hàng, ba lần PASS và đổi locator chưa thể thay bằng ảnh Lab nếu ảnh Lab không chứng minh đúng test. Hãy chèn ảnh chạy thực tế vào vị trí tương ứng trong báo cáo ASM.

## 8. Danh sách ảnh cần bổ sung cho ASM Y1–Y4

| Mã minh chứng | Thao tác / nội dung phải chụp | Tình trạng |
|---|---|---|
| `ASM-Y1` | Build Solution và test tiêu đề SauceDemo Passed | Chờ ảnh thực tế |
| `ASM-GIT` | Lịch sử commit phù hợp Conventional Commits | Chờ ảnh thực tế |
| `ASM-Y2-CART` | Test giỏ hàng Passed, tên và số lượng sản phẩm | Chờ ảnh thực tế |
| `ASM-Y3-3LAN` | Ba test Dynamic Loading chạy đều Passed | Chờ ảnh thực tế |
| `ASM-Y4` | Các test POM Passed, kiểm tra không có locator trong test | Chờ ảnh thực tế |
| `ASM-Y4-DOI-LOCATOR` | Locator trước/sau và kết quả test trước/sau | Chờ ảnh thực tế |

> **Trạng thái xác minh:** README mô tả mã nguồn và cách chạy đã có trong ZIP; chưa có kết quả xác nhận `dotnet test` hoặc ảnh Test Explorer cho các bài ASM mới. Không ghi kết quả PASS/FAIL cho đến khi chạy thực tế.
