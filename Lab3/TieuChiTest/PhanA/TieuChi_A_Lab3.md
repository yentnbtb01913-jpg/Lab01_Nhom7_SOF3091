# Tiêu chí Phần A – Lab 3

**Tên:** Chứng minh vấn đề đồng bộ

**Tỷ trọng:** 25%

**Nguồn:** `Lab3/DeBai.pdf` — mục 2, Phần A.

## Nguyên văn tiêu chí theo đề

```text
Phần A — Chứng minh vấn đề đồng bộ (25%)
  • Dùng trang the-internet.herokuapp.com/dynamic_loading/2: bấm nút Start,
    chờ phần tử #finish (chứa chữ “Hello World!”) được JavaScript tạo ra sau vài giây.
  • Bước 1 — viết test tìm #finish NGAY sau khi bấm Start (không Wait); chụp ảnh
    test RỚT kèm thông báo lỗi NoSuchElementException.
  • Bước 2 — thêm Explicit Wait, chụp ảnh test PASS.
  • Viết 2–3 câu giải thích vì sao cùng một code mà kết quả lại khác nhau.
  • Yêu cầu: dùng WebDriverWait + Until(...) — KHÔNG được dùng Thread.Sleep.
  • Lưu ý quan trọng: không dùng performance_glitch_user cho phần này. Tài khoản
    đó chỉ làm trang tải CHẬM, mà việc chờ tải trang thì Selenium đã tự làm giùm
    (pageLoadStrategy = normal) — nên test sẽ chậm nhưng vẫn PASS, không chứng
    minh được điều gì.
```

## Ghi nhận kết quả

- Mã kiểm thử tương ứng: [Điền theo Test Explorer, không tự nhận kết quả]
- Minh chứng ảnh: `Lab3/screenshots/PhanA/`
- Kết quả thực tế: [Sau khi chạy NUnit / kiểm chứng trực tiếp]
- Hạng mục yêu cầu thực hiện thủ công: [Theo yêu cầu trong đề nếu có]
