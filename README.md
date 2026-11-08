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

# Lab 2
# Phần A — Bảng locator
STT
Phần tử
Locator 1
Locator 2
Locator được chọn
Lý do
1
Username
By.Id("user-name")
By.CssSelector("[data-test='username']")
By.Id("user-name")
ID rõ ràng, dễ sử dụng và định danh trực tiếp phần tử.
2
Password
By.Id("password")
By.CssSelector("[data-test='password']")
By.Id("password")
ID ổn định, dễ đọc và dễ bảo trì.
3
Login Button
By.Id("login-button")
By.CssSelector("[data-test='login-button']")
By.Id("login-button")
ID xác định chính xác nút đăng nhập.
4
Product Name
By.ClassName("inventory_item_name")
By.CssSelector("[data-test='inventory-item-name']")
By.CssSelector("[data-test='inventory-item-name']")
Thuộc tính data-test thể hiện rõ mục đích của phần tử.
5
Product Price
By.ClassName("inventory_item_price")
By.CssSelector("[data-test='inventory-item-price']")
By.CssSelector("[data-test='inventory-item-price']")

# Phần D — Lồng ghép AI
Prompt đã sử dụng
Tôi đang thực hiện kiểm thử tự động bằng Selenium C#. Đây là outerHTML của nút Login. Hãy đề xuất locator bền vững nhất để tìm phần tử này, ưu tiên locator ít bị ảnh hưởng khi giao diện CSS thay đổi. Giải thích lý do lựa chọn và đưa ra cú pháp Selenium C#.
 Câu trả lời của AI
AI đề xuất sử dụng CSS Selector:
By.CssSelector("[data-test='login-button']")
Lý do: Thuộc tính data-test được thiết kế dành cho kiểm thử tự động, dễ nhận biết và ít phụ thuộc vào giao diện CSS.
 Tự kiểm chứng bằng Ctrl + F
•	Locator kiểm tra: [data-test='login-button']
•	Công cụ: Chrome DevTools → Elements → Ctrl + F.
•	Kết quả: 1 of 1.
•	Đánh giá: Tìm thấy chính xác phần tử nút Login. 
Nhận xét và đánh giá
AI đã đề xuất locator phù hợp với phần tử nút Login. Locator [data-test='login-button'] dễ đọc, dễ bảo trì và ít phụ thuộc vào giao diện CSS. Tôi đã kiểm chứng bằng Ctrl + F trong Chrome DevTools và kết quả hiển thị 1 of 1. Như vậy, đề xuất của AI là chính xác và có thể sử dụng trong Selenium C#.

# Lab 3
# Phần A — Chứng minh vấn đề đồng bộ 
Khi sử dụng Selenium tìm phần tử ngay sau khi nhấn nút Start, test có thể bị lỗi NoSuchElementException vì JavaScript chưa kịp tạo phần tử #finish.
Sau khi sử dụng Explicit Wait với WebDriverWait và Until(), Selenium sẽ chờ cho đến khi phần tử xuất hiện. Vì vậy, test có thể tìm thấy phần tử và kiểm tra nội dung Hello World! thành công.
# Phần E — AI chẩn đoán flaky test
Tôi đang sử dụng C#, NUnit và Selenium WebDriver để kiểm thử tự động.
Test của tôi gặp lỗi OpenQA.Selenium.NoSuchElementException khi tìm phần tử #finish trên trang Dynamic Loading Example 2.
Code sử dụng FindElement ngay sau khi nhấn nút Start, không có cơ chế chờ.
Tần suất lỗi: [Điền kết quả chạy 10 lần].
Hãy giúp tôi:
1.	Phân tích nguyên nhân gây lỗi.
2.	Xác định các nguyên nhân có thể gây Flaky Test.
3.	Đề xuất cách khắc phục bằng WebDriverWait và Until().
4.	Giải thích vì sao không nên dùng Thread.Sleep.
5.	Đưa ra cách kiểm chứng sau khi sửa lỗi
# Nhận xét
AI xác định nguyên nhân chính là JavaScript chưa tạo xong phần tử #finish khi Selenium tìm kiếm. Sau khi kiểm tra, tôi nhận thấy việc sử dụng WebDriverWait kết hợp Until() giúp chờ phần tử xuất hiện trước khi kiểm tra. Giải pháp này phù hợp hơn so với Thread.Sleep vì có thể tiếp tục ngay khi điều kiện được đáp ứng. Qua đó, tôi hiểu rõ hơn cách phát hiện và khắc phục lỗi Flaky Test.