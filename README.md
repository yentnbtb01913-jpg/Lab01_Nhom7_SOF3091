# Lab 1 – Nhập môn kiểm thử tự động \& Thiết lập môi trường

**Sinh viên:** Trần Nguyễn Bình Yên – Lớp SD2003 – FPT Polytechnic Đồng Nai

## Công nghệ

.NET SDK (11.0.100-rc.2), C#, NUnit, Selenium.WebDriver 4.52.0, Google Chrome, Visual Studio 2026

## Cách chạy project

1. Cài .NET SDK, Visual Studio (workload ".NET desktop development") và Google Chrome
2. Clone repo về máy:

```
   git clone https://github.com/yentnbtb01913-jpg/Lab01\_Nhom7\_SOF3091.git
   ```

3. Mở file `.sln` bằng Visual Studio
4. Menu **Test** → **Test Explorer** → **Run All Tests**
5. Chrome tự bật lên, vào trang example.com rồi tự đóng; test hiển thị **Passed**

> Lưu ý: không chạy bằng nút ▶ trên thanh công cụ (nút này chạy project như chương trình thường, không chạy test).

## Nội dung test

`FirstSeleniumTest.OpenPage\_ShouldHaveExpectedTitle`:
mở Chrome → vào https://example.com → đọc và in tiêu đề trang → `Assert` tiêu đề là "Example Domain" → đóng trình duyệt bằng `Quit()`.

## Ảnh minh chứng

Xem thư mục [`screenshots/`](screenshots/):

* File `.csproj` có gói Selenium.WebDriver
* Kết quả test **Passed**
* Lịch sử commit

\---

## Phần D – Trải nghiệm dùng AI

### Tình huống: nhờ AI giải thích code đang viết

**Tình huống:** Test đã Passed nhưng mình chưa hiểu vì sao code chạy được, nên nhờ AI giải thích 3 điểm.

**Prompt đã dùng** (mình dán toàn bộ code `FirstSeleniumTest.cs` kèm 3 câu hỏi):

> "Mình là sinh viên đang làm Lab 1 (NUnit + Selenium bằng C# trong Visual Studio). \[...code FirstSeleniumTest.cs...] Mình chưa hiểu mấy điểm sau, giải thích ngắn gọn bằng tiếng Việt, dễ hiểu:
>
> 1. \[SetUp], \[Test], \[TearDown] chạy theo thứ tự nào, và tại sao đóng Chrome để trong TearDown mà không để cuối hàm test?
> 2. Mình không hề tải ChromeDriver mà Chrome vẫn mở được, vì sao?
> 3. driver.Quit() khác driver.Close() ở điểm nào, và tại sao code vẫn gọi thêm Dispose()?"

**Câu trả lời nhận được:**

1. Với mỗi test, NUnit chạy `\[SetUp]` → `\[Test]` → `\[TearDown]`. Đóng Chrome ở `\[TearDown]` vì khi `Assert` sai, hàm test dừng ngay và các dòng phía sau không chạy; còn `\[TearDown]` luôn được chạy nên Chrome luôn được đóng.
2. Do Selenium bản mới (từ 4.6) có **Selenium Manager** tự tìm Chrome trên máy và tự tải ChromeDriver khớp phiên bản. AI nói thẳng điểm này dựa trên kiến thức của nó, chưa tra nguồn.
3. `Close()` chỉ đóng cửa sổ đang focus, còn `Quit()` đóng tất cả cửa sổ và kết thúc phiên WebDriver. Trong bản C#, `Quit()` gọi `Dispose()`, nên gọi thêm `Dispose()` sau `Quit()` là dư nhưng không gây hại (AI nói đây là suy luận từ các nguồn nó tra được). AI cũng cho biết khi tra web thì nguồn mâu thuẫn: một câu trả lời cũ nói `Dispose()` đã bị bỏ, người khác phản bác là vẫn còn.

### Nhận xét

AI giải thích khá dễ hiểu, nhất là vì sao phải đóng Chrome trong \[TearDown]: assert sai thì test dừng luôn, để Quit() ở cuối là Chrome bị bỏ lại. Chỗ chưa chắc là Quit() và Dispose(), vì các nguồn AI tra được nói ngược nhau, còn phần Selenium Manager thì AI chỉ nói theo kiến thức chứ chưa có nguồn.

