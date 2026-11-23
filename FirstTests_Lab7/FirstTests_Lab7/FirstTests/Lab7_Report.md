# LAB 7 – KHAI THÁC AI CHUYÊN SÂU TRONG QUY TRÌNH KIỂM THỬ

**Project:** FirstTests – Selenium C# / NUnit  
**Website:** https://www.saucedemo.com/  
**Tính năng được chọn:** Thêm Sauce Labs Backpack vào giỏ hàng và xác minh nội dung giỏ hàng.


---

## Phần A – Prompt & sinh bộ test case (20%)

### A1. Prompt nguyên văn

```text
[VAI TRÒ]
Bạn là QA Automation Engineer có kinh nghiệm kiểm thử web UI bằng Selenium C# và NUnit.

[NGỮ CẢNH FRAMEWORK]
Tôi đang làm project FirstTests, target .NET 10, NUnit 4 và Selenium WebDriver.
Framework đã có BaseTest (tạo và đóng IWebDriver, logging, chụp ảnh khi test lỗi),
BasePage với hàm Tim(By locator) dùng WebDriverWait, và Page Object:
- LoginPages.DangNhap(username, password) trả về InventoryPages.
- InventoryPages có ThemSanPhamVaoGio(), LaySoLuongTrongGio(), MoGioHang().
- CartPages có LayTenSanPhamTrongGio().
Locator hiện có trong InventoryPages:
  By.Id("add-to-cart-sauce-labs-backpack")
  By.CssSelector("[data-test='shopping-cart-badge']")
  By.CssSelector("[data-test='shopping-cart-link']")
Website là https://www.saucedemo.com/; tài khoản hợp lệ standard_user / secret_sauce.

[YÊU CẦU RÕ]
Hãy đề xuất 10 test case cho tính năng thêm sản phẩm vào giỏ hàng.
Phải có ca thường, ca biên và ca lỗi. Mỗi ca gồm: ID, dữ liệu/tiền điều kiện,
các bước, kết quả mong đợi. Chỉ dùng chức năng thực sự có trên SauceDemo.
Không tự bịa wishlist, lưu giỏ hàng, chỉnh số lượng nếu giao diện không có.
Phân biệt rõ ca có thể tự động hóa bằng Page Object hiện tại và ca cần bổ sung POM.
Không đề xuất Thread.Sleep hoặc tạo WebDriver mới trong test.

[ĐỊNH DẠNG ĐẦU RA]
Trả về bảng Markdown gồm 10 dòng, sau bảng ghi chú các giả định cần xác minh.
```

### A2. Danh sách 10 test case AI đề xuất

| ID | Loại | Tiền điều kiện / dữ liệu | Các bước | Kết quả mong đợi |
|---|---|---|---|---|
| CART-01 | Thường | Đăng nhập `standard_user` | Thêm Sauce Labs Backpack | Badge giỏ hàng hiển thị `1` |
| CART-02 | Thường | CART-01 đã thêm sản phẩm | Mở giỏ hàng | URL có `cart.html` và thấy Sauce Labs Backpack |
| CART-03 | Thường | Đăng nhập, chưa thêm sản phẩm | Thêm Sauce Labs Bike Light | Badge hiển thị `1`; giỏ có Bike Light |
| CART-04 | Thường | Đăng nhập, chưa thêm sản phẩm | Thêm hai sản phẩm khác nhau | Badge hiển thị `2`; giỏ có đúng hai sản phẩm |
| CART-05 | Biên | Đăng nhập, giỏ hàng ban đầu rỗng | Mở giỏ hàng mà chưa thêm sản phẩm | Không có cart badge; danh sách hàng trong giỏ rỗng |
| CART-06 | Biên | Đăng nhập, đã thêm Backpack | Nhấn nút đổi trạng thái `Remove` của Backpack | Badge biến mất khi giỏ trở về rỗng |
| CART-07 | Biên | Đăng nhập, đã thêm Backpack | Mở giỏ hàng và quay lại Inventory | Sản phẩm vẫn được giữ trong giỏ |
| CART-08 | Lỗi / bảo mật phiên | Chưa đăng nhập | Truy cập trực tiếp `/cart.html` | Hệ thống chuyển về trang đăng nhập hoặc không cho xem giỏ hàng |
| CART-09 | Lỗi nghiệp vụ | Đăng nhập, chưa thêm sản phẩm | Tìm chức năng thêm sản phẩm vào Wishlist | Không áp dụng: SauceDemo không có chức năng Wishlist |
| CART-10 | Lỗi nghiệp vụ | Đăng nhập, đã mở Cart | Tìm ô nhập số lượng để đổi từ 1 thành 2 | Không áp dụng: giao diện Cart hiển thị số lượng, không có ô nhập số lượng để sửa |

