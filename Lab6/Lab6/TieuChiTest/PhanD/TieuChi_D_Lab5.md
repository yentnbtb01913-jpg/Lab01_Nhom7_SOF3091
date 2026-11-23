# Tiêu chí Phần D – Lab 5

**Tên:** Data-Driven Testing

**Tỷ trọng:** 25%

**Nguồn:** `Lab5/DeBai.pdf` — mục 2, Phần D.

## Nguyên văn tiêu chí theo đề

```text
Phần D — Data-Driven Testing (25%)

 • Tạo Data/users.json với tối thiểu 5 bộ dữ liệu; mỗi bộ gồm Username, Password,
   ThanhCong, GhiChu.
 • Bắt buộc có ít nhất 2 ca biên (ô trống, khoảng trắng thừa, ký tự đặc biệt, chuỗi rất
   dài…) — không chấp nhận chỉ liệt kê tài khoản saucedemo cho sẵn.
 • Dùng [TestCaseSource] + nameof(...); nguồn dữ liệu phải static và trả về
   IEnumerable<TestCaseData>.
 • Dùng .SetName(...) để tên ca test đọc được trong Test Explorer.
 • Nộp ảnh chứng minh: thêm 1 bộ dữ liệu vào JSON → số ca test tăng lên mà KHÔNG
    sửa file .cs nào.
 • Lưu ý bắt buộc: sửa lại InventoryPage.DaHienThi() để nó trả về false khi không
    vào được trang sản phẩm, thay vì ném WebDriverTimeoutException. Bản Bài 4
    dùng Tim() nên sẽ chờ hết timeout rồi ném lỗi ở mọi ca nghịch — câu Assert
    không bao giờ chạy tới.
```

## Ghi nhận kết quả

- Mã kiểm thử tương ứng: [Điền theo Test Explorer, không tự nhận kết quả]
- Minh chứng ảnh: `Lab5/screenshots/PhanD/`
- Kết quả thực tế: [Sau khi chạy NUnit / kiểm chứng trực tiếp]
- Hạng mục yêu cầu thực hiện thủ công: [Theo yêu cầu trong đề nếu có]