---

# Lab 2 – Selenium WebDriver cơ bản: Tương tác với trình duyệt và phần tử web

**Môn học:** SOF3091  
**Website kiểm thử:** [SauceDemo – Swag Labs](https://www.saucedemo.com/)  
**Dự án:** Tiếp tục sử dụng project `FirstTests` và repository của Lab 1.  
**Mã nguồn Lab 2:** `FirstTests/LoginTests.cs` (kiểm tra đường dẫn thực tế trong repository).  

> **Tình trạng minh chứng:** Các vị trí ảnh và kết quả kiểm thử bên dưới được để sẵn để cập nhật từ Chrome DevTools và Visual Studio. Không mặc định rằng test đã Passed hay locator đã khớp `1 of 1` trước khi có ảnh thực tế.

## Phần A – Bảng locator (20%)

Đã lựa chọn 5 phần tử trên SauceDemo. Mỗi phần tử có ít nhất 2 cách định vị; locator sử dụng trong bài cần được kiểm tra trên DOM thực tế bằng DevTools `Ctrl + F`.

| STT | Phần tử | Locator cách 1 | Locator cách 2 | Locator chọn dùng | Lý do lựa chọn |
|:--:|---|---|---|---|---|
| 1 | Ô Username | `By.Id("user-name")` | `By.Name("user-name")` | `By.Id("user-name")` | ID mô tả trực tiếp ô Username, ngắn gọn và dễ đọc. |
| 2 | Ô Password | `By.Id("password")` | `By.Name("password")` | `By.Id("password")` | ID chỉ rõ ô Password, không dựa trên thứ tự hiển thị. |
| 3 | Nút Login | `By.Id("login-button")` | `By.Name("login-button")` | `By.Id("login-button")` | ID định vị trực tiếp nút Login, thuận tiện cho thao tác Click. |
| 4 | Tên sản phẩm Sauce Labs Backpack | `By.CssSelector("#item_4_title_link .inventory_item_name")` | `By.XPath("//a[@id='item_4_title_link']//div[@data-test='inventory-item-name']")` | CSS Selector theo ID sản phẩm | Giới hạn vào tên của một sản phẩm cụ thể thay vì khớp toàn bộ tên sản phẩm. |
| 5 | Giá sản phẩm Sauce Labs Backpack | `By.CssSelector("div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']")` | `By.XPath("//a[@id='item_4_title_link']/ancestor::div[contains(@class,'inventory_item_description')]//div[@data-test='inventory-item-price']")` | CSS Selector theo vùng sản phẩm | Chỉ tìm giá thuộc sản phẩm Sauce Labs Backpack, tránh nhầm với giá khác. |

**Ghi nhận kiểm chứng:** Các locator trên là nội dung đề xuất để kiểm tra. Chỉ ghi “`1 of 1`” sau khi DevTools thực sự cho kết quả duy nhất và tô sáng đúng phần tử. Nếu DOM hiện tại khác ví dụ, cập nhật bảng theo HTML thực tế trước khi nộp. Không dùng XPath tuyệt đối bắt đầu bằng `/html/body/...`.

### A.1. Ảnh minh chứng Username

**Cách 1 – ID:** `#user-name`  
![Minh chứng Username bằng ID](screenshots/A01_Username_ID.png)

**Cách 2 – Name:** `[name='user-name']`  
![Minh chứng Username bằng Name](screenshots/A02_Username_Name.png)

### A.2. Ảnh minh chứng Password

**Cách 1 – ID:** `#password`  
![Minh chứng Password bằng ID](screenshots/A03_Password_ID.png)

**Cách 2 – Name:** `[name='password']`  
![Minh chứng Password bằng Name](screenshots/A04_Password_Name.png)

### A.3. Ảnh minh chứng Login

**Cách 1 – ID:** `#login-button`  
![Minh chứng Login bằng ID](screenshots/A05_Login_ID.png)

**Cách 2 – Name:** `[name='login-button']`  
![Minh chứng Login bằng Name](screenshots/A06_Login_Name.png)

### A.4. Ảnh minh chứng tên sản phẩm

**Cách 1 – CSS Selector:** `#item_4_title_link .inventory_item_name`  
![Minh chứng tên sản phẩm bằng CSS Selector](screenshots/A07_ProductName_CSS.png)

**Cách 2 – XPath tương đối:** `//a[@id='item_4_title_link']//div[@data-test='inventory-item-name']`  
![Minh chứng tên sản phẩm bằng XPath](screenshots/A08_ProductName_XPath.png)

### A.5. Ảnh minh chứng giá sản phẩm

**Cách 1 – CSS Selector:** `div.inventory_item:has(#item_4_title_link) [data-test='inventory-item-price']`  
![Minh chứng giá sản phẩm bằng CSS Selector](screenshots/A09_ProductPrice_CSS.png)

**Cách 2 – XPath tương đối:** `//a[@id='item_4_title_link']/ancestor::div[contains(@class,'inventory_item_description')]//div[@data-test='inventory-item-price']`  
![Minh chứng giá sản phẩm bằng XPath](screenshots/A10_ProductPrice_XPath.png)

> **Ảnh phần A:** Mỗi ảnh cần thể hiện đúng locator đang tìm, dòng HTML được chọn và chỉ số kết quả của DevTools. Các liên kết ảnh sẽ hiển thị sau khi các tệp `.png` tương ứng được thêm vào thư mục `screenshots/`.

## Phần B – Kịch bản đăng nhập thuận và nghịch (30%)

Các ca kiểm thử được triển khai trong `LoginTests.cs`, sử dụng `[SetUp]`, `[Test]`, `[TearDown]` và hàm `Login(username, password)` dùng chung. Sau mỗi ca, code gọi `driver.Quit()` và `driver.Dispose()`.

| Ca kiểm thử | Dữ liệu đầu vào | Kết quả mong đợi | Câu lệnh kiểm tra chính | Kết quả chạy thực tế |
|---|---|---|---|---|
| `Login_Success` | `standard_user` / `secret_sauce` | Đăng nhập thành công, chuyển sang `inventory.html`. | `Assert.That(currentUrl, Does.Contain("inventory.html"))` | **[Điền sau khi chạy]** |
| `Login_LockedOutUser` | `locked_out_user` / `secret_sauce` | Thông báo lỗi hiển thị đúng nội dung, không chuyển sang trang sản phẩm. | Kiểm tra `errorMessage.Displayed`, nội dung thông báo và URL không chứa `inventory.html`. | **[Điền sau khi chạy]** |

**Nội dung thông báo mong đợi cho tài khoản bị khóa:**

```text
Epic sadface: Sorry, this user has been locked out.
```

### B.1. Ảnh kết quả kiểm thử

![Test Explorer - hai ca đăng nhập Passed](screenshots/B01_LoginTests_2Passed.png)

**Ghi nhận thực tế:** [Điền tổng số Passed / Failed và kết quả hai ca sau khi chạy.]  
**Ảnh riêng nếu cần:** [Chèn thêm ảnh chi tiết `Login_Success` và `Login_LockedOutUser` nếu một ảnh tổng hợp không hiển thị đủ.]

## Phần C – Thu thập dữ liệu sản phẩm (25%)

Ca kiểm thử `GetProductInformation()` trong `LoginTests.cs` tái sử dụng hàm `Login("standard_user", "secret_sauce")`, sau đó đọc danh sách sản phẩm bằng `FindElements`:

```csharp
IReadOnlyCollection<IWebElement> items =
    driver.FindElements(By.CssSelector("[data-test='inventory-item']"));
```

Trong vòng lặp `foreach`, tên và giá được lấy **bên trong từng sản phẩm** bằng `item.FindElement(...)`:

```csharp
string name = item.FindElement(By.CssSelector("[data-test='inventory-item-name']")).Text;
string price = item.FindElement(By.CssSelector("[data-test='inventory-item-price']")).Text;
```

Bài kiểm thử in tên và giá của từng sản phẩm ra kết quả chạy, đồng thời có các Assert:

| STT | Nội dung kiểm tra | Điều kiện mong đợi | Kết quả thực tế |
|:--:|---|---|---|
| 1 | Số lượng sản phẩm | `items.Count == 6` | **[Điền sau khi chạy]** |
| 2 | Tên sản phẩm | Tất cả tên không rỗng (`Has.All.Not.Empty`) | **[Điền sau khi chạy]** |
| 3 | Giá sản phẩm | Tất cả giá bắt đầu bằng `$` (`Has.All.StartsWith("$")`) | **[Điền sau khi chạy]** |
| 4 | Sản phẩm cụ thể | Danh sách chứa `Sauce Labs Backpack` | **[Điền sau khi chạy]** |

### C.1. Kết quả tên và giá sản phẩm

| STT | Tên sản phẩm lấy từ Test Output | Giá lấy từ Test Output |
|:--:|---|---|
| 1 | [Điền theo kết quả chạy] | [Điền] |
| 2 | [Điền theo kết quả chạy] | [Điền] |
| 3 | [Điền theo kết quả chạy] | [Điền] |
| 4 | [Điền theo kết quả chạy] | [Điền] |
| 5 | [Điền theo kết quả chạy] | [Điền] |
| 6 | [Điền theo kết quả chạy] | [Điền] |

**Ảnh Test Output có tên và giá 6 sản phẩm:**

![Test Output - danh sách sản phẩm](screenshots/C01_ProductOutput.png)

**Ảnh Test Explorer có ca `GetProductInformation`:**

![Test Explorer - kết quả kiểm thử sản phẩm](screenshots/C02_ProductPassed.png)

**Trạng thái ca kiểm thử:** [Điền Passed hoặc Failed theo kết quả thực tế.]

## Phần D – Lồng ghép AI (10%)

### D.1. Phần tử được chọn và outerHTML

**Phần tử:** Ô Username tại trang đăng nhập SauceDemo.

**OuterHTML đã sao chép từ Chrome DevTools:**

```html
[DÁN NGUYÊN VĂN OUTERHTML ĐÃ SAO CHÉP TỪ DEVTOOLS VÀO ĐÂY]
```

### D.2. Prompt đã gửi AI

> [DÁN NGUYÊN VĂN PROMPT ĐÃ THỰC SỰ GỬI CHO AI. Chưa có nội dung prompt nguyên văn trong README gốc.]

### D.3. Câu trả lời AI đã nhận

**Phân tích thuộc tính từ outerHTML (theo câu trả lời AI):**

| Thuộc tính | Giá trị được AI nêu | Nhận xét của AI |
|---|---|---|
| `id` | `user-name` | Định danh của phần tử, theo chuẩn HTML nên duy nhất. |
| `data-test` | `username` | Thuộc tính dành cho kiểm thử, ít đổi khi sửa giao diện. |
| `name` | `user-name` | Tên trường của form, khá ổn định. |
| `aria-label` | `Username` | Nhãn hỗ trợ truy cập, phụ thuộc nội dung chữ. |
| `placeholder` | `Username` | Chữ gợi ý, dễ đổi khi sửa nội dung hoặc ngôn ngữ. |
| `class` | `input_error form_input` | Dùng để tạo kiểu, dễ đổi hoặc trùng phần tử khác. |
| `type` | `text` | Quá chung để nhận diện riêng. |
| `autocorrect`, `autocapitalize`, `value` | `off`, `none`, rỗng | Cấu hình nhập liệu, không ưu tiên định vị. |

**Các locator Selenium C# AI đề xuất:**

```csharp
By.Id("user-name")                                  // L1
By.CssSelector("[data-test='username']")            // L2
By.Name("user-name")                                // L3
By.CssSelector("[aria-label='Username']")           // L4
By.CssSelector("input.input_error.form_input")      // L5 (không khuyến khích)
```

| Locator AI đề xuất | Chuỗi kiểm tra tại DevTools (Ctrl+F) | Đánh giá độ ổn định của AI |
|---|---|---|
| L1 – ID | `#user-name` | Cao nhất; ngắn, không phụ thuộc vị trí. |
| L2 – data-test | `[data-test="username"]` | Cao; hướng đến kiểm thử. |
| L3 – Name | `[name="user-name"]` | Trung bình–cao; `name` không bắt buộc duy nhất. |
| L4 – aria-label | `[aria-label="Username"]` | Trung bình; có thể đổi theo ngôn ngữ. |
| L5 – Class | `input.input_error.form_input` | Thấp; có thể đổi hoặc trùng phần tử khác. |

**Locator AI lựa chọn:** `By.Id("user-name")`.

**Lý do AI đưa ra:** Ưu tiên ID vì ngắn gọn, mô tả danh tính của ô Username và không phụ thuộc đường đi trong DOM; locator theo `data-test` là phương án dự phòng. AI cũng yêu cầu tự kiểm chứng vì chưa biết có phần tử khác trùng selector hay không.

### D.4. Kết quả tự kiểm chứng

| Locator kiểm tra | Kết quả Ctrl+F | Phần tử được tô sáng | Đánh giá |
|---|---|---|---|
| `#user-name` | [Điền `1 of 1` hoặc kết quả thật] | [Điền] | [Điền] |
| `[data-test='username']` | [Điền kết quả thật] | [Điền] | [Điền] |

**Ảnh outerHTML của Username:**

![OuterHTML Username trong DevTools](screenshots/D01_Username_OuterHTML.png)

**Ảnh kiểm chứng locator AI:**

![Kiểm chứng locator AI bằng Ctrl+F](screenshots/D02_AI_Locator_Verification.png)

### D.5. Nhận xét cá nhân (3–5 câu)

Qua quá trình sử dụng AI để phân tích outerHTML của ô Username trên website SauceDemo, tôi nhận thấy AI đã đề xuất các locator phù hợp như `By.Id("user-name")` và `By.CssSelector("[data-test='username']")`. Tôi lựa chọn `By.Id("user-name")` vì cách định vị này ngắn gọn, dễ sử dụng và không phụ thuộc vào vị trí của phần tử trong cấu trúc HTML. Tuy nhiên, AI chỉ đưa ra đề xuất dựa trên các thuộc tính được cung cấp, không thể bảo đảm locator luôn chính xác khi website thay đổi. Vì vậy, tôi cần tự kiểm chứng bằng DevTools với Ctrl+F để xác nhận locator có tìm đúng một phần tử hay không. Qua đó, tôi nhận thấy AI có thể hỗ trợ lựa chọn locator nhưng kết quả vẫn cần được kiểm tra thực tế trước khi sử dụng trong Selenium.

> Cập nhật thêm kết quả đúng/sai cụ thể từ DevTools vào đoạn nhận xét trên sau khi có ảnh kiểm chứng.

## Phần E – Quản lý mã nguồn bằng GitHub (15%)

**Repository Lab 1 được tiếp tục sử dụng:** [Lab01_Nhom7_SOF3091](https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091).

Yêu cầu: Tối thiểu **3 commit mới** theo chuẩn Conventional Commits; các nội dung Lab 2 được push lên chính repository trên, không tạo repo mới.

| STT | Commit message dự kiến (ví dụ) | Trạng thái thực tế |
|:--:|---|---|
| 1 | `test: add SauceDemo login tests` | [Điền sau khi commit] |
| 2 | `test: add product information assertions` | [Điền sau khi commit] |
| 3 | `docs: add Lab 2 locator and AI evidence` | [Điền sau khi commit] |

**Ảnh lịch sử commit:**

![Lịch sử commit GitHub](screenshots/E01_CommitHistory.png)

**Ảnh repository public đã cập nhật:**

![Repository GitHub có nội dung Lab 2](screenshots/E02_Repository.png)

**Kết quả push:** [Ghi lại sau khi xác nhận có đủ 3 commit mới và các file Lab 2 xuất hiện trên GitHub.]

## Tổng hợp tình trạng Lab 2

| Phần | Tỷ trọng | Nội dung cần có | Tình trạng minh chứng |
|---|---:|---|---|
| A | 20% | Bảng 5 phần tử, 2 locator/phần tử, lý do chọn, ảnh DevTools | [Đối chiếu ảnh thực tế] |
| B | 30% | Hai ca đăng nhập thuận/nghịch, Assert, SetUp/TearDown, ảnh Passed | [Đối chiếu Test Explorer] |
| C | 25% | `FindElements`, `item.FindElement`, in 6 tên/giá, Assert | [Đối chiếu Test Output] |
| D | 10% | outerHTML, prompt, câu trả lời AI, Ctrl+F, nhận xét 3–5 câu | [Bổ sung prompt và ảnh kiểm chứng] |
| E | 15% | Ít nhất 3 commit và push lên repo Lab 1 | [Đối chiếu lịch sử GitHub] |

**Link nộp bài:** https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091


---

# Lab 3 – Xử lý các tình huống thực tế phức tạp

**Dự án:** Tiếp tục sử dụng project `FirstTests` và repository của Lab 1–2.
**Mã nguồn Lab 3:** `FirstTests/PhanA_Lab03.cs`, `PhanB_Lab03.cs`, `PhanC_Lab03.cs`, `PhanD_Lab03.cs`.

**Cách chạy:** menu **Test** → **Test Explorer**, chuột phải test hoặc class → **Run**.
Hai test cố ý rớt (`A1_RotKhongWait`, `D1_CoYRot`) được đánh dấu `[Explicit]` nên **Run All sẽ bỏ qua**, phải chuột phải từng test rồi chọn Run.

## Phần A – Chứng minh vấn đề đồng bộ (25%)

Trang kiểm thử: `the-internet.herokuapp.com/dynamic_loading/2`. Sau khi bấm Start, phần tử `#finish` (chữ "Hello World!") do JavaScript tạo ra sau vài giây. Bài không dùng `Thread.Sleep` và không dùng `performance_glitch_user`.

- **Bước 1 – không Wait** (`A1_RotKhongWait`): tìm `#finish` ngay sau khi bấm Start nên test RỚT với `NoSuchElementException`.
- **Bước 2 – có Explicit Wait** (`A2_PassCoWait`): chờ tối đa 10 giây tới khi `#finish` xuất hiện nên test PASS.

```csharp
// Bước 1: tìm ngay -> RỚT
IWebElement finish = driver.FindElement(By.Id("finish"));

// Bước 2: wait khai báo trong [SetUp]: new WebDriverWait(driver, TimeSpan.FromSeconds(10))
IWebElement finish = wait.Until(d => d.FindElement(By.Id("finish")));
```

| Ca kiểm thử | Kết quả thực tế |
|---|---|
| `A1_RotKhongWait` | **Failed** – `NoSuchElementException` |
| `A2_PassCoWait` | **Passed** |

**Ảnh minh chứng (test RỚT và test PASS):** xem trong file Word nộp kèm, mục Phần A.

**Giải thích:** Cùng một thao tác "bấm Start rồi tìm `#finish`" nhưng kết quả khác nhau vì `#finish` do JavaScript tạo ra sau vài giây. Selenium chỉ tự chờ khi chuyển trang, không chờ phần tử JavaScript tạo ra sau đó, nên test tìm ngay sẽ báo `NoSuchElementException`. Với `WebDriverWait` + `Until(...)`, test thử lại liên tục (tối đa 10 giây) tới khi phần tử xuất hiện rồi mới đi tiếp.

## Phần B – Dropdown và Assert đúng nghiệp vụ (25%)

Ca kiểm thử `B1_GiaTangDan` (`PhanB_Lab03.cs`): đăng nhập `standard_user`, dùng `SelectElement` chọn **Price (low to high)**, lấy 6 giá bằng `FindElements`, bỏ ký tự `$` và chuyển sang số. Assert so sánh danh sách vừa lấy với chính nó sau khi sắp xếp, nên giá phải **thực sự tăng dần** chứ không chỉ đếm đủ 6 sản phẩm.

```csharp
var dropdown = new SelectElement(driver.FindElement(By.ClassName("product_sort_container")));
dropdown.SelectByText("Price (low to high)");

List<decimal> prices = driver.FindElements(By.ClassName("inventory_item_price"))
    .Select(e => decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
    .ToList();

Assert.That(prices.Count, Is.EqualTo(6));
Assert.That(prices, Is.EqualTo(prices.OrderBy(p => p).ToList()));   // tăng dần thật sự
```

**Kết quả thực tế:** `B1_GiaTangDan` **Passed**.

**Ảnh minh chứng:** xem trong file Word nộp kèm, mục Phần B.

## Phần C – Alert và iframe (20%)

Trang kiểm thử: `/javascript_alerts` và `/iframe`. Mỗi ca alert đều Assert nội dung hộp thoại **trước** khi Accept/Dismiss, rồi Assert nội dung hiển thị ở `#result`. Với iframe: vào bằng `SwitchTo().Frame("mce_0_ifr")`, gõ chữ vào trình soạn thảo, và luôn thoát bằng `DefaultContent()` trong `finally` (vào thì phải ra, như đóng ngoặc).

| Ca kiểm thử | Thao tác | Kết quả mong đợi ở `#result` | Kết quả thực tế |
|---|---|---|---|
| `C1_Alert_Accept` | Accept | `You successfully clicked an alert` | **Passed** |
| `C2_Confirm_Dismiss` | Dismiss | `You clicked: Cancel` | **Passed** |
| `C3_Prompt_SendKeys_Accept` | SendKeys + Accept | `You entered: <chữ đã gõ>` | **Passed** |
| `C4_Iframe_GoChu` | Gõ chữ vào editor trong iframe, rồi `DefaultContent()` | Chữ trong editor đúng bằng chữ đã gõ | [Điền sau khi chạy lại] |

```csharp
// Alert
IAlert alert = driver.SwitchTo().Alert();
Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));   // Assert TRƯỚC khi Accept
alert.Accept();
Assert.That(driver.FindElement(By.Id("result")).Text, Is.EqualTo("You successfully clicked an alert"));

// iframe
try
{
    wait.Until(d => d.SwitchTo().Frame("mce_0_ifr"));
    IWebElement body = driver.FindElement(By.Id("tinymce"));
    body.Click();
    body.SendKeys(Keys.Control + "a");
    body.SendKeys(Keys.Delete);
    body.SendKeys(noiDung);
    Assert.That(body.Text.Trim(), Is.EqualTo(noiDung));
}
finally
{
    driver.SwitchTo().DefaultContent();   // vào thì phải ra
}
```

**Ảnh minh chứng:** xem trong file Word nộp kèm, mục Phần C.

## Phần D – Chụp ảnh khi test thất bại (15%)

Cơ chế nằm trong `[TearDown]` của `PhanB_Lab03.cs`, `PhanC_Lab03.cs` và `PhanD_Lab03.cs`: **chỉ chụp khi** `Outcome.Status == TestStatus.Failed`, đặt tên file ảnh theo tên ca test, đính kèm vào kết quả bằng `TestContext.AddTestAttachment(path)`.

```csharp
[TearDown]
public void TearDown()
{
    try
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            string dir = Path.GetFullPath(Path.Combine(
                TestContext.CurrentContext.TestDirectory, "..", "..", "..", "screenshots"));
            Directory.CreateDirectory(dir);

            string path = Path.Combine(dir, TestContext.CurrentContext.Test.Name + ".png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }
    }
    finally
    {
        driver?.Quit();
        driver?.Dispose();
    }
}
```

Test cố ý rớt để sinh ảnh minh chứng (`PhanD_Lab03.cs`):

```csharp
[Test, Explicit("Cố ý rớt để sinh ảnh minh chứng Phần D")]
public void D1_CoYRot()
{
    driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");
    Assert.That(driver.Title, Is.EqualTo("TIEU DE SAI CO Y"));   // cố ý sai
}
```

**Ảnh minh chứng chụp tự động khi `D1_CoYRot` Failed:** xem trong file Word nộp kèm, mục Phần D.

## Phần E – AI chẩn đoán flaky test (15%)

**Test được chọn:** `A1_RotKhongWait` (tình huống ở Phần A).

### E.1. Prompt đã gửi AI

> Test Selenium C# (NUnit) của mình bị lỗi, hãy chẩn đoán nguyên nhân và đề xuất cách chữa.
>
> Code:
> ```csharp
> [Test]
> public void A1_RotKhongWait()
> {
>     driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
>     driver.FindElement(By.CssSelector("#start button")).Click();
>     IWebElement finish = driver.FindElement(By.Id("finish"));
>     Assert.That(finish.Text, Is.EqualTo("Hello World!"));
> }
> ```
>
> Lỗi nguyên văn:
> `OpenQA.Selenium.NoSuchElementException : no such element: Unable to locate element: {"method":"css selector","selector":"#finish"}`
>
> Tần suất rớt: rớt 10/10 lần chạy, lần nào cũng báo cùng một lỗi tại dòng tìm phần tử `#finish`.
>
> Điều kiện chạy: Windows 11, Visual Studio, .NET 8.0, Chrome 142, Selenium.WebDriver 4.x, NUnit 3.x, chạy bằng Test Explorer, mạng nhà ổn định. Trong test không dùng Implicit Wait, không dùng Thread.Sleep. Quan sát bằng mắt: sau khi bấm Start thì thanh loading chạy vài giây rồi chữ "Hello World!" mới hiện ra, nhưng test báo lỗi gần như ngay sau khi bấm Start.

### E.2. Nguyên nhân AI đề xuất và kết quả tự kiểm chứng

| STT | Gợi ý của Claude | Kết quả | Cách kiểm chứng |
|:--:|---|---|---|
| 1 | Thiếu wait: `Click()` trả về ngay, còn `#finish` do JavaScript tạo sau vài giây | Đúng | F12: lúc vừa bấm Start chưa có `#finish` trong DOM |
| 2 | Trang `/2` tạo phần tử mới, không phải phần tử có sẵn nhưng bị ẩn như trang `/1` | Đúng | F12 như trên |
| 3 | Chữa bằng `WebDriverWait` + `Until` | Đúng | `A2_PassCoWait` Passed |
| 4 | Không trộn Implicit Wait với Explicit Wait; `ExpectedConditions` đã tách gói | Đúng | Khớp mục 3 và 5 trong hướng dẫn của đề |
| 5 | Thêm điều kiện kiểm tra phần tử đang hiển thị (`Displayed`) | Không cần thiết | `A2` không có điều kiện này vẫn Passed |
| 6 | Sửa trực tiếp trong test `A1_RotKhongWait` | Không phù hợp | Sửa vậy thì `A1` không còn rớt để chụp ảnh minh chứng |

### E.3. Nhận xét cá nhân

Claude chỉ ra đúng nguyên nhân: sau khi bấm nút Start, chữ "Hello World!" phải đợi vài giây mới hiện ra, nhưng test lại tìm nó ngay lập tức nên báo lỗi không thấy. Cách chữa Claude đề xuất cũng đúng, đó là cho test chờ tối đa 10 giây tới khi chữ hiện ra, và khi áp dụng ở `A2_PassCoWait` thì test Passed. Hai chỗ chưa phù hợp: Claude sửa thẳng vào test bị lỗi nên làm theo thì test không còn rớt để chụp ảnh minh chứng, và điều kiện kiểm tra "đang hiển thị" Claude thêm vào là không cần, vì bản của tôi không có vẫn Passed. Test của tôi rớt 10/10 lần, tức là lỗi xảy ra mọi lần chứ không lúc rớt lúc không, nên đây chưa phải flaky test (test chập chờn) thực sự.

## Quản lý mã nguồn Lab 3

Lab 3 được làm tiếp trên **cùng repository** Lab 1–2, commit theo chuẩn Conventional Commits.

| STT | Commit message | Trạng thái thực tế |
|:--:|---|---|
| 1 | `test: them test dong bo explicit wait (phan A)` | [Điền sau khi commit] |
| 2 | `test: them test dropdown va assert gia tang dan (phan B)` | [Điền sau khi commit] |
| 3 | `test: them test alert va iframe (phan C)` | [Điền sau khi commit] |
| 4 | `feat: chup anh khi test failed trong TearDown (phan D)` | [Điền sau khi commit] |
| 5 | `docs: them anh minh chung va README lab 3` | [Điền sau khi commit] |

## Tổng hợp tình trạng Lab 3

| Phần | Tỷ trọng | Nội dung cần có | Tình trạng minh chứng |
|---|---:|---|---|
| A | 25% | Test rớt và pass, 2 ảnh, giải thích, dùng `WebDriverWait` + `Until` (không `Thread.Sleep`) | [Đối chiếu ảnh trong file Word] |
| B | 25% | `SelectElement`, Assert giá thực sự tăng dần | [Đối chiếu ảnh trong file Word] |
| C | 20% | 3 loại alert có Assert `#result`, gõ chữ vào iframe rồi `DefaultContent()` | [Đối chiếu ảnh trong file Word, kể cả C4] |
| D | 15% | `[TearDown]` chỉ chụp khi Failed, ảnh minh chứng | [Đối chiếu ảnh trong file Word] |
| E | 15% | Prompt, nguyên nhân AI đề xuất, tự kiểm chứng, nhận xét | [Đối chiếu số lần rớt với prompt] |

**Link nộp bài:** https://github.com/yentnbtb01913-jpg/Lab01_Nhom7_SOF3091
