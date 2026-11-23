# PHẦN A – PROMPT VÀ DANH SÁCH TEST CASE DO AI ĐỀ XUẤT

Tính năng chọn: thêm sản phẩm vào giỏ và thanh toán (Checkout) trên https://www.saucedemo.com/.
AI sử dụng: ChatGPT. Ảnh hội thoại: `screenshots/minhchung/A01a_*.png` đến `A01e_*.png` (prompt và toàn bộ bảng trả lời).

## Prompt gửi AI (nguyên văn)

```text
VAI TRÒ: Bạn là Senior QA Automation Engineer dùng NUnit và Selenium C#.
NGỮ CẢNH: Project FirstTests chạy .NET 10, NUnit 4, Selenium WebDriver 4, Page Object Model gồm BasePage, LoginPage, InventoryPage, CheckoutPage; dữ liệu đưa vào checkout_cases.json. Website cần kiểm thử là https://www.saucedemo.com/. Checkout gồm giỏ hàng, nhập First Name, Last Name, Zip/Postal Code, trang overview và Finish.
YÊU CẦU: Đề xuất tối thiểu 12 test case gồm ca thường, ca lỗi và ca biên. Không giả định hành vi trang là đã được kiểm chứng. Mỗi ca có ID, bước, dữ liệu, expected, và điều cần xác minh; nếu bạn đề xuất chức năng ngoài checkout hiện có hãy ghi rõ giả thuyết.
ĐỊNH DẠNG: Xuất bảng Markdown với các cột ID | Kịch bản | Dữ liệu | Expected đề xuất | Điều cần kiểm chứng. Không sinh code ở phần này.
```

Bốn thành phần: vai trò (Senior QA Automation Engineer), ngữ cảnh framework (.NET 10, NUnit 4, Selenium 4, POM, JSON), yêu cầu rõ (tối thiểu 12 ca; ca thường, lỗi, biên; ghi giả thuyết), định dạng đầu ra (bảng Markdown 5 cột, không sinh code).

## Đầu ra của AI – 20 test case

ChatGPT ghi chú: các Expected là kỳ vọng hoặc giả thuyết kiểm thử, chưa phải kết quả đã xác nhận bằng Selenium. Cột "Nhóm" do người thẩm định gán để thống kê: 6 ca thường, 8 ca lỗi, 6 ca biên.