**Ghi chú của AI:** CART-03, CART-04 cần POM hỗ trợ chọn sản phẩm theo tên hoặc nhiều nút Add to cart. CART-06 cần POM có thao tác Remove. Không nên giả định website có Wishlist hoặc chức năng chỉnh số lượng.

---

## Phần B – Chọn lọc & thẩm định test case (15%)

Đối chiếu với giao diện SauceDemo và locator/POM đang có trong project.

| ID | Phân loại | Lý do thẩm định |
|---|---|---|
| CART-01 | GIỮ | Project đã có `ThemSanPhamVaoGio()` và `LaySoLuongTrongGio()`; kiểm tra được badge bằng số `1`. |
| CART-02 | GIỮ | Project đã có `MoGioHang()` và `LayTenSanPhamTrongGio()`; kiểm tra được URL và tên sản phẩm. |
| CART-03 | SỬA | Sản phẩm Bike Light có trên Inventory, nhưng POM hiện chỉ có locator cho Backpack. Cần bổ sung cách tìm nút Add theo tên sản phẩm trước khi tự động hóa. |
| CART-04 | SỬA | Có thể thêm nhiều sản phẩm khác nhau trên trang Inventory, nhưng POM hiện tại chỉ hỗ trợ Backpack; cần mở rộng POM và xác minh badge bằng `2`. |
| CART-05 | SỬA | Có thể kiểm tra giỏ rỗng, nhưng CartPages hiện chưa có hàm đếm sản phẩm/kiểm tra rỗng. Cần bổ sung method có explicit wait phù hợp. |
| CART-06 | SỬA | Trên Inventory, nút Add đổi thành Remove sau khi thêm. POM hiện chưa có method Remove; cần bổ sung locator/method và kiểm tra badge biến mất. |
| CART-07 | GIỮ | Có thể xác minh sản phẩm vẫn còn trong giỏ sau khi điều hướng qua lại; cần thực hiện đúng trình tự và kiểm tra lại trên trang Cart. |
| CART-08 | GIỮ | Có thể kiểm tra bảo vệ phiên bằng cách mở `/cart.html` khi chưa đăng nhập; cần dùng một test riêng không đăng nhập trước. |
| CART-09 | BỎ | SauceDemo không cung cấp chức năng Wishlist trên giao diện sản phẩm. Không có nút hoặc luồng nghiệp vụ này để kiểm thử. |
| CART-10 | BỎ | Trang Cart không cung cấp ô nhập để người dùng tự đổi số lượng sản phẩm. Kỳ vọng nhập số lượng 2 là không khớp giao diện thật. |
CART-08 – Kiểm tra truy cập giỏ hàng khi chưa đăng nhập
BỎ
- Lý do: Test case này kiểm tra khả năng xác thực người dùng và bảo vệ trang khi chưa đăng nhập.
- Không tập trung vào chức năng thêm, xóa hoặc kiểm tra sản phẩm trong giỏ hàng.
- Đây là nội dung thuộc nhóm kiểm thử đăng nhập và phân quyền truy cập hơn là kiểm thử nghiệp vụ giỏ hàng.
- Vì vậy, loại khỏi phạm vi kiểm thử giỏ hàng và có thể chuyển sang nhóm kiểm thử bảo mật.
CART-09 – Kiểm tra chức năng Wishlist
BỎ
- Lý do: Website SauceDemo không cung cấp chức năng Wishlist (danh sách sản phẩm yêu thích) trên giao diện.
- Không có nút hoặc thao tác thêm sản phẩm vào danh sách yêu thích.
- Nếu thực hiện test case này sẽ không tìm thấy thành phần giao diện tương ứng để kiểm thử.
- Vì vậy, test case không phù hợp với chức năng thực tế của website và cần được loại bỏ.
CART-10 – Kiểm tra thay đổi số lượng sản phẩm trực tiếp trong giỏ hàng
BỎ
- Lý do: Giao diện trang Cart của SauceDemo không cung cấp ô nhập số lượng để người dùng tự thay đổi số lượng sản phẩm.
- Người dùng chỉ có thể xem sản phẩm, tiếp tục mua sắm hoặc xóa sản phẩm khỏi giỏ hàng.
- Do đó, bước nhập số lượng 2 và kỳ vọng hệ thống cập nhật số lượng không phù hợp với giao diện thực tế.
- Vì vậy, test case này không thể thực hiện theo yêu cầu ban đầu và cần được loại bỏ.


