# PHẦN A – PROMPT VÀ BẢN ĐỀ XUẤT TEST CASE CỦA AI

VAI TRÒ: Bạn là Senior QA Automation Engineer dùng NUnit và Selenium C#.
NGỮ CẢNH: Project FirstTests chạy .NET 10, NUnit 4, Selenium WebDriver 4, Page Object Model gồm BasePage, LoginPage, InventoryPage, CheckoutPage; dữ liệu đưa vào checkout_cases.json. Website cần kiểm thử là https://www.saucedemo.com/. Checkout gồm giỏ hàng, nhập First Name, Last Name, Zip/Postal Code, trang overview và Finish.
YÊU CẦU: Đề xuất tối thiểu 12 test case gồm ca thường, ca lỗi và ca biên. Không giả định hành vi trang là đã được kiểm chứng. Mỗi ca có ID, bước, dữ liệu, expected, và điều cần xác minh; nếu bạn đề xuất chức năng ngoài checkout hiện có hãy ghi rõ giả thuyết.
ĐỊNH DẠNG: Xuất bảng Markdown với các cột ID | Kịch bản | Dữ liệu | Expected đề xuất | Điều cần kiểm chứng. Không sinh code ở phần này.

## Đầu ra AI - danh sách đề xuất

| ID | Kịch bản | Ten | Ho | Postal | Expected đề xuất | Thẩm định sơ bộ |
|---|---|---|---|---|---|---|
| TC01 | Thêm 01 sản phẩm, checkout đủ dữ liệu | John | Doe | 10000 | Tại trang hoàn tất xuất hiện Thank you for your order! | GIỮ |
| TC02 | Bỏ trống cả ba trường |  |  |  | Error: First Name is required | GIỮ |
| TC03 | Bỏ trống Last Name | John |  | 10000 | Error: Last Name is required | GIỮ |
| TC04 | Bỏ trống Postal Code | John | Doe |  | Error: Postal Code is required | GIỮ |
| TC05 | First Name, Last Name và ZIP dài 1 ký tự | A | B | 1 | Hoàn tất nếu giao diện chấp nhận | SỬA |
| TC06 | Tên có tiếng Việt và ký tự đặc biệt | Nguyễn @#$ | Văn A %^& | 70000! | Kiểm tra hành vi thật, chưa mặc định từ chối | SỬA |
| TC07 | Checkout với 2 sản phẩm | John | Doe | 70000 | Đúng 2 sản phẩm trong overview và tổng giá | SỬA |
| TC08 | Hủy checkout khi ở trang thông tin | John | Doe | 70000 | Quay về giỏ hoặc trang phù hợp theo UI | SỬA |
| TC09 | Nhập mã giảm giá tại checkout | John | Doe | 10000 | Giảm 10% đơn hàng | BỎ |
| TC10 | Thanh toán bằng thẻ Visa tại checkout | John | Doe | 10000 | Bắt buộc nhập số thẻ/CVV | BỎ |
| TC11 | Nhận OTP qua số điện thoại khi checkout | John | Doe | 10000 | Yêu cầu OTP trước Finish | BỎ |
| TC12 | Nhập chuỗi kiểu SQL vào First Name | ' OR 1=1 -- | Doe | 10000 | Hệ thống phải báo SQL injection | SỬA |

**Trạng thái:** Đây là bản nháp do AI đề xuất, chưa phải kết quả chạy Selenium.
