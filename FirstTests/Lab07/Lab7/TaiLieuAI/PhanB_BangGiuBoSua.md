# PHẦN B – THẨM ĐỊNH GIỮ / BỎ / SỬA

| ID | Quyết định | Căn cứ |
|---|---|---|
| TC01 | **GIỮ** | Luồng thuận; có test checkout JSON tương ứng |
| TC02 | **GIỮ** | Kiểm tra trường đầu tiên bắt buộc |
| TC03 | **GIỮ** | Ca âm theo yêu cầu trường |
| TC04 | **GIỮ** | Ca âm theo yêu cầu trường |
| TC05 | **SỬA** | Không khẳng định độ dài tối thiểu nếu chưa kiểm chứng |
| TC06 | **SỬA** | Cần kiểm tra thực tế thay vì tự giả định quy tắc nhập |
| TC07 | **SỬA** | Cần thêm Page Object cho giỏ và đọc tổng giá |
| TC08 | **SỬA** | Chưa có POM Cancel, cần bổ sung rồi xác minh |
| TC09 | **BỎ** | Checkout mẫu không có trường nhập coupon trong giao diện đã khảo sát |
| TC10 | **BỎ** | Mẫu checkout chỉ nhập First/Last Name, Postal Code; không có form Visa |
| TC11 | **BỎ** | Không có trường số điện thoại hay bước OTP trong luồng mẫu |
| TC12 | **SỬA** | Không suy đoán có cơ chế báo lỗi SQL injection; chỉ ghi nhận hành vi nhập và điều hướng |

**BỎ tối thiểu ba ca:** TC09 (coupon), TC10 (thẻ Visa), TC11 (OTP). Cần kiểm chứng UI bằng trình duyệt và chụp DevTools khi nộp bài.