---

## Phần C – Sinh code đúng chuẩn framework (25%)

### C1. Prompt nguyên văn

```text
Bạn là QA Automation Engineer chuyên Selenium C# và NUnit.

Ngữ cảnh: project FirstTests, .NET 10, NUnit 4, Selenium 4.
Tôi cung cấp các Page Object hiện có: LoginPages, InventoryPages, CartPages,
BasePage. Test phải kế thừa BaseTest trong namespace FirstTests.
BaseTest đã quản lý vòng đời IWebDriver, logging và screenshot khi test fail.

Hãy viết MỘT test method mới cho tính năng thêm Sauce Labs Backpack vào giỏ hàng.
Quy ước bắt buộc:
1. Test class kế thừa BaseTest; tuyệt đối không tạo ChromeDriver/IWebDriver mới.
2. Dùng lại LoginPages, InventoryPages và CartPages hiện có.
3. Page Object kế thừa BasePage; thao tác tìm phần tử dùng Tim() có explicit wait.
4. Locator phải là private readonly By và phải có trên DOM SauceDemo.
5. Không dùng Thread.Sleep.
6. Assert đúng nghiệp vụ: badge = 1, URL có cart.html, tên sản phẩm là
   Sauce Labs Backpack.
7. Không sửa/xóa các test cũ.

Trả về code hoàn chỉnh của file Test/CartFeatureTests.cs và giải thích ngắn
cách mỗi Assert xác minh nghiệp vụ.
```

### C2. Code được chọn sau thẩm định

File mới: `Test/CartFeatureTests.cs`

```csharp
using FirstTests.Page;
using NUnit.Framework;

namespace FirstTests
{
    [TestFixture]
    [Category("Cart")]
    public class CartFeatureTests : BaseTest
    {
        private InventoryPages inventoryPage = null!;

        [SetUp]
        public void MoTrangVaDangNhap()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            LoginPages loginPage = new LoginPages(driver);

            inventoryPage = loginPage.DangNhap(
                "standard_user",
                "secret_sauce"
            );

            // Chờ danh sách sản phẩm tải xong trước khi kiểm thử.
            inventoryPage.LayDanhSachTenSanPham();
        }

        [Test]
        [Category("Smoke")]
        public void ThemBackpackVaoGio_VaKiemTraThongTin()
        {
            // Act: thêm Sauce Labs Backpack vào giỏ hàng.
            inventoryPage.ThemSanPhamVaoGio();

            // Assert 1: badge giỏ hàng phải hiển thị số lượng 1.
            Assert.That(
                inventoryPage.LaySoLuongTrongGio(),
                Is.EqualTo("1"),
                "Giỏ hàng phải hiển thị 1 sản phẩm sau khi thêm Backpack."
            );

            // Act: mở trang giỏ hàng.
            CartPages cartPage = inventoryPage.MoGioHang();

            // Assert 2: chuyển đúng trang Cart.
            Assert.That(
                driver.Url,
                Does.Contain("cart.html"),
                "Sau khi nhấn biểu tượng giỏ hàng phải mở trang Cart."
            );

            // Assert 3: đúng sản phẩm đã được thêm.
            Assert.That(
                cartPage.LayTenSanPhamTrongGio(),
                Is.EqualTo("Sauce Labs Backpack"),
                "Giỏ hàng phải chứa đúng Sauce Labs Backpack."
            );
        }
    }
}
```

