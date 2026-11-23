# Tiêu chí Phần B – Lab 5

**Tên:** Driver Factory

**Tỷ trọng:** 20%

**Nguồn:** `Lab5/DeBai.pdf` — mục 2, Phần B.

## Nguyên văn tiêu chí theo đề

```text
Phần B — Driver Factory (20%)

  • Viết Core/DriverFactory.cs hỗ trợ tối thiểu 2 trình duyệt (ví dụ Chrome +
    Firefox hoặc Chrome + Edge).
  • Hàm Create phải trả về kiểu IWebDriver (không phải ChromeDriver) — đây là chỗ
    tính đa hình phát huy tác dụng.
  • Trình duyệt không hỗ trợ → ném lỗi có thông báo rõ ràng nêu đúng tên bị gõ sai.
  • Hỗ trợ chế độ Headless đọc từ config — và phải áp cho MỌI trình duyệt, không chỉ
    Chrome (new FirefoxDriver(FirefoxOpts(cfg)), không phải new
    FirefoxDriver()).
  • Nộp 2 ảnh: chạy Chrome và chạy Firefox — từ CÙNG một code, chỉ khác 1 chữ
    trong appsettings.json.
```

## Ghi nhận kết quả

- Mã kiểm thử tương ứng: [Điền theo Test Explorer, không tự nhận kết quả]
- Minh chứng ảnh: `Lab5/screenshots/PhanB/`
- Kết quả thực tế: [Sau khi chạy NUnit / kiểm chứng trực tiếp]
- Hạng mục yêu cầu thực hiện thủ công: [Theo yêu cầu trong đề nếu có]
