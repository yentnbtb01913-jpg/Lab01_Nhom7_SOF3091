# PHẦN E – THẨM ĐỊNH AI VÀ BẮT LỖI AI

## Đối tượng thẩm định
Câu trả lời của ChatGPT cho prompt Phần A (xem `PhanA_PromptVaDanhSachAI.md`; ảnh `screenshots/minhchung/A01a_*.png` đến `A01e_*.png`): 20 ca C01–C20, đúng bảng năm cột, có ca thường, lỗi, biên.
Prompt yêu cầu "Không sinh code ở phần này" nên không có locator, Thread.Sleep hay API trong câu trả lời; lỗi cần tìm nằm ở Expected và giả thuyết.

## Bốn tiêu chí đối chiếu
- **T1** – Expected cụ thể: có chuỗi thông báo, URL hoặc giá trị đối chiếu, không chứa nhánh "hoặc".
- **T2** – Expected quan sát được trên giao diện để Selenium có đối tượng Assert.
- **T3** – Hành vi ngoài mô tả của đề được gắn nhãn "Giả thuyết", không dùng làm kết quả đã xác nhận.
- **T4** – Khớp hành vi thật của SauceDemo và sáu ca đã chạy ở Phần C (19 Passed ở lượt TestReport).

## Các chỗ AI sai hoặc không dùng được
| Ca AI | Vì sao sai hoặc không dùng được | Cách phát hiện | Xử lý |
|---|---|---|---|
| C13, C14, C16 | Expected có nhánh "hoặc" hoặc điều kiện "nếu" nên ca nào cũng "đúng"; Assert viết theo đó không bao giờ fail (T1). | Ca JSON 05, 06 đã chạy thật: Postal Code "1", "70000!" và tên Nguyễn @#$ đều hoàn tất. | C13 SỬA (Expected = hoàn tất); C14 SỬA (thử tay xác định giới hạn); C16 BỎ (trùng ca 05, 06). |
| C15, C20 | Không quan sát được: Postal Code không hiển thị lại sau Continue/Finish; không có màn hình liệt kê đơn để so sánh (T2). | Quan sát Overview, Complete (`screenshots/minhchung/B02b_*.png`, ảnh Complete). | BỎ. |
| C04, C09–C11 | AI tự đặt quy tắc (chặn giỏ rỗng, cắt khoảng trắng) mà đề và giao diện không nêu (T3, T4). | Sáu ca đã chạy chỉ có thông báo "required" cho ô rỗng; chưa có ca về khoảng trắng hay giỏ rỗng. | C04 BỎ; C09–C11 SỬA (chạy thử thủ công rồi viết Expected). |
| C17, C18 | Không nêu trang đích của Cancel, không nói giỏ còn sản phẩm hay không (T1). | Cả Your Information và Overview đều có nút Cancel; CheckoutPage chưa có phương thức Cancel. | SỬA: bổ sung Cancel, quan sát trang đích rồi gán Expected. |
| C02, C19 | Không nêu giá, thuế suất, quy tắc làm tròn (T1). | Overview: Item total $29.99, Tax $2.40, Total $32.39 (Tax xấp xỉ 8%, cần xác nhận với nhiều sản phẩm). | SỬA: Assert Total = Item total + Tax, đọc từ trang. |
| C05–C08 | Thiếu chuỗi thông báo chính xác; C08 nghi ngờ nhiều thông báo cùng lúc (T1). | Ca JSON 03, 04, 02 (C06, C07, C08) chạy thật; cả ba ô trống chỉ có thông báo First Name. | C05 SỬA; C06–C08 GIỮ. |

## Phần AI làm đúng (đã kiểm)
Đúng bảng năm cột; có ca thường, lỗi, biên; gắn nhãn "Giả thuyết" ở C04, C09–C11, C13, C16, C20; C01 và C06–C08 khớp ca JSON 01, 03, 04, 02 đã chạy Passed.

## Sai lệch trong source mẫu (không quy cho AI)
1. Source mẫu đặt kỳ vọng `khong_ton_tai / sai_mat_khau => true`; đã sửa thành false trong `LoginTests.cs`.
2. Source mẫu dùng `driver.FindElement` trực tiếp trong `DataDrivenCheckoutTests.cs`; đã chuyển sang `InventoryPage.ThemBackpack().MoGioHang()`.
3. Source mẫu chấp nhận `Checkout: Complete!` HOẶC `Thank you for your order!` trong cùng một Assert; đã chỉ kiểm đúng `h2.complete-header` sau Finish.

## Prompt reviewer dự phòng (chưa dùng, chỉ chạy khi cần thẩm định mã AI sinh)
```text
VAI TRÒ: Reviewer độc lập đánh giá code AI sinh cho Selenium C#.
NGỮ CẢNH: FirstTests sử dụng BaseTest + POM + Explicit Wait; SauceDemo checkout có first-name, last-name, postal-code, Continue, Finish.
YÊU CẦU: Rà mã gợi ý và kiểm tra locator thực tế, Assert theo nghiệp vụ, có Tim() thay Sleep, đúng tầng POM, không sinh chức năng ngoài web, người học giải thích được mã. Chỉ ra lỗi hoặc điểm cần xác minh.
ĐỊNH DẠNG: Bảng Hạng mục | Đánh giá | Bằng chứng cần thu thập. Không tuyên bố PASS nếu chưa chạy.
```
