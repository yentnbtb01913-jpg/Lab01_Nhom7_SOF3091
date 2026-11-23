# PHẦN E – THẨM ĐỊNH AI

## Prompt reviewer

VAI TRÒ: Reviewer độc lập đánh giá code AI sinh cho Selenium C#.
NGỮ CẢNH: FirstTests sử dụng BaseTest + POM + Explicit Wait; SauceDemo checkout có first-name, last-name, postal-code, Continue, Finish.
YÊU CẦU: Rà mã gợi ý và kiểm tra locator thực tế, Assert theo nghiệp vụ, có Tim() thay Sleep, đúng tầng POM, không sinh chức năng ngoài web, người học giải thích được mã. Chỉ ra lỗi hoặc điểm cần xác minh.
ĐỊNH DẠNG: Bảng Hạng mục | Đánh giá | Bằng chứng cần thu thập. Không tuyên bố PASS nếu chưa chạy.

## Những khuyến nghị cần loại hoặc sửa
1. **Assert sai nghiệp vụ**: nguồn giảng viên ban đầu đặt `khong_ton_tai / sai_mat_khau => true`. Đã sửa thành false; đây là lỗi trong *source mẫu*, không tự khẳng định AI là người gây lỗi.
2. **Làm tắt POM**: source mẫu dùng `driver.FindElement` trực tiếp trong `DataDrivenCheckoutTests.cs`. Đã chuyển sang `InventoryPage.ThemBackpack().MoGioHang()`.
3. **Assert mơ hồ**: source mẫu chấp nhận `Checkout: Complete!` HOẶC `Thank you for your order!` trong cùng một Assert; đã chỉ kiểm tra đúng `h2.complete-header` sau Finish.
4. **Ba ca AI đề xuất không áp dụng**: coupon, Visa và OTP (TC09–TC11).

## Bằng chứng để hoàn tất Phần E
- [ ] Ảnh prompt và câu trả lời AI thật.
- [ ] Ảnh DevTools/website chứng minh tính năng không có.
- [ ] Lời giải thích của người học về lỗi phát hiện, tiêu chí đối chiếu.