| ID | Nhóm | Kịch bản | Dữ liệu | Expected đề xuất | Điều cần kiểm chứng |
|---|---|---|---|---|---|
| C01 | Thường | Đăng nhập → Thêm 1 sản phẩm → Cart → Checkout → Nhập thông tin → Continue → Finish. | First Name: An; Last Name: Nguyen; Zip: 700000 | Hoàn tất thanh toán, hiển thị thông báo đặt hàng thành công. | Xác minh thông báo hoàn tất, trạng thái đơn và điều hướng. |
| C02 | Thường | Đăng nhập → Thêm 2 sản phẩm → Checkout → Continue → Overview. | First Name: Binh; Last Name: Tran; Zip: 76000 | Overview hiển thị đúng 2 sản phẩm và tổng giá trị. | Xác minh tên, số lượng, đơn giá, tổng tiền và thuế nếu có. |
| C03 | Thường | Thêm sản phẩm → Mở giỏ hàng → Xóa sản phẩm → Kiểm tra giỏ. | 1 sản phẩm bất kỳ | Sản phẩm được xóa khỏi giỏ, số lượng cập nhật. | Kiểm tra giỏ hàng, badge và dữ liệu sau khi xóa. |
| C04 | Lỗi | Mở giỏ hàng rỗng → Chọn Checkout. | Giỏ hàng không có sản phẩm | Giả thuyết: Hệ thống không cho hoàn tất đơn hàng rỗng. | Xác minh có được chuyển đến Checkout không và Finish có bị chặn không. |
| C05 | Lỗi | Checkout → Để trống First Name → Continue. | First Name: (trống); Last Name: Nguyen; Zip: 700000 | Hiển thị thông báo yêu cầu nhập First Name. | Xác minh nội dung thông báo, trạng thái và việc không chuyển sang Overview. |
| C06 | Lỗi | Checkout → Để trống Last Name → Continue. | First Name: An; Last Name: (trống); Zip: 700000 | Hiển thị thông báo yêu cầu nhập Last Name. | Xác minh thông báo chính xác và trang hiện tại. |
| C07 | Lỗi | Checkout → Để trống Postal Code → Continue. | First Name: An; Last Name: Nguyen; Zip: (trống) | Hiển thị thông báo yêu cầu nhập Postal Code. | Xác minh thông báo và việc không chuyển trang. |
| C08 | Lỗi | Checkout → Để trống cả 3 trường → Continue. | First Name, Last Name, Zip: (trống) | Hệ thống từ chối tiếp tục và hiển thị lỗi trường bắt buộc. | Xác minh thứ tự ưu tiên thông báo khi nhiều trường cùng rỗng. |
| C09 | Lỗi | Checkout → Nhập First Name chỉ chứa khoảng trắng → Continue. | First Name: (chỉ khoảng trắng); Last Name: Nguyen; Zip: 700000 | Giả thuyết: Hệ thống từ chối First Name chỉ có khoảng trắng. | Kiểm chứng website có trim dữ liệu và chặn giá trị trắng không. |
| C10 | Lỗi | Checkout → Nhập Last Name chỉ chứa khoảng trắng → Continue. | First Name: An; Last Name: (chỉ khoảng trắng); Zip: 700000 | Giả thuyết: Hệ thống từ chối Last Name không có ký tự hợp lệ. | Kiểm chứng quy tắc xử lý khoảng trắng của Last Name. |
| C11 | Lỗi | Checkout → Postal Code chỉ chứa khoảng trắng → Continue. | First Name: An; Last Name: Nguyen; Zip: (chỉ khoảng trắng) | Giả thuyết: Hệ thống từ chối Postal Code chỉ khoảng trắng. | Kiểm chứng khoảng trắng có được coi là dữ liệu hợp lệ hay không. |
| C12 | Biên | Checkout → Nhập tên tiếng Việt có dấu → Continue. | First Name: Bình Yên; Last Name: Trần Nguyễn; Zip: 700000 | Dữ liệu Unicode được giữ nguyên và có thể tiếp tục nếu không có giới hạn ký tự. | Kiểm chứng hỗ trợ Unicode, lỗi mã hóa và xử lý dấu tiếng Việt. |
| C13 | Biên | Checkout → Nhập tên chứa ký tự đặc biệt → Continue. | First Name: An@#; Last Name: Nguyen!; Zip: 700000 | Giả thuyết: Hệ thống từ chối hoặc chấp nhận theo quy tắc nhập liệu được áp dụng. | Xác định website có kiểm tra ký tự đặc biệt không. |
| C14 | Biên | Checkout → Nhập tên rất dài → Continue. | First Name: chuỗi 256 ký tự A; Last Name: Nguyen; Zip: 700000 | Không xảy ra lỗi ứng dụng; dữ liệu được giới hạn hoặc xử lý nhất quán. | Xác minh maxlength, thông báo lỗi và giới hạn thực tế. |
| C15 | Biên | Checkout → Nhập Postal Code bắt đầu bằng số 0 → Continue. | First Name: An; Last Name: Nguyen; Zip: 00123 | Nếu được chấp nhận, giá trị 00123 phải giữ nguyên, không mất số 0 đầu. | Xác minh kiểu dữ liệu và việc chuyển đổi Postal Code thành số. |
| C16 | Biên | Checkout → Nhập Postal Code gồm chữ và số → Continue. | First Name: An; Last Name: Nguyen; Zip: SW1A 1AA | Giả thuyết: Có thể tiếp tục nếu hệ thống hỗ trợ mã bưu chính chữ và số. | Kiểm chứng có validation định dạng Postal Code hay không. |
| C17 | Thường | Checkout → Nhập thông tin → Chọn Cancel trước Overview. | First Name: An; Last Name: Nguyen; Zip: 700000 | Quay về màn hình trước theo luồng điều hướng, không tạo đơn hoàn tất. | Kiểm tra trang đích, sản phẩm trong giỏ và dữ liệu còn được giữ hay không. |
| C18 | Thường | Checkout → Continue → Overview → Chọn Cancel. | Dữ liệu hợp lệ, 1 sản phẩm | Hủy quy trình thanh toán và không xuất hiện trạng thái hoàn tất. | Xác minh trang đích của Cancel và trạng thái giỏ hàng. |
| C19 | Thường | Thêm nhiều sản phẩm → Checkout → Overview → Đối chiếu tiền. | 2–3 sản phẩm có giá khác nhau | Item total bằng tổng giá các sản phẩm; Total bằng Item total cộng thuế và các khoản được hiển thị. | Xác minh công thức, làm tròn số và giá trị hiển thị. |
| C20 | Biên | Checkout hợp lệ → Finish → Tải lại trang hoàn tất. | First Name: An; Last Name: Nguyen; Zip: 700000 | Giả thuyết: Tải lại không tạo thêm một đơn hàng mới. | Kiểm chứng hành vi refresh, trạng thái hoàn tất và khả năng phát sinh thao tác trùng lặp. |
