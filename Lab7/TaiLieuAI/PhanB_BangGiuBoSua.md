# PHẦN B – THẨM ĐỊNH GIỮ / BỎ / SỬA (20 CA CỦA CHATGPT)

Quy ước: **GIỮ** = dùng ý tưởng của ca, đã hoặc đang triển khai; **SỬA** = giữ ý tưởng nhưng sửa Expected, dữ liệu hoặc bổ sung Page Object; **BỎ** = không đưa vào bộ test.
Tham chiếu "ca JSON 01–06" là sáu ca trong `FirstTests/Data/checkout_cases.json` (01 hợp lệ, 02 bỏ trống tất cả, 03 bỏ trống Last Name, 04 bỏ trống Postal Code, 05 một ký tự, 06 Unicode và ký tự đặc biệt).

| ID | Quyết định | Căn cứ |
|---|---|---|
| C01 | **GIỮ** | Luồng thuận; trùng ca JSON 01 (John / Doe / 10000), đã chạy Passed. |
| C02 | **SỬA** | Cần thêm thao tác thêm sản phẩm thứ hai và đọc Item total, Tax, Total ở Overview; Expected phải là công thức cụ thể. |
| C03 | **SỬA** | Chưa có Page Object cho giỏ hàng (xóa sản phẩm, đọc badge); “số lượng cập nhật” phải đổi thành giá trị cụ thể (badge biến mất, giỏ trống). |
| C04 | **BỎ** | Quy tắc “không cho hoàn tất đơn rỗng” do AI tự đặt, không có trong luồng đã khảo sát; mọi ca đã chạy đều có sẵn một sản phẩm. Xem B1. |
| C05 | **SỬA** | Chưa có ca riêng trong JSON; Expected phải ghi đúng chuỗi “Error: First Name is required” (đã xác nhận ở ca JSON 02). |
| C06 | **GIỮ** | Trùng ca JSON 03; Expected được thay bằng chuỗi “Error: Last Name is required”, đã Passed. |
| C07 | **GIỮ** | Trùng ca JSON 04; Expected được thay bằng chuỗi “Error: Postal Code is required”, đã Passed. |
| C08 | **GIỮ** | Trùng ca JSON 02; khi cả ba ô trống chỉ xuất hiện thông báo “Error: First Name is required”, đã Passed. |
| C09 | **SỬA** | Quy tắc xử lý khoảng trắng chưa được xác nhận; chạy thử thủ công rồi mới viết Expected. |
| C10 | **SỬA** | Như C09, áp dụng cho Last Name. |
| C11 | **SỬA** | Như C09, áp dụng cho Postal Code. |
| C12 | **GIỮ** | Được phủ bởi ca JSON 06 (Nguyễn @#$ / Văn A %^& / 70000!) có dấu tiếng Việt, đã Passed. |
| C13 | **SỬA** | Expected có nhánh “hoặc”; ca JSON 06 cho thấy ký tự đặc biệt được chấp nhận nên Expected = hoàn tất đơn hàng. |
| C14 | **SỬA** | Chưa biết giới hạn độ dài; thử thủ công để xác định rồi mới viết Expected (hiện có nhánh “hoặc”). |
| C15 | **BỎ** | Postal Code không hiển thị lại ở Overview và Complete nên không có gì để Assert. Xem B1. |
| C16 | **BỎ** | Trùng ca JSON 05 và 06 (Postal Code “1”, “70000!” đều hoàn tất); Expected chỉ có điều kiện “nếu hệ thống hỗ trợ”. Xem B1. |
| C17 | **SỬA** | Chưa có phương thức Cancel trong CheckoutPage; phải quan sát trang đích thật rồi gán Expected cụ thể. |
| C18 | **SỬA** | Như C17, áp dụng cho nút Cancel ở trang Overview. |
| C19 | **SỬA** | Cần Expected có số: Total = Item total + Tax, đọc giá trị từ trang; không đặt cứng thuế suất. |
| C20 | **BỎ** | Luồng không có màn hình liệt kê đơn hàng để so sánh trước và sau khi tải lại. Xem B1. |

Tổng hợp: GIỮ 5 (C01, C06, C07, C08, C12), SỬA 11, BỎ 4 (C04, C15, C16, C20).

## Lý do BỎ
- **C04 – giỏ rỗng:** AI tự đặt giả thuyết "không cho hoàn tất đơn hàng rỗng"; đề và luồng đã khảo sát không mô tả quy tắc này, sáu ca đã chạy đều có sẵn một sản phẩm.
- **C15 – Postal Code bắt đầu bằng số 0:** Overview và Complete không hiển thị lại Postal Code nên không có gì để Assert.
- **C16 – Postal Code chữ và số:** ca JSON 05 ("1") và 06 ("70000!") đều hoàn tất; Expected chỉ là điều kiện "nếu hệ thống hỗ trợ", không thêm thông tin mới.
- **C20 – tải lại trang hoàn tất:** luồng không có màn hình liệt kê đơn hàng để so sánh số đơn trước và sau khi tải lại.

Ảnh đối chiếu giao diện: `screenshots/minhchung/B01_*.png`, `B02a_*.png`, `B02b_*.png`.