### C3. Checklist thẩm định 6 điểm (đánh dấu sau khi tự kiểm)

| STT | Tiêu chí theo đề | Kết quả | Bằng chứng / ghi chú |
|---|---|---|---|
| 1 | Locator thật | ☑ | Đối chiếu `add-to-cart-sauce-labs-backpack`, `shopping-cart-badge`, `shopping-cart-link` với locator đang có trong `InventoryPages.cs`; tên sản phẩm dùng `[data-test='inventory-item-name']` trong `CartPages.cs`. |
| 2 | Assert đúng nghiệp vụ | ☑ | Badge = 1; URL có `cart.html`; tên sản phẩm đúng `Sauce Labs Backpack`. |
| 3 | Dùng wait | ☑ | POM gọi `Tim()`/`WebDriverWait`; `LayDanhSachTenSanPham()` chờ đủ 6 sản phẩm; `LaySoLuongTrongGio()` chờ badge xuất hiện. |
| 4 | Đúng chuẩn framework | ☑ | Test kế thừa `BaseTest`, tái sử dụng các Page Object, không tự tạo driver. |
| 5 | Không thừa | ☑ | Chỉ có một test method cho luồng đã chọn; không thêm thao tác không liên quan. |
| 6 | Hiểu từng dòng code | ☐ | Sinh viên đọc và giải thích được SetUp, đăng nhập, thao tác Add, từng Assert trước khi commit. |

**Lưu ý:** Mục 6 chỉ đánh dấu sau khi bạn tự đọc và hiểu code. Chạy `dotnet build` và `dotnet test` trên máy cá nhân rồi ghi kết quả thực tế; không đánh dấu Passed khi chưa chạy.

### C4. Lệnh xác nhận

```powershell
dotnet build
dotnet test --filter "FullyQualifiedName~CartFeatureTests"
```

---

## Phần D – Debug có AI (25%)

### D1. Tình huống lỗi thật được tạo có chủ đích

Trong `InventoryPages.cs`, locator đúng hiện tại là:

```csharp
private readonly By addBackpackButton =
    By.Id("add-to-cart-sauce-labs-backpack");
```

Tạo lỗi bằng cách tạm thay thành locator sai:

```csharp
private readonly By addBackpackButton =
    By.Id("add-to-cart-backpack");
```

Lưu file và chạy riêng test:

```powershell
dotnet test --filter "FullyQualifiedName~CartFeatureTests"
```

### D2. Phán đoán của sinh viên TRƯỚC khi hỏi AI

**Dự đoán nguyên nhân:** Test không tìm thấy nút thêm Backpack vì ID trong locator có thể không trùng với thuộc tính `id` thực tế của nút trên trang SauceDemo. Cần kiểm tra DOM bằng DevTools và so sánh với locator trong Page Object.

> Chụp ảnh kết quả lỗi thực tế và stack trace ở bước này trước khi hỏi AI. Không dùng đoạn stack trace giả.

### D3. Prompt hỏi AI sau khi đã tự phán đoán

```text
Tôi đang debug Selenium C# / NUnit trong project FirstTests.
Đây là lỗi thực tế khi chạy CartFeatureTests: [DÁN STACK TRACE THỰC TẾ].
Locator đang dùng:
private readonly By addBackpackButton = By.Id("add-to-cart-backpack");

Hãy:
1. Chẩn đoán nguyên nhân dựa trên stack trace, không đoán chức năng khác.
2. Chỉ ra cách kiểm tra locator trong DOM SauceDemo.
3. Đề xuất sửa tối thiểu tại đúng một chỗ trong Page Object.
4. Không tạo WebDriver mới, không dùng Thread.Sleep, không viết lại framework.
```

