# PHẦN B – LAB 6

**Logging trong BaseTest (25%)**

- [ ] Viết helper Log có tối thiểu Info và Error, mỗi dòng có dấu thời gian và mức độ.
- [ ] Bắt đầu test ghi Info ở [SetUp], kết thúc ghi OK/RỚT ở [TearDown].
- [ ] Test không tự ghi log; log đến từ lớp BaseTest.
- [ ] Có đoạn log thực tế gồm ca Passed và ít nhất một ca Failed/ERROR.
- [ ] Dùng TestContext.Progress.WriteLine; có thể dotnet test -l "console;verbosity=detailed".

**Bằng chứng:** Ảnh thật từ Visual Studio/terminal; không điền trạng thái PASS giả trước khi chạy.
