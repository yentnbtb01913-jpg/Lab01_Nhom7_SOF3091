# Lab 6 – Phần E: AI sinh và thẩm định test case

## 1. Prompt đã sử dụng

```text
Tôi đang kiểm thử form đăng nhập của website SauceDemo bằng Selenium C# và NUnit.
Hãy sinh đúng 10 bộ dữ liệu kiểm thử cho file Data/users.json.
Mỗi bộ dữ liệu phải đúng schema gồm 4 trường: Username (string), Password (string),
ThanhCong (boolean), GhiChu (string). Bao gồm ca hợp lệ, không hợp lệ và ca biên
(username trống, password trống, sai mật khẩu, tài khoản bị khóa, username lạ,
username có khoảng trắng). Không tự giả định website có chức năng đăng ký, CAPTCHA,
khóa tài khoản sau N lần sai hoặc chính sách độ dài nếu không có bằng chứng.
Trả về JSON hợp lệ và ghi chú các giả định cần xác minh.
```

## 2. Toàn bộ kết quả AI trả về (10 bộ dữ liệu)

```json
[
  {
    "Username": "standard_user",
    "Password": "secret_sauce",
    "ThanhCong": true,
    "GhiChu": "Đăng nhập hợp lệ – tài khoản chuẩn"
  },
  {
    "Username": "locked_out_user",
    "Password": "secret_sauce",
    "ThanhCong": false,
    "GhiChu": "Tài khoản bị khóa – không được đăng nhập"
  },
  {
    "Username": "",
    "Password": "secret_sauce",
    "ThanhCong": false,
    "GhiChu": "Ca biên: bỏ trống username"
  },
  {
    "Username": "standard_user",
    "Password": "",
    "ThanhCong": false,
    "GhiChu": "Ca biên: bỏ trống password"
  },
  {
    "Username": "standard_user",
    "Password": "wrong_password",
    "ThanhCong": false,
    "GhiChu": "Sai mật khẩu"
  },
  {
    "Username": "unknown_user_123",
    "Password": "secret_sauce",
    "ThanhCong": false,
    "GhiChu": "Username không tồn tại"
  },
  {
    "Username": "standard_user ",
    "Password": "secret_sauce",
    "ThanhCong": false,
    "GhiChu": "Ca biên: username có khoảng trắng cuối"
  },
  {
    "Username": "problem_user",
    "Password": "secret_sauce",
    "ThanhCong": true,
    "GhiChu": "Tài khoản hợp lệ khác của SauceDemo"
  },
  {
    "Username": "admin",
    "Password": "admin123",
    "ThanhCong": true,
    "GhiChu": "Tài khoản quản trị mặc định"
  },
  {
    "Username": "standard_user",
    "Password": "secret_sauce ",
    "ThanhCong": true,
    "GhiChu": "Hệ thống tự động xóa khoảng trắng cuối mật khẩu"
  }
]
```

## 3. Thẩm định – các ca không sử dụng

1. **Ca 9 – `admin / admin123`, kỳ vọng đăng nhập thành công:** không dùng được vì SauceDemo không cung cấp tài khoản quản trị mặc định này. Không được suy diễn tài khoản của website từ hệ thống khác. Nếu chạy, kết quả thực tế là đăng nhập thất bại, trái với `ThanhCong: true`.
2. **Ca 10 – mật khẩu có dấu cách cuối nhưng kỳ vọng thành công:** không dùng được vì không có yêu cầu hoặc bằng chứng rằng form tự động Trim mật khẩu. Website xác thực mật khẩu đúng chuỗi; không thể tự đặt kỳ vọng rằng khoảng trắng sẽ bị xóa.

## 4. Kết quả đã chọn để chạy

Tám ca đầu được đưa vào `Data/users.json`. Hai ca 9 và 10 được giữ trong kết quả AI để minh chứng việc thẩm định, nhưng bị loại khỏi dữ liệu chạy vì kỳ vọng không có căn cứ. Schema trong file JSON giữ nguyên bốn trường mà `DataDrivenLoginTests.cs` đang đọc.