### D4. Chẩn đoán và hướng sửa

Nếu stack trace chỉ ra `NoSuchElementException` hoặc `WebDriverTimeoutException` tại `ThemSanPhamVaoGio()`/`Tim()`, kiểm tra ID nút Add Backpack trên DOM. Locator đúng trong source Page Object hiện tại là:

```csharp
By.Id("add-to-cart-sauce-labs-backpack")
```

Sửa lại duy nhất locator `addBackpackButton` trong `InventoryPages.cs` về ID trên. Không thay đổi BaseTest, không thêm Thread.Sleep.

### D5. Chạy lại sau sửa

```powershell
dotnet build
dotnet test --filter "FullyQualifiedName~CartFeatureTests"
```

Kết quả mong đợi sau khi sửa: `CartFeatureTests` Passed. Chỉ ghi kết quả Passed sau khi lệnh chạy thực tế thành công.

### D6. Kết luận debug

Lỗi được tạo do locator ID sai. Stack trace giúp xác định vị trí thao tác tìm nút Add Backpack. Sau khi đối chiếu DOM và sửa locator tại Page Object, test được chạy lại để xác nhận. Cách sửa tập trung ở một nơi, không làm thay đổi BaseTest hoặc cấu trúc POM.

---

## Phần E – Thẩm định & bắt AI trả lời sai (15%)

### E1. Prompt dùng để yêu cầu AI viết locator

```text
Hãy viết locator Selenium C# cho nút thêm Sauce Labs Backpack trên trang
Inventory của SauceDemo. Chỉ trả về một dòng By.Id(...).
```

### E2. Bản nháp AI sai cần bắt lỗi

```csharp
private readonly By addBackpackButton =
    By.Id("add-to-cart-backpack");
```

Đây là **locator sai**: ID `add-to-cart-backpack` không khớp locator thật đang được project sử dụng. Locator đã đối chiếu trong Page Object của project là:

```csharp
private readonly By addBackpackButton =
    By.Id("add-to-cart-sauce-labs-backpack");
```

### E3. Vì sao sai và cách phát hiện

- AI đã rút gọn ID và bỏ mất phần `sauce-labs`.
- Selenium tìm theo ID chính xác, không tự hiểu tên gần giống.
- Khi chạy test, thao tác `Tim(addBackpackButton)` không tìm được nút.
- Cách phát hiện: xem stack trace để tìm dòng lỗi, mở DevTools trên SauceDemo, kiểm tra thuộc tính `id` của nút, sau đó đối chiếu locator trong DOM với locator AI đề xuất.

### E4. Bằng chứng cần chụp

1. Prompt yêu cầu AI sinh locator.
2. Đoạn AI đề xuất locator sai.
3. DOM/DevTools thể hiện ID đúng.
4. Code sau khi sửa và kết quả test Passed.

**Kết luận:** AI có thể tạo locator nhìn hợp lý nhưng sai một phần tên thuộc tính. Vì vậy, mọi locator phải được kiểm tra trên DOM thật trước khi đưa vào Page Object và commit.

---

## Danh sách sản phẩm nộp Lab 7

- [ ] Repository GitHub public, tiếp tục từ repository Bài 6.
- [ ] `Lab7_Report.md`: prompt và kết quả AI cho A–E.
- [ ] `Test/CartFeatureTests.cs`: test method mới theo BaseTest/POM.
- [ ] Bảng GIỮ / BỎ / SỬA ở Phần B.
- [ ] Checklist 6 điểm ở Phần C.
- [ ] `screenshots/`: ảnh hội thoại AI, test lỗi trước sửa và test xanh sau sửa.
- [ ] Kết quả `dotnet build` và `dotnet test`.
- [ ] Commit theo Conventional Commits, ví dụ: `test: add cart feature test with AI review`.

