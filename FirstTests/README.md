# Y3 – Xử lý tình huống thực tế và chờ tường minh

## 1. Mục tiêu
- Sử dụng Explicit Wait thay cho Thread.Sleep.
- Xử lý tình huống phần tử tải chậm trên trang web.
- Đảm bảo Selenium chỉ thực hiện kiểm tra khi phần tử đã xuất hiện và hiển thị.
- Kiểm tra kết quả thực tế của phần tử trên trang web.

## 2. Nội dung thực hiện

Sử dụng file `DynamicLoadingTest.cs` để kiểm thử chức năng Dynamic Loading trên trang web.

Đường dẫn kiểm thử: `https://the-internet.herokuapp.com/dynamic_loading/1`

Các bước thực hiện:
1. Mở trang Dynamic Loading.
2. Nhấn nút Start.
3. Sử dụng WebDriverWait để chờ phần tử có ID `finish` xuất hiện.
4. Kiểm tra phần tử đã hiển thị.
5. Kiểm tra nội dung phần tử bằng `Hello World!`.
6. Thực hiện Assert để xác nhận kết quả kiểm thử.

## 3. Chờ tường minh (Explicit Wait)

Sử dụng đoạn code:

```csharp
IWebElement finish = wait.Until(d =>
{
    var element = d.FindElement(By.Id("finish"));

    return element.Displayed &&
           element.Text == "Hello World!"
        ? element
        : null;
});

Assert.That(finish.Text, Is.EqualTo("Hello World!"));
```

### Giải thích
- `WebDriverWait`: Chờ phần tử trong thời gian tối đa 10 giây.
- `wait.Until()`: Chờ điều kiện được đáp ứng.
- `element.Displayed`: Kiểm tra phần tử đã hiển thị.
- `Assert.That()`: Kiểm tra nội dung thực tế với nội dung mong đợi.

## 4. Kết quả kiểm thử

| Nội dung | Kết quả |
|---|---|
| Mở trang Dynamic Loading | Passed |
| Nhấn nút Start | Passed |
| Chờ phần tử xuất hiện | Passed |
| Kiểm tra nội dung Hello World! | Passed |
| Sử dụng Explicit Wait | Passed |
| Không sử dụng Thread.Sleep trong code C# | Passed |
| Chạy kiểm thử lặp lại 3 lần | Passed nếu cả 3 lần đều thành công |

## 5. Kết luận

Đã áp dụng Explicit Wait trong kiểm thử Selenium NUnit để xử lý tình huống phần tử tải chậm. Việc sử dụng WebDriverWait giúp chương trình chờ đúng điều kiện cần thiết, hạn chế thời gian chờ không cần thiết và nâng cao độ ổn định của kiểm thử tự động.

## 6. Minh chứng

Các ảnh minh chứng được lưu trong thư mục `screenshots`:
- Ảnh code sử dụng WebDriverWait.
- Ảnh kết quả chạy Test Explorer.
- Ảnh kết quả chạy test 3 lần liên tiếp.
- Ảnh tìm kiếm Thread.Sleep trên toàn Solution.